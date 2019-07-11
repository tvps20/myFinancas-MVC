using myFinancas.MVC.Interfaces.Repository;
using myFinancas.MVC.Models.Domain;
using myFinancas.MVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Services
{
    public class CartaoService : BaseService<CartaoModel>
    {
        public CartaoService(IRepository<CartaoModel> repository) : base(repository) { }

        public CartaoRepository GetRepository()
        {
            return (CartaoRepository) this.repository;
        }

        public CartaoModel BuscarPeloNome(string Nome)
        {
            return this.GetRepository().GetByName(Nome);
        }
    }
}