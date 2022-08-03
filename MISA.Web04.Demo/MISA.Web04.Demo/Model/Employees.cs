namespace MISA.Web04.Demo.Model
{
    /// <summary>
    /// Nhân viên
    /// </summary>
    public class Employees
    {
        /// <summary>
        /// khóa chính
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; } = string.Empty;

        /// <summary>
        /// Họ và tên
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Giới tính
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string? Email { get; set; }
        public string? Mobile { get; set; } 
        public string? PhoneNumber { get; set; }
        public string? IdentityNumber { get; set; }
        public string? IdentityPlace { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid PositionId { get; set; }
        public double? Salary { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set; }
        /// <summary>
        /// Ngày sửa đổi
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Người sửa đổi
        /// </summary>
        public string? ModifiedBy { get; set; }
    }
}
