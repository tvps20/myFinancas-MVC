using myFinancas.MVC.Models;
using myFinancas.MVC.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Repositories
{
    public class CartaoRepository
    {
        public static List<CartaoModel> ListAll()
        {
            using(var db = new ContextoDB())
            {
                return db.Cartoes.OrderBy(c => c.Id).ToList();
            }
        }

        public static CartaoModel RecuperarPeloId(long id)
        {
            using (var db = new ContextoDB())
            {
                return db.Cartoes.Find(id);
            }
        }

        public static int Salvar(CartaoModel Cartao)
        {
            using(var db = new ContextoDB())
            {
                var cartaoBd = RecuperarPeloId(Cartao.Id);

                if(cartaoBd == null)
                {
                    Cartao.CreatedAt = DateTime.UtcNow;
                    Cartao.UpdateAt = DateTime.UtcNow;
                    db.Cartoes.Add(Cartao);
                }
                else
                {
                    Cartao.UpdateAt = DateTime.UtcNow;
                    db.Cartoes.Attach(Cartao);
                    db.Entry(Cartao).State = EntityState.Modified;
                }

                return db.SaveChanges();
            }
        }
    }
}