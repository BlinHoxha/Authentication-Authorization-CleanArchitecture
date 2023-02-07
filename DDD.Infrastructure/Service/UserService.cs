using AutoMapper;
using DDD.Application.Exceptions;
using DDD.Application.InterfaceRepositories.Users;
using DDD.Application.InterfaceServices;
using DDD.Contracts.DTO.User;
using DDD.Domain.Identity;
using EnsureThat;

namespace DDD.Infrastructure.Service;

public class UserService : IUserService
{
    private IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(
    IUserRepository userRepository,
    IMapper mapper)
    {
        _userRepository = EnsureArg.IsNotNull(userRepository, nameof(userRepository));
        _mapper = EnsureArg.IsNotNull(mapper, nameof(mapper));
    }

    public async Task<IEnumerable<UserDTO>> GetUsersAsync(CancellationToken cancellationToken = default)
    {
        var users = await _userRepository.GetAllUsersAsync(cancellationToken);
        var usersDto = _mapper.Map<IEnumerable<UserDTO>>(users);
        return usersDto;
    }

    public async Task<UserDTO> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var users = await _userRepository.GetByIdentityId(userId, cancellationToken);
        if (users is null)
        {
            throw new UserNotFoundException("User not found");
        }
        var userDTO = _mapper.Map<UserDTO>(users);
        return userDTO;
    }

    public async Task<UserDTO> AddUserAsync(CreateUserDTO user, CancellationToken cancellationToken = default)
    {
        var _mappedUser = _mapper.Map<ApplicationUser>(user);

        await _userRepository.AddUser(_mappedUser);
        await _userRepository.CompleteAsync();

        return _mapper.Map<UserDTO>(_mappedUser);
    }

    public async Task UpdateUserAsync(Guid userId, UpdateUserDTO userUpdateDto, CancellationToken cancellationToken = default)
    {
        var users = await _userRepository.GetByIdentityId(userId, cancellationToken);
        if (users is null)
        {
            throw new UserNotFoundException("User not found");
        }
        _mapper.Map(userUpdateDto, users);

         await _userRepository.Update(users);               
        await _userRepository.CompleteAsync();
    }

    public async Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdentityId(userId, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException("User not found");
        }
        _userRepository.DeleteUser(user);
        await _userRepository.CompleteAsync();
    }
}