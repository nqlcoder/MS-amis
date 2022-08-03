using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.core.Entities
{
    /// <summary>
    /// Tạo attribute cho những cột mong muốn xuất ra excel
    /// </summary>
    /// CreatedBy: NQLINH (18/5/2022)
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyNameDisplay : Attribute
    {
        // Khởi tạo tên cột khi xuất ra excel
        public string propName = String.Empty;
        /// <summary>
        /// Tên cột khi xuất ra excel
        /// </summary>
        /// <param name="name"></param>
        /// CreatedBy: NQLINH (18/5/2022)
        public PropertyNameDisplay(string name)
        {
            propName = name;
        }
    }
    // Đặt attribute cho mỗi cột khi muốn xuất ra excel
    [AttributeUsage(AttributeTargets.Property)]
    public class MISAExcelColumn : Attribute
    {

    }
}
