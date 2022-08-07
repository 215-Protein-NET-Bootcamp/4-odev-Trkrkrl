using Business.Abstract;
using Core.Entities.Concrete;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase

    {

        private readonly  IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }
        [HttpGet]
        public async Task<IActionResult> GetPaginationAsync([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            
            var route = Request.Path.Value;

            PaginationFilter pagination = new PaginationFilter(pageNumber, pageSize);

            var result = await _personService.GetPaginationAsync(pagination,new PersonDto(), route);

            if (!result.Success)
            {
                return BadRequest(result);


            }

            /*if (result.Response is null)
                return NoContent();*/

            return Ok(result);
        }

        
        

        
    }
}
