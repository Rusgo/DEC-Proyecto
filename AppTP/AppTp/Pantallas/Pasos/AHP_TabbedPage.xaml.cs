using AppTp.Entidades;
using AppTp.Metodos;

namespace AppTp.Pantallas.Pasos;

public partial class AHP_TabbedPage : TabbedPage
{
	public AHP_TabbedPage(List<AHP> ahp, MultiCriterio pl)
	{
		InitializeComponent();
		List<string[,]> matricesComparacion = new List<string[,]>();
        List<string[,]> matricesNormalizadas = new List<string[,]>();
        int cont = 0;
        List<string> listaLetras = new List<string>
            {
                "Pesos Relativos"
            };
        List<float[]> pesosRela = new List<float[]>();
        int max = 0;
        int colmax = 0;
        foreach (AHP elemento in ahp)
		{
            if (cont == 0)
            {
                matricesComparacion.Add(Entidades.formatoAhp.formatoExcel(elemento.matriz, true, "C" + cont.ToString()));
                pesosRela.Add(elemento.promedioFilas);
                matricesNormalizadas.Add(formatoAhp.AgregarColumna(Entidades.formatoAhp.formatoExcel(elemento.matrizNormalizada, true, "C" + cont.ToString()), pesosRela, listaLetras));
                if (max < elemento.matriz.GetLength(0) + 1)
                {
                    max = elemento.matriz.GetLength(0) + 1;
                }
                if (colmax < elemento.matriz.GetLength(1) + 2)
                {
                    colmax = elemento.matriz.GetLength(1) + 2;
                }
                pesosRela = new List<float[]>();

            }
            else
            {
                matricesComparacion.Add(Entidades.formatoAhp.formatoExcel(elemento.matriz, false, "C" + cont.ToString()));
                pesosRela.Add(elemento.promedioFilas);
                matricesNormalizadas.Add(formatoAhp.AgregarColumna(Entidades.formatoAhp.formatoExcel(elemento.matrizNormalizada, false, "C" + cont.ToString()), pesosRela, listaLetras));
                if (max < elemento.matriz.GetLength(0) + 1)
                {
                    max = elemento.matriz.GetLength(0) + 1;
                }
                if (colmax < elemento.matriz.GetLength(1) + 2)
                {
                    colmax = elemento.matriz.GetLength(1) + 2;
                }
                pesosRela = new List<float[]>();
            }
            cont++;
		}
        string[,] comparaciones = formatoAhp.juntarMatrices(matricesComparacion,max,colmax);
        string[,] normalizadas = formatoAhp.juntarMatrices(matricesNormalizadas,max,colmax);
        Metodos.formatoTabla.CreateTable(comparaciones, "Paso 1", GridAHP_Matrices);
        Metodos.formatoTabla.CreateTable(normalizadas, "Paso 2",GridNormalizado);
        Metodos.formatoTabla.CreateTable(pl.agregacionExcel(), "Paso 3", GridPL);
        resultado.mostrarResultados(pl.ordenarResultado());

    }
}