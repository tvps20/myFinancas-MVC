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
    public class CartaoRepository : IRepository<CartaoModel>
    {
        // Instancia unica
        private static CartaoRepository uniqueInstance;

        // Construtor privado. Usando o padrão Sngleton
        private CartaoRepository() {}

        // retornando a instância unica.
        public static CartaoRepository getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new CartaoRepository();

            return uniqueInstance;
        }

        public List<CartaoModel> ListAll()
        {
            using (var db = new ContextoDB())
            {
                return db.Cartoes.OrderBy(c => c.Id).ToList();
            }
        }

        public CartaoModel GetById(long id)
        {
            using (var db = new ContextoDB())
            {
                return db.Cartoes.Find(id);
            }
        }

        public CartaoModel GetByName(string name)
        {
            using (var db = new ContextoDB())
            {
                return db.Cartoes.FirstOrDefault(x => x.Nome == name);
            }
        }

        public CartaoModel Save(CartaoModel entity)
        {
            using (var db = new ContextoDB())
            {
                var cartaoBd = GetById(entity.Id);

                if (cartaoBd == null)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                    entity.UpdateAt = DateTime.UtcNow;
                    db.Cartoes.Add(entity);
                }
                else
                {
                    entity.UpdateAt = DateTime.UtcNow;
                    db.Cartoes.Attach(entity);
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
                var Cartao = new CartaoModel { Id = id };
                db.Cartoes.Attach(Cartao);
                db.Entry(Cartao).State = EntityState.Deleted;
                db.SaveChanges();
                return true;
            }
        }
    }
}