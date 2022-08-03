using MISA.core.Entities;
using MISA.core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace MISA.infrastructure.Repository
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        
        public DepartmentRepository(IConfiguration configuration):base(configuration)
        {
            
        }
        public bool CheckDepartmentIdDuplidate(string departmentId)
        {
            var sqlQuery = "SELECT DepartmentId FROM Department WHERE DepartmentId = @m_DepartmentId";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@m_DepartmentId", departmentId);
            var DepartmentDuplidate = SqlConnection.QueryFirstOrDefault(sqlQuery, param: parameters);
            if (DepartmentDuplidate != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }      
        
    }
}
