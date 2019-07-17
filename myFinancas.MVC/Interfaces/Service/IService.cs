using myFinancas.MVC.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myFinancas.MVC.Interfaces.Service
{
    public interface IService<T> where T : EntityModel
    {
        List<T> ListarTodos();
        T RecuperarPeloId(long id);
        long Salvar(T entity);
        bool Remover(long id);
    }
}
