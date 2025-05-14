using Microsoft.AspNetCore.DataProtection;
using Pawnshop.Application.CryptographyApplication.Interface;

namespace Pawnshop.Infrastructure.Services.CryptographyInfrastructure.Services
{
    internal sealed class CryptographyService : ICryptographyService
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private const string PurposeString = "f&gghs%33:{FSF>#:@?#@??#@SFD<fF$34f3f#$FF#f34f3gasdfsdfsf";

        public CryptographyService(IDataProtectionProvider dataProtectionProvider)
        {
            _dataProtectionProvider = dataProtectionProvider;
        }

        public string Encrypt(string data)
        {
            if (string.IsNullOrEmpty(data))
                return data;

            var protector = _dataProtectionProvider.CreateProtector(PurposeString);
            return protector.Protect(data);
        }

        public string Decrypt(string encryptedData)
        {
            if (string.IsNullOrEmpty(encryptedData))
                return encryptedData;

            var protector = _dataProtectionProvider.CreateProtector(PurposeString);
            return protector.Unprotect(encryptedData);
        }
    }
}
