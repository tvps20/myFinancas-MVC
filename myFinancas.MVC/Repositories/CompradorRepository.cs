using myFinancas.MVC.Models;
using myFinancas.MVC.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Repositories
{
    public class CompradorRepository
    {
        public static List<CompradorModel> ListAll()
        {
            using (var db = new ContextoDB())
            {
                return db.Compradores.OrderBy(c => c.Id).ToList();
            }
        }

        public static CompradorModel RecuperarPeloId(long id)
        {
            using (var db = new ContextoDB())
            {
                return db.Compradores.Find(id);
            }
        }

        public static int Salvar(CompradorModel Comprador)
        {
            using (var db = new ContextoDB())
            {
                var cartaoBd = RecuperarPeloId(Comprador.Id);

                if (cartaoBd == null)
                {
                    Comprador.CreatedAt = DateTime.UtcNow;
                    Comprador.UpdateAt = DateTime.UtcNow;
                    db.Compradores.Add(Comprador);
                }
                else
                {
                    Comprador.UpdateAt = DateTime.UtcNow;
                    db.Compradores.Attach(Comprador);
                    db.Entry(Comprador).State = EntityState.Modified;
                }

                return db.SaveChanges();
            }
        }

        public static bool Remover(long id)
        {
            using (var db = new ContextoDB())
            {
                var Comprador = new CompradorModel { Id = id };
                db.Compradores.Attach(Comprador);
                db.Entry(Comprador).State = EntityState.Deleted;
                db.SaveChanges();
                return true;
            }
        }
    }
}