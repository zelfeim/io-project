using System.Security.Cryptography;

namespace Application.Features.Employee.Authentication;

public static class SecretHasher
{
    private const int SaltSize = 16;
    private const int KeySize = 32;
    private const int Iterations = 10000;

    private const char Delimiter = ':';
    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;

    public static string Hash(string input)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(input, salt, Iterations, Algorithm, KeySize);

        return string.Join(Delimiter, Convert.ToHexString(hash), Convert.ToHexString(salt), Iterations, Algorithm);
    }

    public static bool Verify(string secret, string input)
    {
        var segments = secret.Split(Delimiter);
        var hash = Convert.FromHexString(segments[0]);
        var salt = Convert.FromHexString(segments[1]);
        var iterations = int.Parse(segments[2]);
        var algorithmName = new HashAlgorithmName(segments[3]);

        var inputHash = Rfc2898DeriveBytes.Pbkdf2(input, salt, iterations, algorithmName, hash.Length);

        return CryptographicOperations.FixedTimeEquals(inputHash, hash);
    }
}