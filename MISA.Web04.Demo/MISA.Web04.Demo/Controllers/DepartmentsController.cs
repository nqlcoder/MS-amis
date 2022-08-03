using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web04.Demo.Model;
using MySqlConnector;

namespace MISA.Web04.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        string _connectionString;
        /// <summary>
        /// Gọi chuỗi connectString được lưu bên appsetting
        /// </summary>
        /// <param name="configuration"></param>
        public DepartmentsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("NQLINH");
        }
        /// <summary>
        ///  Lấy ra toàn bộ phòng ban
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetDepartments()
        {
            try
            {
                //1. Kết nối db
                //- Khai báo thông tin db:
                //var connectionString = "Host = 3.0.89.182; Port = 3306; Database = MISA.WEB04.NQLINH; User Id = dev; Password = 12345678";
                //- Khởi tạo kết nối
                var sqlConnection = new MySqlConnection(_connectionString);
                //2. Lấy dữ liệu db
                var departments = sqlConnection.Query<Department>("Proc_GetDepartments", commandType: System.Data.CommandType.StoredProcedure);
                //3. Trả dữ liệu về client
                return StatusCode(200, departments);
            }
            catch(Exception ex)
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
        /// lấy ra phòng ban theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // truyền theo query string
        //[HttpGet("{search}")]
        // truyền theo router
        [HttpGet("{id}")]
        public IActionResult GetDepartmentById(string? id)
        {
            try
            {
                if(id == null)
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
                    var sqlQuery = $"SELECT * FROM Department WHERE DepartmentID = '{id}'";
                    var getDepartmentById = sqlConnection.Query<Department>(sqlQuery);
                    //3. Trả dữ liệu về client
                    return StatusCode(200, getDepartmentById);
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
        /// <summary>
        /// Post 1 phòng ban
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(Department department)
        {
            try
            {
                //validate dữ liệu
                if (string.IsNullOrEmpty(department.DepartmentName))
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
                    var sqlCommand = "Proc_InsertDepartment";
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@m_DepartmentName", department.DepartmentName);

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
        /// Sửa 1 phòng ban theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateDepartmentById(string id, Department department)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    var errorValidate = new
                    {
                        userMsg = "Id phòng ban không đợc phép trống"
                    };
                    return StatusCode(400, errorValidate);
                }
                else
                {
                    //1. kết nối đến db
                    // - Khỏi tạo kết nối
                    var sqlConnection = new MySqlConnection(_connectionString);
                    //2. Lấy dữ liệu trong db
                    //Thực hiện thêm mới
                    var sqlCommand = "Proc_UpdateDepartment";

                    
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@m_DepartmentId", department.DepartmentId);
                    parameters.Add("@m_DepartmentName", department.DepartmentName);

                    var res = sqlConnection.Execute(sqlCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);

                    //3. Trả dữ liệu về
                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra"
                };
                return StatusCode(500, res);
            }
        }

        /// <summary>
        /// Xóa phòng ban theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteDepartmentById(string? id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    var errorValidate = new
                    {
                        userMsg = "Id phòng ban không đợc phép trống"
                    };
                    return StatusCode(400, errorValidate);
                } else
                {
                    //1. Kết nối db
                    //- Khai báo thông tin db:
                    //var connectionString = "Host = 3.0.89.182; Port = 3306; Database = MISA.WEB04.NQLINH; User Id = dev; Password = 12345678";
                    //- Khởi tạo kết nối
                    var sqlConnection = new MySqlConnection(_connectionString);
                    //2. Lấy dữ liệu db

                    var sqlQuery = $"DELETE FROM Department WHERE DepartmentID  = '{id}'";
                    var deleteDepartmentById = sqlConnection.Query<Department>(sqlQuery);
                    //3. Trả dữ liệu về client
                    return StatusCode(200, deleteDepartmentById);
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
