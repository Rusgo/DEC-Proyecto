using AppTp.Metodos;
using CommunityToolkit.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AppTp.Metodos
{
    public class MultiCriterio
    {
        public float[,] matrizOriginal {  get; set; }
        public float[,] matriz { get; set; }
        public float[,] matrizNormalizada { get; set; }
        public float[,] matrizPonderada { get; set; }
        public List<float> pesos { get; set; }
        public float[] resultado { get; set; }
        public float[] sumaFinal {  get; set; }
        //verdadero es suma
        public bool metodo { get; set; }
        public List<bool> max { get; set; }
        public MultiCriterio(float[,] matriz, List<float> pesos, List<bool> max, bool metodo)
        {
            this.matriz = matriz;
            this.pesos = pesos;
            this.max = max;
            this.metodo = metodo;
        }
        public void resolver()
        {
            int columnas = matriz.GetLength(1);
            int filas = matriz.GetLength(0);
            this.matrizNormalizada = new float[filas, columnas];
            this.matrizPonderada = new float[filas, columnas];
            resultado = new float[filas];
            // Normalizar
            normalizar(filas, columnas);
            //Ponderar
            ponderar(filas, columnas);
            //Agregacion
            agregacion(filas, columnas);
        }
        public virtual void normalizar(int filas, int columnas)
        {
            float[] sumaColumnas = new float[columnas];
            // Iterar sobre cada columna
            for (int j = 0; j < columnas; j++)
            {
                float suma = 0;

                // Sumar los elementos de la columna actual
                for (int i = 0; i < matriz.GetLength(0); i++)
                {
                    float res = verificar((this.metodo) ? matriz[i, j] : (float)Math.Pow(matriz[i, j], 2), max[j]);
                    suma += res;
                }

                // Almacenar la suma en el array
                sumaColumnas[j] = (this.metodo) ? suma : (float)Math.Sqrt(suma);
            }
            sumaFinal = sumaColumnas;
            //normalizar
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    matrizNormalizada[i, j] = (matriz[i, j] / sumaColumnas[j]);
                }
            }
        }
        //este metodo tiene como finalidad definir si es columna de maximo y minimo para aplicar los cambios necesarios
        public virtual float verificar(float a, bool b)
        {
            return a;
        }
        //pondero cada de la matriz normalizada por su peso
        public virtual void ponderar(int filas, int columnas)
        {
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    matrizPonderada[i, j] = (matrizNormalizada[i, j] * pesos[j]);
                }
            }
        }
        //resuelve genericamente, se va a sobreescribir en los demas
        public virtual void agregacion(int filas, int columnas)
        {
            float acu = 0;
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    acu = acu + (matrizPonderada[i, j]);
                }
                resultado[i] = acu;
                acu = 0;
            }
        }
        public virtual float[,] matrizAgrandador(float[,] originalMatriz, List<float[]> rowsToAdd, List<float[]> columnsToAdd) //con este vamos a hacer que las matrices tengan 3 elementos de mas abajo y a la derecha
        {
            int originalRows = originalMatriz.GetLength(0);
            int originalCols = originalMatriz.GetLength(1);

            int newRows = originalRows + 3;

            float[,] newMatrix = new float[newRows, originalCols + 3];

            // Copiar los elementos de la matriz original a la nueva matriz
            for (int i = 0; i < originalRows; i++)
            {
                for (int j = 0; j < originalCols; j++)
                {
                    newMatrix[i, j] = originalMatriz[i, j];
                }
            }
            /*
            // Agregar las nuevas filas
            int rowIndex = originalRows;
            foreach (var row in rowsToAdd)
            {
                for (int j = 0; j < originalCols; j++)
                {
                    newMatrix[rowIndex, j] = row[j];
                }
                rowIndex++;
            }

            // Agregar las nuevas columnas
            int colIndex = originalCols;
            foreach (var col in columnsToAdd)
            {
                for (int i = 0; i < originalRows; i++)
                {
                    newMatrix[i, colIndex] = col[i];
                }
                colIndex++;
            }
            */
            return newMatrix;
        }
        public virtual async void guardarExcel()
        {
            float[] vacio = new float[pesos.Count];
            float[,] matrizConSuma = matrizAgrandador(matriz, new List<float[]> { sumaFinal,vacio,vacio}, new List<float[]> { vacio, vacio, vacio });
            float[,] matrizNormalizadaConPesos = matrizAgrandador(matrizNormalizada, new List<float[]> { sumaFinal, vacio, vacio }, new List<float[]> { vacio, vacio, vacio });
            float[,] matrizAgregacion = matrizAgrandador(matrizPonderada, new List<float[]> { sumaFinal, vacio, vacio }, new List<float[]> { resultado, vacio, vacio });

            string fileNameExport = "";
            List<string> lista = new List<string>
            {
                "Paso1",
                "Paso2",
                "Paso3",
                "Paso4",
                "Paso5"
            };

            List<string> teoria = new List<string>
            {
                "Mostramos la matriz orginal cargada en la aplicacion",
                "Lo primero que debemos hacer es asegurarnos de que todos los criterios esten en maximizar.",
                "Normalizamos la matriz por el metodo de la suma",
                "Ponderamos cada uno de los elemento de la matriz normalizada con sus respectivos pesos",
                "Realizamos la funcion de agregacion"
            };

            float[,] res = new float[1, this.resultado.Count()];
            int cont = 0;
            foreach (float f in resultado)
            {
                res[cont, 0] = f;
                cont++;
            }
            
            List<float[,]> matrices = new List<float[,]>
            {
                this.matrizOriginal,
                matrizConSuma,
                matrizNormalizadaConPesos,
                this.matrizPonderada,
                res
            };
            Entidades.ExcelExporter e = new Entidades.ExcelExporter();

            var folder = await FolderPicker.PickAsync(default);
            while (folder == null)
            {
                folder = await FolderPicker.PickAsync(default);
            }

            if(folder != null)
            {
                var FilePath = Path.Combine(folder.Folder.Path, "archivo.xlsx");
                e.ExportToExcel(lista, matrices, FilePath, teoria);
            }

                
        }
    }
}
