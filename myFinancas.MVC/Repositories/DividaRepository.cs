using myFinancas.MVC.Models;
using myFinancas.MVC.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Repositories
{
    public class DividaRepository
    {
        public static List<DividaModel> ListAll()
        {
            using (var db = new ContextoDB())
            {
                return db.Dividas.OrderBy(c => c.Id).ToList();
            }
        }

        public static DividaModel RecuperarPeloId(long id)
        {
            using (var db = new ContextoDB())
            {
                return db.Dividas.Find(id);
            }
        }

        public static int Salvar(DividaModel Divida)
        {
            using (var db = new ContextoDB())
            {
                var cartaoBd = RecuperarPeloId(Divida.Id);

                if (cartaoBd == null)
                {
                    Divida.CreatedAt = DateTime.UtcNow;
                    Divida.UpdateAt = DateTime.UtcNow;
                    db.Dividas.Add(Divida);
                }
                else
                {
                    Divida.UpdateAt = DateTime.UtcNow;
                    db.Dividas.Attach(Divida);
                    db.Entry(Divida).State = EntityState.Modified;
                }

                return db.SaveChanges();
            }
        }

        public static bool Remover(long id)
        {
            using (var db = new ContextoDB())
            {
                var Divida = new DividaModel { Id = id };
                db.Dividas.Attach(Divida);
                db.Entry(Divida).State = EntityState.Deleted;
                db.SaveChanges();
                return true;
            }
        }
    }
}