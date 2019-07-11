using myFinancas.MVC.Interfaces.Repository;
using myFinancas.MVC.Models.Domain;
using myFinancas.MVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Services
{
    public class FaturaService : BaseService<FaturaModel>
    {
        public FaturaService(IRepository<FaturaModel> repository) : base(repository) { }

        public List<FaturaModel> ListarTodosPeloCartao(long id)
        {
            try
            {
                return this.GetRepository().ListAllByCartao(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FaturaRepository GetRepository()
        {
            return (FaturaRepository) this.repository;
        }        
    }
}