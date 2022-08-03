using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web04.Demo.Model;
using MySqlConnector;
using Dapper;
using System.Data;

namespace MISA.Web04.Demo.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        string _connectionString;
        MySqlConnection _mySqlConnection;
        /// <summary>
        /// Gọi chuỗi connectString được lưu bên appsetting
        /// </summary>
        /// <param name="configuration"></param>
        public EmployeesController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("NQLINH");
            _mySqlConnection = new MySqlConnection(_connectionString);
        }
        /// <summary>
        ///  Lấy ra toàn bộ thông tin nhân viên
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetEmployee()
        {
            try
            {
                //1. Kết nối db
                //- Khai báo thông tin db:
                //var connectionString = "Host = 3.0.89.182; Port = 3306; Database = MISA.WEB04.NQLINH; User Id = dev; Password = 12345678";
                //- Khởi tạo kết nối
                //2. Lấy dữ liệu db
                var employee = _mySqlConnection.Query<Employees>("Proc_GetEmployees", commandType: System.Data.CommandType.StoredProcedure);
                //3. Trả dữ liệu về client
                return StatusCode(200, employee);
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
        /// lấy ra nhân viên theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // truyền theo query string
        //[HttpGet("{search}")]
        // truyền theo router
        [HttpGet("{id}")]
        public IActionResult GeEmployeeById(string? id)
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
                    //2. Lấy dữ liệu db
                    var sqlQuery = $"SELECT * FROM Employee WHERE EmployeeId = '{id}'";
                    var getEmployeeById = _mySqlConnection.Query<Employees>(sqlQuery);
                    //3. Trả dữ liệu về client
                    return StatusCode(200, getEmployeeById);
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
        /// Post 1 nhân viên
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(Employees employee)
        {
            try
            {
                //validate dữ liệu
                if (string.IsNullOrEmpty(employee.EmployeeCode))
                {
                    var errorValidate = new
                    {
                        userMsg = "Mã NV không được phép để trống"
                    };
                    return StatusCode(400, errorValidate);
                }
                if (string.IsNullOrEmpty(employee.FullName))
                {
                    var errorValidate = new
                    {
                        userMsg = "Tên nhân viên không được phép để trống"
                    };
                    return StatusCode(400, errorValidate);
                }
                if (string.IsNullOrEmpty(employee.Email))
                {
                    var errorValidate = new
                    {
                        userMsg = "Email không được phép để trống"
                    };
                    return StatusCode(400, errorValidate);
                }
                //2. Kiểm tra định dạng Email
                var isValidEmail = IsValidEmail(employee.Email);
                if (!isValidEmail)
                {
                    var serviceResult = new {
                        userMsg = "Email không đúng định dạng, vui lòng kiểm tra lại"
                    };
                    return BadRequest(serviceResult);
                }
                //3. Kiểm tra ngày sinh có lớn hơn hiện tại
                var dateOfBirth = employee.DateOfBirth;
                var currentDate = DateTime.Now;
                if(dateOfBirth > currentDate)
                {
                    var serviceResult = new
                    {
                        userMsg = "Ngày sinh không được lớn hơn ngày hiện tại, vui lòng kiểm tra lại"
                    };
                    return BadRequest(serviceResult);
                }
                //4. Kiểm tra mã nv đã tồn tại hay chưa và có độ dài lớn hơn 20 kí tự hay không?
                if(employee.EmployeeCode.Length > 20)
                {
                    var serviceResult = new
                    {
                        userMsg = "Mã nhân viên không được phép lớn hơn 20 kí tự, vui lòng kiểm tra lại"
                    };
                    return BadRequest(serviceResult);
                }
                var sqlCheckEmployeeCode = $"SELECT EmployeeCode FROM Employee WHERE EmployeeCode = @EmployeeCode'";
                DynamicParameters paramCheck = new DynamicParameters();
                paramCheck.Add("@EmployeeCode", employee.EmployeeCode);
                var employeeCodeDuplicate = _mySqlConnection.QueryFirstOrDefault(sqlCheckEmployeeCode, param: paramCheck);
                if (employeeCodeDuplicate != null)
                {
                    var serviceResult = new
                    {
                        userMsg = "Mã nhân viên đã tồn tại, vui lòng kiểm tra lại"
                    };
                    return BadRequest(serviceResult);
                }
                //1. Kết nối db
                //- Khai báo thông tin db:
                //var connectionString = "Host = 3.0.89.182; Port = 3306; Database = MISA.WEB04.NQLINH; User Id = dev; Password = 12345678";
                //- Khởi tạo kết nối
                //2. Thực hiện thêm mới

                var sqlCommand = "Proc_InsertEmployee";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@m_EmployeeID", employee.EmployeeId);
                parameters.Add("@m_EmployeeCode", employee.EmployeeCode);
                parameters.Add("@m_EmployeeName", employee.FullName);
                parameters.Add("@m_Gender", employee.Gender);
                parameters.Add("@m_Email", employee.Email);
                parameters.Add("@m_Mobile", employee.Mobile);
                parameters.Add("@m_Salary", employee.Salary);

                var res = _mySqlConnection.Execute(sqlCommand, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                //3. Trả dữ liệu về client
                return StatusCode(201, res);
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
        /// Sửa 1 nhân viên
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult UpdateEmployeeById(string id, Employees employees)
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
                }
                else
                {
                    //1. kết nối db
                    //- khởi tạo kết nối
                    //2. thực hiện lấy dữ liệu trong db
                    //- thực hiện sửa
                    var sqlCommnad = "Proc_UpdateEmployee";
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@m_EmployeeId", employees.EmployeeId);
                    parameters.Add("@m_EmployeeCode", employees.EmployeeCode);
                    parameters.Add("@m_EmployeeName", employees.FullName);
                    parameters.Add("@m_EmployeeGender", employees.Gender);
                    parameters.Add("@m_EmployeeDOB", employees.DateOfBirth);
                    parameters.Add("@m_EmployeeEmail", employees.Email);
                    parameters.Add("@m_EmployeeMobile", employees.Mobile);
                    parameters.Add("@m_EmployeeIdentityNumber", employees.IdentityNumber);
                    parameters.Add("@m_EmployeeIdentityPlace", employees.IdentityPlace);
                    parameters.Add("@m_DepartmentId", employees.DepartmentId);
                    parameters.Add("@m_PositionId", employees.PositionId);
                    parameters.Add("@m_EmployeeSalary", employees.Salary);
                    var res = _mySqlConnection.Execute(sqlCommnad, param: parameters, commandType: System.Data.CommandType.StoredProcedure);

                    //3. trả về dữ liệu
                    return Ok(res);
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
        /// Xóa nhân viên theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult DeletePositionById(string? id)
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
                    //1. Kết nối db
                    //- Khai báo thông tin db:
                    //var connectionString = "Host = 3.0.89.182; Port = 3306; Database = MISA.WEB04.NQLINH; User Id = dev; Password = 12345678";
                    //- Khởi tạo kết nối
                    //2. Lấy dữ liệu db

                    var sqlQuery = $"DELETE FROM Employee WHERE EmployeeId  = '{id}'";
                    var deleteEmployeeById = _mySqlConnection.Query<Employees>(sqlQuery);
                    //3. Trả dữ liệu về client
                    return StatusCode(200, deleteEmployeeById);
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
        ///  Lấy ra mã nv mới
        /// </summary>
        /// <returns></returns>
        [HttpGet("{EmployeeCode}")]
        public IActionResult GetEmployeeCode()
        {
            try
            {
                //1. Kết nối db
                //- Khai báo thông tin db:
                //var connectionString = "Host = 3.0.89.182; Port = 3306; Database = MISA.WEB04.NQLINH; User Id = dev; Password = 12345678";
                //- Khởi tạo kết nối
                //2. Lấy dữ liệu db
                var employee = _mySqlConnection.Query<Employees>("Proc_GetEmployeeCode", commandType: System.Data.CommandType.StoredProcedure);
                //3. Trả dữ liệu về client
                return StatusCode(200, employee);
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
        /// Tìm kiếm, phân trang danh sách nhân viên theo (Mã hoặc họ tên hoặc số điện thoại)
        /// </summary>
        [HttpGet("filter")]
        public IActionResult FiterEmployees(int? pageSize, int? pageNumber, string? employeeFilter, string? departmentId, string? positionId)
        {
            try
            {
                //1. kết nối đến db
                // - Khỏi tạo kết nối
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@m_KeyWord", employeeFilter);
                parameters.Add("@m_PageSize", pageSize);
                parameters.Add("@m_PageNumber", pageNumber);
                parameters.Add("@m_DepartmentId", departmentId);
                parameters.Add("@m_PositionId", positionId);
                parameters.Add("@m_TotalPage", DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@m_TotalRecord", DbType.Int32, direction: ParameterDirection.Output);
                var Data = _mySqlConnection.Query<Employees>("Proc_FilterEmployee", parameters, commandType: System.Data.CommandType.StoredProcedure);
                int TotalPage = parameters.Get<int>("@m_TotalPage");
                int TotalRecord = parameters.Get<int>("@m_TotalRecord");
                var data = new
                {
                    TotalRecord,
                    TotalPage,
                    Data
                };
                return Ok(data);
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
        /// Hàm kiểm tra định dạng email
        /// </summary>
        /// <param name="email">Chuỗi email</param>
        /// <returns>true - hợp lệ; false - không hợp lệ</returns>
        /// CreatedBy: NQL (11/06/2022)
        private bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}
