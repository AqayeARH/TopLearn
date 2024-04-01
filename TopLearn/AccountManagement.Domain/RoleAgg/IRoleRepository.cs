using _0.Framework.Domain;
using AccountManagement.Application.Contracts.Role;

namespace AccountManagement.Domain.RoleAgg;

public interface IRoleRepository : IGenericRepository<int, Role>
{
    Task<List<RoleViewModel>> GetList();
}