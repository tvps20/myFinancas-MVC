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

        public List<LancamentoModel> ListarTodosPeloCompradorComFatura(long id)
        {
            try
            {
                return this.GetRepository().ListAllByCompradorIncludeFatura(id);
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

        public List<LancamentoModel> ListarTodosPelaFaturaIncludeComprador(long id)
        {
            try
            {
                return this.GetRepository().ListAllByFaturaIncludeComprador(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Dictionary<string, List<LancamentoModel>> ListarTodosLancamentosCompradorNPagos(long idComprador)
        {
            try
            {
                Dictionary<string, List<LancamentoModel>> LancamentosByFatura = new Dictionary<string, List<LancamentoModel>>();
                List<LancamentoModel> Lancamentos = this.GetRepository().ListarTodosLancamentosCompradorNPagos(idComprador);

                foreach (LancamentoModel lancamento in Lancamentos)
                {
                    if (!lancamento.Fatura.IsPaga)
                    {
                        string chave = lancamento.Fatura.MesReferente + " " + lancamento.Fatura.Cartao.Nome;

                        if (!LancamentosByFatura.Keys.Contains(chave))
                        {
                            LancamentosByFatura.Add(chave, new List<LancamentoModel>());
                        }

                        LancamentosByFatura[chave].Add(lancamento);
                    }
                }

                return LancamentosByFatura;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal CalcularValorLancamentos(List<LancamentoModel> lancamentos)
        {
            decimal valorTotal = lancamentos.Sum(x => x.Valor);
            return valorTotal;
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