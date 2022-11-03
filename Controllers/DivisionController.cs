using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DivisionController : ControllerBase
    {
        private DivisionRepository divisionRepository;
        public DivisionController(DivisionRepository divisionRepository)
        {
            this.divisionRepository = divisionRepository;
        }

        //Get All
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var data = divisionRepository.Get();
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

        //Get By Id
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            try
            {
                var data = divisionRepository.GetById(id);
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

        //Create
        [HttpPost]
        public ActionResult Create(Division division)
        {
            try
            {
                var data = divisionRepository.Create(division);
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
        //Update
        [HttpPut]
        public ActionResult Update(Division division)
        {
            try
            {
                var data = divisionRepository.Update(division);
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
        //Delete
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                var data = divisionRepository.Delete(id);
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
