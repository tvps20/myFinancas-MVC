using myFinancas.MVC.Interfaces.Repository;
using myFinancas.MVC.Models;
using myFinancas.MVC.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Repositories
{
    public class LancamentoRepository : IRepository<LancamentoModel>
    {
        // Instancia unica
        private static LancamentoRepository uniqueInstance;

        // Construtor privado. Usando o padrão Sngleton
        private LancamentoRepository() { }

        // retornando a instância unica.
        public static LancamentoRepository getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new LancamentoRepository();

            return uniqueInstance;
        }

        public List<LancamentoModel> ListAll()
        {
            using (var db = new ContextoDB())
            {
                return db.Lancamentos.OrderByDescending(f => f.DataCompra).ToList();
            }
        }

        public List<LancamentoModel> ListAllByFaturaIncludeComprador(long idFatura)
        {
            using (var db = new ContextoDB())
            {
                List<LancamentoModel> lancamentos = db.Lancamentos.Include("Comprador").Where(l => l.IdFatura == idFatura).ToList();
                return lancamentos;
            }
        }

        public List<LancamentoModel> ListAllByCompradorAndFatura(long idComprador, long idFatura)
        {
            using (var db = new ContextoDB())
            {
                List<LancamentoModel> lancamentos = db.Lancamentos.Include("Fatura").Where(x => (x.IdComprador == idComprador) && (x.IdFatura == idFatura)).ToList();
                return lancamentos;
            }
        }

        public List<LancamentoModel> ListAllByCompradorWithFaturaIsFechadaAndIsPagaFalse(long idComprador)
        {
            using (var db = new ContextoDB())
            {
                List<LancamentoModel> lancamentos = db.Lancamentos.Include("Fatura").Where(l => (l.IdComprador == idComprador) && (l.Fatura.IsFechada == false) && (l.Fatura.IsPaga == false)).ToList();
                return lancamentos;
            }
        }

        public LancamentoModel GetById(long id)
        {
            using (var db = new ContextoDB())
            {
                return db.Lancamentos.Find(id);
            }
        }

        public LancamentoModel Save(LancamentoModel entity)
        {
            using (var db = new ContextoDB())
            {
                var lancamentoBd = GetById(entity.Id);

                if (lancamentoBd == null)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                    entity.UpdateAt = DateTime.UtcNow;
                    db.Lancamentos.Add(entity);
                }
                else
                {
                    entity.UpdateAt = DateTime.UtcNow;
                    db.Lancamentos.Attach(entity);
                    db.Entry(entity).State = EntityState.Modified;
                }

                db.SaveChanges();
                return entity;
            }
        }

        public bool Delete(long id)
        {
            using (var db = new ContextoDB())
            {
                var Lancamento = new LancamentoModel { Id = id };
                db.Lancamentos.Attach(Lancamento);
                db.Entry(Lancamento).State = EntityState.Deleted;
                db.SaveChanges();
                return true;
            }
        }
    }
}