using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RHCalculos;

namespace SisRH
{
    public partial class frmPrincipal : Form
    {
        private List<TextBox> Campos;
        private int Acao = 1;

        public frmPrincipal()
        {
            InitializeComponent();

            Campos = new List<TextBox>();
            Campos.Add(txtSalarioBruto);
            Campos.Add(txtBonus);
            Campos.Add(txtMesesTrabalhados);
            Campos.Add(txtDiasDeFerias);
            Campos.Add(txt13Liquido);
            Campos.Add(txtBaseDeCalculo);
            Campos.Add(txtFeriasLiquido);
            Campos.Add(txtINSS);
            Campos.Add(txtIRRF);
            Campos.Add(txtSalarioLiquido);
        }

        private void MostrarErro(string Mensagem)
        {
            MessageBox.Show(Mensagem, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void LimparRespostas()
        {
            for (int i = 4; i < Campos.Count; i++)
                Campos.ElementAt(i).Clear();
        }

        private void CalcularSalarioLiquido()
        {
            try
            {
                float SalarioBruto;
                float Bonus;
                float INSS;
                float BaseDeCalculo;
                float IRRF;

                if (string.IsNullOrEmpty(txtSalarioBruto.Text))
                    throw new Exception("O campo salário bruto não pode estar vazio.");

                if (!float.TryParse(txtSalarioBruto.Text, out SalarioBruto))
                    throw new Exception("O salário bruto deve ser um número.");

                if (SalarioBruto < 0)
                    throw new Exception("O salário bruto não pode ser um valor menor do que zero.");

                if (string.IsNullOrEmpty(txtBonus.Text))
                    throw new Exception("O campo bônus não pode estar vazio.");

                if (!float.TryParse(txtBonus.Text, out Bonus))
                    throw new Exception("O bônus deve ser um número.");

                if (Bonus < 0)
                    throw new Exception("O bônus não pode ser um valor menor do que zero.");

                INSS = RHCalculos.Geral.ObterINSS(SalarioBruto + Bonus);
                BaseDeCalculo = RHCalculos.Geral.ObterBaseDeCalculo(SalarioBruto + Bonus, INSS);
                IRRF = RHCalculos.Geral.ObterIRRF(BaseDeCalculo);

                txtINSS.Text = RHCalculos.Geral.FormatarParaReal(INSS);
                txtBaseDeCalculo.Text = RHCalculos.Geral.FormatarParaReal(BaseDeCalculo);
                txtIRRF.Text = RHCalculos.Geral.FormatarParaReal(IRRF);
                txtSalarioLiquido.Text = RHCalculos.Geral.FormatarParaReal(BaseDeCalculo - IRRF);

            }
            catch (Exception Erro)
            {
                MostrarErro(Erro.Message);
                btnLimpar_Click(null, null);
            }
        }

        private void CalcularFeriasLiquido()
        {
            try
            {
                float SalarioBruto;
                int DiasDeFerias;
                float INSS;
                float BaseDeCalculo;
                float IRRF;
                float ValorTotalFerias;

                if (string.IsNullOrEmpty(txtSalarioBruto.Text))
                    throw new Exception("O campo salário bruto não pode estar vazio.");

                if (!float.TryParse(txtSalarioBruto.Text, out SalarioBruto))
                    throw new Exception("O salário bruto deve ser um número.");

                if (SalarioBruto < 0)
                    throw new Exception("Por favor, informar um valor que seja maior que zero para o campo Salário Bruto");

                if (string.IsNullOrEmpty(txtDiasDeFerias.Text))
                    throw new Exception("O campo dias de férias não pode estar vazio.");

                if (!int.TryParse(txtDiasDeFerias.Text, out DiasDeFerias))
                    throw new Exception("O nº dias de férias deve ser um número inteiro.");

                if (DiasDeFerias < 10 || DiasDeFerias > 30)
                    throw new Exception("Por favor, informar um valor que seja entre 10 e 30 para o campo Dias de Férias");

                ValorTotalFerias = RHCalculos.BotaoFeriasLiquido.ObterValorTotalDeFerias(SalarioBruto, DiasDeFerias);
                INSS = RHCalculos.Geral.ObterINSS(ValorTotalFerias);
                BaseDeCalculo = RHCalculos.Geral.ObterBaseDeCalculo(ValorTotalFerias, INSS);
                IRRF = RHCalculos.Geral.ObterIRRF(BaseDeCalculo);

                txtINSS.Text = RHCalculos.Geral.FormatarParaReal(INSS);
                txtBaseDeCalculo.Text = RHCalculos.Geral.FormatarParaReal(BaseDeCalculo);
                txtIRRF.Text = RHCalculos.Geral.FormatarParaReal(IRRF);
                txtFeriasLiquido.Text = RHCalculos.Geral.FormatarParaReal(BaseDeCalculo - IRRF);

            }
            catch (Exception Erro)
            {
                MostrarErro(Erro.Message);
                btnLimpar_Click(null, null);
            }
        }

        private void Calcular13Liquido()
        {
            try
            {
                float SalarioBruto;
                int MesesDeFerias;
                float INSS;
                float BaseDeCalculo;
                float IRRF;
                float ValorTotal;

                if (string.IsNullOrEmpty(txtSalarioBruto.Text))
                    throw new Exception("O campo salário bruto não pode estar vazio.");

                if (!float.TryParse(txtSalarioBruto.Text, out SalarioBruto))
                    throw new Exception("O salário bruto deve ser um número.");

                if (SalarioBruto < 0)
                    throw new Exception("Por favor, informar um valor que seja maior que zero para o campo Salário Bruto");

                if (string.IsNullOrEmpty(txtMesesTrabalhados.Text))
                    throw new Exception("O campo meses trabalhados não pode estar vazio.");

                if (!int.TryParse(txtMesesTrabalhados.Text, out MesesDeFerias))
                    throw new Exception("O nº de meses trabalhados deve ser um número inteiro.");

                if (MesesDeFerias < 1 && MesesDeFerias > 12)
                    throw new Exception("Por favor, informar um valor que seja entre 1 e 12 para o campo Nº Meses Trabalhado");

                ValorTotal = RHCalculos.Botao13Liquido.ObterValorTotal(SalarioBruto, MesesDeFerias);
                INSS = RHCalculos.Geral.ObterINSS(ValorTotal);
                BaseDeCalculo = RHCalculos.Geral.ObterBaseDeCalculo(ValorTotal, INSS);
                IRRF = RHCalculos.Geral.ObterIRRF(BaseDeCalculo);

                txtINSS.Text = RHCalculos.Geral.FormatarParaReal(INSS);
                txtBaseDeCalculo.Text = RHCalculos.Geral.FormatarParaReal(BaseDeCalculo);
                txtIRRF.Text = RHCalculos.Geral.FormatarParaReal(IRRF);
                txt13Liquido.Text = RHCalculos.Geral.FormatarParaReal(BaseDeCalculo - IRRF);

            }
            catch (Exception Erro)
            {
                MostrarErro(Erro.Message);
                btnLimpar_Click(null, null);
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            foreach (TextBox txtTextbox in Campos)
                txtTextbox.Clear();

            txtSalarioBruto.Focus();
        }

        private void rdSalarioLiquido_CheckedChanged(object sender, EventArgs e)
        {
            Acao = 1;

            foreach (TextBox txtTextbox in Campos)
            {
                txtTextbox.ReadOnly = true;
                if (!txtTextbox.Equals(txtSalarioBruto))
                    txtTextbox.Clear();
            }

            txtSalarioBruto.ReadOnly = false;
            txtBonus.ReadOnly = false;
        }

        private void rdFeriasLiquido_CheckedChanged(object sender, EventArgs e)
        {
            Acao = 2;

            foreach (TextBox txtTextbox in Campos)
            {
                txtTextbox.ReadOnly = true;
                if (!txtTextbox.Equals(txtSalarioBruto))
                    txtTextbox.Clear();
            }

            txtSalarioBruto.ReadOnly = false;
            txtDiasDeFerias.ReadOnly = false;
        }

        private void rd13Liquido_CheckedChanged(object sender, EventArgs e)
        {
            Acao = 3;

            foreach (TextBox txtTextbox in Campos)
            {
                txtTextbox.ReadOnly = true;
                if (!txtTextbox.Equals(txtSalarioBruto))
                    txtTextbox.Clear();
            }

            txtSalarioBruto.ReadOnly = false;
            txtMesesTrabalhados.ReadOnly = false;
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            LimparRespostas();
            switch (Acao)
            {
                case 1:
                    CalcularSalarioLiquido();
                    break;
                case 2:
                    CalcularFeriasLiquido();
                    break;
                case 3:
                    Calcular13Liquido();
                    break;
            }
        }

        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Você realmente deseja sair?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
                e.Cancel = false;
            else if (dialogResult == DialogResult.No)
                e.Cancel = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
