using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChat.Models.Entities;

namespace LiveChat.Data.Repository.SQLRepository
{
    public class CopropietarioRepository : ICopropietarioRepository
    {
        private readonly ApplicationDbContext _Context;

        public CopropietarioRepository(ApplicationDbContext context)
        {
            _Context = context;
        }

        public bool Add(Copropietario addElement)
        {
            _Context.Copropietarios.Add(addElement);
            return SaveChanges();
        }

        public bool Delete(int deleteElementId)
        {
            var elementToDelete = _Context.Copropietarios.FirstOrDefault(r => r.CopropietarioId == deleteElementId);
            _Context.Copropietarios.Remove(elementToDelete);
            return SaveChanges();
        }

        public Copropietario GetElement(int id)
        {
            var element = _Context.Copropietarios.FirstOrDefault(r => r.CopropietarioId == id);
            return element;
        }

        public IEnumerable<Copropietario> GetsElements()
        {
            var elements= _Context.Copropietarios.ToList();
            return elements;
        }

        public bool Modify(Copropietario modifyElement)
        {
            _Context.Copropietarios.Update(modifyElement);
            
            return SaveChanges();
        }

        private bool SaveChanges()
        {
            bool result=Convert.ToBoolean(_Context.SaveChanges());
            return result;
        }
    }
}
