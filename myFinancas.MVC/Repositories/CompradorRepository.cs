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
    public class CompradorRepository : IRepository<CompradorModel>
    {
        // Instancia unica
        private static CompradorRepository uniqueInstance;

        // Construtor privado. Usando o padrão Sngleton
        private CompradorRepository() { }

        // retornando a instância unica.
        public static CompradorRepository getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new CompradorRepository();

            return uniqueInstance;
        }

        public List<CompradorModel> ListAll()
        {
            using (var db = new ContextoDB())
            {
                return db.Compradores.OrderBy(c => c.Id).ToList();
            }
        }

        public List<CompradorModel> ListAllPagantes()
        {
            using (var db = new ContextoDB())
            {
                return db.Compradores.OrderBy(c => c.Id).Where(x => x.DividaTotalRestante > 0).ToList();
            }
        }

        public List<CompradorModel> ListAllDevedores()
        {
            using (var db = new ContextoDB())
            {
                return db.Compradores.OrderBy(c => c.Id).Where(x => x.DividaTotalRestante < 0).ToList();
            }
        }

        public CompradorModel GetById(long id)
        {
            using (var db = new ContextoDB())
            {
                return db.Compradores.Find(id);
            }
        }

        public CompradorModel GetByName(string name)
        {
            using (var db = new ContextoDB())
            {
                return db.Compradores.FirstOrDefault(x => x.Nome == name);
            }
        }

        public CompradorModel Save(CompradorModel entity)
        {
            using (var db = new ContextoDB())
            {
                var cartaoBd = GetById(entity.Id);

                if (cartaoBd == null)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                    entity.UpdateAt = DateTime.UtcNow;
                    db.Compradores.Add(entity);
                }
                else
                {
                    entity.UpdateAt = DateTime.UtcNow;
                    db.Compradores.Attach(entity);
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
                var Comprador = new CompradorModel { Id = id };
                db.Compradores.Attach(Comprador);
                db.Entry(Comprador).State = EntityState.Deleted;
                db.SaveChanges();
                return true;
            }
        }
    }
}