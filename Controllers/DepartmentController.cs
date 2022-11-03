using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private DepartmentRepository departmentRepository;
        public DepartmentController(DepartmentRepository departmentRepository)
        {
            this.departmentRepository = departmentRepository;
        }
        //Get All
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var data = departmentRepository.Get();
                if (data == null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Not Found"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Found",
                        Data = data
                    });
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
        public ActionResult GetById(int id)
        {
            try
            {
                var data = departmentRepository.GetById(id);
                if (data == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "Data Not Found"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Found",
                        Data = data
                    });
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
        public ActionResult Create(Department department)
        {
            try
            {
                var data = departmentRepository.Create(department);
                if (data == 0)
                {
                    return BadRequest(new
                    {
                        StatusCode = 400,
                        Message = "Can't Create Data"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode=200,
                        Message = "Success To Create Data"
                    });
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
        public ActionResult Update(Department department)
        {
            try
            {
                var data = departmentRepository.Update(department);
                if (data == 0)
                {
                    return BadRequest(new
                    {
                        StatusCode = 400,
                        Message = "Can't Update Data"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Success To Update Data "
                    });
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

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                var data = departmentRepository.Delete(id);
                if (data == 0)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Can't Delete Data"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Success To Delete Data"
                    });
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
