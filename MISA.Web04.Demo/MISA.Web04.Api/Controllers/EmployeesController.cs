using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.core.Entities;
using MISA.core.Exceptions;
using MISA.core.Interfaces;
using MISA.core.Resources;
using OfficeOpenXml;
using System.Drawing;

namespace MISA.Web04.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : MISABaseController<Employee>
    {
        IEmployeeService _service;
        IEmployeeRepository _repository;
        public EmployeesController(IEmployeeService service, 
            IEmployeeRepository repository):base(repository, service)
        {
            _service = service;
            _repository = repository;
        }

        /// <summary>
        /// Phương thức get:filter: lọc nhân viên và tìm kiếm
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="employeeFilter"></param>
        /// <param name="departmentId"></param>
        /// <param name="positionId"></param>
        /// <returns>Tổng bản ghi và số trang</returns>
        /// CreateBy: NQLINH (18/6/2022)
        [HttpGet("filter")]
        public IActionResult FilterEmployees(int? pageSize, int? pageNumber, string? employeeFilter, string? departmentId, string? positionId)
        {
            return Ok(_repository.FilterEmployees(pageSize, pageNumber, employeeFilter, departmentId, positionId));
        }

        /// <summary>
        /// Lấy ra mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// CreateBy: NQLINH (18/6/2022)
        [HttpGet("NewEmployeeCode")]
        public IActionResult NewEmployeeCode()
        {
            return Ok(_repository.NewEmployeeCode());
        }

        /// <summary>
        /// Phương thức get: exporter: xuất ra danh sách naahn viên excel
        /// </summary>
        /// <returns>xuất ra excel danh sách nhân viên</returns>
        /// CreateBy: NQLINH (18/6/2022)
        [HttpGet("exporter")]
        public IActionResult Export()
        {
            try
            {
                var stream = _service.ExportExcel();

                var fileName = ResourceVN.EmployeeExcelFileName;

                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

    }
}
