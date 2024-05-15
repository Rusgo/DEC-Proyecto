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
            
        }
    }
}
