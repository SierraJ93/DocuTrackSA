using System;
using System.Collections.Generic;



namespace DocuTrack.Models 
{
    public class ArbolBinario // Clase que representa un árbol binario de búsqueda
    {
        public Nodo Raiz {get; private set;}

        public ArbolBinario()
        {
            Raiz = null;
        }

        //  Método para insertar un nuevo nodo en el árbol binario de búsqueda
        public bool Insertar(string nombre, bool esCarpeta, out int comparaciones)
        {
            comparaciones = 0;
            if (string.IsNullOrWhiteSpace(nombre))
                return false;

            if (Raiz == null)
            {
                Raiz = new Nodo(nombre, esCarpeta);
                return true;
            }

            return InsertarRecursivo(Raiz, nombre, esCarpeta, ref comparaciones);
        }

        private bool InsertarRecursivo(Nodo actual, string nombre, bool esCarpeta, ref int comparaciones)
        {
            comparaciones++;
            int comp = string.Compare(nombre, actual.Nombre, StringComparison.OrdinalIgnoreCase);

            if (comp == 0)
                return false; // Rechazar duplicado

            if (comp < 0)
            {
                if (actual.Izquierdo == null)
                {
                    actual.Izquierdo = new Nodo(nombre, esCarpeta);
                    return true;
                }
                return InsertarRecursivo(actual.Izquierdo, nombre, esCarpeta, ref comparaciones);
            }
            else
            {
                if (actual.Derecho == null)
                {
                    actual.Derecho = new Nodo(nombre, esCarpeta);
                    return true;
                }
                return InsertarRecursivo(actual.Derecho, nombre, esCarpeta, ref comparaciones);
            }
        }

        public Nodo Buscar(string nombre, out int comparaciones)
        {
            comparaciones = 0;
            return BuscarRecursivo(Raiz, nombre, ref comparaciones);
        }


    // Método recursivo para buscar un nodo en el árbol binario de búsqueda
        private Nodo BuscarRecursivo(Nodo actual, string nombre, ref int comparaciones)
        {
            if (actual == null)
                return null;

            comparaciones++;
            int comp = string.Compare(nombre, actual.Nombre, StringComparison.OrdinalIgnoreCase);

            if (comp == 0)
                return actual;
            if (comp < 0)
                return BuscarRecursivo(actual.Izquierdo, nombre, ref comparaciones);

            return BuscarRecursivo(actual.Derecho, nombre, ref comparaciones);
        }

// Método para eliminar un nodo del árbol binario de búsqueda
        public bool Eliminar(string nombre, out string tipoCaso)
        {
            tipoCaso = "No encontrado";
            int comp = 0;
            if (Buscar(nombre, out comp) == null)
                return false;

            bool eliminado = false;
            string casoDetectado = string.Empty;
            Raiz = EliminarRecursivo(Raiz, nombre, ref eliminado, ref casoDetectado);
            tipoCaso = casoDetectado;
            return eliminado;
        }

        private Nodo EliminarRecursivo(Nodo actual, string nombre, ref bool eliminado, ref string tipoCaso)
        {
            if (actual == null)
                return null;

            int comp = string.Compare(nombre, actual.Nombre, StringComparison.OrdinalIgnoreCase);

            if (comp < 0)
            {
                actual.Izquierdo = EliminarRecursivo(actual.Izquierdo, nombre, ref eliminado, ref tipoCaso);
            }
            else if (comp > 0)
            {
                actual.Derecho = EliminarRecursivo(actual.Derecho, nombre, ref eliminado, ref tipoCaso);
            }
            else
            {
                eliminado = true;

                // Caso 1: Hoja
                if (actual.Izquierdo == null && actual.Derecho == null)
                {
                    tipoCaso = "Nodo Hoja (sin hijos)";
                    return null;
                }

                // Caso 2: Un solo hijo
                if (actual.Izquierdo == null)
                {
                    tipoCaso = "Nodo con un hijo (derecho)";
                    return actual.Derecho;
                }
                if (actual.Derecho == null)
                {
                    tipoCaso = "Nodo con un hijo (izquierdo)";
                    return actual.Izquierdo;
                }

                // Caso 3: Dos hijos (Estrategia: Sucesor Inorden -> Mínimo del subárbol derecho)
                tipoCaso = "Nodo con dos hijos (reemplazado por sucesor inorden)";
                Nodo sucesor = ObtenerMinimo(actual.Derecho);
                actual.Nombre = sucesor.Nombre;
                actual.EsCarpeta = sucesor.EsCarpeta;

                string subCaso = string.Empty;
                actual.Derecho = EliminarRecursivo(actual.Derecho, sucesor.Nombre, ref eliminado, ref subCaso);
            }

            return actual;
        }

