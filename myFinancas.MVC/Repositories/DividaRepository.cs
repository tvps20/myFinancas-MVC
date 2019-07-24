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
    public class DividaRepository : IRepository<DividaModel>
    {
        // Instancia unica
        private static DividaRepository uniqueInstance;

        // Construtor privado. Usando o padrão Sngleton
        private DividaRepository() { }

        // retornando a instância unica.
        public static DividaRepository getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new DividaRepository();

            return uniqueInstance;
        }

        public List<DividaModel> ListAll()
        {
            using (var db = new ContextoDB())
            {
                return db.Dividas.OrderBy(c => c.Id).ToList();
            }
        }

        public List<DividaModel> ListAllIncludeComprador()
        {
            using (var db = new ContextoDB())
            {
                return db.Dividas.Include("Comprador").OrderBy(c => c.Id).ToList();
            }
        }

        public List<DividaModel> ListAllByCompradorIsPagoFalse(long idComprador)
        {
            using (var db = new ContextoDB())
            {
                List<DividaModel> dividas = db.Dividas.Where(d => (d.IdComprador == idComprador) && (d.isPaga == false)).ToList();
                return dividas;
            }
        }

        public List<DividaModel> ListAllIncludeCompradorAndFaturaIsPagoFalse()
        {
            using (var db = new ContextoDB())
            {
                List<DividaModel> dividas = db.Dividas.Include("Comprador").Include("Fatura").Where(x => x.isPaga == false).ToList();
                return dividas;
            }
        }

        public DividaModel GetById(long id)
        {
            using (var db = new ContextoDB())
            {
                return db.Dividas.Find(id);
            }
        }

        public DividaModel Save(DividaModel entity)
        {
            using (var db = new ContextoDB())
            {
                var cartaoBd = GetById(entity.Id);

                if (cartaoBd == null)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                    entity.UpdateAt = DateTime.UtcNow;
                    db.Dividas.Add(entity);
                }
                else
                {
                    entity.UpdateAt = DateTime.UtcNow;
                    db.Dividas.Attach(entity);
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
                var Divida = new DividaModel { Id = id };
                db.Dividas.Attach(Divida);
                db.Entry(Divida).State = EntityState.Deleted;
                db.SaveChanges();
                return true;
            }
        }
    }
}