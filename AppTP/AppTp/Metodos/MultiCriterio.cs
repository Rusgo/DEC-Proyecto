using AppTp.Metodos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AppTp.Metodos
{
    public class MultiCriterio
    {
        public float[,] matriz { get; set; }
        public float[,] matrizNormalizada { get; set; }
        public float[,] matrizPonderada { get; set; }
        public List<float> pesos { get; set; }
        public float[] resultado { get; set; }
        //verdadero es suma
        public bool metodo { get; set; }
        public List<bool> max { get; set; }
        public MultiCriterio(float[,] matriz, List<float> pesos, List<bool> max, bool metodo)
        {
            this.matriz = matriz;
            this.pesos = pesos;
            this.max = max;
            this.metodo = metodo;
        }
        public void resolver()
        {
            int columnas = matriz.GetLength(1);
            int filas = matriz.GetLength(0); 
            this.matrizNormalizada = new float[filas, columnas];
            this.matrizPonderada = new float[filas, columnas];
            resultado = new float[filas];
            // Normalizar
            normalizar(filas, columnas);
            //Ponderar
            ponderar(filas, columnas);
            //Agregacion
            agregacion(filas, columnas);
        }
        public virtual void normalizar(int filas, int columnas)
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
        //este metodo tiene como finalidad definir si es columna de maximo y minimo para aplicar los cambios necesarios
        public virtual float verificar(float a, bool b)
        {
            return a;
        }
        //pondero cada de la matriz normalizada por su peso
        public virtual void ponderar(int filas, int columnas)
        {
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    matrizPonderada[i, j] = (matrizNormalizada[i, j] * pesos[j]);
                }
            }
        }
        //resuelve genericamente, se va a sobreescribir en los demas
        public virtual void agregacion(int filas, int columnas)
        {
            float acu = 0;
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    acu = acu + (matrizPonderada[i, j]);
                }
                resultado[i] = acu;
                acu = 0;
            }
        }
    }
}
