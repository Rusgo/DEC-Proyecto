using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTp.Metodos
{
    public class PonderacionLineal : MultiCriterio
    {
        public PonderacionLineal(float[,] matriz, List<float> pesos, List<bool> max) : base(matriz, pesos, max)
        {
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

