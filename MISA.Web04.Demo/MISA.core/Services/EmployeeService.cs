using MISA.core.Entities;
using MISA.core.Exceptions;
using MISA.core.Interfaces;
using MISA.core.Resources;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        IEmployeeRepository _repository;
        public EmployeeService(IEmployeeRepository repository) : base(repository)
        {
            _repository = repository;
        }
        protected override bool Validate(Employee employee)
        {
            var isValid = true;
            // Xử lí nghiệp vụ
            //validate dữ liệu
            if (string.IsNullOrEmpty(employee.EmployeeCode))
            {
                isValid = false;
                ErrorValidateMsgs.Add(Resources.ResourceVN.VN_ValidateError_EmployeeCodeNotEmpty);
            }
            if (string.IsNullOrEmpty(employee.FullName))
            {
                isValid = false;
                ErrorValidateMsgs.Add(Resources.ResourceVN.VN_ValidateError_EmployeeNameNotEmpty);
            }
            if (string.IsNullOrEmpty(employee.Email))
            {
                isValid = false;
                ErrorValidateMsgs.Add(Resources.ResourceVN.VN_ValidateError_EmployeeEmailNotEmpty);
            }
            if (string.IsNullOrEmpty(employee.Mobile))
            {
                isValid = false;
                ErrorValidateMsgs.Add(Resources.ResourceVN.VN_ValidateError_EmployeeMobileNotEmpty);
            }
            //2. Kiểm tra định dạng Email
            var isValidEmail = IsValidEmail(employee.Email);
            if (!isValidEmail)
            {
                ErrorValidateMsgs.Add(Resources.ResourceVN.VN_ValidateError_EmployeeEmailNotValid);
            }
            //3. Kiểm tra ngày sinh có lớn hơn hiện tại
            // TODO: đang làm ok
            var dateOfBirth = employee.DateOfBirth;
            var currentDate = DateTime.Now;
            if (dateOfBirth > currentDate)
            {
                isValid = false;
                ErrorValidateMsgs.Add(Resources.ResourceVN.VN_ValidateError_EmployeeDOBNotValid);
            }
            //4. Kiểm tra mã nv đã tồn tại hay chưa và có độ dài lớn hơn 20 kí tự hay không?
            if (employee.EmployeeCode.Length > 20)
            {
                isValid = false;
                ErrorValidateMsgs.Add(Resources.ResourceVN.VN_ValidateError_EmployeeCodeIsNoMoreThan20);
            }

            var isDuplicate = _repository.CheckEmployeeCodeDuplidate(employee.EmployeeCode);
            if (isDuplicate == true)
            {
                isValid = false;
                ErrorValidateMsgs.Add(Resources.ResourceVN.VN_ValidateError_EmployeeCodeDuplicate);

            }
            if (ErrorValidateMsgs.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected override bool ValidateUpdate(Employee employee)
        {
            var isValid = true;
            // Xử lí nghiệp vụ
            //validate dữ liệu
            if (string.IsNullOrEmpty(employee.EmployeeCode))
            {
                isValid = false;
                ErrorValidateMsgs.Add(Resources.ResourceVN.VN_ValidateError_EmployeeCodeNotEmpty);
            }
            if (string.IsNullOrEmpty(employee.FullName))
            {
                isValid = false;
                ErrorValidateMsgs.Add(Resources.ResourceVN.VN_ValidateError_EmployeeNameNotEmpty);
            }
            if (string.IsNullOrEmpty(employee.Email))
            {
                isValid = false;
                ErrorValidateMsgs.Add(Resources.ResourceVN.VN_ValidateError_EmployeeEmailNotEmpty);
            }
            if (string.IsNullOrEmpty(employee.Mobile))
            {
                isValid = false;
                ErrorValidateMsgs.Add(Resources.ResourceVN.VN_ValidateError_EmployeeMobileNotEmpty);
            }
            //2. Kiểm tra định dạng Email
            var isValidEmail = IsValidEmail(employee.Email);
            if (!isValidEmail)
            {
                ErrorValidateMsgs.Add(Resources.ResourceVN.VN_ValidateError_EmployeeEmailNotValid);
            }
            //3. Kiểm tra ngày sinh có lớn hơn hiện tại
            var dateOfBirth = employee.DateOfBirth;
            var currentDate = DateTime.Now;
            if (dateOfBirth > currentDate)
            {
                isValid = false;
                ErrorValidateMsgs.Add(Resources.ResourceVN.VN_ValidateError_EmployeeDOBNotValid);
            }
            //4. Kiểm tra mã nv đã tồn tại hay chưa và có độ dài lớn hơn 20 kí tự hay không?
            if (employee.EmployeeCode.Length > 20)
            {
                isValid = false;
                ErrorValidateMsgs.Add(Resources.ResourceVN.VN_ValidateError_EmployeeCodeIsNoMoreThan20);
            }

            var isDuplicate = _repository.CheckEmployeeCodeDuplidate(employee.EmployeeCode);
            if (isDuplicate == true)
            {
                isValid = true;
                ErrorValidateMsgs.Add(Resources.ResourceVN.VN_ValidateError_EmployeeCodeDuplicate);

            }
            if (ErrorValidateMsgs.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
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
        public MemoryStream ExportExcel()
        {
            var employees = _repository.GetAll();
            var props = typeof(Employee).GetProperties();
            MemoryStream stream;
            using (var package = new ExcelPackage())
            {
                // Tạo sheet
                var worksheet = package.Workbook.Worksheets.Add(ResourceVN.EmployeeExcelFileName);
                // Thêm các header column
                worksheet.Cells[3, 1].Value = "STT";
                int col = 2;
                // Tạo list chứa các propName của excel
                List<string> propExcels = new List<string>();
                foreach (var prop in props)
                {
                    // Kiểm tra xem prop có được hiển thị trên excel không
                    var isExcelColumn = Attribute.IsDefined(prop, typeof(MISAExcelColumn));
                    if (isExcelColumn)
                    {
                        // Lấy ra displayName của prop
                        var displayNameAttribute = Attribute.GetCustomAttribute(prop, typeof(PropertyNameDisplay));
                        var propDisplayName = "";
                        if (displayNameAttribute != null) propDisplayName = (displayNameAttribute as PropertyNameDisplay).propName;
                        // Thêm các column header
                        worksheet.Cells[3, col].Value = propDisplayName;
                        worksheet.Cells[3, col].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[3, col].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[3, col].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[3, col].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        // Add propName của excel vào list
                        propExcels.Add(prop.Name);
                        col++;
                    }
                }
                // Lấy địa chỉ hàng header
                var headerRow = $"A3:{(char)((int)'A' + propExcels.Count())}3";
                // style header
                worksheet.Cells[headerRow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[headerRow].Style.Font.Bold = true;
                worksheet.Cells[headerRow].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[headerRow].Style.Fill.BackgroundColor.SetColor(Color.DarkGray);
                // Lấy các địa chỉ cột đầu và cột cuối cần merge để hiển thị title
                var titleRow = $"A1:{(char)((int)'A' + propExcels.Count())}1";
                // Merge các column ở dòng đầu tiên
                worksheet.Cells[titleRow].Merge = true;
                // Gán Title của Excel và style
                worksheet.Cells[titleRow].Value = ResourceVN.EmployeeExcelFileName;
                worksheet.Cells[titleRow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[titleRow].Style.Font.Bold = true;
                // Thực hiện gán dữ liệu vào từng dòng
                int row = 4;
                foreach (var employee in employees)
                {
                    col = 1;
                    worksheet.Cells[row, col].Value = (row - 3);
                    worksheet.Cells[row, col].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[row, col].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[row, col].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[row, col].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    foreach (var colExcel in propExcels)
                    {
                        col++;
                        var prop = employee.GetType().GetProperty(colExcel);
                        var propValue = (prop.GetValue(employee));
                        if (prop.PropertyType == typeof(DateTime?) && propValue != null)
                        {
                            DateTime date = (DateTime)propValue;
                            worksheet.Cells[row, col].Value = date.ToString("dd/MM/yyyy");
                            worksheet.Cells[row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        }
                        else
                        {
                            worksheet.Cells[row, col].Value = propValue;
                        }
                        worksheet.Cells[row, col].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[row, col].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[row, col].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[row, col].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    }
                    row++;
                }
                // Set size chữ
                worksheet.Cells.Style.Font.Size = 13;
                // Set size title
                worksheet.Cells[titleRow].Style.Font.Size = 24;
                // Set độ rộng auto
                worksheet.Cells.AutoFitColumns();
                // Lưu dữ liệu
                package.Save();
                stream = new MemoryStream(package.GetAsByteArray());
            }
            return stream;
        }

       
    }
}
