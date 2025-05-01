using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Ej8.Domain
{
    public class CuentaCorriente : CuentaBancaria
    {
        private decimal _limiteDeDescubierto;
        private decimal _comision;

        public CuentaCorriente(string numero, decimal saldo, string[] titulares, decimal limiteDeDescubierto, decimal comision) 
            : base(numero, saldo, TipoCuenta.CuentaCorriente, titulares)
        {
            _limiteDeDescubierto = limiteDeDescubierto;
            _comision = comision;
        }

        #region Getters/Setters
        public decimal GetLimiteDeDescubierto() => _limiteDeDescubierto;
        public void SetLimiteDeDescubierto(decimal limiteDeDescubierto) => _limiteDeDescubierto = limiteDeDescubierto;

        public decimal GetComision() => _comision;
        public void SetComision(decimal comision) => _comision = comision;

        #endregion

        public override void Depositar(decimal monto)
        {
            monto -= monto * _comision;
            SetSaldo(GetSaldo() + monto);
        } 

        public override void Retirar(decimal monto)
        {
            decimal nuevoSaldo = GetSaldo() - monto;
            if (nuevoSaldo >= -_limiteDeDescubierto)
            {
                SetSaldo(nuevoSaldo);
                if (nuevoSaldo < 0)
                {
                    SetEstado(Estado.Suspendida);
                }
            }
        }
    }
}
