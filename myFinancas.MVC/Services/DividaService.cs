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

        public DividaRepository GetRepository()
        {
            return (DividaRepository)this.repository;
        }
    }
}