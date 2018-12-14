using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Data.Repository
{
    public interface IDataRepository<T>
    {
        T GetElement(int id);
        IEnumerable<T> GetsElements();
        bool Add(T addElement);
        bool Delete(int deleteElementId);
        bool Modify(T modifyElement);
    }
}
