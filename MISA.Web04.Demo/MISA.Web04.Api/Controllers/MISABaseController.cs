using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.core.Interfaces;

namespace MISA.Web04.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MISABaseController<MISAEntity> : ControllerBase
    {
        IBaseRepository<MISAEntity> _repository;
        IBaseService<MISAEntity> _service;

        public MISABaseController(
            IBaseRepository<MISAEntity> repository,
            IBaseService<MISAEntity> service)
        {
            _repository = repository;
            _service = service;
        }

        /// <summary>
        /// Base: Lấy ra tất cả nhân viên
        /// </summary>
        /// <returns>danh sách nhân viên</returns>
        /// CreateBy: NQLINH (18/6/2022)
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.GetAll());
        }

        /// <summary>
        /// Base: Lấy ra thông tin nhân viên tjeo id đó
        /// </summary>
        /// <param name="id"></param>
        /// <returns>thông tin nhân viên theo id đó</returns>
        /// CreateBy: NQLINH (18/6/2022)
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(_repository.GetByID(id));
        }

        /// <summary>
        /// Base: Thêm nhân viên theo id mới nhất
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>200: Thêm thành công</returns>
        /// CreateBy: NQLINH (18/6/2022)
        [HttpPost]
        public IActionResult Post(MISAEntity entity)
        {
            return Ok(_service .InsertService(entity));
        }

        /// <summary>
        /// Base: Sửa nhân viên theo id
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>200: Sửa thành công</returns>
        /// CreateBy: NQLINH (18/6/2022)
        [HttpPut("{id}")]
        public IActionResult Update(MISAEntity entity)
        {
            return Ok(_service.UpdateService(entity));
        }

        /// <summary>
        /// Base: Xóa nhân viên theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>200: xóa thành công</returns>
        /// CreateBy: NQLINH (18/6/2022)
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return Ok(_repository.Delete(id));
        }
    }
}
