using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Util
{
    public class CustomUtil
    {
        public static int caculaNumeroPagina(int pagina, int lastPage)
        {
            if (pagina == 1)
            {
                return 2;
            }
            else if (pagina >= lastPage)
            {
                return lastPage - 1;
            }

            return pagina;
        }
    }
}