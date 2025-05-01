using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Ej8.Domain
{
    public class CajaDeAhorro : CuentaBancaria
    {
        private decimal _tasaDeInteres;

        public CajaDeAhorro(string numero, decimal saldo, string[] titulares, decimal tasaDeInteres) 
            : base (numero, saldo, TipoCuenta.CajaDeAhorro, titulares)
        {
            _tasaDeInteres = tasaDeInteres;
        }

        #region Getters/Setters
        public decimal GetTasaDeInteres() => _tasaDeInteres;
        public void SetTasaDeInteres(decimal tasaDeInteres) => _tasaDeInteres = tasaDeInteres;

        #endregion
        public override void Depositar(decimal monto)
        {
            SetSaldo(GetSaldo() + monto);
        }

        public override void Retirar (decimal monto)
        {
            SetSaldo(GetSaldo() - monto);
        }

        public override void AplicarInteres()
        {
            SetSaldo(GetSaldo() * _tasaDeInteres);
        }
    }
}
