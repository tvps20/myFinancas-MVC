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
                return db.Faturas.OrderByDescending(c => c.DataVencimento).ToList();
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
                var cartaoBd = RecuperarPeloId(Fatura.Id);

                if (cartaoBd == null)
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
    }
}