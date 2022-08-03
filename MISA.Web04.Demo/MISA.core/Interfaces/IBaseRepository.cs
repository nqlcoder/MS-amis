using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.core.Interfaces
{
    public interface IBaseRepository<MISAEntity>
    {
        /// <summary>
        /// Lấy ra toàn bộ thông tin nhân viên
        /// </summary>
        /// <returns>Danh sách nhân viên</returns>
        /// CreatedBy: NQLINH (18/5/2022)
        IEnumerable<MISAEntity> GetAll();

        /// <summary>
        /// lấy ra thông tin nhân viên theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Nhân viên ứng với id đó</returns>
        /// CreatedBy: NQLINH (18/5/2022)
        MISAEntity GetByID(Guid id);

        /// <summary>
        /// Thêm mới nhân viên
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>200: Thêm mới nhân viên thành công</returns>
        /// CreatedBy: NQLINH (18/5/2022)
        int Insert(MISAEntity employee);

        /// <summary>
        /// Sửa thông tin nhân viên
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employee"></param>
        /// <returns>200: sửa nhân viên thành công</returns>
        /// CreatedBy: NQLINH (18/5/2022)
        int Update(MISAEntity employee);

        /// <summary>
        /// Xóa nhân viên
        /// </summary>
        /// <param name="id"></param>
        /// <returns>200: Xóa nhân viên thành công</returns>
        int Delete(Guid id);
       
    }
}
