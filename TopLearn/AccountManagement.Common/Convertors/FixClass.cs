namespace AccountManagement.Common.Convertors;

public static class FixClass
{
    public static string FixEmail(this string email)
    {
        return email.Trim().ToLower();
    }
}