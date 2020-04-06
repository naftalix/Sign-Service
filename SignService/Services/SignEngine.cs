using System;
using System.Collections.Generic;
using System.Text;

namespace SignService
{
    public class SignEngine : ISignEngine
    {
        public void DeleteKeyPair(string id)
        {
            return;
        }

        public string GenerateKeyPair(int keySize)
        {
            return Guid.NewGuid().ToString();
        }

        public byte[] SignData(byte[] dataToSign, string keyID)
        {
            return Encoding.ASCII.GetBytes("signature");
        }
    }
}
