using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private EmployeeRepository employeeRepository;
        public EmployeeController(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        //Get All
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var data = employeeRepository.Get();
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
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }

        }

        //Get By Id
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            try
            {
                var data = employeeRepository.GetById(id);
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
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }

        }

        //Create
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            try
            {
                var data = employeeRepository.Create(employee);
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
                        StatusCode = 200,
                        Message = "Success To Create Data"
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }

        }
        //Update
        [HttpPut]
        public ActionResult Update(Employee employee)
        {
            try
            {
                var data = employeeRepository.Update(employee);
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
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }

        }
        //Delete
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                var data = employeeRepository.Delete(id);
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
            catch (Exception ex)
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
