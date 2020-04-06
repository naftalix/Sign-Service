using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using SignService.Models;
using SignService.Infrastructure;
using System.Linq;

namespace SignService
{
    public class SignService : ISignService
    {

        private readonly ISignEngine _signEngine;
        private readonly IStorage _storage;
        private readonly IHostEnvironment _host;
        private readonly string _mapPath;
        private readonly string _mapId;
        private Dictionary<string, string> _mapContext;

        public SignService(ISignEngine signEngine, IStorage storage, IHostEnvironment host)
        {
            _signEngine = signEngine;
            _storage = storage;
            _host = host;

            var uploadFolder = Path.Combine(_host.ContentRootPath, "MapContextStorage");
            var filePath = Path.Combine(uploadFolder, "map.json");
            _mapPath = filePath;

            using (StreamReader r = new StreamReader(filePath))
            { 
                string json = r.ReadToEnd();

                dynamic map = JsonConvert.DeserializeObject(json);

                _mapId = (string)map.Id;

                var mapBinaryData = _storage.GetData(_mapId);

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
            var dataModel = new UserModel
            {
                Name = name,
                LastName = lastName,
                Email = email
            };

            var binaryData = dataModel.ToByteArray();

            var binaryModelKey = _storage.AddData(binaryData);

            _mapContext.Add(name, binaryModelKey);

            UpdateStorageMap();

            return dataModel;
        }

        public void DeleteUser(string name)
        {
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

        public Tuple<string,int> GenerateUserSigningKey(string userName, int keySize)
        {
            var key = _signEngine.GenerateKeyPair(keySize);
            var user = GetUser(userName);
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
            var userStorageKey = _mapContext[name];

            var userBinaryData = _storage.GetData(userStorageKey);

            var result =  userBinaryData.FromByteArray<UserModel>();
            return result;

        }

        public string GetUserKey(string name, string keyId)
        {
            var user = GetUser(name);

            return user.Keys.Where(k => k == keyId).First();
        }
    }
}
