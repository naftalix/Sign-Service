using System;
using System.Collections.Generic;
using System.Text;
using SignService.Models;

namespace SignService
{
    public interface ISignService
    {
        public UserModel GetUser(string name);
        public List<string> GetAllUsers();
        public UserModel CreateUser(string Name, string lastName, string email);
        public void DeleteUser(string name);
        public string GetUserKey(string name, string keyId);
        public Tuple<string,int> GenerateUserSigningKey(string userName, int keySize);
        public void DeleteUserKey(string name, string keyId);



    }
}
