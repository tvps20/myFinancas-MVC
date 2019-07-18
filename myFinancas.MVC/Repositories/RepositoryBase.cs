using myFinancas.MVC.Interfaces.Repository;
using myFinancas.MVC.Models;
using myFinancas.MVC.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

//TODO: CLasse errada, corrigir e alterar o nome
namespace myFinancas.MVC.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : EntityModel
    {
        private DbSet<T> DbSet;
        // Instancia unica
        private static T uniqueInstance;

        // Construtor privado. Usando o padrão Sngleton
        private RepositoryBase(DbSet<T> DbSet)
        {
            this.DbSet = DbSet;
        }

        // retornando a instância unica.
        public static T GetInstance(DbSet<T> DbSet)
        {
            if (uniqueInstance == null)
            {
                uniqueInstance = (T)Activator.CreateInstance(typeof(T), DbSet);
            }

            return uniqueInstance;
        }

        public List<T> ListAll()
        {
            throw new NotImplementedException();
        }

        public T GetById(long id)
        {
            throw new NotImplementedException();
        }

        public T Save(T entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}