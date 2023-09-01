﻿using API.DTOs.Accounts;
using API.DTOs.Employees;
using API.Models;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/employees")]
    /*[Authorize]*/
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet("GetAllEmployeeinIdle")]
        public IActionResult GetAllEmployeeinIdle()
        {
            var data = _employeeService.GetAllEmployeeinIdle();
            if (data == null)
            {
                return StatusCode(404, new ResponseHandler<GetReportEmployee>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data is not Found",
                });
            }
            return Ok(new ResponseHandler<IEnumerable<GetReportEmployee>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Retrieved",
                Data = data
            });
        }
        [HttpGet("GetAllEmployeeinSite")]
        public IActionResult GetAllEmployeeinSite()
        {
            var data = _employeeService.GetAllEmployeeinSite();
            if (data == null)
            {
                return StatusCode(404, new ResponseHandler<GetReportEmployee>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data is not Found",
                });
            }
            return Ok(new ResponseHandler<IEnumerable<GetReportEmployee>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Retrieved",
                Data = data
            });
        }
        [HttpGet("GetEmployeeOuterJoin")]
        public IActionResult GetEmployeeOuterJoin()
        {
            var data = _employeeService.GetEmployeeinGrade();
            if (data == null)
            {
                return StatusCode(404, new ResponseHandler<GetEmployeeinGrade>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data is not Found",
                });
            }
            return Ok(new ResponseHandler<IEnumerable<GetEmployeeinGrade>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Retrieved",
                Data = data
            });
        }
        [HttpGet("GetAllReportEmployee")]
        public IActionResult GetAllReportEmployee()
        {
            var data = _employeeService.GetAllReportedEmployee();
            if (data == null)
            {
                return StatusCode(404, new ResponseHandler<GetReportEmployee>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data is not Found",
                });
            }
            return Ok(new ResponseHandler<IEnumerable<GetReportEmployee>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Retrieved",
                Data = data
            });
        }
        [HttpGet("GetReportEmployee/{guid}")]
        public IActionResult GetReportEmployee(Guid guid)
        {
            var data = _employeeService.GetReportedEmployee(guid);
            if (data == null)
            {
                return StatusCode(404, new ResponseHandler<GetReportEmployee>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data is not Found",
                });
            }
            return Ok(new ResponseHandler<GetReportEmployee>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Retrieved",
                Data = data
            });
        }
        [HttpGet("GetEmployeeNotification/{guid}")]
        public IActionResult GetEmployeeNotification(Guid guid)
        {
            var data = _employeeService.GetNotification(guid);
            if (data == null)
            {
                return StatusCode(404, new ResponseHandler<GetEmployeeNotification>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data is not Found",
                });
            }
            return Ok(new ResponseHandler<GetEmployeeNotification>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Retrieved",
                Data = data
            });
        }
        [HttpGet("CountEmployee")]
        public IActionResult CountEmployeewithoutRole()
        {
            var data = _employeeService.CountEmployee();
            if (data == null)
            {
                return StatusCode(404, new ResponseHandler<int>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data is not Found",
                });
            }
            return Ok(new ResponseHandler<int>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Retrieved",
                Data = data
            });
        }
        [HttpGet("GetCountedGender")]
        public IActionResult CountGender()
        {
            var data = _employeeService.CountGender();
            if (data == null)
            {
                return StatusCode(404, new ResponseHandler<GetCountedGender>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data is not Found",
                });
            }
            return Ok(new ResponseHandler<GetCountedGender>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Retrieved",
                Data = data
            });
        }
        [HttpGet("GetCountedStatus")]
        public IActionResult CountStatus()
        {
            var data = _employeeService.CountStatus();
            if (data == null)
            {
                return StatusCode(404, new ResponseHandler<GetCountedStatus>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data is not Found",
                });
            }
            return Ok(new ResponseHandler<GetCountedStatus>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Retrieved",
                Data = data
            });
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _employeeService.GetAll();
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<EmployeeDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<EmployeeDto>>
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
            var result = _employeeService.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseHandler<EmployeeDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is Not Found"
                });
            }

            return Ok(new ResponseHandler<EmployeeDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }

        [HttpPost]
        public IActionResult Insert(NewEmployeeDto newEmployeeDto)
        {
            var result = _employeeService.Create(newEmployeeDto);
            if (result is null)
            {
                return StatusCode(500, new ResponseHandler<EmployeeDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error Retrieve From Database"
                });
            }

            return Ok(new ResponseHandler<NewEmployeeDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Insert Data Success",
                Data = newEmployeeDto
            });
        }

        [HttpPut]
        public IActionResult Update(UpdateEmployeeDto employeeDto)
        {
            var result = _employeeService.Update(employeeDto);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<UpdateEmployeeDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            if (result is 0)
            {
                return StatusCode(500, new ResponseHandler<UpdateEmployeeDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error Retrieve From Database"
                });
            }

            return Ok(new ResponseHandler<UpdateEmployeeDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Update Data Success"
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var result = _employeeService.Delete(guid);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<EmployeeDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Not Found"
                });
            }

            if (result is 0)
            {
                return StatusCode(500, new ResponseHandler<EmployeeDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error Retrieve From Database"
                });
            }

            return Ok(new ResponseHandler<EmployeeDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Delete Data Success"
            });
        }
    }
}
