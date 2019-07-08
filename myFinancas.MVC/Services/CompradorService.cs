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

        public Dictionary<string, List<LancamentoModel>> ListarTodosLancamentosData(DateTime data, long id)
        {
            Dictionary<string, List<LancamentoModel>> LancamentosByFatura = new Dictionary<string, List<LancamentoModel>>();
            List<LancamentoModel> Lancamentos = this.lancamentoService.ListarTodosPeloCompradorComFatura(id);

            foreach(LancamentoModel lancamento in Lancamentos)
            {
                if (lancamento.DataCompra.ToString("MMMM").Equals(data.ToString("MMMM")))
                {
                    if (!LancamentosByFatura.Keys.Contains(lancamento.Fatura.Cartao.Nome))
                    {
                        LancamentosByFatura.Add(lancamento.Fatura.Cartao.Nome, new List<LancamentoModel>());
                    }

                    LancamentosByFatura[lancamento.Fatura.Cartao.Nome].Add(lancamento);
                }
            }

            return LancamentosByFatura;
        }

        public int ContaLancamentos(Dictionary<string, List<LancamentoModel>> Lancamentos)
        {
            int count = 0;
            foreach(List<LancamentoModel> LancamentosList in Lancamentos.Values)
            {
                count += LancamentosList.Count;
            }

            return count;
        }
    }
}