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
    }
}
