using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTp.Metodos
{
    public class MooraPuntoRef : MultiCriterio
    {
        public MooraPuntoRef(float[,] matriz, List<float> pesos, List<bool> max) : base(matriz, pesos, max)
        {
        }

        float[,] MatrizPR { get; set; }
        float distancia { get; set; }
        
        
        

        public override void agregacion(int filas, int columnas)
        {
            List<float> rj = new List<float>();
            

            for (int j = 0; j < columnas; j++)
            {
                float mejor = 0;
                for (int i = 0; i < filas; i++)
                {
                    if (max[j] && matrizPonderada[i, j] > mejor)
                    {
                        mejor = matrizPonderada[i, j];
                    }
                    else if (!max[j] && mejor == 0)
                    {
                        mejor = matrizPonderada[i, j];
                    }
                    else if (!max[j] && mejor != 0 && matrizPonderada[i, j] < mejor)
                    {
                        mejor = matrizPonderada[i, j];
                    }
                }
                rj.Add(mejor);
            }
            
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    if (max[j])
                    {
                        float distancia = rj[j] - matrizPonderada[i, j];
                    }
                    else
                    {
                        float distancia = matrizPonderada[i, j] - rj[j];
                    }
                    MatrizPR[i, j] = distancia;
                }
            }

            for (int i = 0; i < filas; i++)
            {
                float max = 0;
                for (int j = 0; j < columnas; j++)
                {
                    if (matrizPonderada[i, j] > max)
                    {
                        max = matrizPonderada[i, j];
                    }
                }
                resultado[i] = max;
            }

            



        }
    }
}
