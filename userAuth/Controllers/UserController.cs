using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using userAuth.Enums;
using userAuth.Model;
using userAuth.Model.ViewModels;

namespace userAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        public readonly UserContext _context;
        public UserController(IConfiguration config, UserContext context)
        {
            _config = config;
            _context = context;
        }
        [HttpGet("RegisterRole")]
        public async Task<IActionResult> GetRegisterRole()
        {
            return Ok(_context.Roles.ToList());
        }
     /*  [AllowAnonymous]
        [HttpPost("createUser")]

       public async Task<IActionResult> Create(UserViewModel user)
        {
            var response = new SurveyResponse();
            try
            {
                var userExists = await _context.Users.Where(u => u.Email == user.Email).FirstOrDefaultAsync();
                if (userExists != null)
                    return Ok("AlreadyExist");
                
                var dbUser =  new Model.User()
                {
                    Email = user.Email,
                  
                    Password = user.Password
                };
                
                await _context.Users.AddAsync(dbUser);        
               


                int countOfChanges = await _context.SaveChangesAsync();

                if (countOfChanges > 0)
                {
                    //user has been successfully created in database
                    int userId = dbUser.UserId;
                    
                    //now, we need to add this user's role in database
                    if (user.IsStudent)
                    {
                        int stuentRoleId = (int)UserRoles.Student;
                        await _context.UserRoles.AddAsync(new UserRole()
                        {
                            UserId = userId,
                            RoleId = stuentRoleId
                        });
                    }
                    else
                    {
                        await _context.UserRoles.AddAsync(new UserRole()
                        {
                            UserId = userId,
                            RoleId = (int)UserRoles.Employer
                        });
                    }
                   await _context.SaveChangesAsync();
                }

                response.Success = true;
                return Ok(response);
            }
            catch (Exception exception)
            {
                response.Success = false;
                response.ErrorMessage = exception.Message;
                return Ok(response);
            }*/
        } 
        [AllowAnonymous]
        [HttpPost("createRegisterUser")]

        public async Task<IActionResult>CreateRegister(UserViewModel user)
        
        {
            var response = new SurveyResponse();
            try
            {
                //var userExists = await _context.Registers.Where(u => u.Email == user.Email).FirstOrDefaultAsync();
            
                if (userExists != null)
                    return Ok("AlreadyExist");

                var dbUser = new Model.RegisterUser()
                {
                    Email = user.Email,
                   // FullName = user.FullName,
                    //copy other properties
                    //UserName = user.UserName,
                    Password = user.Password,
                    
                };
                await _context.Registers.AddAsync(dbUser);
                int countOfChanges = await _context.SaveChangesAsync();

                if (countOfChanges > 0)
                {
                    //user has been successfully created in database
                    int userId = dbUser.UserId;

                    //now, we need to add this user's role in database
                    if (user.IsStudent)
                    {
                        int stuentRoleId = (int)UserRoles.Student;
                        await _context.UserRoles.AddAsync(new UserRole()
                        {
                            UserId = userId,
                            RoleId = stuentRoleId
                        });
                    }
                    else
                    {
                        await _context.UserRoles.AddAsync(new UserRole()
                        {
                            UserId = userId,
                            RoleId = (int)UserRoles.Employer
                        });
                    }
                   // await _context.SaveChangesAsync();
                }

                response.Success = true;
                return Ok(response);
            }
            catch (Exception exception)
            {
                response.Success = false;
                response.ErrorMessage = exception.Message;
                return Ok(response);
            }
        }

        [AllowAnonymous]
        [HttpGet("getUser")]
        public async Task<IActionResult> GetUser()
        {
          // var response = new SurveyResponse();
            return Ok(await _context.Registers.ToListAsync());


        }
      /*  [AllowAnonymous]
        [HttpGet]
       [ Route("{id:int}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            // var response = new SurveyResponse();
            // return Ok(await _context.Registers.ToListAsync());

            var specficUserDetail = await _context.Users.FindAsync(id);
            
            
           
            await _context.SaveChangesAsync();
            return Ok(specficUserDetail);
        }*/


        [AllowAnonymous]
        [HttpPost("loginUser")]
        public async Task<IActionResult> Login(Login user)
         { 
            
             var userExists = await _context.Registers.Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefaultAsync();
             if (userExists != null)
             {
                 var userRole = _context.UserRoles.FirstOrDefault(p => p.UserId == userExists.UserId);

                 return Ok(new jwtService(_config).GenerateToken(
                     userExists.UserId.ToString(),
                    // userExists.CNIC,
                     userExists.Email,
                     // userExists.FullName,
                      userRole != null ? userRole.RoleId : null
                      //userExists.IsApproved
                     )
                    );

             }
             else
                 return Ok("Failure");


         }
    /* [AllowAnonymous]
     [HttpGet("ApproveUser")]
     [Route("{id:int}")]
     public async Task<IActionResult> ApproveUser(LoginViewModel user, int id)
     {
         var userFind = await _context.Users.FindAsync(id);
         var flag=user.IsApproved = true;

         await _context.SaveChangesAsync();
         return Ok(userFind);
     }*/

    [AllowAnonymous]
    [HttpPost("ApproveUser/{id}")]
}
    }

