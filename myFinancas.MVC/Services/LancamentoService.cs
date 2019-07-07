using myFinancas.MVC.Interfaces.Repository;
using myFinancas.MVC.Models.Domain;
using myFinancas.MVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Services
{
    public class LancamentoService : BaseService<LancamentoModel>
    {
        public LancamentoService(IRepository<LancamentoModel> repository) : base(repository) { }

        public LancamentoRepository GetRepository()
        {
            return (LancamentoRepository) this.repository;
        }

        public List<LancamentoModel> ListarTodosPeloComprador(long id)
        {
            try
            {
                return this.GetRepository().ListAllByComprador(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LancamentoModel> ListarTodosPelaFatura(long id)
        {
            try
            {
                return this.GetRepository().ListAllByFatura(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}