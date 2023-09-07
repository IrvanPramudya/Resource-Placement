using API.DTOs.Interviews;
using API.DTOs.Placements;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/interviews")]
    [Authorize(Roles = "Admin,Operasional")]
    [EnableCors]
    public class InterviewController : ControllerBase
    {
        private readonly InterviewService _interviewService;

        public InterviewController(InterviewService interviewService)
        {
            _interviewService = interviewService;
        }

        [HttpGet("CountEmployeeClientInterview")]
        public IActionResult CountEmployeeinInterview()
        {
            var data = _interviewService.GetCountedInterview();
            if (data == null)
            {
                return StatusCode(404, new ResponseHandler<GetCountedClient>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data is not Found",
                });
            }
            return Ok(new ResponseHandler<IEnumerable<GetCountedClient>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Retrieved",
                Data = data
            });
        }
        [HttpGet("GetAllEmployee")]
        public IActionResult GetAllEmployee()
        {
            var result = _interviewService.GetAllEmployee().Where(inter => inter.InterviewDate == null && inter.PlacementGuid == null); ;
            if (result == null)
            {
                return NotFound(new ResponseHandler<GetRemainingEmployee>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<GetRemainingEmployee>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }
        [HttpGet("CountEmployeeStatusinInterview")]
        public IActionResult CountEmployeeStatusinInterview()
        {
            var result = _interviewService.CountInterviewStatus();
            if (result == null)
            {
                return NotFound(new ResponseHandler<GetCountedInterviewStatus>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<GetCountedInterviewStatus>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }
        [HttpGet("GetAllEmployeeRejectedbyClient")]
        public IActionResult GetAllEmployeeRejectedbyClient()
        {
            var result = _interviewService.GetAllInterviewEmployeePlacement().Where(inter=>inter.Status == Utilities.Enums.InterviewLevel.RejectedbyClient);
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<GetRemainingEmployee>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<GetRemainingEmployee>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }
        [HttpGet("GetAllEmployeeAcceptedbyClient")]
        public IActionResult GetAllEmployeeAcceptedbyClient()
        {
            var result = _interviewService.GetAllInterviewEmployeePlacement().Where(inter=>inter.Status == Utilities.Enums.InterviewLevel.AcceptedbyClient);
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<GetRemainingEmployee>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<GetRemainingEmployee>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }
        [HttpGet("GetAllEmployeeRejectedbyEmployee")]
        public IActionResult GetAllEmployeeRejectedbyEmployee()
        {
            var result = _interviewService.GetAllInterviewEmployeePlacement().Where(inter=>inter.Status == Utilities.Enums.InterviewLevel.RejectedbyEmployee);
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<GetRemainingEmployee>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<GetRemainingEmployee>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }
        [HttpGet("GetAllEmployeeAcceptedbyEmployee")]
        public IActionResult GetAllEmployeeAcceptedbyEmployee()
        {
            var result = _interviewService.GetAllInterviewEmployeePlacement().Where(inter=>inter.Status == Utilities.Enums.InterviewLevel.AcceptedbyEmployee);
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<GetRemainingEmployee>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<GetRemainingEmployee>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }
        [HttpGet("GetAllEmployeeWaiting")]
        public IActionResult GetAllEmployeeWaiting()
        {
            var result = _interviewService.GetAllInterviewEmployeePlacement().Where(inter=>inter.Status == Utilities.Enums.InterviewLevel.EmployeeResponWaiting);
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<GetRemainingEmployee>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<GetRemainingEmployee>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }
        [HttpPut("UpdateFullInterview")]
        [AllowAnonymous]
        public IActionResult UpdateFullInterview(UpdateInterviewDto interviewDto)
        {
            var result = _interviewService.UpdateFullInterview(interviewDto);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<InterviewDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            if (result is 0)
            {
                return StatusCode(500, new ResponseHandler<InterviewDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error Retrieve From Database"
                });
            }

            return Ok(new ResponseHandler<InterviewDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Update Data Success"
            });
        }
        [HttpGet("GetRemainingEmployeeinPlacement")]
        public IActionResult GetRemainingEmployeeinPlacement()
        {
            var result = _interviewService.GetEmployeeOuterJoinPlacement();
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<GetRemainingEmployee>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<GetRemainingEmployee>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }
        [HttpGet("GetRemainingEmployeeinInterview")]
        public IActionResult GetRemainingEmployeeinInterview()
        {
            var result = _interviewService.GetEmployeeOuterJoinInterview();
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<GetRemainingEmployee>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<GetRemainingEmployee>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }
        [HttpGet("InterviewBeforeAcceptedbyClient")]
        public IActionResult GetInterviewBeforeAcceptedbyClient()
        {
            var result = _interviewService.GetUnresultInterviews();
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<InformatifInterview>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<InformatifInterview>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }
        [HttpGet("InterviewInformatif")]
        public IActionResult GetInterviewInformatif()
        {
            var result = _interviewService.GetInterviews();
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<InformatifInterview>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<InformatifInterview>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _interviewService.GetAll();
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<InterviewDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<InterviewDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }

        [HttpGet("{guid}")]
        [AllowAnonymous]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _interviewService.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseHandler<InterviewDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is Not Found"
                });
            }

            return Ok(new ResponseHandler<InterviewDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }

        [HttpPost]
        public IActionResult Insert(NewInterviewDto newInterviewDto)
        {
            var result = _interviewService.Create(newInterviewDto);
            if (result is null)
            {
                return StatusCode(500, new ResponseHandler<InterviewDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error Retrieve From Database",
                    AdditionalMessage = "Employee Already in Site or Employee Have Not Register its Grade yet"
                });
            }

            return Ok(new ResponseHandler<NewInterviewDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Insert Data Success",
                Data = newInterviewDto
            });
        }

        [HttpPut]
        public IActionResult Update(NewInterviewDto interviewDto)
        {
            var result = _interviewService.Update(interviewDto);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<InterviewDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            if (result is 0)
            {
                return StatusCode(500, new ResponseHandler<InterviewDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error Retrieve From Database"
                });
            }

            return Ok(new ResponseHandler<InterviewDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Update Data Success"
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var result = _interviewService.Delete(guid);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<InterviewDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Not Found"
                });
            }

            if (result is 0)
            {
                return StatusCode(500, new ResponseHandler<InterviewDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error Retrieve From Database"
                });
            }

            return Ok(new ResponseHandler<InterviewDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Delete Data Success"
            });
        }
    }
}
