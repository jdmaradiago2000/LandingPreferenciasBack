using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WebApi_LandingPreferencias.App_Tools
{
    public class EncryptionHelper
    {

        public string Decrypt(string value)
        {
            Encryption.EncryptionHelper encryptionHelper = new Encryption.EncryptionHelper();
            return encryptionHelper.Decrypt(value);
        }

        public string Encrypt(string value)
        {
            Encryption.EncryptionHelper encryptionHelper = new Encryption.EncryptionHelper();
            return encryptionHelper.Encrypt(value);
        }
      
    }
}