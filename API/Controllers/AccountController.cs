using API.DTOs.Accounts;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    [Authorize(Roles = "Admin")]
    [EnableCors]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }


        [HttpGet("GetDetailAccount/{guid}")]
        public IActionResult GetDetailAccount(Guid guid)
        {
            var result = _accountService.DetailAccount(guid);
            if (result == null)
            {
                return NotFound(new ResponseHandler<GetAccountDetail>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<GetAccountDetail>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }
        [HttpGet("GetAccountwithNameOuterJoin")]
        public IActionResult GetAccountwithNameOuterJoin()
        {
            var result = _accountService.GetAccountwithNamesOuterJoin();
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<GetAccountwithName>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<GetAccountwithName>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }
        [HttpGet("GetAccountwithNameandRole")]
        public IActionResult GetAccountwithNameandRole()
        {
            var result = _accountService.GetAccountwithNamesAndRolesOuterJoin();
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<GetAccountwithNameAndRoles>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<GetAccountwithNameAndRoles>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }
        [HttpGet("GetAccountwithName")]
        public IActionResult GetAccountwithName()
        {
            var result = _accountService.GetAccountwithNames();
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<GetAccountwithName>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<GetAccountwithName>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login(LoginDto loginDto)
        {
            var result = _accountService.Login(loginDto);

            if (result is "-1")
            {
                return NotFound(new ResponseHandler<LoginDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Email or Password is incorrect"
                });
            }

            if (result is "-2")
            {
                return StatusCode(500, new ResponseHandler<LoginDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error when generated token"
                });
            }

            return Ok(new ResponseHandler<object>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Login Success",
                Data = new TokenDto
                {
                    Token = result
                }
            }); ;
        }


        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register(RegisterDto register)
        {
            var data = _accountService.Register(register);
            if (data == -1)
            {
                return StatusCode(500, new ResponseHandler<RegisterDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Register Failed",
                });
            }
            if (data == 0)
            {
                return StatusCode(404, new ResponseHandler<RegisterDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Email or Phone Number Already Used",
                });
            }
            return Ok(new ResponseHandler<int>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Successfull Register",
                Data = data
            });
        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public IActionResult ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var isUpdated = _accountService.ForgotPassword(forgotPasswordDto);
            if (isUpdated is 0)
            {
                return NotFound(new ResponseHandler<ForgotPasswordDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Email Not Found"
                });
            }

            if (isUpdated is -1)
            {
                return StatusCode(500, new ResponseHandler<ForgotPasswordDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error Retrieve From Database"
                });
            }
            return Ok(new ResponseHandler<ForgotPasswordDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Otp has been sent to your email"
            });
        }

        [HttpPost("changepassword")]
        [AllowAnonymous]
        public IActionResult UpdatePassword(ChangePasswordDto changePasswordDto)
        {
            var update = _accountService.ChangePassword(changePasswordDto);
            if (update is 0)
            {
                return NotFound(new ResponseHandler<ChangePasswordDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Email Not Found"
                });
            }

            if (update is -1)
            {
                return NotFound(new ResponseHandler<ChangePasswordDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "OTP doesn't match"
                });
            }

            if (update is -2)
            {
                return NotFound(new ResponseHandler<ChangePasswordDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "OTP is used"
                });
            }

            if (update is -3)
            {
                return NotFound(new ResponseHandler<ChangePasswordDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "OTP already expired"
                });
            }

            if (update == -4)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<ChangePasswordDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error retrieving data from the database"
                });
            }

            return Ok(new ResponseHandler<ChangePasswordDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Password Updated Success"
            });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _accountService.GetAll();
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<AccountDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<AccountDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _accountService.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseHandler<AccountDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is Not Found"
                });
            }

            return Ok(new ResponseHandler<AccountDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }

        [HttpPost]
        public IActionResult Insert(NewAccountDto newAccountDto)
        {
            var result = _accountService.Create(newAccountDto);
            if (result is null)
            {
                return StatusCode(500, new ResponseHandler<AccountDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error Retrieve From Database"
                });
            }

            return Ok(new ResponseHandler<NewAccountDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Insert Data Success",
                Data = newAccountDto
            });
        }

        [HttpPut]
        public IActionResult Update(NewAccountDto accountDto)
        {
            var result = _accountService.Update(accountDto);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<AccountDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            if (result is 0)
            {
                return StatusCode(500, new ResponseHandler<AccountDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error Retrieve From Database"
                });
            }

            return Ok(new ResponseHandler<AccountDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Update Data Success"
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var result = _accountService.Delete(guid);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<AccountDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Not Found"
                });
            }

            if (result is 0)
            {
                return StatusCode(500, new ResponseHandler<AccountDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error Retrieve From Database"
                });
            }

            return Ok(new ResponseHandler<AccountDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Delete Data Success"
            });
        }
    }
}
