using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisRH2.Classes.Calculos
{
    class IRRF
    {
        public IRRF(double _Bruto, double _INSS, bool Meses = false, double TotalMeses = 0)
        {

            if (Meses)
                BaseCalculo = ((_Bruto * TotalMeses) / 12) - _INSS;
            else
                BaseCalculo = _Bruto - _INSS;


            if (BaseCalculo <= 1903.98)
            {
                Aliquota = 0;
                ValorDeduzir = 0;
            }
            else if (BaseCalculo <= 2826.65)
            {
                Aliquota = 7.5;
                ValorDeduzir = 142.80;
            }
            else if (BaseCalculo <= 3751.05)
            {
                Aliquota = 15;
                ValorDeduzir = 354.80;
            }
            else if (BaseCalculo <= 4664.68)
            {
                Aliquota = 22.5;
                ValorDeduzir = 636.13;
            }
            else
            {
                Aliquota = 27.5;
                ValorDeduzir = 869.36;
            }

            Valor = (BaseCalculo * (Aliquota / 100)) - ValorDeduzir;
        }

        private double _Aliquota;

        public double Aliquota
        {
            get { return _Aliquota; }
            set { _Aliquota = value; }
        }

        private double _ValorDeduzir;

        public double ValorDeduzir
        {
            get { return _ValorDeduzir; }
            set { _ValorDeduzir = value; }
        }

        private double _BaseCalculo;

        public double BaseCalculo
        {
            get { return _BaseCalculo; }
            set { _BaseCalculo = value; }
        }

        private double _Valor;

        public double Valor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }
    }
}
