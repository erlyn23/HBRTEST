using System;
using System.Collections.Generic;
using System.Text;

namespace HBRTEST.Core.Interfaces
{
    public interface IRepository<T> where T: class
    {
        List<T> GetAll();

        T GetEntityById(int ID);

        void Add(T Entity);

        void Update(T Entity);

        void Delete(int ID);
    }
}
