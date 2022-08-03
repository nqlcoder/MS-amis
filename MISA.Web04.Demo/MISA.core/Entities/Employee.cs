using MISA.core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.core.Entities
{
    /// <summary>
    /// Nhân viên
    /// </summary>
    /// CreatedBy: NQLINH (18/5/2022)
    public class Employee
    {
        /// <summary>
        /// khóa chính
        /// </summary>
        /// CreatedBy: NQLINH (18/5/2022)
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        /// CreatedBy: NQLINH (18/5/2022)
        [MISAExcelColumn]
        [PropertyNameDisplay("Mã nhân viên")]
        public string EmployeeCode { get; set; } = string.Empty;

        /// <summary>
        /// Họ và tên
        /// </summary>
        /// CreatedBy: NQLINH (18/5/2022)
        [MISAExcelColumn]
        [PropertyNameDisplay("Tên nhân viên")]
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Giới tính
        /// </summary>
        /// CreatedBy: NQLINH (18/5/2022)
        public Gender? Gender { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        /// CreatedBy: NQLINH (18/5/2022)
        [MISAExcelColumn]
        [PropertyNameDisplay("Ngày sinh")]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        /// CreatedBy: NQLINH (18/5/2022)
        [MISAExcelColumn]
        [PropertyNameDisplay("Email")]
        public string? Email { get; set; }

        /// <summary>
        /// SĐT di động
        /// </summary>
        /// CreatedBy: NQLINH (18/5/2022)
        [MISAExcelColumn]
        [PropertyNameDisplay("SĐT")]
        public string? Mobile { get; set; }

        /// <summary>
        /// SĐT cố định
        /// </summary>
        /// CreatedBy: NQLINH (18/5/2022)
        [MISAExcelColumn]
        [PropertyNameDisplay("Số ĐT cố định")]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Số CMND
        /// </summary>
        /// CreatedBy: NQLINH (18/5/2022)
        [MISAExcelColumn]
        [PropertyNameDisplay("Số CMND")]
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// Nơi cấp
        /// </summary>
        /// CreatedBy: NQLINH (18/5/2022)
        [MISAExcelColumn]
        [PropertyNameDisplay("Nơi cấp")]
        public string? IdentityPlace { get; set; }

        /// <summary>
        /// Ngày cấp
        /// </summary>
        /// CreatedBy: NQLINH (18/5/2022)
        [MISAExcelColumn]
        [PropertyNameDisplay("Ngày cấp")]
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Mã phòng ban
        /// </summary>
        /// CreatedBy: NQLINH (18/5/2022)
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Mã chức vụ
        /// </summary>
        /// CreatedBy: NQLINH (18/5/2022)
        public Guid PositionId { get; set; }

        /// <summary>
        /// Lương
        /// </summary>
        /// CreatedBy: NQLINH (18/5/2022)
        public double? Salary { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        /// CreatedBy: NQLINH (18/5/2022)
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        /// CreatedBy: NQLINH (18/5/2022)
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa đổi
        /// </summary>
        /// CreatedBy: NQLINH (18/5/2022)
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa đổi
        /// </summary>
        /// CreatedBy: NQLINH (18/5/2022)
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        /// CreatedBy: NQLINH (18/5/2022)
        [MISAExcelColumn]
        [PropertyNameDisplay("Giới tính")]
        public string GenderName
        {
            get
            {
                switch (Gender)
                {
                    case Enum.Gender.FeMale: 
                        return "Nữ";
                        break;
                    case Enum.Gender.Male:
                        return "Nam";
                        break;
                    default:
                        return "";
                        break;
                }
            }
        }

        /// <summary>
        /// Chức vụ
        /// </summary>
        /// CreatedBy: NQLINH (18/5/2022)
        [MISAExcelColumn]
        [PropertyNameDisplay("Chức vụ")]
        public string? PositionName { get; set; }

        /// <summary>
        /// Phòng ban
        /// </summary>
        /// CreatedBy: NQLINH (18/5/2022)
        [MISAExcelColumn]
        [PropertyNameDisplay("Phòng ban")]
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Số TK
        /// </summary>
        /// CreatedBy: NQLINH (18/5/2022)
        [MISAExcelColumn]
        [PropertyNameDisplay("Số TK")]
        public string? BankAccountNumber { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        /// CreatedBy: NQLINH (18/5/2022)
        [MISAExcelColumn]
        [PropertyNameDisplay("Tên ngân hàng")]
        public string? BankName { get; set; }

        /// <summary>
        /// Chi nhánh
        /// </summary>
        /// CreatedBy: NQLINH (18/5/2022)
        [MISAExcelColumn]
        [PropertyNameDisplay("Chi nhánh")]
        public string? BankBranchName { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        /// CreatedBy: NQLINH (18/5/2022)
        [MISAExcelColumn]
        [PropertyNameDisplay("Địa chỉ")]
        public string? Address { get; set; }
    }
}
