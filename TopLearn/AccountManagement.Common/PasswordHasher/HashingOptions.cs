namespace AccountManagement.Common.PasswordHasher
{
    public sealed class HashingOptions
    {
        public int Iterations { get; set; } = 10000;
    }
}