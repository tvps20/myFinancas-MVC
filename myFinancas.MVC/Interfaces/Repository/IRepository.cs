using myFinancas.MVC.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myFinancas.MVC.Interfaces.Repository
{
    public interface IRepository<T> where T : EntityModel
    {
        List<T> ListAll();
        T GetById(long id);
        long Save(T entity);
        bool Delete(long id);
    }
}
