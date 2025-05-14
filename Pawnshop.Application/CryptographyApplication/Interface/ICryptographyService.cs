namespace Pawnshop.Application.CryptographyApplication.Interface
{
    public interface ICryptographyService
    {
        string Encrypt(string data);
        string Decrypt(string encryptedData);
    }
}
