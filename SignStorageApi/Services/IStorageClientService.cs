using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SignStorageApi.Services
{
    public interface IStorageClientService
    {
        Task<string> AddData(byte[] data);
        Task UpdateData(string dataID, byte[] newData);
        Task DeleteData(string dataID);
        Task<byte[]> GetData(string dataID);
    }
}
