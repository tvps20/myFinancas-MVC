using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Models.Enuns
{
    public enum TipoIcone
    {
        [Description("done")]
        SUCESSO = 1,
        [Description("error")]
        ERRO = 0,
        [Description("info")]
        INFO = 2,

        [Description("payment")]
        CARTAO = 3,
        [Description("store")]
        CONTAS = 4,
        [Description("local_offer")]
        NOTA = 5,
        [Description("insert_drive_file")]
        FATURA = 6,
        [Description("access_time")]
        TEMPO = 7,
        [Description("cloud_download")]
        DONWLOAD = 8,
        [Description("shop")]
        COMPRAS = 9,

        [Description("edit")]
        EDITAR = 10,
        [Description("description")]
        DETALHES = 11,
        [Description("close")]
        REMOVER = 12,

        [Description("dashboard")]
        DASHBOARD = 13,
        [Description("payment")]
        CARTOES = 14,
        [Description("picture_as_pdf")]
        FATURAS = 15,
        [Description("people")]
        COMPRADORES = 16,
        [Description("shop_two")]
        DIVIDAS = 17,
        [Description("library_books")]
        RELATORIOS = 18
    }
}