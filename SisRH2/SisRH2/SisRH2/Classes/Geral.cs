using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisRH2.Classes
{
    class Geral
    {
        public static string ErrorListToString(List<string> errors)
        {
            string Output = "";
            foreach(string error in errors)
            {
                Output += string.Format("{0} {1}\n", "•", error);
            }
            return Output;
        }

        public static double FormatNumber(double Input)
        {
            return Math.Floor(Input * 100) / 100;
        }

        public static string FormatarParaReal(double Valor)
        {
            return string.Format("{0:R$#,##0.00;(R$#,##0.00)}", Geral.FormatNumber(Valor));
        }

        public static string GetText(params string[] args)
        {
            string Text = string.Format(
                "Funcionário: {0}\t\tSetor: {1}\n" +
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------\n" +
                "SALÁRIO MENSAL\n" + 
                "Salário Bruto: {2}\tBônus: {3}\t\tValor Total Bruto: {4}\n"+
                "Alíquota de INSS: {5}\tINSS: {6}\t\tBase de cálculo: {7}\n"+
                "Alíquota de IRRF: {8}\tValor a deduzir: {9}\tIRRF: {10}\n"+
                "Salário Líquido: {11}\n" +
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------\n" +
                "FÉRIAS\nDias de férias: {12}\t\tValor Total de férias: {13}\n" +
                "Alíquota de INSS: {14}\tINSS: {15}\t\tBase de cálculo: {16}\n" +
                "Alíquota de IRRF: {17}\tValor a deduzir: {18}\tIRRF: {19}\n" +
                "Férias Líquido: {20}\n" +
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------\n" +
                "13º SALÁRIO\nNº de meses trabalhados: {21}\tValor Total Trabalhado: {22}\n" +
                "Alíquota de INSS: {23}\tINSS: {24}\t\tBase de cálculo: {25}\n" +
                "Alíquota de IRRF: {26}\tValor a deduzir: {27}\tIRRF: {28}\n" +
                "13º Salário Líquido: {29}\n" +
                "=================================================================================\n\n\n",
                args);
            return Text;
        }
    }
}
