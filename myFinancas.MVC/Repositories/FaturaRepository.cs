using myFinancas.MVC.Models;
using myFinancas.MVC.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Repositories
{
    public class FaturaRepository
    {
        public static List<FaturaModel> ListAll()
        {
            using (var db = new ContextoDB())
            {
                return db.Faturas.OrderByDescending(f => f.DataVencimento).ToList();
            }
        }

        public static List<FaturaModel> ListAllByCartao(long id)
        {
            using (var db = new ContextoDB())
            {
                List<FaturaModel> faturas = db.Faturas.Where(f => f.IdCartao == id).ToList();
                return faturas;
            }
        }

        public static FaturaModel RecuperarPeloId(long id)
        {
            using (var db = new ContextoDB())
            {
                return db.Faturas.Find(id);
            }
        }

        public static int Salvar(FaturaModel Fatura)
        {
            using (var db = new ContextoDB())
            {
                var faturaBd = RecuperarPeloId(Fatura.Id);

                if (faturaBd == null)
                {
                    Fatura.CreatedAt = DateTime.UtcNow;
                    Fatura.UpdateAt = DateTime.UtcNow;
                    db.Faturas.Add(Fatura);
                }
                else
                {
                    Fatura.UpdateAt = DateTime.UtcNow;
                    db.Faturas.Attach(Fatura);
                    db.Entry(Fatura).State = EntityState.Modified;
                }

                return db.SaveChanges();
            }
        }

        public static bool Remover(long id)
        {
            using (var db = new ContextoDB())
            {
                var Fatura = new FaturaModel { Id = id };
                db.Faturas.Attach(Fatura);
                db.Entry(Fatura).State = EntityState.Deleted;
                db.SaveChanges();
                return true;
            }
        }
    }
}