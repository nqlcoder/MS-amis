using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.core.Entities;

namespace MISA.core.Interfaces
{
    public interface IPositionRepository
    {
        IEnumerable<Positions> GetAllPosition();
        IEnumerable<Positions> GetByID(Guid id);
        int Insert(Positions positions);
        int Update(string id, Positions positions);
        IEnumerable<Positions> Delete(Guid id);
    }
}
