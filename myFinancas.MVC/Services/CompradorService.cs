using myFinancas.MVC.Interfaces.Repository;
using myFinancas.MVC.Models.Domain;
using myFinancas.MVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Services
{
    public class CompradorService : BaseService<CompradorModel>
    {
        private LancamentoService lancamentoService = new LancamentoService(LancamentoRepository.getInstance());

        public CompradorService(IRepository<CompradorModel> repository) : base(repository) { }

        public CompradorRepository GetRepository()
        {
            return (CompradorRepository) this.repository;
        }    

        public CompradorModel BuscarPeloNome(string Nome)
        {
            return this.GetRepository().GetByName(Nome);
        }
    }
}