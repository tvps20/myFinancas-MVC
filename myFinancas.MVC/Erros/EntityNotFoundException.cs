using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Erros
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string mensagem) : base(mensagem) { }
    }
}