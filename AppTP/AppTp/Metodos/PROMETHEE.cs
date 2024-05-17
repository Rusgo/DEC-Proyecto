﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AppTp.Metodos
{
    public class PROMETHEE : MultiCriterio
    {
        public List<float[,]> tablasPrimeraParte = new List<float[,]>();
        public List<float[,]> tablasSegundaParte = new List<float[,]>();
        public List<float[,]> matricesPonderadas = new List<float[,]>();
        public float[] flujoPositivo {  get; set; }
        public float[] flujoNegativo { get; set; }

        public PROMETHEE(float[,] matriz, List<float> pesos, List<bool> max, bool metodo) : base(matriz, pesos, max, metodo)
        {

        }
        public float[,] tabla_criterio(List<float> alternativas, int col)
        {
            float[,] matrizDiferencias = new float[alternativas.Count, alternativas.Count];
            
            for(int j = 0; j < alternativas.Count; j++)
            {
                for (int i = 0; i < alternativas.Count; i++)
                {
                    if (i == j)
                    {
                        matrizDiferencias[i, j] = 0;
                    }
                    else
                    {
                        if (this.max[col])
                        {
                            float diferencia = alternativas[j] - alternativas[i];
                            if (diferencia >= 0)
                            {
                                matrizDiferencias[j, i] = diferencia;
                            }
                        }
                        else
                        {
                            float diferencia = alternativas[i] - alternativas[j];
                            if (diferencia >= 0)
                            {
                                matrizDiferencias[j, i] = diferencia;
                            }
                        }


                    }
                        
                }
            }
            
            return matrizDiferencias;
        }
        public float[,] tabla_Funciones(Entidades.Funcion funcion, int cantidad, float[,] diferencias)
        {
            float[,] matrizDiferencias = new float[cantidad, cantidad];

            for (int i = 0; i < cantidad; i++)
            {
                for (int j = 0; j < cantidad; j++)
                {
                        matrizDiferencias[i, j] = funcion.resolver(diferencias[i, j]);
                }
            }

            return matrizDiferencias;
        }

        public List<float[,]> ponderarTablas(List<float[,]> matrices, List<float> peso)
        {
            List<float[,]> matricesPonderadasNueva = new List<float[,]>();
            int cont = 0;

            foreach (float[,] matriz in matrices)
            {
                
                int filas = matriz.GetLength(0);
                int columnas = matriz.GetLength(1);

                float[,] matrizNueva = new float[filas, columnas];

                for (int i = 0; i < filas; i++)
                {
                    for(int j = 0; j < columnas; j++)
                    {
                        matrizNueva[i, j] = (pesos[cont] * matriz[i, j]);
                    }
                }
                matricesPonderadasNueva.Add(matrizNueva);

                cont++;
            }

            return matricesPonderadasNueva;
        }

        static float[,] SumMatrices(List<float[,]> matrices)
        {
            int rows = matrices[0].GetLength(0);
            int cols = matrices[0].GetLength(1);

            // Crear una nueva matriz para almacenar el resultado
            float[,] result = new float[rows, cols];

            // Iterar a través de la lista de matrices y sumar los elementos correspondientes
            foreach (var matrix in matrices)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        result[i, j] += matrix[i, j];
                    }
                }
            }

            return result;
        }

        public float[] sumaFilas(float[,] matriz)
        {
            float[] filas = new float[matriz.GetLength(0)];
            for(int i = 0;i < matriz.GetLength(0); i++)
            {
                float acu = 0;
                for(int j = 0;j < matriz.GetLength(1); j++)
                {
                    acu += matriz[i,j];
                }
                filas[i] = acu;
            }
            return filas;
        }

        public float[] sumaColumnas(float[,] matriz)
        {
            float[] columnas = new float[matriz.GetLength(0)];
            for (int j = 0; j < matriz.GetLength(1); j++)
            {
                float acu = 0;
                for (int i = 0; i < matriz.GetLength(1); i++)
                {
                    acu += matriz[i, j];
                }
                columnas[j] = acu;
            }
            return columnas;
        }

        public float[] diferencias()
        {
            float[] res = new float[this.flujoPositivo.GetLength(0)];
            for(int i = 0; i < this.flujoPositivo.GetLength(0); i++)
            {
                res[i] = this.flujoPositivo[i] - this.flujoNegativo[i];
            }
            return res;
        }


        public void resolver(List<Entidades.Funcion> funciones)
        {
            int rows = matriz.GetLength(0);
            int cont = 0;
            foreach (Entidades.Funcion func in funciones)
            {
                
                List<List<float>> listacolumna = new List<List<float>>();
                for (int i = 0;i < funciones.Count;i++)
                {
                    List<float> columna = new List<float>();
                    for (int j = 0; j < rows; j++)
                    {
                        columna.Add(matriz[j, i]);
                    }
                    listacolumna.Add(columna);
                }
                float[,] matrizDiferencia = tabla_criterio(listacolumna[cont], cont);
                tablasPrimeraParte.Add(matrizDiferencia);
                tablasSegundaParte.Add(tabla_Funciones(func, rows, matrizDiferencia));
                cont++;
                

            }
            
            matricesPonderadas = ponderarTablas(tablasSegundaParte, pesos);
            this.matrizPonderada = SumMatrices(matricesPonderadas);//menter tablas ponderadas
            this.flujoPositivo = sumaFilas(this.matrizPonderada);
            this.flujoNegativo = sumaColumnas(this.matrizPonderada);
            this.resultado = diferencias();
            
        }
    }
}
