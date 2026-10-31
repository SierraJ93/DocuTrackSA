using DocuTrack.Models;
using DocuTrack.Views;

namespace DocuTrack.Controllers
{
    public class DocumentoController
    {
        private readonly ArbolBinario _arbol;
        private readonly ArbolView _vista;

        public DocumentoController()
        {
            _arbol = new ArbolBinario();
            _vista = new ArbolView();
        }

        public void EjecutarFlujoCompleto()
        {
            Fase1_Construccion();
            Fase2_BusquedasRapidas();
            Fase3_ActualizacionesSelectivas();
            Fase4_EliminacionesSelectivas();
            Fase5_RecorridosYMetricas();
        }

// Fase 1: Construcción Inicial del Árbol
        private void Fase1_Construccion()
        {
            _vista.MostrarEncabezado("Fase 1: Construcción Inicial del Árbol (mínimo 14 elementos)");
            var elementos = new (string Nombre, bool EsCarpeta)[]
            {
                ("Mundo", true),         // Raíz
                ("Docs", true),          // Izquierda de Mundo
                ("Proyectos", true),     // Derecha de Mundo
                ("Archivos", true),      // Izquierda de Docs
                ("Facturas", true),      // Derecha de Docs
                ("Zapata.pdf", false),   // Derecha de Proyectos
                ("Aviso.txt", false),    // Izquierda de Archivos
                ("Config.sys", false),   // Izquierda de Facturas
                ("Manual.docx", false),  // Derecha de Facturas
                ("Reporte.pdf", false),  // Izquierda de Zapata.pdf
                ("Backend", true),       // Izquierda de Config.sys -> Hijo único
                ("Kratos.png", false),   // Derecha de Manual.docx -> Hijo único
                ("Mundo", true),         // Intentar Duplicado (Rechazo)
                ("Aviso.txt", false),    // Intentar Duplicado (Rechazo)
                ("Zeta.log", false),     // Derecha de Zapata.pdf
                ("Datos.csv", false)     // Derecha de Aviso.txt -> Completa 15 inserciones
            };

            foreach (var item in elementos)
            {
                bool insertado = _arbol.Insertar(item.Nombre, item.EsCarpeta, out int comp);
                if (insertado)
                {
                    _vista.MostrarMensaje($"Insertado: '{item.Nombre}' | Comparaciones: {comp}");
                }
                else
                {
                    _vista.MostrarError($"No se pudo insertar duplicado: '{item.Nombre}' | Comparaciones: {comp}");
                }
            }

            _vista.MostrarArbolASCII(_arbol.Raiz);
        }
 

 // Fase 2: Búsquedas Rápidas
        private void Fase2_BusquedasRapidas()
        {
            _vista.MostrarEncabezado("Fase 2: Búsquedas Rápidas (2 Izq, 2 Der, 2 Inexistentes)");

            string[] busquedas = new string[]
            {
                "Archivos",     // Subárbol Izquierdo
                "Config.sys",   // Subárbol Izquierdo
                "Proyectos",    // Subárbol Derecho
                "Zapata.pdf",   // Subárbol Derecho
                "Inventario",   // Inexistente
                "Nomina.xls"    // Inexistente
            };

            foreach (var nombre in busquedas)
            {
                Nodo resultado = _arbol.Buscar(nombre, out int comp);
                _vista.MostrarResultadoBusqueda(nombre, resultado != null, comp, resultado?.EsCarpeta ?? false);
            }
        }

        // Fase 3: Actualizaciones Selectivas Eliminar + Insertar
        private void Fase3_ActualizacionesSelectivas()
        {
            _vista.MostrarEncabezado("Fase 3: Actualizaciones Selectivas (Eliminar + Insertar)");

            //  Actualizar Hoja
            EjecutarActualizacion("Kratos.png", "Kratos_V2.png", false, "Actualización en Hoja");

            // Actualizar Hijo con un hijo
            EjecutarActualizacion("Manual.docx", "Guia_Usuario.docx", false, "Actualización en Hijo con un hijo");

            // Actualizar Raíz
            EjecutarActualizacion("Mundo", "RaizPrincipal", true, "Actualización en Raíz");
        }

    //ejecutar la actualizacion de un nodo en el arbol binario de busqueda
            private void EjecutarActualizacion(string antiguo, string nuevo, bool esCarpeta, string caso)
        {
            _vista.MostrarMensaje($"--- Realizando: {caso} ('{antiguo}' -> '{nuevo}') ---");
            bool exito = _arbol.Actualizar(antiguo, nuevo, esCarpeta, out string log);
            if (exito)
                _vista.MostrarMensaje(log);
            else
                _vista.MostrarError(log);

            _vista.MostrarArbolASCII(_arbol.Raiz);
        }

    // Fase 4: Eliminaciones Selectivas
        private void Fase4_EliminacionesSelectivas()
        {
            _vista.MostrarEncabezado("Fase 4: Eliminaciones Selectivas");

            // 1. Eliminar Hoja
            EjecutarEliminacion("Zeta.log", "Eliminar Hoja");

            // 2. Eliminar Hijo con un hijo
            EjecutarEliminacion("Config.sys", "Eliminar Hijo con un Hijo");

            // 3. Eliminar Raíz
            EjecutarEliminacion("RaizPrincipal", "Eliminar Raíz (Caso 2 hijos)");
        }

        private void EjecutarEliminacion(string nombre, string caso)
        {
            _vista.MostrarMensaje($"--- Realizando: {caso} ('{nombre}') ---");
            bool ok = _arbol.Eliminar(nombre, out string tipoCaso);
            if (ok)
                _vista.MostrarMensaje($"Nodo '{nombre}' eliminado exitosamente. Caso aplicado: {tipoCaso}");
            else
                _vista.MostrarError($"No se pudo eliminar el nodo '{nombre}'.");

            _vista.MostrarArbolASCII(_arbol.Raiz);
        }

        private void Fase5_RecorridosYMetricas()
        {
            _vista.MostrarEncabezado("Fase 5: Recorridos Finales de Verificación y Métricas");

            _vista.MostrarRecorridos(
                _arbol.ObtenerPreorden(),
                _arbol.ObtenerInorden(),
                _arbol.ObtenerPostorden(),
                _arbol.ObtenerPorNiveles()
            );

            _vista.MostrarAltura(_arbol.CalcularAltura());
        }
    }
}
