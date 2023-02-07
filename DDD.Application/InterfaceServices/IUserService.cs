using DDD.Contracts.DTO.User;

namespace DDD.Application.InterfaceServices;
public interface IUserService
{
    Task<IEnumerable<UserDTO>> GetUsersAsync(CancellationToken cancellationToken = default);
    Task<UserDTO> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<UserDTO> AddUserAsync(CreateUserDTO userCreateDto, CancellationToken cancellationToken = default);
    Task UpdateUserAsync(Guid userId, UpdateUserDTO userUpdateDto, CancellationToken cancellationToken = default);
    Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken = default);
}
