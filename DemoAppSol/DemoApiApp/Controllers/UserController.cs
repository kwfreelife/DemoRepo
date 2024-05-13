using DemoApiApp.Interface;
using DemoApiApp.Model;
using DemoApiApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoApiApp.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
       
        //Create a variable to hold the instance of GenericRepository
        private IGenericRepository<UserDemo> genericRepository = null;
        private IUserRepository userRepository = null;
        public UserController(ILogger<UserController> logger, IConfiguration configuration)
        {
            _logger = logger;
            //Initializing the genericRepository through a parameterless constructor
            this.genericRepository = new GenericRepository<UserDemo>(configuration);
            this.userRepository = new UserRepository(configuration);

            //init user
            //_users = new List<UserDemo>();
            //_users.Add(new UserDemo() { Id = 1, UserName = "Kom", DateOfBirth = new DateTime(1974, 7, 12) });
            //_users.Add(new UserDemo() { Id = 2, UserName = "Din", DateOfBirth = new DateTime(2015, 5, 29) });
        }

        //[HttpGet("[action]")] // GET /api/User
        //[HttpGet("/api/[controller]/users/")]
        //GET api/user
        [HttpGet("")]
        public IEnumerable<UserDemo> GetUsers()
        {
            return genericRepository.GetAll();
        }

        //[HttpGet("/api/[controller]/user/{idToFind}")]
        //GET api/experimental/cars/123
        [HttpGet("{idToFind}")]// GET /api/User/3
        public UserDemo GetUserById(int idToFind)
        {
            return genericRepository.GetById(idToFind);
        }

        //GET api/user/named/Maz ==> [HttpGet("named/{filter?}")]
        //GET api/user/named?filter=Maz&rating=2 ==> [HttpGet("named")]  
        [HttpGet("named")]        
        public IEnumerable<UserDemo> GetUserByName(
            [FromQuery] string filter="", 
            [FromQuery] int rating =2)
        {
            return userRepository.GetByName(filter);
        }


        [HttpPost(Name = "AddUser")] //http://localhost:4000/api/User with method POST
        [Produces("application/json")]
        public string AddUser([FromBody] UserDemo dataValue)
        {
            if (dataValue != null)
            {                
                genericRepository.Add(dataValue);
                genericRepository.Save();
                return $"Add data {dataValue.UserName} success";
            }
            return "No data to append";
            
        }

        [HttpPut(Name = "UpdateUser")] //http://localhost:4000/api/User with method PUT
        [Produces("application/json")]
        public string UpdateUser([FromBody] UserDemo dataValue)
        {
            UserDemo tmp = null;
            if (dataValue != null)
            {
                if (dataValue.Id <= 0)
                {                
                    tmp = GetUserByName(dataValue.UserName).FirstOrDefault();
                    if (tmp != null)
                    {
                        dataValue.Id = tmp.Id; 
                    }
				}
				genericRepository.Update(dataValue);
				genericRepository.Save();
				return $"Update data {dataValue.UserName} success";

			}
            return "No data to update";

        }

        [HttpDelete(Name = "DeleteUser")] //http://localhost:4000/api/User with method Delete
        [Produces("application/json")]
        public string DeleteUser([FromBody] UserDemo dataValue)
        {
            UserDemo tmp = null;
            if (dataValue != null)
            {
                tmp = GetUserByName(dataValue.UserName).FirstOrDefault();
                if (tmp != null)
                {
                    dataValue.Id = tmp.Id;
                    string retval = userRepository.DeleteById(tmp.Id);
                    return $"Deleted data {retval} success";
                }
            }
            return "No data to delete";
        }

		[HttpDelete("{idToDelete}",Name = "DeleteUserById")] //http://localhost:4000/api/User/{idToDelete} with method Delete
		public string DeleteUserById(int idToDelete)
		{
			if (idToDelete > 0)
			{
				string retval = userRepository.DeleteById(idToDelete);
				return $"Deleted data {retval} success";
			}
			return "No id to delete";
		}
	}
}
