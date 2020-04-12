using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SignStorageApiTest
{
    public class MockStorageClientService : IStorageClientService
    {
        private readonly string m_storagePath = null;

        public MockStorageClientService()
        {
            string tempPath = Path.GetTempPath();
            m_storagePath = Path.Combine(tempPath, "StorageApi");

            Directory.CreateDirectory(m_storagePath);
        }

        
        public async Task<string> AddData(byte[] data)
        {

            string storageId = Path.GetRandomFileName();
            await File.WriteAllBytesAsync(Path.Combine(m_storagePath, storageId), data);

            return storageId;
        }


        public async Task UpdateData(string dataID, byte[] newData)
        {
            await File.WriteAllBytesAsync(Path.Combine(m_storagePath, dataID), newData);
        }

        public async Task DeleteData(string dataID)
        {
             File.Delete(Path.Combine(m_storagePath, dataID));
        }

        public async Task<byte[]> GetData(string dataID)
        {
            try
            {
                byte[] data = await File.ReadAllBytesAsync(Path.Combine(m_storagePath, dataID));
                return data;
            }
            catch (FileNotFoundException fex)
            {
                return null;
            }

        }
    }
}
