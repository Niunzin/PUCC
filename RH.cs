using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class RH
    {
        enum TipoDoCaculo
        {
            DESCONTAR,
            CALCULAR
        }

        /*
         * Retorna o valor com imposto do INSS
        */
        public static float INSS(float ValorBruto, TipoDoCaculo Tipo)
        {
            try
            {
                double[] Aliquotas = new double[] { 0.8, 0.9, 0.11 };

                float ValorComDesconto = 0;
                if (ValorBruto < 1556.95)
                    // Desconto de 8%
                    ValorComDesconto = ValorBruto * (float)((Tipo == TipoDoCaculo.DESCONTAR) ? 1 - Aliquotas[0] : Aliquotas[0]);
                // Maior que R$1.556,95 e menor que R$2.594,92
                else if (ValorBruto < 2594.92)
                    // Desconto de 9%
                    ValorComDesconto = ValorBruto * (float)((Tipo == TipoDoCaculo.DESCONTAR) ? 1 - Aliquotas[1] : Aliquotas[1]);
                // Maior do que R$2.594,93
                else
                {
                    if (Tipo == TipoDoCaculo.DESCONTAR)
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
                }

                return ValorComDesconto;
            }
            catch (Exception Erro)
            {
                System.Windows.Forms.MessageBox.Show(
                    string.Format("{0}", Erro.Message),
                    "Erro!", System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Warning
                );
            }

            return 0;
        }

        public static float IRRF(string sValorBruto)
        {
            try
            {
                float ValorBruto;
                if (!float.TryParse(sValorBruto, out ValorBruto))
                    throw new Exception("O valor bruto digitado não é um número!");

                if (ValorBruto < 1903.98)
                    return ValorBruto;
                else if (ValorBruto < 2826.65)
                    ValorBruto = ValorBruto * (float)0.925;
                else if (ValorBruto < 3751.05)
                    ValorBruto = ValorBruto * (float)0.850;
                else if (ValorBruto < 4664.68)
                    ValorBruto = ValorBruto * (float)0.775;
                else
                    ValorBruto = ValorBruto * (float)0.725;

                return ValorBruto;
            } catch (Exception Erro)
            {
                System.Windows.Forms.MessageBox.Show(
                    string.Format("{0}", Erro.Message),
                    "Erro!", System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Warning
                );
            }
            return 0;
        }

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

                // Retorna o valor já calculado
                return RH.INSS(ValorBruto, TipoDoCaculo.DESCONTAR);
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
            try
            {
                if (string.IsNullOrEmpty(sSalárioBruto) || string.IsNullOrEmpty(sDiasDeFerias))
                    throw new Exception("Preencha todos os campos antes de continuar!");

                float SalarioBruto;
                int DiasDeFerias;

                if (!float.TryParse(sSalárioBruto, out SalarioBruto))
                    throw new Exception("O salário bruto digitado não é um número!");

                if (!int.TryParse(sDiasDeFerias, out DiasDeFerias))
                    throw new Exception("Os dias de férias foram digitados incorretamente. Certifique-se do valor ser um número.");

                if (DiasDeFerias < 1 || DiasDeFerias > 30)
                    throw new Exception("Os dias de férias devem estar entre 1 e 30.");

                SalarioBruto = SalarioBruto * (DiasDeFerias / 30);

                return RH.INSS(SalarioBruto, TipoDoCaculo.DESCONTAR);
            } catch (Exception Erro)
            {
                System.Windows.Forms.MessageBox.Show(
                    string.Format("{0}", Erro.Message),
                    "Erro!", System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Warning
                );
            }
            return 0;
        }

        public static float ValorTotalTrabalhado(string sSalarioBruto, string sMesesTrabalhados)
        {
            try
            {
                float SalarioBruto;
                int MesesTrabalhados;

                if (!float.TryParse(sSalarioBruto, out SalarioBruto))
                    throw new Exception("O salário bruto digitado não é um número!");

                if (!int.TryParse(sMesesTrabalhados, out MesesTrabalhados))
                    throw new Exception("O número de meses trabalhados não é um número");

                if (MesesTrabalhados > 12 || MesesTrabalhados < 1)
                    throw new Exception("O número de meses deve ser um valor de 1 a 12.");

                return RH.INSS(SalarioBruto * (MesesTrabalhados / 12), TipoDoCaculo.DESCONTAR);
            }
            catch (Exception Erro)
            {
                System.Windows.Forms.MessageBox.Show(
                    string.Format("{0}", Erro.Message),
                    "Erro!", System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Warning
                );
            }
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
