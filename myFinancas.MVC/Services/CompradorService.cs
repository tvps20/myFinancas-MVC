using myFinancas.MVC.Interfaces.Repository;
using myFinancas.MVC.Models.Domain;
using myFinancas.MVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Services
{
    public class CompradorService : BaseService<CompradorModel>
    {
        public CompradorService(IRepository<CompradorModel> repository) : base(repository) { }
    }
}