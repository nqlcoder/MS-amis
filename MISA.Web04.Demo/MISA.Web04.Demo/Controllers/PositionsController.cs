using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Dapper;
using MISA.Web04.Demo.Model;

namespace MISA.Web04.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        string _connectionString;
        /// <summary>
        /// Gọi chuỗi connectString được lưu bên appsetting
        /// </summary>
        /// <param name="configuration"></param>
        public PositionsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("NQLINH");
        }
        /// <summary>
        ///  Lấy ra toàn bộ vị trí
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetPositions()
        {
            try
            {
                //1. Kết nối db
                //- Khai báo thông tin db:
                //var connectionString = "Host = 3.0.89.182; Port = 3306; Database = MISA.WEB04.NQLINH; User Id = dev; Password = 12345678";
                //- Khởi tạo kết nối
                var sqlConnection = new MySqlConnection(_connectionString);
                //2. Lấy dữ liệu db
                var positions = sqlConnection.Query<Positions>("Proc_GetPositions", commandType: System.Data.CommandType.StoredProcedure);
                //3. Trả dữ liệu về client
                return StatusCode(200, positions);
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
        /// <summary>
        /// lấy ra vị trí theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // truyền theo query string
        //[HttpGet("{search}")]
        // truyền theo router
        [HttpGet("{id}")]
        public IActionResult GetPositionById(string? id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }
                else
                {
                    //1. Kết nối db
                    //- Khai báo thông tin db:
                    //var connectionString = "Host = 3.0.89.182; Port = 3306; Database = MISA.WEB04.NQLINH; User Id = dev; Password = 12345678";
                    //- Khởi tạo kết nối
                    var sqlConnection = new MySqlConnection(_connectionString);
                    //2. Lấy dữ liệu db
                    var sqlQuery = $"SELECT * FROM Positions WHERE PositionID = '{id}'";
                    var getPositionById = sqlConnection.Query<Positions>(sqlQuery);
                    //3. Trả dữ liệu về client
                    return StatusCode(200, getPositionById);
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
        /// <summary>
        /// Post 1 vị trí
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(Positions position)
        {
            try
            {
                //validate dữ liệu
                if (string.IsNullOrEmpty(position.PositionName))
                {
                    var errorValidate = new
                    {
                        userMsg = "Tên phòng ban không được phép để trống"
                    };
                    return StatusCode(400, errorValidate);
                }
                else
                {
                    //1. Kết nối db
                    //- Khai báo thông tin db:
                    //var connectionString = "Host = 3.0.89.182; Port = 3306; Database = MISA.WEB04.NQLINH; User Id = dev; Password = 12345678";
                    //- Khởi tạo kết nối
                    var sqlConnection = new MySqlConnection(_connectionString);
                    //2. Thực hiện thêm mới
                    var sqlCommand = "Proc_InsertPosition";
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@m_PositionName", position.PositionName);

                    var res = sqlConnection.Execute(sqlCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                    //3. Trả dữ liệu về client
                    return StatusCode(201, res);
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
        /// <summary>
        /// sửa 1 vị trí
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdatePositionById(string id, Positions position)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    var error = new
                    {
                        userMsg = "Mã id không được để trống"
                    };
                    return StatusCode(400, error);
                } else
                {
                    //1. kết nối db
                    //- khởi tạo kết nối
                    var sqlConnection = new MySqlConnection(_connectionString);
                    //2. thực hiện lấy dữ liệu trong db
                    //- thực hiện sửa
                    var sqlCommnad = "Proc_UpdatePosition";
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@m_PositionId", position.PositionId);
                    parameters.Add("@m_PositionName", position.PositionName);
                    var res = sqlConnection.Execute(sqlCommnad, param : parameters, commandType: System.Data.CommandType.StoredProcedure);

                    //3. trả về dữ liệu
                    return Ok(res);
                }
            } catch(Exception ex)
            {
                var res = new {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra, liên hệ MISA để được trợ giúp"
                };
                return StatusCode(500, res);
            }
        }
        /// <summary>
        /// xóa 1 vị trí
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeletePositionById(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    var res = new { 
                        userMsg = "Id không được để trống"
                    };
                    return StatusCode(400, res);
                } else
                {
                    //1. Kết nối db
                    //- khởi tạo kết nối
                    var sqlConnection = new MySqlConnection(_connectionString);
                    //2. Thực hiện lấy dữ liệu db
                    //- thực hiện xóa
                    var sqlQuery = $"DELETE FROM Positions WHERE PositionId  = '{id}'";
                    var deletePosition = sqlConnection.Query<Positions>(sqlQuery);
                    //3. trả về dữ liệu
                    return Ok(deletePosition);
                }
            } catch(Exception ex)
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
