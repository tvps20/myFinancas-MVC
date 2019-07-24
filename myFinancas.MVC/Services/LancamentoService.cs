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

        public List<LancamentoModel> ListarTodosPelaFaturaIncludeComprador(long idFatura)
        {
            try
            {
                return this.GetRepository().ListAllByFaturaIncludeComprador(idFatura);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LancamentoModel> ListarTodosComFaturaECartao()
        {
            try
            {
                return this.GetRepository().ListAllIncludeFaturaIncludeCartao();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Dictionary<string, List<LancamentoModel>> ListarLancamentosPorDividas(List<DividaModel> dividas)
        {
            Dictionary<string, List<LancamentoModel>> lancamentosByDivida = new Dictionary<string, List<LancamentoModel>>();

            foreach(DividaModel divida in dividas)
            {
                if(divida.IdFatura != null)
                {
                    List<LancamentoModel> lancamentos = this.GetRepository().ListAllByCompradorAndFatura(divida.IdComprador, (long) divida.IdFatura);
                    lancamentosByDivida.Add(divida.Descricao, lancamentos);
                }
            }

            return lancamentosByDivida;
        }

        public Dictionary<string, List<LancamentoModel>> ListarLancamentosDoCompradorAtuais(long idComprador)
        {
            Dictionary<string, List<LancamentoModel>> lancamentosByComprador = new Dictionary<string, List<LancamentoModel>>();
            List<LancamentoModel> lancamentos = this.GetRepository().ListAllByCompradorWithFaturaIsFechadaAndIsPagaFalse(idComprador);

            foreach(LancamentoModel lancamento in lancamentos)
            {
                string chave = lancamento.Fatura.MesReferente;

                if (!lancamentosByComprador.Keys.Contains(chave))
                {
                    lancamentosByComprador.Add(chave, new List<LancamentoModel>());
                }

                lancamentosByComprador[chave].Add(lancamento);
            }

            return lancamentosByComprador;
        }

        public Dictionary<string, List<LancamentoModel>> OrganizarLancamentosPorComprador(List<LancamentoModel> lancamentos)
        {
            Dictionary<string, List<LancamentoModel>> LancamentosByComprador = new Dictionary<string, List<LancamentoModel>>();

            foreach (LancamentoModel lancamento in lancamentos)
            {
                string chave = lancamento.Comprador.Nome;

                if (!LancamentosByComprador.Keys.Contains(chave))
                {
                    LancamentosByComprador.Add(chave, new List<LancamentoModel>());
                }

                LancamentosByComprador[chave].Add(lancamento);
            }

            return LancamentosByComprador;
        }       
    }
}