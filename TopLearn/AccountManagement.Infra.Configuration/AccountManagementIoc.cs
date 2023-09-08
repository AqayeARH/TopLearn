using AccountManagement.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Infra.EfCore;
using AccountManagement.Infra.EfCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AccountManagement.Infra.Configuration;

public static class AccountManagementIoc
{
    public static void Configure(IServiceCollection services, string connectionString)
    {
        #region Account

        services.AddTransient<IAccountRepository, AccountRepository>();
        services.AddTransient<IAccountApplication, AccountApplication>();

        #endregion

        #region Role

        services.AddTransient<IRoleRepository, RoleRepository>();
        services.AddTransient<IRoleApplication, RoleApplication>();

        #endregion

        #region db context

        services.AddDbContext<AccountManagementContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        #endregion
    }
}