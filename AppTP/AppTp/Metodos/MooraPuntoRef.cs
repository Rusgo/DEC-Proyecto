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
    }
}
