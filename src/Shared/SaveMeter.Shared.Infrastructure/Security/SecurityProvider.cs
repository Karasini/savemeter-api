using System.Text.Encodings.Web;
using SaveMeter.Shared.Infrastructure.Security.Encryption;

namespace SaveMeter.Shared.Infrastructure.Security;

internal sealed class SecurityProvider : ISecurityProvider
{
    private readonly IEncryptor _encryptor;
    private readonly IHasher _hasher;
    private readonly IRng _rng;
    private readonly SecurityOptions _securityOptions;
    private readonly string _key;

    public SecurityProvider(IEncryptor encryptor, IHasher hasher,
        IRng rng, SecurityOptions securityOptions)
    {
        _encryptor = encryptor;
        _hasher = hasher;
        _rng = rng;
        _securityOptions = securityOptions;
        _key = securityOptions.Encryption.Key;
    }

    public string Encrypt(string data)
        => _securityOptions.Encryption.Enabled ? _encryptor.Encrypt(data, _key) : data;

    public string Decrypt(string data)
        => _securityOptions.Encryption.Enabled ? _encryptor.Decrypt(data, _key) : data;

    public string Hash(string data) => _hasher.Hash(data);

    public string Rng(int length, bool removeSpecialChars = true) => _rng.Generate(length, removeSpecialChars);
}