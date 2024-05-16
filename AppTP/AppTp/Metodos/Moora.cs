using CommunityToolkit.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTp.Metodos
{
    public class Moora:MultiCriterio
    {
        public Moora(float[,] matriz, List<float> pesos, List<bool> max,bool metodo) : base(matriz, pesos, max, metodo)
        {
        }

        public override void agregacion(int filas, int columnas)
        {
            float acu = 0;
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    acu = acu + validar(matrizPonderada[i,j], max[j]);
                }
                resultado[i] = acu;
                acu = 0;
            }
        }
        public override float verificar(float a, bool b)
        {
            return a;
        }
        public float validar(float a, bool b)
        {
            if (b)
            {
                return a;
            }
            return (-a);
        }
        public virtual string[,] agregacionExcel()
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
        public override async void guardarExcel()
        {
            string fileNameExport = "";

            List<string> lista = new List<string>
            {
                "RaizDeSumatoria",
                "Normalizar",
                "Ponderar",
                "Agregacion"
            };
            

            List<float[]> lista2 = new List<float[]>();
            List<string[,]> matrices = new List<string[,]>
            {
                sinNormalizarExcel(),
                normalizarExcel(),
                ponderarExcel(),
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
                "Aplicamos la funcion de agregacion a cada una de las alternativas consideradas explicar we"
            };
            if (folder != null)
            {
                var FilePath = Path.Combine(folder.Folder.Path, "archivo.xlsx");
                e.ExportToExcel(lista, matrices, FilePath, textos);
            }
        }
    }
}
