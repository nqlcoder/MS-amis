using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.core.Entities
{
    /// <summary>
    /// Vị trí
    /// </summary>
    public class Positions
    {
        /// <summary>
        /// khóa chính
        /// </summary>
        public Guid? PositionId { get; set; }
        /// <summary>
        /// tên vị trí
        /// </summary>
        public string? PositionName { get; set; }
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
