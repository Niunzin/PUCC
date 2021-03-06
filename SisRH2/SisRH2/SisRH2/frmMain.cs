using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SisRH2.Classes;
using SisRH2.Classes.Calculos;

namespace SisRH2
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Seleciona a primeira opção da combo
            cbSetor.SelectedIndex = 0;
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            // Lista de erros
            List<string> Errors = new List<string>();

            try
            {
                decimal SalarioBruto = 0;
                decimal Bonus = 0;
                int MesesTrabalhados = 0;
                int DiasFerias = 0;
                float NomeDoFuncionarioErrado = 0;

                if(string.IsNullOrEmpty(txtFuncionario.Text.Trim()))
                    Errors.Add("Você precisa digitar o nome do funcionário.");

                if (float.TryParse(txtFuncionario.Text, out NomeDoFuncionarioErrado))
                    Errors.Add("O nome do funcionário não pode ser numérico.");
                //else if (txtFuncionario.Text.IndexOfAny("0123456789".ToCharArray()) != -1)
                    //Errors.Add("O nome do funcionário não pode conter números.");

                if (cbSetor.SelectedIndex == 0)
                    Errors.Add("Você não selecionou o cargo do funcionário.");

                if (!decimal.TryParse(txtSalario.Text, out SalarioBruto))
                    Errors.Add("O campo \"Salário Bruto\" precisa ser numérico e positivo.");
                else if (SalarioBruto <= 0)
                    Errors.Add("O campo \"Salário Bruto\" deve ser maior que zero.");

                if (!decimal.TryParse(txtBonus.Text, out Bonus))
                    Errors.Add("O campo \"Bônus\" precisa ser numérico e positivo.");
                else if (Bonus <= 0)
                    Errors.Add("O campo \"Bônus\" deve ser maior que zero.");

                if (!int.TryParse(txtMesesTrabalhados.Text, out MesesTrabalhados))
                    Errors.Add("O campo \"Nº Meses Trabalhados\" precisa ser numérico e entre 1 e 12.");
                else if (MesesTrabalhados < 1 || MesesTrabalhados > 12)
                    Errors.Add("Por favor, informar um valor que seja entre 1 e 12 para o campo \"Nº Meses Trabalhado\".");

                if (!int.TryParse(txtDiasDeFerias.Text, out DiasFerias))
                    Errors.Add("O campo \"Dias de Férias\" precisa ser numérico e entre 10 e 30.");
                else if (DiasFerias < 10 || DiasFerias > 30)
                    Errors.Add("Por favor, informar um valor que seja entre 10 e 30 para o campo \"Dias de Férias\".");

                // Verifica a existência de erros internos
                if(Errors.Count > 0)
                    throw new Exception("");

                SalarioBruto = Geral.FormatNumber(SalarioBruto);
                Bonus = Geral.FormatNumber(Bonus);

                // INSS => Salário Líquido
                INSS INSS_SalarioBruto = new INSS(SalarioBruto, Bonus, INSS.Tipo.SALARIO_BRUTO);

                // INSS => Férias
                INSS INSS_Ferias = new INSS(SalarioBruto, DiasFerias, INSS.Tipo.DIAS_FERIAS);

                // INSS => 13º
                INSS INSS_MesesTrabalhados = new INSS(SalarioBruto, MesesTrabalhados, INSS.Tipo.MESES_TRABALHADOS);
                
                // IRRF => Salário Líquido
                IRRF IRRF_SalarioBruto = new IRRF(INSS_SalarioBruto.ValorTotalBruto, INSS_SalarioBruto.Contribuicao);
                
                // IRRF => Férias
                IRRF IRRF_Ferias = new IRRF(INSS_Ferias.ValorTotalBruto, INSS_Ferias.Contribuicao);

                //IRRF => 13º
                IRRF IRRF_MesesTrabalhados = new IRRF(INSS_MesesTrabalhados.ValorTotalBruto, INSS_MesesTrabalhados.Contribuicao);


                txtRegistro.AppendText(Geral.GetText(
                    txtFuncionario.Text,
                    cbSetor.SelectedItem.ToString(),

                    #region CALCULO SALARIO LIQUIDO
                    Geral.FormatarParaReal(SalarioBruto),
                    Geral.FormatarParaReal(Bonus),
                    Geral.FormatarParaReal(INSS_SalarioBruto.ValorTotalBruto),
                    string.Format("{0:0.00}%", INSS_SalarioBruto.Aliquota),
                    Geral.FormatarParaReal(INSS_SalarioBruto.Contribuicao),

                    Geral.FormatarParaReal(IRRF_SalarioBruto.BaseCalculo),
                    string.Format("{0:0.00}%", IRRF_SalarioBruto.Aliquota),
                    Geral.FormatarParaReal(IRRF_SalarioBruto.ValorDeduzir),
                    Geral.FormatarParaReal(IRRF_SalarioBruto.Valor),
                    Geral.FormatarParaReal(IRRF_SalarioBruto.BaseCalculo - IRRF_SalarioBruto.Valor),
                    #endregion

                    #region CALCULO FERIAS LIQUIDO
                    DiasFerias.ToString(),
                    Geral.FormatarParaReal(INSS_Ferias.ValorTotalBruto),
                    string.Format("{0:0.00}%", INSS_Ferias.Aliquota),
                    Geral.FormatarParaReal(INSS_Ferias.Contribuicao),

                    Geral.FormatarParaReal(IRRF_Ferias.BaseCalculo),
                    string.Format("{0:0.00}%", IRRF_Ferias.Aliquota),
                    Geral.FormatarParaReal(IRRF_Ferias.ValorDeduzir),
                    Geral.FormatarParaReal(IRRF_Ferias.Valor),
                    Geral.FormatarParaReal(IRRF_Ferias.BaseCalculo - IRRF_Ferias.Valor),
                    #endregion

                    #region CALCULO 13 LIQUIDO
                    MesesTrabalhados.ToString(),
                    Geral.FormatarParaReal(INSS_MesesTrabalhados.ValorTotalBruto),
                    string.Format("{0:0.00}%", INSS_MesesTrabalhados.Aliquota),
                    Geral.FormatarParaReal(INSS_MesesTrabalhados.Contribuicao),

                    Geral.FormatarParaReal(IRRF_MesesTrabalhados.BaseCalculo),
                    string.Format("{0:0.00}%", IRRF_MesesTrabalhados.Aliquota),
                    Geral.FormatarParaReal(IRRF_MesesTrabalhados.ValorDeduzir),
                    Geral.FormatarParaReal(IRRF_MesesTrabalhados.Valor),
                    Geral.FormatarParaReal(IRRF_MesesTrabalhados.BaseCalculo - IRRF_MesesTrabalhados.Valor)
                    #endregion
                ));
            } catch (Exception error)
            {
                // Tratamento de erro interno
                if (string.IsNullOrEmpty(error.Message))
                {
                    btnLimpar_Click(error, null);
                    MessageBox.Show(string.Format("{0}\n\n{1}",
                        "Atenção:",Errors.ElementAt(0)), "Atenção!" , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                } else
                // Tratamento de erro externo
                {
                    MessageBox.Show(error.Message + "\n" + error.StackTrace, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Limpar(bool Registro = false)
        {
            // Limpa todos os campos
            txtBonus.Clear();
            txtDiasDeFerias.Clear();
            txtFuncionario.Clear();
            txtMesesTrabalhados.Clear();
            txtSalario.Clear();

            if(Registro)
                txtRegistro.Clear();

            // Seleciona o cargo padrão
            cbSetor.SelectedIndex = 0;

            // Foca no campo "Funcionário"
            txtFuncionario.Focus();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            // Verifica se o ativador é uma exceção
            if (sender is Exception)
                // Se for, apenas limpa o formulário
                this.Limpar();
            // Se não, pergunta ao cliente se o mesmo deseja continuar
            else if (MessageBox.Show("Esta ação irá limpar TODOS os dados inseridos, você realmente deseja continuar?", "Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                this.Limpar(true);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Deseja sair do programa de RH?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
                this.Activate();
            }   
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                btnCadastrar_Click(sender, null);
        }

        private void txtBox_Leave(object sender, EventArgs e)
        {
            if (!(sender is TextBox)) return;
            TextBox txtBox = (TextBox)sender;

            if (txtBox.Text.Contains("R$"))
                txtBox.Text = txtBox.Text.Replace("R$", "").Trim();
        }

        private void txtBox_Clear(object sender, KeyPressEventArgs e)
        {
            if (!(sender is TextBox)) return;
            TextBox txtBox = (TextBox)sender;

            switch (e.KeyChar)
            {
                case '0': e.Handled = false; break;
                case '1': e.Handled = false; break;
                case '2': e.Handled = false; break;
                case '3': e.Handled = false; break;
                case '4': e.Handled = false; break;
                case '5': e.Handled = false; break;
                case '6': e.Handled = false; break;
                case '7': e.Handled = false; break;
                case '8': e.Handled = false; break;
                case '9': e.Handled = false; break;
                default: e.Handled = true; break;
            }
        }
    }
}
