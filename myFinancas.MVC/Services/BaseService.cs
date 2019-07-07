using myFinancas.MVC.Interfaces.Repository;
using myFinancas.MVC.Interfaces.Service;
using myFinancas.MVC.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Services
{
    public class BaseService<T> : IService<T> where T : EntityModel
    {
        protected readonly IRepository<T> repository;

        public BaseService(IRepository<T> repository)
        {
            this.repository = repository;
        }

        public List<T> ListarTodos()
        {
            try
            {
                List<T> listEntity = this.repository.ListAll();
                return listEntity != null ? listEntity : new List<T>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public T RecuperarPeloId(long id)
        {
            try
            {
                T entity = this.repository.GetById(id);
                return entity;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool Remover(long id)
        {
            try
            {
                return this.repository.Delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Salvar(T entity)
        {
            try
            {
                return this.repository.Save(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}