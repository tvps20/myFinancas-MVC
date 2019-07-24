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

        public List<DividaModel> ListarTodasDividasCompradorNPagas(long idComprador)
        {
            List<DividaModel> dividas = this.GetRepository().ListAllByCompradorIsPagoFalse(idComprador);
            return dividas;
        }

        public Dictionary<string, List<DividaModel>> ListarDividasNaoPagas()
        {
            List<DividaModel> dividas = this.GetRepository().ListAllIncludeCompradorAndFaturaIsPagoFalse();
            return this.OrganizarDividas(dividas);
        }

        public DividaRepository GetRepository()
        {
            return (DividaRepository) this.repository;
        }

        private Dictionary<string, List<DividaModel>> OrganizarDividas(List<DividaModel> dividas)
        {
            Dictionary<string, List<DividaModel>> dividasDicionario = new Dictionary<string, List<DividaModel>>();

            foreach(DividaModel divida in dividas)
            {
                string chave = divida.Comprador.Nome;

                if (!dividasDicionario.Keys.Contains(chave))
                {
                    dividasDicionario.Add(chave, new List<DividaModel>());
                }

                dividasDicionario[chave].Add(divida);
            }

            return dividasDicionario;
        }
    }
}