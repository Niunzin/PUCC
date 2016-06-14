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

        public static string FormatarParaReal(float Valor)
        {
            return string.Format("{0:R$#,##0.00;(R$#,##0.00)}", Valor);
        }

        public static string GetText(params string[] args)
        {
            string Text = string.Format(
                "Funcionário: {0}\t\tSetor: {1}\n-------------------------------------------------------------------------------------------------------------------------------------------------------------------\n" +
                "SALÁRIO MENSAL\nSalário Bruto: {2}\tBônus: {3}\t\tValor Total Bruto: {4}\n"+
                "Alíquota de INSS: {5}\tINSS: {6}\t\tBase de cálculo: {7}\n"+
                "Alíquota de IRRF: {8}\tValor a deduzir: {9}\tIRRF: {10}\n"+
                "Salário Líquido: {11}\n-------------------------------------------------------------------------------------------------------------------------------------------------------------------\n" +
                "FÉRIAS\nDias de férias: {12}\t\tValor Total de férias: {13}\n" +
                "Alíquota de INSS: {14}\tINSS: {15}\t\tBase de cálculo: {16}\n" +
                "Alíquota de IRRF: {17}\tValor a deduzir: {18}\tIRRF: {19}\n" +
                "Férias Líquido: {20}\n-------------------------------------------------------------------------------------------------------------------------------------------------------------------\n" +
                "13º SALÁRIO\nNº de meses trabalhados: {21}\tValor Total Trabalhado: {22}\n" +
                "Alíquota de INSS: {23}\tINSS: {24}\t\tBase de cálculo: {25}\n" +
                "Alíquota de IRRF: {26}\tValor a deduzir: {27}\tIRRF: {28}\n" +
                "13º Salário Líquido: {29}\n=================================================================================\n\n\n",
                args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8], args[9], args[10], args[11],
                args[12], args[13], args[14], args[15], args[16], args[17], args[18], args[19], args[20], args[21], args[22], args[23],
                args[24], args[25], args[26], args[27], args[28], args[29]);
            return Text;
        }
    }
}
