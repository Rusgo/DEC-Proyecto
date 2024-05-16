using CommunityToolkit.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTp.Metodos
{
    public class Topsis:MultiCriterio
    {
        public Topsis(float[,] matriz, List<float> pesos, List<bool> max, bool metodo) : base(matriz, pesos, max, metodo)
        {
        }

        public float[] SMas {  get; set; }
        public float[] SMenos { get; set; }
        public float[] AMas { get; set; }
        public float[] AMenos { get; set; }
        public float[,] matrizTopsisMas { get; set; }
        public float[,] matrizTopsisMenos { get; set; }
        public override void agregacion(int filas, int columnas)
        {
            this.AMas = new float[columnas];
            this.AMenos = new float[columnas];

            // Inicializar los mínimos y máximos con el primer elemento de cada columna
            for (int j = 0; j < columnas; j++)
            {
                AMenos[j] = matrizPonderada[0, j];
                AMas[j] = matrizPonderada[0, j];
            }
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    if (matrizPonderada[i, j] < AMenos[j] && max[j])
                    {
                        AMenos[j] = matrizPonderada[i, j];
                    }
                    else if(matrizPonderada[i, j] > AMenos[j] && !max[j])
                    {
                        AMenos[j] = matrizPonderada[i, j];
                    }

                    if (matrizPonderada[i, j] > AMas[j] && max[j])
                    {
                        AMas[j] = matrizPonderada[i, j];
                    }
                    else if (matrizPonderada[i, j] < AMas[j] && !max[j])
                    {
                        AMas[j] = matrizPonderada[i, j];
                    }

                }
            }
            //Con el ideal
            matrizTopsisMas = new float[filas, columnas];
            matrizTopsisMenos = new float[filas, columnas];
            for (int j = 0; j < columnas; j++)
            {
                for (int i = 0; i < filas; i++)
                {
                    //restamos al ideal el valor del elemento ij
                    matrizTopsisMas[i,j] = AMas[j] - matrizPonderada[i,j];
                }

               
            }
            //con el antiideal
            for (int j = 0; j < columnas; j++)
            {

                
                for (int i = 0; i < matriz.GetLength(0); i++)
                {
                    //restamos al ideal  elementasdassdasdaasd ya sabes el valor del elemento ij
                    matrizTopsisMenos[i, j] = matrizPonderada[i, j] - AMenos[j];
                }


            }
            SMas = new float[filas]; 
            SMenos = new float[filas]; 
            //sacamos resultados de S+ Y S-
            for (int i = 0; i < filas; i++)
            {
                SMas[i] = 0;
                SMenos[i] = 0;
                //acumulamos valores
                for (int j = 0; j < columnas; j++)
                {
                    SMas[i] += (float)Math.Pow(matrizTopsisMas[i, j], 2);
                    SMenos[i] += (float)Math.Pow(matrizTopsisMenos[i, j], 2);
                }
                SMas[i] = (float)Math.Sqrt(SMas[i]);
                SMenos[i] = (float)Math.Sqrt(SMenos[i]);
            }
            for (int i = 0; i < filas; i++)
            {
                resultado[i] = (SMenos[i]) / (SMas[i] + SMenos[i]);
            }
        }
        public virtual string[,] agregacionExcel()
        {
            List<string> Lresul = new List<string>
            {
                "U(X)"
            };
            float[] res = new float[this.resultado.Count()];
            int cont = 0;
            foreach (float f in resultado)
            {
                res[cont] = f;
                cont++;
            }
            cont = 0;

            List<float[]> agrega = new List<float[]>
            {
                res

            };

            List<string> listaLetras = new List<string>
            {
                "Raiz de Cuadrados",
                "Pesos"
            };
            if (this.metodo)
            {
                listaLetras = new List<string>
            {
                "Suma",
                "Pesos"
            };
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


            return Agregarfila(AgregarColumna(formatoExcel(this.matrizPonderada), agrega, Lresul), matrizSumyPeso, listaLetras);
        }
        public string[,] tablaSenExcel(int i)
        {
            List<string> Lresul1 = new List<string>
            {
                "S+"
            };
            List<string> Lresul2 = new List<string>
            {
                "S-"
            };
            

            List<float[]> agrega1 = new List<float[]>
            {
                this.SMas

            };

            List<float[]> agrega2 = new List<float[]>
            {
                this.SMenos

            };

            if (i == 1)
            {
                return AgregarColumna(formatoExcel(this.matrizPonderada), agrega1, Lresul1);
            }
                
            return AgregarColumna(formatoExcel(this.matrizPonderada), agrega2, Lresul2);
        }
        public override string[,] ponderarExcel()
        {
            List<string> listaLetras = new List<string>
            {
                "A+",
                "A-"
            };

            List<float[]> matrizSumyPeso = new List<float[]>
            {
                this.AMas,
                this.AMenos

            };


            return Agregarfila(formatoExcel(this.matrizPonderada), matrizSumyPeso, listaLetras);
        }
        public override async void guardarExcel()
        {
            string fileNameExport = "";

            List<string> lista = new List<string>
            {
                "RaizDeSumatoria",
                "Normalizar",
                "Ponderar",
                "Calcular S+",
                "Calcular S-",
                "Agregacion"
            };
            if (this.metodo)
            {
                lista = new List<string>
            {
                "Sumatoria",
                "Normalizar",
                "Ponderar",
                "Calcular S+",
                "Calcular S-",
                "Agregacion"
            };
            }

            List<float[]> lista2 = new List<float[]>();
            List<string[,]> matrices = new List<string[,]>
            {
                sinNormalizarExcel(),
                normalizarExcel(),
                ponderarExcel(),
                tablaSenExcel(1),
                tablaSenExcel(2),
                agregacionExcel()
            };
            Entidades.ExcelExporter e = new Entidades.ExcelExporter();

            var folder = await FolderPicker.PickAsync(default);
            while (folder == null)
            {
                folder = await FolderPicker.PickAsync(default);
            }
            List<string> textos = new List<string>
            {
                "Aplicamos el metodo seleccionado calculando el valor para cada colu blabla",
                "Normalizamos dividiendo a cada valor por su respectivo valor de columna bla blka",
                "Ponderamos la matriz con los pesos",
                "Calculamos la solucion ideal para cada alternativa",
                "Calculamos la solucion anti-ideal para cada alternativa",
                "Aplicamos la funcion de agregacion a cada una de las alternativas consideradas"
            };
            if (folder != null)
            {
                var FilePath = Path.Combine(folder.Folder.Path, "archivo.xlsx");
                e.ExportToExcel(lista, matrices, FilePath, textos);
            }


        }


    }
}
