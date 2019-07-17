﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace myFinancas.MVC.Models.Enuns
{
    public static class EnumExtensions
    {
        public static string EnumToDescriptionString(this TipoMensagem val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
                .GetType()
                .GetField(val.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }

        public static string EnumToDescriptionString(this TipoIcone val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
                .GetType()
                .GetField(val.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }

        public static string EnumToDescriptionString(this TipoMes val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
                .GetType()
                .GetField(val.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }

        public static List<string> getDescriptionsTipoMes()
        {
            List<string> descricoes = new List<string>();

            foreach (TipoMes value in Enum.GetValues(typeof(TipoMes)))
            {
                descricoes.Add(EnumToDescriptionString(value));
            }

            return descricoes;
        }
    }
}