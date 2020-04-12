using System;
using System.Collections.Generic;
using System.Text;
using SignStorageApi.Services;

namespace SignStorageApi
{
    public class SignEngine : ISignEngine
    {
        //TODO implementation
        public void DeleteKeyPair(string id)
        {
            return;
        }

        //TODO implement key generate by size param
        public string GenerateKeyPair(int keySize)
        {
            return Guid.NewGuid().ToString();
        }

        //TODO implement the sign data
        public byte[] SignData(byte[] dataToSign, string keyID)
        {
            return Encoding.ASCII.GetBytes("signature");
        }
    }
}
