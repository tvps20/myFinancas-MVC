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
        LANCAMENTO = 9,
        [Description("exposure")]
        DIVIDA = 10,

        [Description("edit")]
        EDITAR = 11,
        [Description("description")]
        DETALHES = 12,
        [Description("close")]
        REMOVER = 13,

        [Description("dashboard")]
        DASHBOARD = 14,
        [Description("payment")]
        CARTOES = 15,
        [Description("picture_as_pdf")]
        FATURAS = 16,
        [Description("people")]
        COMPRADORES = 17,
        [Description("swap_vert")]
        DIVIDAS = 18,
        [Description("shop_two")]
        LANCAMENTOS = 19,
        [Description("library_books")]
        RELATORIOS = 20,

        [Description("monetization_on")]
        MONEY = 21,

        [Description("keyboard_arrow_down")]
        DOWNMOREMAIS = 22,
        [Description("keyboard_arrow_up")]
        UPMOREMAIS = 23

    }
}