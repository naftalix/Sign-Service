using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.Json.Serialization;

namespace SignStorageApi.Models
{
    public class UserModel
    {

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public List<string> Keys { get; set; }

        public List<string> Files { get; set; }


    }
}
