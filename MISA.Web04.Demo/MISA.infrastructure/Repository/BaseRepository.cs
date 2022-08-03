using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.core.Interfaces;

namespace MISA.infrastructure.Repository
{
    public class BaseRepository<MISAEntity> : IBaseRepository<MISAEntity> where MISAEntity : class
    {
        protected string ConnectionString;
        protected MySqlConnection SqlConnection;
        private string _className;
        public BaseRepository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("NQLINH");
            SqlConnection = new MySqlConnection(ConnectionString);
            _className = typeof(MISAEntity).Name;
        }
        public IEnumerable<MISAEntity> GetAll() => GetAll(null);
        /// <summary>
        /// Lấy ra tất cả đối tượng
        /// </summary>
        /// <param name="storeName"></param>
        /// <returns></returns>
        /// Created By: NQL(16/06/2022)
        public IEnumerable<MISAEntity> GetAll(string? storeName = null) 
        {
            var className = typeof(MISAEntity).Name;
            if(storeName == null)
            {
                storeName = $"Proc_Get{_className}s";
            }
            
            var entities = SqlConnection.Query<MISAEntity>(storeName, commandType: System.Data.CommandType.StoredProcedure);
            return entities;
        }
        /// <summary>
        /// Lay doi tuong theo id
        /// </summary>
        /// <param name="id">Gia tri khoa chinh</param>
        /// <returns>doi tuong</returns>
        /// Created By: NQL(16/06/2022)
        public MISAEntity GetByID(Guid id)
        {
            var sql = $"SELECT * FROM {_className} WHERE {_className}Id = @{_className}Id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"{_className}Id", id);
            var entity = SqlConnection.QueryFirstOrDefault<MISAEntity>(sql, param: parameters);
            return entity;
        }
        /// <summary>
        /// Hàm base: Thêm một đối tượng mới
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Created By: NQL(16/06/2022)
        public int Insert(MISAEntity entity)
        {
            var storeInsert = $"Proc_Insert{_className}";
            var rowEffects = SqlConnection.Execute(storeInsert, param: entity, commandType: System.Data.CommandType.StoredProcedure);
            return rowEffects;
        }
        /// <summary>
        /// hàm base: Update, sửa thông tin đối tượng
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Created By: NQL(16/06/2022)
        public int Update(MISAEntity entity)
        {
            var storeUpdate = $"Proc_Update{_className}";
            var rowEffects = SqlConnection.Execute(storeUpdate, param: entity, commandType: System.Data.CommandType.StoredProcedure);
            return rowEffects;
        }
        /// <summary>
        /// Hàm base: Xóa một đối tượng theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Created By: NQL(16/06/2022)
        public int Delete(Guid id)
        {
            var sql = $"DELETE FROM {_className} WHERE {_className}Id = @{_className}Id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"{_className}Id", id);
            var rowEffects = SqlConnection.Execute(sql, param: parameters);
            return rowEffects;
        }

       
    }
   
}
