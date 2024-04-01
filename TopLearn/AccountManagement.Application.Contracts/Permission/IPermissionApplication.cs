using _0.Framework.Application;

namespace AccountManagement.Application.Contracts.Permission;

public interface IPermissionApplication
{
    Task<OperationResult> AddPermission(AddPermissionCommand command);
}