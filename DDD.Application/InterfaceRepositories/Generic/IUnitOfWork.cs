using DDD.Application.InterfaceRepositories.Users;

namespace DDD.Application.InterfaceRepositories.Generic;

public interface IUnitOfWork
    {
        IUserRepository Users { get; }

        //IRefreshTokensRepository RefreshTokens { get; }
        Task CompleteAsync();
    }

