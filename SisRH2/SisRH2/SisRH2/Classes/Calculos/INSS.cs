using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisRH2.Classes.Calculos
{
    class INSS
    {
        public INSS(float _SalarioBruto, float _Parametro, Tipo _Tipo)
        {
            if (_Tipo == Tipo.SALARIO_BRUTO)
                ValorTotalBruto = _SalarioBruto + _Parametro;
            else if (_Tipo == Tipo.DIAS_FERIAS)
                ValorTotalBruto = _SalarioBruto * (_Parametro / 30);
            else if  (_Tipo == Tipo.MESES_TRABALHADOS)
                ValorTotalBruto = _SalarioBruto * (_Parametro / 12);

            if (ValorTotalBruto < 1556.94) Aliquota = 8;
            else if (ValorTotalBruto < 2594.92) Aliquota = 9;
            else if (ValorTotalBruto < 5189.82) Aliquota = 11;
            else Aliquota = 11;

            Contribuicao = ValorTotalBruto * (Aliquota / 100);
            if (Contribuicao > 570.88)
                Contribuicao = (float)570.88;
        }

        public enum Tipo
        {
            SALARIO_BRUTO,
            DIAS_FERIAS,
            MESES_TRABALHADOS
        }

        private float _Aliquota;

        public float Aliquota
        {
            get { return _Aliquota; }
            set { _Aliquota = value; }
        }

        private float _Contribuicao;

        public float Contribuicao
        {
            get { return _Contribuicao; }
            set { _Contribuicao = value; }
        }

        private float _ValorTotalBruto;

        public float ValorTotalBruto
        {
            get { return _ValorTotalBruto; }
            set { _ValorTotalBruto = value; }
        }
    }
}
