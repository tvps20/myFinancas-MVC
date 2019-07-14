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
        private LancamentoService lancamentoService = new LancamentoService(LancamentoRepository.getInstance());
        private DividaService dividaService = new DividaService(DividaRepository.getInstance());

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

        public void PagarFatura(long Id)
        {
            try
            {
                FaturaModel fatura = this.RecuperarPeloId(Id);
                fatura.IsPaga = true;
                this.Salvar(fatura);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FecharFatura(long Id)
        {
            try
            {
                FaturaModel fatura = this.GetRepository().GetByIdIncludeCartao(Id);
                List<LancamentoModel> lancamentos = this.lancamentoService.ListarTodosPelaFaturaIncludeComprador(Id);
                Dictionary<long, decimal> dividas = new Dictionary<long, decimal>();
                foreach(LancamentoModel lancamento in lancamentos)
                {
                    if (!dividas.Keys.Contains(lancamento.Comprador.Id))
                    {
                        dividas.Add(lancamento.Comprador.Id, 0);
                    }

                    decimal divida = dividas[lancamento.Comprador.Id];
                    divida += lancamento.Valor;
                    dividas[lancamento.Comprador.Id] = divida;
                }

                foreach(long id in dividas.Keys)
                {
                    DividaModel novaDivida = new DividaModel();
                    novaDivida.Data = DateTime.Now;
                    novaDivida.IdComprador = id;
                    novaDivida.Descricao = "Divida da fatura " + fatura.DataVencimento.ToString("dd/MM/yyyy") + " do " + fatura.Cartao.Nome;
                    novaDivida.ValorDivida = dividas[id];
                    novaDivida.CalcularValorRestante();

                    this.dividaService.Salvar(novaDivida);
                }

                fatura.IsFechada = true;
                this.Salvar(fatura);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}