


namespace AppTp.Metodos
{
    public class Electre : MultiCriterio
    {
        public float[,] matrizConcordancia {  get; set; }
        public float[,] matrizDiscordancia { get; set; }
        public float[,] matrizSuperacion { get; set; }
        public List<float[,]> listaDiscordancia { get; set; }
        public float[] maximos {  get; set; }
        public float[] min { get; set; }
        public float[] diferencias { get; set; }
        public List<float[]> ListaMaximosDeFilas { get; set; }
        public List<float[]>  ListaMaximosDeFilasDivD { get; set; }
        public float maxDif {  get; set; }
        public float ci { get; set; }
        public float di { get; set; }

        public Electre(float[,] matriz, List<float> pesos, List<bool> max, bool metodo) : base(matriz, pesos, max, metodo)
        {

        }

        public override void resolver()
        {
            int columnas = matriz.GetLength(1);
            int filas = matriz.GetLength(0);
            this.matrizNormalizada = new float[filas, columnas];
            resultado = new float[filas];
            // Normalizar
            normalizar(filas, columnas);
            //Concordancia
            this.matrizConcordancia = indicesConcordancia(filas, columnas);
            //discordancia
            indicesDiscordancia(filas, columnas);
            //hago la matriz
            hacerMatrizDiscordancia(filas, columnas);
            //veo superadoras
        }
        public void hacerMatrizDiscordancia(int filas, int columnas)
        {
            matrizDiscordancia = new float[filas, filas];
            for(int i = 0; i < filas; i++)
            {
                int contador = 0;
                for (int j = 0; j < filas; j++)
                {
                    if (i != j)
                    {
                        matrizDiscordancia[i, j] = ListaMaximosDeFilasDivD[i][contador];
                        contador++;
                    }
                }
            }
        }
        public void indicesDiscordancia(int filas, int columnas)
        {
            this.maximos = new float[columnas];
            this.min = new float[columnas];

            for (int j = 0; j < columnas; j++)
            {
                min[j] = matrizNormalizada[0, j];
                maximos[j] = matrizNormalizada[0, j];
            }
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    if (this.matrizNormalizada[i, j] < min[j] && this.max[j])
                    {
                        min[j] = this.matrizNormalizada[i, j];
                    }
                    else if (this.matrizNormalizada[i, j] > min[j] && !max[j])
                    {
                        min[j] = this.matrizNormalizada[i, j];
                    }

                    if (this.matrizNormalizada[i, j] > maximos[j] && max[j])
                    {
                        maximos[j] = this.matrizNormalizada[i, j];
                    }
                    else if (this.matrizNormalizada[i, j] < maximos[j] && !max[j])
                    {
                        maximos[j] = this.matrizNormalizada[i, j];
                    }

                }
            }
            diferencias = new float[columnas];
            for (int i = 0; i < columnas; i++)
            {
                diferencias[i] = maximos[i] - min[i];
                if (i == 0)
                {
                    maxDif = maximos[i] - min[i];
                    diferencias[i] = (maximos[i] - min[i]);
                }
                else if(maxDif < (maximos[i] - min[i]))
                {
                    maxDif = maximos[i] - min[i];
                    diferencias[i] = (maximos[i] - min[i]);
                }
                else
                {
                    diferencias[i] = (maximos[i] - min[i]);
                }
            }
            int contador = 0;
            listaDiscordancia = new List<float[,]>();
            this.ListaMaximosDeFilas = new List<float[]>();
            for (int alternativas = 0; alternativas < filas; alternativas++)
            {
                float[] MaximosDeFilas = new float[filas - 1];
                contador = 0;
                float[,] matrizAlternativas = new float[filas-1,columnas];
                for(int i = 0; i < filas; i++)
                {
                    bool bandera = true;
                    float maximoFila = 0;
                    if (i != alternativas)
                    {
                        for (int j = 0; j < columnas; j++)
                        {
                            //aca tambien ver si es maximo o minimo
                            float rta =  validar(alternativas, i, j);
                            if (bandera)
                            {
                                maximoFila = rta;
                                MaximosDeFilas[contador] = rta;
                                bandera = false;
                            }
                            else if(maximoFila < rta)
                            {
                                maximoFila = rta;
                                MaximosDeFilas[contador] = rta;
                            }
                            matrizAlternativas[contador, j] = rta;
                        }
                        contador++;
                       
                    }

                    
                }
                this.ListaMaximosDeFilas.Add(MaximosDeFilas);
                listaDiscordancia.Add(matrizAlternativas);
                

            }
            contador = 0;
            this.ListaMaximosDeFilasDivD = new List<float[]>();
            foreach (float[] maximoss in this.ListaMaximosDeFilas)
            {
                float[] MaximosDeFilasDivD = new float[filas - 1];
                for (int i = 0; i < maximoss.GetLength(0); i++)
                {
                    MaximosDeFilasDivD[i] = maximoss[i] / maxDif;
                }
                this.ListaMaximosDeFilasDivD.Add(MaximosDeFilasDivD);
                
            }
        }

        public float validar(int alternativaComparada, int fila, int col )
        {
            float res = 0;
            if (matrizNormalizada[alternativaComparada, col] < matrizNormalizada[fila, col] && max[col])
            {
                res = Math.Abs(matrizNormalizada[fila, col] - matrizNormalizada[alternativaComparada, col]);
            }
            else if (matrizNormalizada[alternativaComparada, col] > matrizNormalizada[fila, col] && !max[col])
            {
                res = Math.Abs(matrizNormalizada[alternativaComparada, col] - matrizNormalizada[fila, col]);
            }
            else
            {
                res = 0;
            }
            return res;
        }

        public float[,] indicesConcordancia(int alternativas, int criterios)
        {
            float[,] matrizConcordancias = new float[alternativas, alternativas];
           for(int alter=0; alter < alternativas; alter++)
            {
                for (int i = 0; i < alternativas; i++)
                {
                    if(alter==i)
                    {
                        matrizConcordancias[alter, i] = 0;

                    }
                    else
                    {
                        float acu = 0;
                        for (int j = 0; j < criterios; j++)
                        {

                            //cambiar en caso de min
                            if (this.matrizNormalizada[alter, j] > this.matrizNormalizada[i, j] && max[j])
                            {
                                acu += this.pesos[j];
                            }
                            else if (this.matrizNormalizada[alter, j] < this.matrizNormalizada[i, j] && !max[j])
                            {
                                acu += this.pesos[j];
                            }
                            else if(this.matrizNormalizada[alter, j] == this.matrizNormalizada[i, j])
                            {
                                acu += this.pesos[j];
                            }
                        }
                        matrizConcordancias[alter, i] = acu;
                    }
                }
            }
           /*
           for (int i = 0; i < alternativas; i++)
            {
                for(int j = 0;j < alternativas; j++)
                {
                    if (i == j)
                    {
                        matrizConcordancias[i, j] = 0;
                    }
                    else if(i < j)
                    {
                        matrizConcordancias[j, i] = 1 - matrizConcordancias[i, j];
                    }
                }
            }*/
           

            return matrizConcordancias;
        }

        public override void agregacion(int filas, int columnas)
        {
            this.matrizSuperacion = new float[filas, filas];
            for(int i = 0; i < filas; i++) 
            {
                for(int j=0; j < filas; j++)
                {
                    if(i != j)
                    {
                        if (matrizConcordancia[i, j] > ci && matrizDiscordancia[i, j] < di)
                        {
                            matrizSuperacion[i, j] = 1;
                        }
                        else
                        {
                            matrizSuperacion[i,j] = 0;
                        }
                    }
                }
            }
        }
    }
}
