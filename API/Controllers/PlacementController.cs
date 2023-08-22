using API.DTOs.Placements;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/placements")]
    /*[Authorize]*/
    public class PlacementController : ControllerBase
    {
        private readonly PlacementService _placementService;

        public PlacementController(PlacementService placementService)
        {
            _placementService = placementService;
        }

<<<<<<< Updated upstream
=======
        [HttpGet("GetEmployeeClientName")]
        public IActionResult GetEmployeeClientName()
        {
            var data = _placementService.GetEmployeeClientName();
            if (data == null)
            {
                return StatusCode(404, new ResponseHandler<GetEmployeeClientName>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data is not Found",
                });
            }
            return Ok(new ResponseHandler<IEnumerable<GetEmployeeClientName>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Retrieved",
                Data = data
            });
        }

        [HttpGet("CountClient")]
        public IActionResult CountEmployee()
        {
            var data = _placementService.GetCountedClient();
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
>>>>>>> Stashed changes
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _placementService.GetAll();
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<PlacementDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<PlacementDto>>
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
            var result = _placementService.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseHandler<PlacementDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is Not Found"
                });
            }

            return Ok(new ResponseHandler<PlacementDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }

        [HttpPost]
        public IActionResult Insert(NewPlacementDto newPlacementDto)
        {
            var result = _placementService.Create(newPlacementDto);
            if (result is null)
            {
                return StatusCode(500, new ResponseHandler<PlacementDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error Retrieve From Database"
                });
            }

            return Ok(new ResponseHandler<NewPlacementDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Insert Data Success",
                Data = newPlacementDto
            });
        }

        [HttpPut]
        public IActionResult Update(PlacementDto placementDto)
        {
            var result = _placementService.Update(placementDto);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<PlacementDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            if (result is 0)
            {
                return StatusCode(500, new ResponseHandler<PlacementDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error Retrieve From Database"
                });
            }

            return Ok(new ResponseHandler<PlacementDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Update Data Success"
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var result = _placementService.Delete(guid);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<PlacementDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Not Found"
                });
            }

            if (result is 0)
            {
                return StatusCode(500, new ResponseHandler<PlacementDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error Retrieve From Database"
                });
            }

            return Ok(new ResponseHandler<PlacementDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Delete Data Success"
            });
        }
    }
}
