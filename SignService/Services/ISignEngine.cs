using System;
using System.Collections.Generic;
using System.Text;

namespace SignService
{
    public interface ISignEngine
    {
        /// <summary>
        /// Generates a new key pair
        /// </summary>
        /// <returns>The id of the newly created key pair</returns>
        string GenerateKeyPair(int keySize);

        /// <summary>
        /// Deletes a key pair
        /// </summary>
        /// <param name="id"> The id of the key pair to delete</param>
        void DeleteKeyPair(string id);


        /// <summary>
        /// Signs a data using the requested key 
        /// </summary>
        /// <param name="dataToSign"> The data to sign</param>
        /// <param name="keyID"> The key id of the key to sign with</param>
        /// <returns> Signature </returns>
        byte[] SignData(byte[] dataToSign, string keyID);
    }
}
