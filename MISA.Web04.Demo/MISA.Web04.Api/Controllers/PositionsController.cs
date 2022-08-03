using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.core.Entities;
using MISA.core.Interfaces;
using MISA.infrastructure.Repository;

namespace MISA.Web04.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        IPositionRepository _repository;
        public PositionsController(IConfiguration configuration, IPositionRepository repository)
        {
            _repository = repository;
        }
      
        [HttpGet]
        public IActionResult GetAllPosition()
        {
            var positions = _repository.GetAllPosition();
            return Ok(positions);
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(Guid id)
        {
            try
            {
                var positions = _repository.GetByID(id);
                return Ok(positions);
            }
            catch (Exception ex)
            {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra, liên hệ MISA để được trợ giúp"
                };
                return StatusCode(500, res);
            }
        }
       
        [HttpPost]
        public IActionResult Insert(Positions positions)
        {
            try
            {
                if (string.IsNullOrEmpty(positions.PositionName))
                {
                    var serviceResult = new
                    {
                        userMsg = "Tên vị trí không được phép để trống"
                    };
                    return BadRequest(serviceResult);
                }
                else
                {
                    var Insertposition = _repository.Insert(positions);
                    return Ok(Insertposition);
                }
            }
            catch (Exception ex)
            {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra, liên hệ MISA để được trợ giúp"
                };
                return StatusCode(500, res);
            }
        }
      
        [HttpPut("{id}")]
        public IActionResult Update(string id, Positions positions)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    var serviceResult = new
                    {
                        userMsg = "Id vị trí không được phép để trống"
                    };
                    return BadRequest(serviceResult);
                }
                if (string.IsNullOrEmpty(positions.PositionName))
                {
                    var serviceResult = new
                    {
                        userMsg = "Id phòng ban không được phép để trống"
                    };
                    return BadRequest(serviceResult);
                }
                else
                {
                    var UpdatePositions = _repository.Update(id, positions);
                    return Ok(UpdatePositions);
                }
            }
            catch (Exception ex)
            {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra, liên hệ MISA để được trợ giúp"
                };
                return StatusCode(500, res);
            }
        }
       
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var DeletePosition = _repository.Delete(id);
                return Ok(DeletePosition);
            }
            catch (Exception ex)
            {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra, liên hệ MISA để được trợ giúp"
                };
                return StatusCode(500, res);
            }
        }
    }
}
