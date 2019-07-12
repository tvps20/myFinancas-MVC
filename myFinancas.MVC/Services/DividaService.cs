using myFinancas.MVC.Interfaces.Repository;
using myFinancas.MVC.Models.Domain;
using myFinancas.MVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Services
{
    public class DividaService : BaseService<DividaModel>
    {
        public DividaService(IRepository<DividaModel> repository) : base(repository) { }

        public List<DividaModel> ListarTodosPeloComprador(long id)
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

        public List<DividaModel> ListarTodosIncludeComprador()
        {
            try
            {
                return this.GetRepository().ListAllIncludeComprador();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DividaModel> ListarTodasDividasCompradorNPagos(long idComprador)
        {
            List<DividaModel> dividas = this.ListarTodosPeloComprador(idComprador).Where(x => x.IdComprador == idComprador && !x.isPaga).ToList();

            return dividas;
        }

        public DividaRepository GetRepository()
        {
            return (DividaRepository) this.repository;
        }
    }
}