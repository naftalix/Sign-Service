using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SignStorageApi.Infrastructure;
using SignStorageApi.Models;
using SignStorageApi.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SignStorageApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SignController : Controller
    {

        private readonly ISignService _signService;
        private readonly ILogger<SignController> _logger;

        public SignController(ISignService signService, ILogger<SignController> logger)
        {
            _signService = signService;
            _logger = logger;
        }

        [HttpGet(Name = nameof(GetAllUsers))]
        [ValidateModel]
        public IEnumerable<string> GetAllUsers()
        {
            return _signService.GetAllUsers();
        }

        [HttpGet("{name}", Name = nameof(GetUser))]
        [ValidateModel]
        public IActionResult GetUser(string name)
        {
            try
            {
                var user = _signService.GetUser(name);
                return new OkObjectResult(user);

            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpPost("create", Name = nameof(CreateUser))]
        [ValidateModel]
        public IActionResult CreateUser([FromBody]UserModel user)
        {
            if (user == null || !user.Name.NotEmpty())
            {
                return BadRequest();
            }

            try
            {
                _signService.CreateUser(user.Name, user.LastName, user.Email);

                return CreatedAtRoute("GetUser", new { name = user.Name }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("update/{userName}")]
        [ValidateModel]
        public IActionResult UpdateUser(string userName, [FromBody]UserModel data)
        {
            if (data == null || !userName.NotEmpty() || data.Name != userName)
            {
                return BadRequest();
            }

            try
            {
                /**/
                var user = _signService.GetUser(userName);

               // _signService.UpdateUser(data);

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("generatekey/{userName}/size/{keySize}")]
        [ValidateModel]
        public IActionResult GenerateKeyForUser(string userName, int keySize)
        {

            if (!userName.NotEmpty())
            {
                return BadRequest();
            }

            try
            {
                var user = _signService.GenerateUserSigningKey(userName, keySize);               

                return Json(user);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("delete/{userName}")]
        [ValidateModel]
        public IActionResult Delete(string userName)
        {

            try
            {
                _signService.DeleteUser(userName);

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}





