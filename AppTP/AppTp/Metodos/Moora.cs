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


            return AgregarColumna(formatoExcel(this.matrizPonderada), agrega, Lresul);
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
                "Aplicamos el metodo seleccionado calculando el valor para cada columna",
                "Normalizamos aplicando el metodo de la distancia euclidiana",
                "Ponderamos la matriz multiplicando cada valor de alternativa/criterios por su respectivo peso",
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
