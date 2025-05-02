using static Dsw2025Ej8.Domain.Exceptions.Exceptions;

namespace Dsw2025Ej8.Domain;

public abstract class CuentaBancaria
{
    public string Numero {  get; private set; }
    public decimal Saldo { get; protected set; }
    public Estado Estado { get; protected set; }
    public string[] Titulares { get; }
    public TipoCuenta Tipo { get; }

    public CuentaBancaria(string numero, decimal saldo, TipoCuenta tipo, string[] titulares)
    {
        if (saldo <= 0)
        {
            throw new MontoNoValidoException();
        }

        Numero = numero;
        Saldo = saldo;
        Estado = Estado.Activa;
        Titulares = titulares;
        Tipo = tipo;
    }

    protected void ValidarOperacion(decimal monto)
    {
        if (monto <= 0)
            throw new MontoNoValidoException();

        if (Estado != Estado.Activa)
            throw new CuentaNoActivaException(Estado);
    }

    public virtual void Depositar(decimal monto) { }
    public virtual void Retirar(decimal monto) { }
    public virtual void AplicarInteres() { }

}
