using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisRH2.Classes.Calculos
{
    class IRRF
    {
        public IRRF(double _Bruto, double _INSS, bool Meses = false, double TotalMeses = 0, double inss = 0)
        {

            BaseCalculo = _Bruto - _INSS;

            if (Meses)
                BaseCalculo = ((_Bruto * TotalMeses ) / 12) - inss;

            if (BaseCalculo < 1903.98) { Aliquota = 0; ValorDeduzir = 0; }
            else if (BaseCalculo < 2826.65) { Aliquota = (double)7.5; ValorDeduzir = (double)142.80; }
            else if (BaseCalculo < 3751.05) { Aliquota = 15; ValorDeduzir = (double)354.80; }
            else if (BaseCalculo < 4664.68) { Aliquota = (double)22.5; ValorDeduzir = (double)636.13; }
            else { Aliquota = (double)27.5; ValorDeduzir = (double)869.36; }

            Valor = ((double)BaseCalculo * ((double)Aliquota / 100)) - (double)ValorDeduzir;
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
