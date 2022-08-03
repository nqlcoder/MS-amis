using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.core.Entities;

namespace MISA.core.Interfaces
{
    public interface IEmployeeRepository:IBaseRepository<Employee>
    {
        /// <summary>
        /// Lọc nhân viên và phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="employeeFilter"></param>
        /// <param name="departmentId"></param>
        /// <param name="positionId"></param>
        /// <returns>Tất cả bản ghi và số trang</returns>
        /// CreatedBy: NQLINH (18/6/2022)
        Object FilterEmployees(int? pageSize, int? pageNumber, string? employeeFilter, string? departmentId, string? positionId);
       
        /// <summary>
        /// Kiểm tra mã nv đã tồn tại hay chưa
        /// </summary>
        /// <param name="employeeCode">Mã nv</param>
        /// <returns>true - đã có; false - chưa có</returns>
        /// createdBy: NQL (12/06/2022)
        bool CheckEmployeeCodeDuplidate(string employeeCode);
        public string NewEmployeeCode();
        
    }
}
