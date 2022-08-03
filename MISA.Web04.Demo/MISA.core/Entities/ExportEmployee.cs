using MISA.core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.core.Entities
{
    public class ExportEmployee
    {
        public ExportEmployee(int index, Employee employee)
        {
            this.Index = index;
            this.EmployeeCode = employee.EmployeeCode;
            this.FullName = employee.FullName;
            this.GenderNam = employee.GenderName;
            this.DateOfBirth = employee.DateOfBirth;
            this.Email = employee.Email;
            this.Mobile = employee.Mobile;
            this.IdentityNumber = employee.IdentityNumber;
            this.DepartmentName= employee.DepartmentName;
            this.PositionName = employee.PositionName;
        }
        public int Index { get; set; }
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
        /// Ngày sinh
        /// </summary>
        public string GenderNam { get; set; }

        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? PhoneNumber { get; set; }
        public string? IdentityNumber { get; set; }
        public string? PositionName { get; set; }
        public string? DepartmentName { get; set; }
    }
}
