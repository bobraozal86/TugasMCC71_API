using API.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.InteropServices;

namespace API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<Repository, Entity> : ControllerBase
        where Repository : class, IRepository<Entity, int>
        where Entity : class
    {
        Repository repository;
        public BaseController(Repository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
            var data = repository.Get();
                if(data != null)
                {
                return Ok(new { message = "Data Has Been Retrieved", StatusCode = 200, data = data });

                }
                else
                {
                    return Ok(new { message = "Data Not Found", StatusCode = 404 });
                }

            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
            var data = repository.GetById(id);
                if(data != null)
                {
                    return Ok(new { message = "Data Has Been Retrieved", statusCode = 200, data = data });
                }
                else
                {
                    return Ok(new { message = "Data Has Been Retrieved", statusCode = 200});
                }

            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }
        [HttpPost]
        public IActionResult Create(Entity entity)
        {
            var data =  repository.Create(entity);
            try
            {
                if(data != null)
                {
                    return Ok(new {message = "Data Has Been Created", statusCode = 200, data = data});
                }
                else
                {
                    return Ok(new { message = "Data Has Been Retrieved", statusCode = 200 });
                }

            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }
        [HttpPut]
        public IActionResult Update(Entity entity)
        {
            var data = repository.Update(entity);
            try
            {
                if (data != null)
                {
                    return Ok(new { message = "Data Has Been Updated", statusCode = 200, data = data });
                }
                else
                {
                    return Ok(new { message = "Data Has Been Retrieved", statusCode = 200 });
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data  =  repository.Delete(id);
            try
            {
                if(data != null)
                {
                    return Ok(new { message = "Data Has Been Deleted", statusCode = 200, data = data });

                }
                else
                {
                    return Ok(new { message = "Data Has Been Retrieved", statusCode = 200 });
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }
    }
}
