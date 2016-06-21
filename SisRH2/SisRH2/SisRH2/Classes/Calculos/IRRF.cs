using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisRH2.Classes.Calculos
{
    class IRRF
    {
        public IRRF(decimal _Bruto, decimal _INSS)
        {
            BaseCalculo = _Bruto - _INSS;

            if (BaseCalculo <= (decimal)1903.98)
            {
                Aliquota = 0;
                ValorDeduzir = 0;
            }
            else if (BaseCalculo <= (decimal)2826.65)
            {
                Aliquota = (decimal)7.5;
                ValorDeduzir = (decimal)142.80;
            }
            else if (BaseCalculo <= (decimal)3751.05)
            {
                Aliquota = 15;
                ValorDeduzir = (decimal)354.80;
            }
            else if (BaseCalculo <= (decimal)4664.68)
            {
                Aliquota = (decimal)22.5;
                ValorDeduzir = (decimal)636.13;
            }
            else
            {
                Aliquota = (decimal)27.5;
                ValorDeduzir = (decimal)869.36;
            }

            Valor = Geral.FormatNumber((BaseCalculo * (Aliquota / 100)) - ValorDeduzir);
        }

        private decimal _Aliquota;

        public decimal Aliquota
        {
            get { return _Aliquota; }
            set { _Aliquota = value; }
        }

        private decimal _ValorDeduzir;

        public decimal ValorDeduzir
        {
            get { return _ValorDeduzir; }
            set { _ValorDeduzir = value; }
        }

        private decimal _BaseCalculo;

        public decimal BaseCalculo
        {
            get { return _BaseCalculo; }
            set { _BaseCalculo = value; }
        }

        private decimal _Valor;

        public decimal Valor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }
    }
}
