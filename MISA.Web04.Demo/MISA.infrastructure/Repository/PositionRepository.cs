using MISA.core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.core.Entities;
using MySqlConnector;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace MISA.infrastructure.Repository
{
    public class PositionRepository : IPositionRepository
    {
        string _connectionString;
        MySqlConnection _sqlConnection;
        public PositionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("NQLINH");
            _sqlConnection = new MySqlConnection(_connectionString);
        }
        public IEnumerable<Positions> Delete(Guid id)
        {
            var DeletePosition = _sqlConnection.Query<Positions>($"DELETE FROM Positions WHERE PositionId  = '{id}'");
            return DeletePosition;
        }

        public IEnumerable<Positions> GetAllPosition()
        {
            var position = _sqlConnection.Query<Positions>("SELECT * FROM Positions");
            return position;
        }

        public IEnumerable<Positions> GetByID(Guid id)
        {
            var getPositionById = _sqlConnection.Query<Positions>($"SELECT * FROM Positions WHERE PositionId = '{id}'");
            return getPositionById;
        }

        public int Insert(Positions positions)
        {
            var sqlCommand = "Proc_InsertPosition";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@m_PositionName", positions.PositionName);
            var res = _sqlConnection.Execute(sqlCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
            return res; ;
        }

        public int Update(string id, Positions positions)
        {
            var sqlCommand = "Proc_UpdatePosition";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@m_PositionId", positions.PositionId);
            parameters.Add("@m_PositionName", positions.PositionName);
            var res = _sqlConnection.Execute(sqlCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
            return res;
        }
    }
}
