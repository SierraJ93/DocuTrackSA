using System;
using System.Collections.Generic;
using DocuTrack.Models;

namespace DocuTrack.Views // Clase que representa la vista del árbol binario
{
    public class ArbolView
    {
        public void MostrarEncabezado(string titulo)
        {
            Console.WriteLine();
            Console.WriteLine("==========================================================");
            Console.WriteLine($"  {titulo.ToUpper()}");
            Console.WriteLine("==========================================================");
        }

        public void MostrarMensaje(string mensaje)
        {
            Console.WriteLine($" [INFO] {mensaje}");
        }

        public void MostrarError(string error)
        {
            Console.WriteLine($" [RECHAZADO] {error}");
        }

        public void MostrarResultadoBusqueda(string nombre, bool hallado, int comparaciones, bool esCarpeta)
        {
            string estado = hallado ? $"HALLADO (Tipo: {(esCarpeta ? "Carpeta" : "Archivo")})" : "NO HALLADO";
            Console.WriteLine($"  > Búsqueda: '{nombre,-18}' | Estado: {estado,-30} | Comparaciones: {comparaciones}");
        }


    // Método para mostrar el diagrama estructural del árbol en formato ASCII
        public void MostrarArbolASCII(Nodo raiz)
        {
            Console.WriteLine("\n--- DIAGRAMA ESTRUCTURAL DEL ÁRBOL (ASCII) ---");
            if (raiz == null)
            {
                Console.WriteLine(" [Árbol Vacío]");
                return;
            }
            ImprimirNodoASCII(raiz, "", true);
            Console.WriteLine("-----------------------------------------\n");
        }

        private void ImprimirNodoASCII(Nodo nodo, string indent, bool esUltimo)
        {
            if (nodo == null) return;

            string conector = esUltimo ? "└── " : "├── ";
            string tipo = nodo.EsCarpeta ? "[D]" : "[F]";
            Console.WriteLine($"{indent}{conector}{nodo.Nombre} {tipo}");

            indent += esUltimo ? "    " : "│   ";

            List<Nodo> hijos = new List<Nodo>();
            if (nodo.Izquierdo != null) hijos.Add(nodo.Izquierdo);
            if (nodo.Derecho != null) hijos.Add(nodo.Derecho);

            for (int i = 0; i < hijos.Count; i++)
            {
                ImprimirNodoASCII(hijos[i], indent, i == hijos.Count - 1);
            }
        }

        public void MostrarRecorridos(List<string> pre, List<string> inord, List<string> post, List<string> niveles)
        {
            Console.WriteLine("\n--- RECORRIDOS DE VERIFICACIÓN ---");
            Console.WriteLine($" Preorden   : {string.Join(" -> ", pre)}");
            Console.WriteLine($" Inorden    : {string.Join(" -> ", inord)}");
            Console.WriteLine($" Postorden  : {string.Join(" -> ", post)}");
            Console.WriteLine($" Por Niveles: {string.Join(" -> ", niveles)}");
        }

        public void MostrarAltura(int altura)
        {
            Console.WriteLine($"\n  > Altura actual del árbol: {altura} nivel(es).");
        }
    }
}