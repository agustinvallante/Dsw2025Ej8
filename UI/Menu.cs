using Dsw2025Ej8.Domain;

namespace Dsw2025Ej8.UI;

public static class Menu
{
    private static readonly List<CuentaBancaria> cuentas = [];

    public static void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Sistema Bancario ===");
            Console.WriteLine("1. Crear Cuenta");
            Console.WriteLine("2. Depositar");
            Console.WriteLine("3. Retirar");
            Console.WriteLine("4. Aplicar Interés");
            Console.WriteLine("5. Mostrar Resumen de Cuentas");
            Console.WriteLine("6. Salir");
            Console.Write("Seleccione una opción: ");

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

    private static void MostrarResumen()
    {
        throw new NotImplementedException();
    }

    private static void AplicarInteres()
    {
        throw new NotImplementedException();
    }

    private static void Retirar()
    {
        throw new NotImplementedException();
    }

    private static void Depositar()
    {
        throw new NotImplementedException();
    }

    private static void CrearCuenta()
    {
        throw new NotImplementedException();
    }
}