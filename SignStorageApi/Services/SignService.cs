using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Hosting;
using SignStorageApi.Models;
using SignStorageApi.Infrastructure;
using System.Linq;
using Newtonsoft.Json;

namespace SignStorageApi.Services
{
    public class SignService : ISignService
    {

        private readonly ISignEngine _signEngine;
        private readonly IStorageClientService _storage;
        private readonly IHostEnvironment _host;
        private readonly string _mapPath;
        private readonly string _mapId;
        private Dictionary<string, string> _mapContext;

        public SignService(ISignEngine signEngine, IStorageClientService storage, IHostEnvironment host)
        {
            _signEngine = signEngine;
            _storage = storage;
            _host = host;

            var uploadFolder = Path.Combine(_host.ContentRootPath, "MapContextStorage");
            var filePath = Path.Combine(uploadFolder, "map.json");
            _mapPath = filePath;

            using (var r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();

                dynamic map = JsonConvert.DeserializeObject(json);

                _mapId = (string)map.Id;

                var mapBinaryData = _storage.GetData(_mapId).GetAwaiter().GetResult();

                var mapData = mapBinaryData.FromByteArray<Dictionary<string, string>>();

                _mapContext = mapData;
            }
        }

        private void UpdateStorageMap()
        {
            _storage.UpdateData(_mapId, _mapContext.ToByteArray());
        }


        public UserModel CreateUser(string name, string lastName, string email)
        {
            if (IsUserExist(name))
            {
                throw new ArgumentException($"The user: {name} is already exist");
            }

            var dataModel = new UserModel
            {
                Name = name,
                LastName = lastName,
                Email = email
            };

            var binaryData = dataModel.ToByteArray();

            var binaryModelKey = _storage.AddData(binaryData).GetAwaiter().GetResult();

            _mapContext.Add(name, binaryModelKey);

            UpdateStorageMap();

            return dataModel;
        }

        public void DeleteUser(string name)
        {
            if (!IsUserExist(name))
            {
                throw new ArgumentException($"Can't Delete, the user: {name} is not exist");
            }

            var userStorageKey = _mapContext[name];

            _storage.DeleteData(userStorageKey);

            _mapContext.Remove(name);

            UpdateStorageMap();
        }

        public void DeleteUserKey(string name, string keyId)
        {
            var user = GetUser(name);

            user.Keys.Remove(keyId);

            var userStorageKey = _mapContext[name];

            _storage.UpdateData(userStorageKey, user.ToByteArray());

        }

        public Tuple<string, int> GenerateUserSigningKey(string userName, int keySize)
        {
            var user = GetUser(userName);

            var key = _signEngine.GenerateKeyPair(keySize);

            var userStorageKey = _mapContext[userName];

            user.Keys.Add(key);
            _storage.UpdateData(userStorageKey, user.ToByteArray());

            return new Tuple<string, int>(key, keySize);
        }

        public List<string> GetAllUsers()
        {
            return _mapContext.Select(u => u.Key).ToList();
        }

        public UserModel GetUser(string name)
        {
            if (!IsUserExist(name))
            {
                throw new ArgumentException($"User name: {name} is not exist");
            }

            var userStorageKey = _mapContext[name];

            var userBinaryData = _storage.GetData(userStorageKey).GetAwaiter().GetResult();

            var result = userBinaryData.FromByteArray<UserModel>();
            return result;

        }

        public string GetUserKey(string name, string keyId)
        {
            var user = GetUser(name);

            return user.Keys.Where(k => k == keyId).First();
        }

        private bool IsUserExist(string name)
        {
            var userKey = _mapContext.Where(kv => kv.Key == name).FirstOrDefault();

            return userKey.IsNotNull() ? true : false;
        }
    }
}
