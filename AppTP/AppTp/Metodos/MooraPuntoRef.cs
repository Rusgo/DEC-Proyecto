using CommunityToolkit.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTp.Metodos
{
    public class MooraPuntoRef : MultiCriterio
    {
        public MooraPuntoRef(float[,] matriz, List<float> pesos, List<bool> max, bool metodo) : base(matriz, pesos, max,false)
        {
        }

        public float[,] MatrizPR { get; set; }
        public float distancia { get; set; }
        public float[] rj { get; set; }




        public override void agregacion(int filas, int columnas)
        {
            this.rj = new float[columnas];
            this.MatrizPR = new float[filas, columnas];

            for (int j = 0; j < columnas; j++)
            {
                rj[j] = matrizPonderada[0, j];
            }
            for (int j = 0; j < columnas; j++)
            {
                for (int i = 0; i < filas; i++)
                {
                    if (matrizPonderada[i, j] < rj[j] && !max[j])
                    {
                       rj[j] = matrizPonderada[i, j];
                    }
                    if (matrizPonderada[i, j] > rj[j] && max[j])
                    {
                        rj[j] = matrizPonderada[i, j];
                    }

                }
            }
            
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    if (max[j])
                    {
                        distancia = rj[j] - matrizPonderada[i, j];
                    }
                    else
                    {
                        distancia = matrizPonderada[i, j] - rj[j];
                    }
                    MatrizPR[i, j] = distancia;
                }
            }

            for (int i = 0; i < filas; i++)
            {
                float max = 0;
                for (int j = 0; j < columnas; j++)
                {
                    if (MatrizPR[i, j] > max)
                    {
                        max = MatrizPR[i, j];
                    }
                }
                resultado[i] = max;
            }

       





        }
        public virtual string[,] mejorAlternativaExcel()
        {
            List<string> Lresul = new List<string>
            {
                "S(Xi)"
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
                "rj",
            };

            List<float[]> matrizSumyPeso = new List<float[]>
            {
                this.rj
            };


            return Agregarfila(AgregarColumna(formatoExcel(this.matrizPonderada), agrega, Lresul), matrizSumyPeso, listaLetras);
        }
        public override string[,] agregacionExcel()
        {
            List<string> Lresul = new List<string>
            {
                "Distancia"
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
            return AgregarColumna(formatoExcel(this.MatrizPR), agrega, Lresul);
        }
        public override async void guardarExcel()
        {
            string fileNameExport = "";

            List<string> lista = new List<string>
            {
                "RaizDeSumatoria",
                "Normalizar",
                "Ponderar",
                "Sacar el mejor",
                "Agregacion"
            };
            List<float[]> lista2 = new List<float[]>();
            List<string[,]> matrices = new List<string[,]>
            {
                sinNormalizarExcel(),
                normalizarExcel(),
                ponderarExcel(),
                mejorAlternativaExcel(),
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
                "Sacamos la mejor alternativa de acuerdo a cada criterio",
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
