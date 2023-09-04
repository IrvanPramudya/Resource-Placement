using API.DTOs.Grades;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/grades")]
    [Authorize(Roles = "Admin,Operasional,Trainer")]
    [EnableCors]
    public class GradeController : ControllerBase
    {
        private readonly GradeService _gradeService;

        public GradeController(GradeService gradeService)
        {
            _gradeService = gradeService;
        }

        [HttpGet("CountEmployeeinGrade")]
        public IActionResult CountEmployeeinGrade()
        {
            var result = _gradeService.CountEmployeeinGrade();
            if (result == null)
            {
                return NotFound(new ResponseHandler<CountEmployee>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<CountEmployee>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }
        [HttpGet("GetEmployeeName")]
        public IActionResult GetEmployeeName()
        {
            var result = _gradeService.GetEmployeeNames();
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<GetEmployeeName>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<GetEmployeeName>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }

        [HttpGet("GetUngradedEmployee")]
        public IActionResult GetUngradedEmployee()
        {
            var result = _gradeService.GetAllEmployeewithGrade().Where(grade=>grade.GradeName==null);
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<GradewithName>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<GradewithName>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }
        [HttpGet("GetGradedEmployee")]
        public IActionResult GetGradedEmployee()
        {
            var result = _gradeService.GetAllEmployeewithGrade().Where(grade=>grade.GradeName!=null && grade.Status == Utilities.Enums.StatusLevel.Idle);
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<GradewithName>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<GradewithName>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }
        [HttpGet("GradewithName")]
        public IActionResult GradewithName()
        {
            var result = _gradeService.GetwithName();
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<GradewithName>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<GradewithName>>
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
            var result = _gradeService.GetAll();
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<GradeDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<GradeDto>>
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
            var result = _gradeService.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseHandler<GradeDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is Not Found"
                });
            }

            return Ok(new ResponseHandler<GradeDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }

        [HttpPost]
        public IActionResult Insert(NewGradeDto newGradeDto)
        {
            var result = _gradeService.Create(newGradeDto);
            if (result is null)
            {
                return StatusCode(500, new ResponseHandler<GradeDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error Retrieve From Database"
                });
            }

            return Ok(new ResponseHandler<NewGradeDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Insert Data Success",
                Data = newGradeDto
            });
        }

        [HttpPut]
        public IActionResult Update(GradeDto gradeDto)
        {
            var result = _gradeService.Update(gradeDto);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<GradeDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            if (result is 0)
            {
                return StatusCode(500, new ResponseHandler<GradeDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error Retrieve From Database"
                });
            }

            return Ok(new ResponseHandler<GradeDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Update Data Success"
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var result = _gradeService.Delete(guid);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<GradeDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Not Found"
                });
            }

            if (result is 0)
            {
                return StatusCode(500, new ResponseHandler<GradeDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error Retrieve From Database"
                });
            }

            return Ok(new ResponseHandler<GradeDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Delete Data Success"
            });
        }
    }
}
