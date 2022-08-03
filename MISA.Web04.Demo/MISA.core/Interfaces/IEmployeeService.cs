using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.core.Entities;

namespace MISA.core.Interfaces
{
    public interface IEmployeeService:IBaseService<Employee>
    {
        // luồng dữ liệu được lưu trữ
        public MemoryStream ExportExcel();
    }
}
