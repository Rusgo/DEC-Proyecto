using AppTp.Metodos;
using CommunityToolkit.Maui.Storage;
using DocumentFormat.OpenXml.Office2013.Drawing.Chart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AppTp.Metodos
{
    public class MultiCriterio
    {
        public float[,] matriz { get; set; }
        public float[,] matrizNormalizada { get; set; }
        public float[,] matrizPonderada { get; set; }
        public List<float> pesos { get; set; }
        public float[] resultado { get; set; }
        public float[] sumaFinal { get; set; }
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
            this.sumaFinal = sumaColumnas;

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
        //Agrega A1A2A3
        public string[,] formatoExcel(float[,] matriz)
        {
            int filasTotales = matriz.GetLength(0);
            int columnasTotales = matriz.GetLength(1);

            string[,] matrizString = new string[matriz.GetLength(0) , matriz.GetLength(1)];
            int filaAgregada = 0;
            int coluAgregada = 0;
            for (int i = 0; i < filasTotales; i++)
            {
                for(int j = 0;j < columnasTotales; j++)
                {
                    if(j < matriz.GetLength(1) && i < matriz.GetLength(0))
                    {
                        matrizString[i, j] = matriz[i, j].ToString();
                    }
                    
                }
                if (i < matriz.GetLength(0))
                {
                    filaAgregada++;
                }
            }
            filaAgregada = 0;
            coluAgregada = 0;
            string[,] matrizFormatoExcel = new string[filasTotales + 1, columnasTotales + 1];
            for (int i = 0; i < filasTotales + 1; i++)
            {
                for (int j = 0; j < columnasTotales + 1; j++)
                {
                    //Agrego col de alter
                    if (j == 0 && i< matriz.GetLength(0))
                    {
                        matrizFormatoExcel[i+1, j] = "A" + (filaAgregada + 1).ToString();
                    }
                    else if (j > 0 && i > 0 && j <= matriz.GetLength(1) && i <= matriz.GetLength(0))
                    {
                        matrizFormatoExcel[i, j] = matriz[i-1, j-1].ToString();
                    }
                    //Agrego fila de criterios
                    if (i == 0 && j < matrizString.GetLength(1))
                    {
                        matrizFormatoExcel[i, j + 1] = "C" + (coluAgregada + 1).ToString();
                        coluAgregada++;
                    }
                }
                if (i < matriz.GetLength(0))
                {
                    filaAgregada++;
                }
            }

            return matrizFormatoExcel;
        }
        //AGREGAR FILA A MATRIZ
        public string[,] Agregarfila(string[,] matriz, List<float[]> agregados, List<string> letras)
        {
            int filasTotales = agregados.Count;

            string[,] matrizFormatoExcel = new string[matriz.GetLength(0) + filasTotales, matriz.GetLength(1)];
            int filaAgregada = 0;
            for (int i = 0; i < matrizFormatoExcel.GetLength(0); i++)
            {
                for (int j = 0; j < matrizFormatoExcel.GetLength(1); j++)
                {
                    if (j < matriz.GetLength(1) && i < matriz.GetLength(0))
                    {
                        matrizFormatoExcel[i, j] = matriz[i, j];
                    }
                    if(j==0 && i>= matriz.GetLength(0))
                    {
                        matrizFormatoExcel[i, j] = letras[filaAgregada];
                    }
                    else if (i >= matriz.GetLength(0) && j > 0)
                    {
                        matrizFormatoExcel[i, j] = agregados[filaAgregada][j - 1].ToString();
                    }

                }
                if (i >= matriz.GetLength(0))
                {
                    filaAgregada++;
                }
            }
            return matrizFormatoExcel;
        }
        //AGREGAR columna A MATRIZ
        public string[,] AgregarColumna(string[,] matriz, List<float[]> agregados, List<string> letras)
        {
            int filasTotales = agregados.Count;

            string[,] matrizFormatoExcel = new string[matriz.GetLength(0), matriz.GetLength(1) + filasTotales];
            int filaAgregada = 0;
            for (int i = 0; i < matrizFormatoExcel.GetLength(0); i++)
            {
                for (int j = 0; j < matrizFormatoExcel.GetLength(1); j++)
                {
                    if (j < matriz.GetLength(1) && i < matriz.GetLength(0))
                    {
                        matrizFormatoExcel[i, j] = matriz[i, j];
                    }
                    if (j >= matriz.GetLength(1) && i==0)
                    {
                        matrizFormatoExcel[i, j] = letras[i];
                    }
                    else if (j >= matriz.GetLength(1) && i > 0)
                    {
                        matrizFormatoExcel[i, j] = agregados[filaAgregada][i - 1].ToString();
                    }

                }
            }
            return matrizFormatoExcel;
        }
        public virtual async void guardarExcel()
        {
            string fileNameExport = "";
            List<string> lista = new List<string>
            {
                "Paso1",
                "Paso2",
                "Paso3",
                "Paso4"
            };
            List<string> listaLetras = new List<string>
            {
                "Suma",
                "Pesos"
            };
            List<string> Lresul = new List<string>
            {
                "A(X)?"
            };
            float[] res = new float[this.resultado.Count()];
            int cont = 0;
            foreach (float f in resultado)
            {
                res[cont] = f;
                cont++;
            }
            float[] peso = new float[this.pesos.Count()];
            cont = 0;
            foreach (float f in resultado)
            {
                peso[cont] = f;
                cont++;
            }
            List<float[]> matrizSumyPeso = new List<float[]>
            {
                this.sumaFinal,
                peso
                
            };
            List<float[]> agrega = new List<float[]>
            {
                res

            };
            List<float[]> lista2 = new List<float[]>();
            List<string[,]> matrices = new List<string[,]>
            {
                Agregarfila(formatoExcel(this.matriz), matrizSumyPeso, listaLetras),
                formatoExcel(this.matrizNormalizada),
                formatoExcel(this.matrizPonderada),
                AgregarColumna(formatoExcel(this.matrizPonderada), agrega, Lresul)
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
                e.ExportToExcel(lista, matrices, FilePath);
            }

                
        }
    }
}
