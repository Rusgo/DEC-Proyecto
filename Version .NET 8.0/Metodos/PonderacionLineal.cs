﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTp.Metodos
{
    public class PonderacionLineal : MultiCriterio
    {
        public PonderacionLineal(float[,] matriz, List<float> pesos, List<bool> max, bool metodo) : base(matriz, pesos, max, metodo)
        {
        }
        public override void normalizar(int filas, int columnas)
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
                    matriz[i, j] = res;
                }

                // Almacenar la suma en el array
                sumaColumnas[j] = (this.metodo) ? suma : (float)Math.Sqrt(suma);
            }

            //normalizar
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    matrizNormalizada[i, j] = (matriz[i, j] / sumaColumnas[j]);
                }
            }
        }
        public override float verificar(float a, bool b)
        {
            if (b)
            {
                return a;
            }
            else if (a == 0)
            {
                return 0;
            }
            return (1 / a);
        }
    }
        

        
}
