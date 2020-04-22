using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SignStorageApi.Services
{
    public class StorageClientService : IStorageClientService
    {
        public HttpClient Client { get; }

        public StorageClientService(HttpClient client)
        {
            //example for now
            client.BaseAddress = new Uri("https://api.github.com/");

            Client = client;

            //string tempPath = Path.GetTempPath();
            //m_storagePath = Path.Combine(tempPath, "SignStorageApiStorage");

            //Directory.CreateDirectory(m_storagePath);
        }



        // private readonly string m_storagePath = null;

        //Currently using a synchronize method
        public async Task<string> AddData(byte[] data)
        {
            var content = CreateHttpStreamContent(data);

            var response = await Client.PostAsync("storage/store", content);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            return result;

            //string storageId = Path.GetRandomFileName();
            //File.WriteAllBytes(Path.Combine(m_storagePath, storageId), data);

            //return storageId;
        }


        public async Task UpdateData(string dataID, byte[] newData)
        {
            var content = CreateHttpStreamContent(newData);

            var response = await Client.PutAsync($"storage/update/{dataID}", content);

            response.EnsureSuccessStatusCode();

            //File.WriteAllBytes(Path.Combine(m_storagePath, dataID), newData);
        }

        public async Task DeleteData(string dataID)
        {
            var response = await Client.DeleteAsync($"storage/{dataID}");

            response.EnsureSuccessStatusCode();

            // File.Delete(Path.Combine(m_storagePath, dataID));
        }

        public async Task<byte[]> GetData(string dataID)
        {
            var response = await Client.GetAsync($"storage/{dataID}");

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsByteArrayAsync();

            return result;
            //try
            //{
            //    byte[] data = File.ReadAllBytes(Path.Combine(m_storagePath, dataID));
            //    return data;
            //}
            //catch (FileNotFoundException fex)
            //{
            //    return null;
            //}

        }

        private ByteArrayContent CreateHttpStreamContent(byte[] data)
        {
            var content = new ByteArrayContent(data);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return content;
        }
    }
}
