using System;
using DocuTrack.Controllers;

namespace DocuTrack
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DocumentoController controller = new DocumentoController();
            controller.EjecutarFlujoCompleto();

            Console.WriteLine("\nPresione cualquier tecla para finalizar la ejecución...");
            Console.ReadKey();
        }
    }
}