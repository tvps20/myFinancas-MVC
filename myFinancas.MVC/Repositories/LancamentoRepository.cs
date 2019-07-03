using myFinancas.MVC.Models;
using myFinancas.MVC.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Repositories
{
    public class LancamentoRepository
    {
        public static List<LancamentoModel> ListAll()
        {
            using (var db = new ContextoDB())
            {
                return db.Lancamentos.OrderByDescending(f => f.DataCompra).ToList();
            }
        }

        public static List<LancamentoModel> ListAllByFatura(long id)
        {
            using (var db = new ContextoDB())
            {
                List<LancamentoModel> lancamentos = db.Lancamentos.Where(f => f.IdFatura == id).ToList();
                return lancamentos;
            }
        }

        public static LancamentoModel RecuperarPeloId(long id)
        {
            using (var db = new ContextoDB())
            {
                return db.Lancamentos.Find(id);
            }
        }

        public static int Salvar(LancamentoModel Lancamento)
        {
            using (var db = new ContextoDB())
            {
                var lancamentoBd = RecuperarPeloId(Lancamento.Id);

                if (lancamentoBd == null)
                {
                    Lancamento.CreatedAt = DateTime.UtcNow;
                    Lancamento.UpdateAt = DateTime.UtcNow;
                    db.Lancamentos.Add(Lancamento);
                }
                else
                {
                    Lancamento.UpdateAt = DateTime.UtcNow;
                    db.Lancamentos.Attach(Lancamento);
                    db.Entry(Lancamento).State = EntityState.Modified;
                }

                return db.SaveChanges();
            }
        }

        public static bool Remover(long id)
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