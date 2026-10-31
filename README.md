## Integrantes y Responsabilidades
- *Jhonathan Martínez Gómez*: Arquitectura MVC, implementación de ArbolBinario.cs, lógica CRUD
   e  impresión ASCII.
- *Johnatan Andres Sierra Alvarez*: Implementación de DocumentoController.cs, 
    diseño de casos de prueba y búsquedas.
- *Brisney Nicolle Saldarriaga Aguirre*: Implementación de ArbolView.cs, 
    métricas de rendimiento y validaciones de duplicados.

## Requisitos de Ejecución
- *Plataforma / Framework*: .NET SDK 6.0 / 7.0 / 8.0 o superior.
- *IDE Recomendado*: Visual Studio 2022 o VS Code.

## Arquitectura

- **Models:** Nodo.cs y ArbolBinario.cs.
- **Views:** ArbolView.cs.
- **Controllers:** DocumentoController.cs.
- **Program.cs:** punto de entrada que invoca al controlador.

### Pasos para Ejecutar
1. Clonar el repositorio:
   ```bash
   git clone https://github.com/SierraJ93/DocuTrackSA.git
   cd DocuTrack

Desde la carpeta :  ProyectoDocuTrack:



```bash
dotnet restore
dotnet build
dotnet run
```




## Justificación del Recorrido Ordenado 
El recorrido Inorden (Izquierda - Raíz - Derecha) es el único que confirma visual y formalmente que las propiedades del Binary Search Tree (BST) se mantienen. 
Dado que la clave de ordenamiento es alfabética ordinal e insensible a mayúsculas/minúsculas, el recorrido inorden produce la secuencia de elementos estrictamente ordenada en forma ascendente ($A \rightarrow Z$).

----------------------------------------------------------////--------------//
