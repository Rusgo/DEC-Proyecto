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

        public override async void guardarExcel()
        {
            string fileNameExport = "";
            List<string> lista = new List<string>
            {
                "Paso1",
                "Paso2",
                "Paso3",
                "Paso4"
            };
            float[,] res = new float[1, this.resultado.Count()];
            int cont = 0;
            foreach (float f in resultado)
            {
                res[0, cont] = f;
                cont++;
            }
            List<float[,]> matrices = new List<float[,]>
            {
                this.matriz,
                this.matrizNormalizada,
                this.matrizPonderada,
                res
            };
            Entidades.ExcelExporter e = new Entidades.ExcelExporter();

            var folder = await FolderPicker.PickAsync(default);
            while (folder == null)
            {
                folder = await FolderPicker.PickAsync(default);
            }

            if (folder != null)
            {
                var FilePath = Path.Combine(folder.Folder.Path, "archivo.xlsx");
                e.ExportToExcel(lista, matrices, FilePath);
            }
        }
    }
}
