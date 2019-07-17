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
    public class FaturaRepository : IRepository<FaturaModel>
    {
        // Instancia unica
        private static FaturaRepository uniqueInstance;

        // Construtor privado. Usando o padrão Sngleton
        private FaturaRepository() { }

        // retornando a instância unica.
        public static FaturaRepository getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new FaturaRepository();

            return uniqueInstance;
        }

        public List<FaturaModel> ListAll()
        {
            using (var db = new ContextoDB())
            {
                return db.Faturas.Include("Cartao").OrderByDescending(f => f.DataVencimento).ToList();
            }
        }

        public List<FaturaModel> ListAllByCartao(long id)
        {
            using (var db = new ContextoDB())
            {
                List<FaturaModel> faturas = db.Faturas.Where(f => f.IdCartao == id).ToList();
                return faturas;
            }
        }

        internal FaturaModel getByMes(string mesRefente, long idCartao)
        {
            using (var db = new ContextoDB())
            {
                return db.Faturas.FirstOrDefault(x => x.MesReferente == mesRefente && x.IdCartao == idCartao);
            }
        }

        public FaturaModel GetById(long id)
        {
            using (var db = new ContextoDB())
            {
                return db.Faturas.Find(id);
            }
        }

        public FaturaModel GetByIdIncludeCartao(long id)
        {
            using (var db = new ContextoDB())
            {
                return db.Faturas.Include("Cartao").Single(x=> x.Id == id);
            }
        }

        public long Save(FaturaModel entity)
        {
            using (var db = new ContextoDB())
            {
                var faturaBd = GetById(entity.Id);

                if (faturaBd == null)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                    entity.UpdateAt = DateTime.UtcNow;
                    db.Faturas.Add(entity);
                }
                else
                {
                    entity.UpdateAt = DateTime.UtcNow;
                    db.Faturas.Attach(entity);
                    db.Entry(entity).State = EntityState.Modified;
                }

                db.SaveChanges();
                return entity.Id;
            }
        }

        public bool Delete(long id)
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