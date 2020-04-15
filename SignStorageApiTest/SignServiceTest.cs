using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SignStorageApi;
using SignStorageApi.Controllers;
using SignStorageApi.Infrastructure;
using SignStorageApi.Models;
using SignStorageApi.Services;
using SignStorageApiTest.Services;
using Xunit;

namespace SignStorageApiTest.UnitTest
{
    public class SignServiceTest
    {
        private IStorageClientService _service { get; set; }

        public SignServiceTest()
        {
            var services = new ServiceCollection();
            services.UseTestServices();

            var serviceProvider = services.BuildServiceProvider();

            var service = serviceProvider.GetRequiredService<IStorageClientService>();

            _service = service;
        }


        [Fact]
        public async Task AddData()
        {
            var data = new UserModel
            {
                Email = "test@test",
                Name = "userName",
                LastName = "lastName"
            };
            

            var byteData = data.ToByteArray();

            var dataId = await _service.AddData(byteData);

            var retriveByteData = await _service.GetData(dataId);

            var retriveData = retriveByteData.FromByteArray<UserModel>();

            Assert.NotNull(retriveData);
            Assert.Equal(retriveData.Email, data.Email);
            Assert.Equal(retriveData.Name, data.Name);
            Assert.Equal(retriveData.LastName, data.LastName);

        }
    }
}
