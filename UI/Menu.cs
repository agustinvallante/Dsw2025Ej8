using Dsw2025Ej8.Domain;
using static Dsw2025Ej8.Domain.Exceptions.Exceptions;

namespace Dsw2025Ej8.UI;

public static class Menu
{
    private static readonly List<CuentaBancaria> cuentas = [];

    // Metodo usado para mostrar el menu en la consola
    public static void Show()
    {
        while (true)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;


            Console.WriteLine("====================================");
            Console.WriteLine("     === SISTEMA BANCARIO ===   ");
            Console.WriteLine("====================================\n");

            Console.ResetColor();

            Console.WriteLine("Menú principal:\n");

            Console.WriteLine("  1. Crear Cuenta");
            Console.WriteLine("  2. Depositar");
            Console.WriteLine("  3. Retirar");
            Console.WriteLine("  4. Aplicar Interés");
            Console.WriteLine("  5. Mostrar Resumen de Cuentas");
            Console.WriteLine("  6. Salir\n");


            switch (Console.ReadLine())
            {
                case "1":
                    CrearCuenta();
                    break;
                case "2":
                    Depositar();
                    break;
                case "3":
                    Retirar();
                    break;
                case "4":
                    AplicarInteres();
                    break;
                case "5":
                    MostrarResumen();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Opción inválida. Presione una tecla para continuar...");
                    Console.ReadKey();
                    break;
            }
        }
    }
    private static void CrearCuenta()
    {

        Console.Clear();
        Console.WriteLine("=== Crear Cuenta ===");

        string numero;
        decimal saldo;
        int tipo;
        string[] titulares;

        try
        {
            // Se pide ingresar el número de cuenta
            Console.Write("Ingrese el número de cuenta: ");
            numero = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(numero))
            {
                throw new ArgumentException("El número de cuenta no puede estar vacío.");
            }

            // Se pide ingresar el saldo inicial
            Console.Write("Ingrese el saldo inicial: ");
            if (!decimal.TryParse(Console.ReadLine(), out saldo) || saldo <= 0)
            {
                throw new MontoNoValidoException();
            }

            // Se pide seleccionar el tipo de cuenta
            Console.WriteLine("Seleccione el tipo de cuenta:");
            Console.WriteLine("1. Caja de Ahorro");
            Console.WriteLine("2. Cuenta Corriente");
            if (!int.TryParse(Console.ReadLine(), out tipo) || (tipo != 1 && tipo != 2))
            {
                throw new ArgumentException("Tipo de cuenta inválido.");
            }

            // Se pide ingresar los titulares
            Console.Write("Ingrese los titulares (separados por coma): ");
            titulares = (Console.ReadLine() ?? string.Empty).Split(',');

            if (titulares.Length == 0 || titulares.Any(t => string.IsNullOrWhiteSpace(t)))
            {
                throw new ArgumentException("Debe ingresar al menos un titular válido.");
            }

            // Se crea una instancia de la cuenta bancaria según el tipo seleccionado
            CuentaBancaria cuenta;
            if (tipo == 1) 
            {
                // Caja de Ahorro

                cuenta = new CajaDeAhorro(numero, saldo, titulares);

                Console.Write("Ingrese la tasa de interés para la Caja de Ahorro: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal tasaDeInteres) || tasaDeInteres <= 0)
                {
                    throw new MontoNoValidoException();
                }

                // Se asigna la tasa de interés a la cuenta usando la propiedad publica y no desde el constructor
                // (punto 7 del enuciado)
                ((CajaDeAhorro)cuenta).TasaDeInteres = tasaDeInteres;
            }
            else 
            {
                // Cuenta Corriente
                Console.Write("Ingrese el límite de descubierto para la Cuenta Corriente: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal limiteDeDescubierto) || limiteDeDescubierto < 0)
                {
                    throw new MontoNoValidoException();
                }

                Console.Write("Ingrese la comisión para la Cuenta Corriente (en porcentaje, ej. 0.05 para 5%): ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal comision) || comision < 0 || comision > 1)
                {
                    throw new ArgumentException("La comisión debe estar entre 0 y 1.");
                }

                cuenta = new CuentaCorriente(numero, saldo, titulares);

                // Luego de crear la cuenta corriente, se asignan los valores de limite de descubierto y comision
                // de esta manera se evita que el constructor de la cuenta corriente reciba estos valores
                ((CuentaCorriente)cuenta).LimiteDeDescubierto = limiteDeDescubierto;
                ((CuentaCorriente)cuenta).Comision = comision;
            }

            // Se agrega la cuenta a la lista de cuentas
            cuentas.Add(cuenta);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Cuenta creada exitosamente.");
            Console.ResetColor();
        }
        catch (MontoNoValidoException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
        catch (ArgumentException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Ocurrió un error inesperado: {ex.Message}");
            Console.ResetColor();
        }
        finally
        {
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }
    }

    //Resumen de cuentas
    private static void MostrarResumen()
    {
        Console.Clear();
        Console.WriteLine("=== Resumen de Cuentas ===\n");

        if (cuentas.Count == 0)
        {
            Console.WriteLine("No hay cuentas registradas.");
        }
        else
        {
            var resumenes = cuentas.Select(cuenta => new
            {
                Numero = cuenta.Numero,
                Tipo = cuenta.Tipo,
                Saldo = cuenta.Saldo
            });

            foreach (var resumen in resumenes)
            {
                Console.WriteLine($"Número: {resumen.Numero}");
                Console.WriteLine($"Tipo: {resumen.Tipo}");
                Console.WriteLine($"Saldo: ${resumen.Saldo:F2}\n");
            }
        }

        Console.WriteLine("Presione una tecla para continuar...");
        Console.ReadKey();
    }

    private static void AplicarInteres()
    {
        Console.Clear();
        Console.WriteLine("=== Aplicar Interés ===\n");

        if (cuentas.Count == 0)
        {
            Console.WriteLine("No hay cuentas registradas.");
        }
        else
        {
            foreach (var cuenta in cuentas)
            {
                try
                {
                    cuenta.AplicarInteres();
                    Console.WriteLine($"Interés aplicado a la cuenta {cuenta.Numero}. Nuevo saldo: ${cuenta.Saldo:F2}");
                }
                catch (CuentaNoActivaException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error en la cuenta {cuenta.Numero}: {ex.Message}");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error inesperado en la cuenta {cuenta.Numero}: {ex.Message}");
                    Console.ResetColor();
                }
            }
        }

        Console.WriteLine("\nPresione una tecla para continuar...");
        Console.ReadKey();
    }

    private static void Retirar()
    {
        Console.Clear();
        Console.WriteLine("=== Retirar ===\n");

        try
        {
            Console.Write("Ingrese el número de cuenta: ");
            string numeroCuenta = Console.ReadLine();

            var cuenta = cuentas.FirstOrDefault(c => c.Numero == numeroCuenta);
            if (cuenta == null)
            {
                Console.WriteLine("Cuenta no encontrada.");
                return;
            }

            Console.Write("Ingrese el monto a retirar: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal monto) || monto <= 0)
            {
                throw new MontoNoValidoException();
            }

            cuenta.Retirar(monto);
            Console.WriteLine($"Retiro realizado con éxito. Nuevo saldo: ${cuenta.Saldo:F2}");
        }
        catch (SaldoInsuficienteException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
        }
        finally
        {
            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }
    }

    private static void Depositar()
    {
        Console.Clear();
        Console.WriteLine("=== Depositar ===\n");

        try
        {
            Console.Write("Ingrese el número de cuenta: ");
            string numeroCuenta = Console.ReadLine();

            var cuenta = cuentas.FirstOrDefault(c => c.Numero == numeroCuenta);
            if (cuenta == null)
            {
                Console.WriteLine("Cuenta no encontrada.");
                return;
            }

            Console.Write("Ingrese el monto a depositar: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal monto) || monto <= 0)
            {
                throw new MontoNoValidoException();
            }

            cuenta.Depositar(monto);
            Console.WriteLine($"Depósito realizado con éxito. Nuevo saldo: ${cuenta.Saldo:F2}");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
        }
        finally
        {
            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }
    }

}