using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Dsw2025Ej8.Domain.Exceptions.Exceptions;

namespace Dsw2025Ej8.Domain
{
    public class CajaDeAhorro : CuentaBancaria
    {
        public decimal TasaDeInteres { get; set; }
        // Se elimina del constructor la propiedad TasaDeInteres, ya que no se debe inicializar en el constructor
        // segun el enunciado
        public CajaDeAhorro(string numero, decimal saldo, string[] titulares) 
            : base (numero, saldo, TipoCuenta.CajaDeAhorro, titulares)
        {
            
            // TasaDeInteres = tasaDeInteres;
        }

        public override void Depositar(decimal monto)
        {
            //SetSaldo(GetSaldo() + monto);
            ValidarOperacion(monto);
            Saldo += monto;
        }

        public override void Retirar (decimal monto)
        {
            ValidarOperacion (monto);
            if (Saldo < monto)
            {
                Estado = Estado.Inactiva;
                throw new SaldoInsuficienteException();
            }
            Saldo -= monto;
        }

        public override void AplicarInteres()
        {
            //SetSaldo(GetSaldo() * _tasaDeInteres);
            if (Estado != Estado.Activa) throw new CuentaNoActivaException(Estado);
            Saldo *= TasaDeInteres;
        }
    }
}
