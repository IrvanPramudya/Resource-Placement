﻿using API.DTOs.Positions;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/positions")]
    [Authorize(Roles = "Admin,Operasional")]
    [EnableCors]
    public class PositionController : ControllerBase
    {
        private readonly PositionService _positionService;

        public PositionController(PositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpGet("GetPosition/{guid}")]
        public IActionResult GetPositionwithClientGuid(Guid guid) 
        {
            var result = _positionService.GetPositionwithClientGuid(guid);
            if(!result.Any())
            {
                return NotFound(new ResponseHandler<GetClientName>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            return Ok(new ResponseHandler<IEnumerable<GetClientName>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });

        }
        [HttpGet("GetClientName")]
        public IActionResult GetClientName() 
        {
            var result = _positionService.GetClientName();
            if(!result.Any())
            {
                return NotFound(new ResponseHandler<GetClientName>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            return Ok(new ResponseHandler<IEnumerable<GetClientName>>
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
            var result = _positionService.GetAll();
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<PositionDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<PositionDto>>
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
            var result = _positionService.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseHandler<PositionDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is Not Found"
                });
            }

            return Ok(new ResponseHandler<PositionDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }

        [HttpPost]
        public IActionResult Insert(NewPositionDto newPositionDto)
        {
            var result = _positionService.Create(newPositionDto);
            if (result is null)
            {
                return StatusCode(500, new ResponseHandler<PositionDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error Retrieve From Database"
                });
            }

            return Ok(new ResponseHandler<NewPositionDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Insert Data Success",
                Data = newPositionDto
            });
        }

        [HttpPut]
        public IActionResult Update(PositionDto positionDto)
        {
            var result = _positionService.Update(positionDto);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<PositionDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            if (result is 0)
            {
                return StatusCode(500, new ResponseHandler<PositionDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error Retrieve From Database"
                });
            }

            return Ok(new ResponseHandler<PositionDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Update Data Success"
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var result = _positionService.Delete(guid);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<PositionDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Not Found"
                });
            }

            if (result is 0)
            {
                return StatusCode(500, new ResponseHandler<PositionDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error Retrieve From Database"
                });
            }

            return Ok(new ResponseHandler<PositionDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Delete Data Success"
            });
        }
    }
}
