using MISA.core.Entities;
using MISA.core.Exceptions;
using MISA.core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.core.Services
{
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        IDepartmentRepository _repository;
        public DepartmentService(IDepartmentRepository repository) : base(repository)
        {
            _repository = repository;
        }
        protected override bool Validate(Department department)
        {
            //validate dữ liệu

            if (string.IsNullOrEmpty(department.DepartmentName))
            {
                ErrorValidateMsgs.Add(Resources.ResourceVN.VN_ValidateError_EmployeeNameNotEmpty);
                return false;
            }
            return true;
        }
        
    }
}
