using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Dsw2025Ej8.Domain.Exceptions.Exceptions;

namespace Dsw2025Ej8.Domain
{
    public class CuentaCorriente : CuentaBancaria
    {
        public decimal LimiteDeDescubierto { get; set; }
        public decimal Comision {  get; set; }

        // Se elimina del constructor la propiedad limiteDeDescubierto y comision, ya que no se deben inicializar en el constructor 
        // (punto 8)
        public CuentaCorriente(string numero, decimal saldo, string[] titulares) 
            : base(numero, saldo, TipoCuenta.CuentaCorriente, titulares)
        {
            // LimiteDeDescubierto = limiteDeDescubierto;
            // Comision = comision;
        }

        public override void Depositar(decimal monto)
        {
            ValidarOperacion(monto);
            var montoFinal = monto - (monto * Comision);
            Saldo += montoFinal;
        } 

        public override void Retirar(decimal monto)
        {
            ValidarOperacion(monto);
            decimal nuevoSaldo = Saldo - monto;

            if (nuevoSaldo < -LimiteDeDescubierto)
            {
                //Saldo = nuevoSaldo;
                Estado = Estado.Suspendida;
                throw new SaldoInsuficienteException();
            }

            Saldo = nuevoSaldo;

            if (Saldo < 0)
            {
                Estado = Estado.Suspendida;
            }
        }
    }
}
