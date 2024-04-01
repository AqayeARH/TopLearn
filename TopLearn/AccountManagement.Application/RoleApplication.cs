using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Application;

public class RoleApplication : IRoleApplication
{
    #region constructor injection

    private readonly IRoleRepository _roleRepository;
    public RoleApplication(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    #endregion

    public async Task<List<RoleViewModel>> GetList()
    {
        return await _roleRepository.GetList();
    }
}