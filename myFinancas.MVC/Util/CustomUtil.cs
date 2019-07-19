using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Util
{
    public class CustomUtil<T>
    {
        public static int caculaNumeroPagina(int pagina, IPagedList<T> pagedList)
        {
            if (pagedList.PageCount >= 3)
            {
                if (pagina == 1)
                {
                    return 2;
                }
                else if (pagina >= pagedList.PageCount)
                {
                    return pagedList.PageCount - 1;
                }

                return pagina;
            } else
            {
                if(pagedList.PageCount == 2)
                {
                    return 2;
                } else
                {
                    return 1;
                }
            }

        }
    }
}