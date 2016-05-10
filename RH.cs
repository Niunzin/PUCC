using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class RH
    {
        /*
         * Calcula o salário bruto do funcionário
        */
        public static float ValorTotalBruto(string sValorBruto, string sValorBonus)
        {
            // Tenta
            try
            {
                // Conversão do Bônus para Float
                float Bonus;
                float ValorBruto;

                // Tenta converter, se não, retorna um erro
                if (!float.TryParse(sValorBonus, out Bonus))
                    // Mensagem de Erro
                    throw new Exception("O bônus digitado não é um número.");

                // Mesmo de cima
                if (!float.TryParse(sValorBruto, out ValorBruto))
                    throw new Exception("O valor bruto digitado não é um número.");

                // Soma do Bônus com o Valor Bruto
                ValorBruto = ValorBruto + Bonus;

                // Declara a variável que retorna o resultado
                float ValorComDesconto = 0;

                // Se for menor que R$1.556,95
                if (ValorBruto < 1556.95)
                    // Desconto de 8%
                    ValorComDesconto = ValorBruto * (float)0.92;
                // Maior que R$1.556,95 e menor que R$2.594,92
                else if (ValorBruto < 2594.92)
                    // Desconto de 9%
                    ValorComDesconto = ValorBruto * (float)0.91;
                // Maior do que R$2.594,93
                else
                {
                    // Obtem a contribuição dele
                    float Contribuicao = ValorBruto * (float)0.11;

                    // Se a contribuição for maior que R$570,88
                    if (Contribuicao > 570.88)
                        // Desconta apenas R$570,88 (limite de contribuição)
                        ValorComDesconto = ValorBruto - (float)570.88;
                    else
                        // A contribuição é menor que o limite, então desconta 11%
                        ValorComDesconto = ValorBruto * (float)0.89;
                }

                // Retorna o valor já calculado
                return ValorComDesconto;
            // Houve um erro
            } catch (Exception Erro)
            {
                // Mostra uma janelinha com o erro
                System.Windows.Forms.MessageBox.Show(
                    string.Format("{0}", Erro.Message),
                    "Erro!", System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Warning
                );
            }
            return 0;
        }

        /*
         * Calcula o valor total de férias
        */
        public static float ValorTotalDeFerias(string sSalárioBruto, string sDiasDeFerias)
        {
            float SalarioBruto = float.Parse(sSalárioBruto);
            int DiasDeFerias = int.Parse(sDiasDeFerias);

            float TotalDeFerias = SalarioBruto * (DiasDeFerias / 30);
            return 0;
        }

        /*
         * Transforma o número bruto em R$
        */
        public static string FormatarNumero(float Numero)
        {
            return string.Format("R${0}", Numero);
        }
    }
}
