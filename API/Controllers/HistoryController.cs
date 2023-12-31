﻿using API.DTOs.Histories;
using API.DTOs.NewHistoryDto;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/histories")]
    [Authorize]
    [EnableCors]
    public class HistoryController : ControllerBase
    {
        private readonly HistoryService _historyService;

        public HistoryController(HistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpGet("CountStatusHistories")]
        public IActionResult CountStatusHistories()
        {
            var result = _historyService.CountStatusHistoies();
            if (result == null)
            {
                return NotFound(new ResponseHandler<CountStatusHistories>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<CountStatusHistories>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }
        [HttpGet("GetLastHistory/{guid}")]
        public IActionResult GetLastHistory(Guid guid)
        {
            var result = _historyService.GetLastHistory(guid);
            if (result == null)
            {
                return NotFound(new ResponseHandler<HistoryDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<HistoryDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }
        [HttpGet("GetHistoryByEmployeeGuid/{guid}")]
        public IActionResult GetHistoryByEmployeeGuid(Guid guid)
        {
            var result = _historyService.GetHistoryByEmployeeGuid(guid);
            if (result == null)
            {
                return NotFound(new ResponseHandler<HistoryDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<HistoryDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }
        [HttpGet("CountHistoriesbyEmployeeGuid/{guid}")]
        public IActionResult CountHistoriesbyEmployeeGuid(Guid guid)
        {
            var result = _historyService.CountHistoriesbyEmployeeGuid(guid);
            if (result == null)
            {
                return NotFound(new ResponseHandler<CountAllHistories>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<CountAllHistories>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }
        [HttpGet("CountAllHistories")]
        public IActionResult CountAllHistories()
        {
            var result = _historyService.CountAllHistories();
            if (result == null)
            {
                return NotFound(new ResponseHandler<CountAllHistories>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<CountAllHistories>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }
        [HttpGet("GetAllHistorieswithName")]
        public IActionResult GetAllHistorieswithName()
        {
            var result = _historyService.GetAllHistoriesWithName();
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<HistoryDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<HistoryDto>>
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
            var result = _historyService.GetAll();
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<HistoryDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<HistoryDto>>
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
            var result = _historyService.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseHandler<HistoryDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is Not Found"
                });
            }

            return Ok(new ResponseHandler<HistoryDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }

        [HttpPost]
        public IActionResult Insert(NewHistoryDto newHistoryDto)
        {
            var result = _historyService.Create(newHistoryDto);
            if (result is null)
            {
                return StatusCode(500, new ResponseHandler<HistoryDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error Retrieve From Database"
                });
            }

            return Ok(new ResponseHandler<NewHistoryDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Insert Data Success",
                Data = newHistoryDto
            });
        }

        [HttpPut]
        public IActionResult Update(HistoryDto historyDto)
        {
            var result = _historyService.Update(historyDto);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<HistoryDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            if (result is 0)
            {
                return StatusCode(500, new ResponseHandler<HistoryDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error Retrieve From Database"
                });
            }

            return Ok(new ResponseHandler<HistoryDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Update Data Success"
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var result = _historyService.Delete(guid);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<HistoryDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Not Found"
                });
            }

            if (result is 0)
            {
                return StatusCode(500, new ResponseHandler<HistoryDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error Retrieve From Database"
                });
            }

            return Ok(new ResponseHandler<HistoryDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Delete Data Success"
            });
        }
    }
}
