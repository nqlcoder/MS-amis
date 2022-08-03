using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.core.Interfaces;
using MISA.infrastructure.Repository;
using MISA.core.Entities;

namespace MISA.Web04.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : MISABaseController<Department>
    {
        IDepartmentService _service;
        IDepartmentRepository _repository;
        public DepartmentsController(IConfiguration configuration, 
            IDepartmentService service, 
            IDepartmentRepository repository):base(service:service, repository:repository)
        {
            _service = service;
            _repository = repository;
        }

        
    }
}