        private Nodo ObtenerMinimo(Nodo nodo)
        {
            Nodo actual = nodo;
            while (actual.Izquierdo != null)
                actual = actual.Izquierdo;
            return actual;
        }

        // Método para actualizar un nodo en el árbol binario de búsqueda
        public bool Actualizar(string nombreAntiguo, string nombreNuevo, bool esCarpetaNuevo, out string logAccion)
        {
            logAccion = string.Empty;

            int compPrueba;
            if (Buscar(nombreAntiguo, out compPrueba) == null)
            {
                logAccion = $"El nodo '{nombreAntiguo}' no existe para ser actualizado.";
                return false;
            }

            if (string.Compare(nombreAntiguo, nombreNuevo, StringComparison.OrdinalIgnoreCase) != 0 &&
                Buscar(nombreNuevo, out compPrueba) != null)
            {
                logAccion = $"No se puede actualizar a '{nombreNuevo}' porque ya existe en el árbol.";
                return false;
            }

            // Estrategia obligatoria: Eliminar(antiguo) + Insertar(nuevo)
            Eliminar(nombreAntiguo, out string casoEliminacion);
            Insertar(nombreNuevo, esCarpetaNuevo, out int compInsert);

            logAccion = $"Eliminado '{nombreAntiguo}' ({casoEliminacion}) e Insertado '{nombreNuevo}' con {compInsert} comparaciones.";
            return true;
        }

        // Métodos de recorrido del árbol binario
        public List<string> ObtenerPreorden()
        {
            List<string> resultado = new List<string>();
            PreordenRecursivo(Raiz, resultado);
            return resultado;
        }

        private void PreordenRecursivo(Nodo nodo, List<string> resultado)
        {
            if (nodo == null) return;
            resultado.Add($"{nodo.Nombre}{(nodo.EsCarpeta ? " [D]" : " [F]")}");
            PreordenRecursivo(nodo.Izquierdo, resultado);
            PreordenRecursivo(nodo.Derecho, resultado);
        }

        public List<string> ObtenerInorden()
        {
            List<string> resultado = new List<string>();
            InordenRecursivo(Raiz, resultado);
            return resultado;
        }

        private void InordenRecursivo(Nodo nodo, List<string> resultado)
        {
            if (nodo == null) return;
            InordenRecursivo(nodo.Izquierdo, resultado);
            resultado.Add($"{nodo.Nombre}{(nodo.EsCarpeta ? " [D]" : " [F]")}");
            InordenRecursivo(nodo.Derecho, resultado);
        }

        public List<string> ObtenerPostorden()
        {
            List<string> resultado = new List<string>();
            PostordenRecursivo(Raiz, resultado);
            return resultado;
        }

        private void PostordenRecursivo(Nodo nodo, List<string> resultado)
        {
            if (nodo == null) return;
            PostordenRecursivo(nodo.Izquierdo, resultado);
            PostordenRecursivo(nodo.Derecho, resultado);
            resultado.Add($"{nodo.Nombre}{(nodo.EsCarpeta ? " [D]" : " [F]")}");
        }

        public List<string> ObtenerPorNiveles()
        {
            List<string> resultado = new List<string>();
            if (Raiz == null) return resultado;

            Queue<Nodo> cola = new Queue<Nodo>();
            cola.Enqueue(Raiz);

            while (cola.Count > 0)
            {
                Nodo actual = cola.Dequeue();
                resultado.Add($"{actual.Nombre}{(actual.EsCarpeta ? " [D]" : " [F]")}");

                if (actual.Izquierdo != null) cola.Enqueue(actual.Izquierdo);
                if (actual.Derecho != null) cola.Enqueue(actual.Derecho);
            }

            return resultado;
        }

    
        // Método para calcular la altura del árbol binario
        public int CalcularAltura()
        {
            return CalcularAlturaRecursivo(Raiz);
        }

        private int CalcularAlturaRecursivo(Nodo nodo)
        {
            if (nodo == null) return 0;
            int alturaIzq = CalcularAlturaRecursivo(nodo.Izquierdo);
            int alturaDer = CalcularAlturaRecursivo(nodo.Derecho);
            return Math.Max(alturaIzq, alturaDer) + 1;
        }
    }
}
