using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisRH2.Classes.Calculos
{
    class INSS
    {
        public INSS(double _SalarioBruto, double _Parametro, Tipo _Tipo)
        {
            switch(_Tipo)
            {
                case Tipo.SALARIO_BRUTO:
                    ValorTotalBruto = _SalarioBruto + _Parametro;
                    break;
                case Tipo.DIAS_FERIAS:
                    ValorTotalBruto = _SalarioBruto * (_Parametro / 30);
                    break;
                case Tipo.MESES_TRABALHADOS:
                    ValorTotalBruto = _SalarioBruto * (_Parametro / 12);
                    break;
                default:
                    break;
            }

            if (ValorTotalBruto < 1556.94) Aliquota = 8;
            else if (ValorTotalBruto < 2594.92) Aliquota = 9;
            else if (ValorTotalBruto < 5189.82) Aliquota = 11;
            else Aliquota = 11;

            Contribuicao = ValorTotalBruto * (Aliquota / 100);
            if (Contribuicao > 570.88)
                Contribuicao = (double)570.88;
        }

        public enum Tipo
        {
            SALARIO_BRUTO,
            DIAS_FERIAS,
            MESES_TRABALHADOS
        }

        private double _Aliquota;

        public double Aliquota
        {
            get { return _Aliquota; }
            set { _Aliquota = value; }
        }

        private double _Contribuicao;

        public double Contribuicao
        {
            get { return _Contribuicao; }
            set { _Contribuicao = value; }
        }

        private double _ValorTotalBruto;

        public double ValorTotalBruto
        {
            get { return _ValorTotalBruto; }
            set { _ValorTotalBruto = value; }
        }
    }
}
