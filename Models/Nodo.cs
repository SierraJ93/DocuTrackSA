// creamos la clase Nodo que representa un nodo en el árbol binario

namespace DocuTrack.Models{
    public class Nodo
    {
        public string Nombre { get; set; }
        public bool EsCarpeta { get; set; }
        public Nodo Izquierdo { get; set; }
        public Nodo Derecho { get; set; }

        public Nodo(string nombre, bool esCarpeta) 
        {
            Nombre = nombre;
            EsCarpeta = esCarpeta;
            Izquierdo = null;
            Derecho = null;
        }
    }
}
