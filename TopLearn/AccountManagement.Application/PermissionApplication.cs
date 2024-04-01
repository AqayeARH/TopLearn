using _0.Framework.Application;
using AccountManagement.Application.Contracts.Permission;
using AccountManagement.Domain.PermissionAgg;

namespace AccountManagement.Application;

public class PermissionApplication:IPermissionApplication
{
    #region constructor injection

    private readonly IPermissionRepository _permissionRepository;
    public PermissionApplication(IPermissionRepository permissionRepository)
    {
        _permissionRepository = permissionRepository;
    }

    #endregion
    public async Task<OperationResult> AddPermission(AddPermissionCommand command)
    {
        if (command.AccountId == 0)
        {
            return OperationResult.Error("حساب کاربری یافت نشد");
        }

        if (command.RolesId == null)
        {
            return OperationResult.Error("نقش یافت نشد");
        }

        foreach (var roleId in command.RolesId)
        {
            var permission = new AccountRole(command.AccountId, roleId);
            await _permissionRepository.AddPermission(permission);
            await _permissionRepository.Save();
        }

        return OperationResult.Success();
    }
}