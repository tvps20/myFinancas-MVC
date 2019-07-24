using myFinancas.MVC.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace myFinancas.MVC.Services
{
    public class RelatorioService
    {
        public void ExportToCsv(List<DividaModel> dividas, 
            Dictionary<string, List<LancamentoModel>> lancamenos, 
            Dictionary<string, List<LancamentoModel>> lancamentosAtuais, 
            CompradorModel comprador,
            HttpResponseBase Response)
        {
            StringWriter sw = new StringWriter();
            string filename = "Relatorio - " + comprador.Nome + DateTime.Now.ToString(" '-' ddMMyyyy");

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".csv");
            Response.ContentType = "tex/csv";

            sw.WriteLine("Relatorio de {0}, {1}", comprador.Nome, DateTime.Now.ToString("dd/MM/yyyy"));

            if (dividas.Count > 0)
            {
                sw.WriteLine();
                sw.WriteLine("Dividas");
                sw.WriteLine("Valor total de R$ {0}", dividas.Sum(d => d.ValorDivida));
                sw.WriteLine("Descricao; Valor da Divida; Valor Pago; Valor Restante; Data da Compra");

                dividas.ForEach(d =>
                {
                    sw.WriteLine(string.Format("{0}; {1}; {2}; {3}; {4}", d.Descricao, d.ValorDivida, d.ValorPago, d.ValorRestante, d.Data.ToString("dd/MM/yyyy")));
                });
            }

            if(lancamentosAtuais.Count > 0)
            {
                sw.WriteLine();
                sw.WriteLine("Compras Atuais");

                foreach (string key in lancamentosAtuais.Keys)
                {                   
                    sw.WriteLine("Compras de {0}", key);
                    sw.WriteLine("Valor total de R$ {0}", lancamentosAtuais[key].Sum(l => l.Valor));
                    sw.WriteLine("Descricao; Valor; Parcela; Observacoes");

                    lancamentosAtuais[key].ForEach(l =>
                    {
                        sw.WriteLine(string.Format("{0}; {1}; {2}; {3};", l.Descricao, l.Valor, l.ParcelaAtual + "/" + l.QtdParcelas, l.Observacao));
                    });
                    sw.WriteLine();
                }
            }

            if (lancamenos.Count > 0)
            {
                sw.WriteLine();
                sw.WriteLine("Lancamentos");

                foreach (string key in lancamenos.Keys)
                {
                    sw.WriteLine(key);
                    sw.WriteLine("Valor total de R$ {0}", lancamenos[key].Sum(l => l.Valor));
                    sw.WriteLine("Descricoo; Valor; Parcela; Observacoes");

                    lancamenos[key].ForEach(l =>
                    {
                        sw.WriteLine(string.Format("{0}; {1}; {2}; {3};", l.Descricao, l.Valor, l.ParcelaAtual + "/" + l.QtdParcelas, l.Observacao));
                    });
                    sw.WriteLine();
                }
            }

            Response.Write(sw.ToString());
            Response.End();
        }


        public void ExportListFromTable(HttpResponseBase Response, List<DividaModel> dividas)
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=Contact.xls");
            Response.AddHeader("Content-Type", "application/vnd.ms-excel");
            WriteHtmlTable(dividas, Response.Output);
            Response.End();
        }


        private void WriteHtmlTable<T>(IEnumerable<T> data, TextWriter output)
        {
            //Writes markup characters and text to an ASP.NET server control output stream. This class provides formatting capabilities that ASP.NET server controls use when rendering markup to clients.
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {

                    //  Create a form to contain the List
                    Table table = new Table();
                    TableRow row = new TableRow();
                    PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
                    foreach (PropertyDescriptor prop in props)
                    {
                        TableHeaderCell hcell = new TableHeaderCell();
                        hcell.Text = prop.Name;
                        hcell.BackColor = System.Drawing.Color.Yellow;
                        row.Cells.Add(hcell);
                    }

                    table.Rows.Add(row);

                    //  add each of the data item to the table
                    foreach (T item in data)
                    {
                        row = new TableRow();
                        foreach (PropertyDescriptor prop in props)
                        {
                            try{
                                TableCell cell = new TableCell();
                                cell.Text = prop.Converter.ConvertToString(prop.GetValue(item));
                                row.Cells.Add(cell);
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }
                        table.Rows.Add(row);
                    }

                    //  render the table into the htmlwriter
                    table.RenderControl(htw);

                    //  render the htmlwriter into the response
                    output.Write(sw.ToString());
                }
            }
        }
    }   
}