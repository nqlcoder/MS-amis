using MISA.core.Entities;
using MISA.core.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace MISA.infrastructure.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {

        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {

        }
        /// <summary>
        /// khi update khiểm tra thông tin nhân viên đó có đúng của mã nhân viên đó ko
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        /// CreateBy: NQLINH (18/6/2022)
        public bool CheckEmployeeCodeDuplidate(string employeeCode)
        {
            var sqlQuery = "SELECT EmployeeCode FROM Employee WHERE EmployeeCode = @m_EmployeeCode";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@m_EmployeeCode", employeeCode);
            var employeeCodeDuplidate = SqlConnection.QueryFirstOrDefault(sqlQuery, param: parameters);
            if (employeeCodeDuplidate != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Hàm phân trang và tìm kiếm nhân viên
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="employeeFilter"></param>
        /// <param name="departmentId"></param>
        /// <param name="positionId"></param>
        /// <returns>Tổng bản ghi và số trang</returns>
        /// CreateBy: NQLINH (18/6/2022)
        public Object FilterEmployees(int? pageSize, int? pageNumber, string? employeeFilter, string? departmentId, string? positionId)
        {
            //var sqlCommand = "Proc_EmployeeFilter";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@m_KeyWord", employeeFilter);
            parameters.Add("@m_PageSize", pageSize);
            parameters.Add("@m_PageNumber", pageNumber);
            parameters.Add("@m_DepartmentId", departmentId);
            parameters.Add("@m_PositionId", positionId);
            parameters.Add("@m_TotalPage", DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@m_TotalRecord", DbType.Int32, direction: ParameterDirection.Output);
            var Data = SqlConnection.Query<Employee>("Proc_EmployeeFilter", parameters, commandType: System.Data.CommandType.StoredProcedure);

            var TotalPage = parameters.Get<dynamic>("@m_TotalPage");
            var TotalRecord = parameters.Get<dynamic>("@m_TotalRecord");
            Object data = new
            {
                TotalRecord,
                TotalPage,
                Data
            };
            return data;
        }

        /// <summary>
        /// Lấy ra mã nhân viên mới mà không bị trùng với mã nhân viên trước đó
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// CreateBy: NQLINH (18/6/2022)
        public string NewEmployeeCode()
        {
            var sqlCommand = "Proc_GetNewEmployeeCode";
            var newEmployeeCode = SqlConnection.QueryFirstOrDefault<string>(sqlCommand, commandType: System.Data.CommandType.StoredProcedure);
            return newEmployeeCode;
        }
    }
}
