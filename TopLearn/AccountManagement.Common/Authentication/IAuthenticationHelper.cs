namespace AccountManagement.Common.Authentication;

public interface IAuthenticationHelper
{
    void Signin(AuthenticationViewModel account);
    void SignOut();
    bool IsAuthenticated();
    long CurrentAccountId();
    AuthenticationViewModel CurrentAccountInfo();
    string CurrentAccountFullname();
}