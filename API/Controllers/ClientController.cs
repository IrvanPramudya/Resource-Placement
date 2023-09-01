using API.DTOs.Clients;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/clients")]
    /*[Authorize]*/
    public class ClientController : ControllerBase
    {
        private readonly ClientService _clientService;

        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }
        [HttpGet("GetCountClient")]
        public IActionResult CountClient()
        {
            var result = _clientService.CountClient();
            if(result == null)
            {
                return NotFound(new ResponseHandler<GetCountClient>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            return Ok(new ResponseHandler<GetCountClient>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }
        [HttpGet("GetUnavailableClient")]
        public IActionResult GetUnavailableClient()
        {
            var result = _clientService.GetAllClient().Where(client=>client.IsAvailable == false);
            if(!result.Any())
            {
                return NotFound(new ResponseHandler<GetAvailableClient>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            return Ok(new ResponseHandler<IEnumerable<GetAvailableClient>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }
        [HttpGet("GetAvailableClient")]
        public IActionResult GetAvailableClient()
        {
            var result = _clientService.GetAllClient().Where(client=>client.IsAvailable == true);
            if(!result.Any())
            {
                return NotFound(new ResponseHandler<GetAvailableClient>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }
            return Ok(new ResponseHandler<IEnumerable<GetAvailableClient>>
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
            var result = _clientService.GetAll();
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<ClientDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<ClientDto>>
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
            var result = _clientService.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseHandler<ClientDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid is Not Found"
                });
            }

            return Ok(new ResponseHandler<ClientDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success Retrieve Data",
                Data = result
            });
        }

        [HttpPost]
        public IActionResult Insert(NewClientDto newClientDto)
        {
            var result = _clientService.Create(newClientDto);
            if (result is null)
            {
                return StatusCode(500, new ResponseHandler<ClientDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error Retrieve From Database"
                });
            }

            return Ok(new ResponseHandler<NewClientDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Insert Data Success",
                Data = newClientDto
            });
        }

        [HttpPut]
        public IActionResult Update(UpdateClientDto clientDto)
        {
            var result = _clientService.Update(clientDto);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<UpdateClientDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            if (result is 0)
            {
                return StatusCode(500, new ResponseHandler<UpdateClientDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error Retrieve From Database"
                });
            }

            return Ok(new ResponseHandler<UpdateClientDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Update Data Success"
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var result = _clientService.Delete(guid);

            if (result is -1)
            {
                return NotFound(new ResponseHandler<ClientDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Not Found"
                });
            }

            if (result is 0)
            {
                return StatusCode(500, new ResponseHandler<ClientDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Error Retrieve From Database"
                });
            }

            return Ok(new ResponseHandler<ClientDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Delete Data Success"
            });
        }
    }
}
