using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisRH2.Classes.Calculos
{
    class INSS
    {
        public INSS(decimal _SalarioBruto, decimal _Parametro, Tipo _Tipo)
        {
            switch(_Tipo)
            {
                case Tipo.SALARIO_BRUTO:
                    ValorTotalBruto = Geral.FormatNumber(_SalarioBruto + _Parametro);
                    break;
                case Tipo.DIAS_FERIAS:
                    ValorTotalBruto = Geral.FormatNumber(_SalarioBruto * _Parametro / 30);
                    break;
                case Tipo.MESES_TRABALHADOS:
                    ValorTotalBruto = Geral.FormatNumber(_SalarioBruto * _Parametro / 12);
                    break;
                default:
                    break;
            }

            if (ValorTotalBruto <= (decimal)1556.94) Aliquota = 8;
            else if (ValorTotalBruto <= (decimal)2594.92) Aliquota = 9;
            else if (ValorTotalBruto <= (decimal)5189.82) Aliquota = 11;
            else Aliquota = 11;

            Contribuicao = Geral.FormatNumber(ValorTotalBruto * (Aliquota / (decimal)100));

            if (Contribuicao > (decimal)570.88)
                Contribuicao = (decimal)570.88;
        }

        public enum Tipo
        {
            SALARIO_BRUTO,
            DIAS_FERIAS,
            MESES_TRABALHADOS
        }

        private decimal _Aliquota;

        public decimal Aliquota
        {
            get { return _Aliquota; }
            set { _Aliquota = value; }
        }

        private decimal _Contribuicao;

        public decimal Contribuicao
        {
            get { return _Contribuicao; }
            set { _Contribuicao = value; }
        }
            
        private decimal _ValorTotalBruto;

        public decimal ValorTotalBruto
        {
            get { return _ValorTotalBruto; }
            set { _ValorTotalBruto = value; }
        }
    }
}
