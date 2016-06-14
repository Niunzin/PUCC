using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisRH2.Classes.Calculos
{
    class IRRF
    {
        public IRRF(float _Bruto, float _INSS, bool Meses = false, float TotalMeses = 0, float inss = 0)
        {

            BaseCalculo = _Bruto - _INSS;

            if (Meses)
                BaseCalculo = ((_Bruto * TotalMeses ) / 12) - inss;

            if (BaseCalculo < 1903.98) { Aliquota = 0; ValorDeduzir = 0; }
            else if (BaseCalculo < 2826.65) { Aliquota = (float)7.5; ValorDeduzir = (float)142.80; }
            else if (BaseCalculo < 3751.05) { Aliquota = 15; ValorDeduzir = (float)354.80; }
            else if (BaseCalculo < 4664.68) { Aliquota = (float)22.5; ValorDeduzir = (float)636.13; }
            else { Aliquota = (float)27.5; ValorDeduzir = (float)869.36; }

            Valor = (BaseCalculo * ((float)Aliquota / 100)) - (float)ValorDeduzir;
        }

        private float _Aliquota;

        public float Aliquota
        {
            get { return _Aliquota; }
            set { _Aliquota = value; }
        }

        private float _ValorDeduzir;

        public float ValorDeduzir
        {
            get { return _ValorDeduzir; }
            set { _ValorDeduzir = value; }
        }

        private float _BaseCalculo;

        public float BaseCalculo
        {
            get { return _BaseCalculo; }
            set { _BaseCalculo = value; }
        }

        private float _Valor;

        public float Valor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }
    }
}
