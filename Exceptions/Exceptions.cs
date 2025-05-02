using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Ej8.Domain.Exceptions
{
    internal class Exceptions
    {
        public class MontoNoValidoException : Exception
        {
            public MontoNoValidoException()
                : base("El monto ingresado no es válido para la operación solicitada.") { }
        }

        public class CuentaNoActivaException : Exception
        {
            public CuentaNoActivaException(Estado estado)
                : base($"No se puede operar con la cuenta {estado}.") { }
        }

        public class SaldoInsuficienteException : Exception
        {
            public SaldoInsuficienteException()
                : base("La cuenta no cuenta con saldo para la operación solicitada. Fue suspendida.") { }
        }
    }
}