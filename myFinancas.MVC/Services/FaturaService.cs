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

        public FaturaModel BuscarPeloMes(string MesRefente, long IdCartao)
        {
            return this.GetRepository().getByMes(MesRefente, IdCartao);
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

                this.GerarDividas(dividas, lancamentos);
                this.SalvarDividas(dividas, fatura);
                this.GerarProximaFatura(fatura, lancamentos);

                fatura.IsFechada = true;
                this.Salvar(fatura);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GerarDividas(Dictionary<long, decimal> dividas, List<LancamentoModel> lancamentos)
        {
            foreach (LancamentoModel lancamento in lancamentos)
            {
                if (!dividas.Keys.Contains(lancamento.Comprador.Id))
                {
                    dividas.Add(lancamento.Comprador.Id, 0);
                }

                decimal divida = dividas[lancamento.Comprador.Id];
                divida += lancamento.Valor;
                dividas[lancamento.Comprador.Id] = divida;
            }
        }

        private void SalvarDividas(Dictionary<long, decimal> dividas, FaturaModel fatura)
        {
            foreach (long id in dividas.Keys)
            {
                DividaModel novaDivida = new DividaModel();
                novaDivida.Data = DateTime.Now;
                novaDivida.IdComprador = id;
                novaDivida.Descricao = "Divida da fatura " + fatura.DataVencimento.ToString("dd/MM/yyyy") + " do " + fatura.Cartao.Nome;
                novaDivida.ValorDivida = dividas[id];
                novaDivida.CalcularValorRestante();

                this.dividaService.Salvar(novaDivida);
            }
        }

        private void GerarProximaFatura(FaturaModel faturaAntiga, List<LancamentoModel> lancamentos)
        {
            FaturaModel novaFatura = new FaturaModel();
            novaFatura.IdCartao = faturaAntiga.IdCartao;
            novaFatura.DataVencimento = faturaAntiga.DataVencimento.AddMonths(1);
            novaFatura.MesReferente = novaFatura.DataVencimento.ToString("MMMM")[0].ToString().ToUpper() + novaFatura.DataVencimento.ToString("MMMM").Substring(1) + novaFatura.DataVencimento.ToString("'/'yyyy");

            FaturaModel busca = this.BuscarPeloMes(novaFatura.MesReferente, novaFatura.IdCartao);

            if(busca == null)
            {
                long idFaturaSalva = this.Salvar(novaFatura);

                foreach(LancamentoModel lancamento in lancamentos)
                {
                    if (lancamento.IsParcelado && (lancamento.ParcelaAtual < lancamento.QtdParcelas))
                    {
                        LancamentoModel novoLancamento = new LancamentoModel();
                        novoLancamento.IdComprador = lancamento.IdComprador;
                        novoLancamento.IdFatura = idFaturaSalva;
                        novoLancamento.IsParcelado = true;
                        novoLancamento.Observacao = lancamento.Observacao;
                        novoLancamento.ParcelaAtual = lancamento.ParcelaAtual + 1;
                        novoLancamento.QtdParcelas = lancamento.QtdParcelas;
                        novoLancamento.Valor = lancamento.Valor;
                        novoLancamento.Descricao = lancamento.Descricao;
                        novoLancamento.DataCompra = lancamento.DataCompra;

                        this.lancamentoService.Salvar(novoLancamento);
                    }
                }
            }
        }
    }
}