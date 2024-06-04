using AppTp.Metodos;
using System.Runtime.CompilerServices;

namespace AppTp.Pantallas.Pasos;

public partial class ElectrePage : TabbedPage
{
    Electre electre;
	public ElectrePage(Electre electre)
	{
		InitializeComponent();
        this.electre = electre;
        var grid = new Grid();
        grid.Margin = 10;
        Metodos.formatoTabla.CreateTable(electre.normalizarExcel(), "Paso 1", grid);
        var grid2 = new Grid();
        grid2.Margin = 10;
        Metodos.formatoTabla.CreateTable(Entidades.formatoAhp.formatoExcel(electre.matrizConcordancia, true, ""), "IndiceConcor", grid2);
        var grid3 = new Grid();
        grid3.Margin = 10;
        Metodos.formatoTabla.CreateTable(Entidades.formatoAhp.formatoExcel(electre.matrizDiscordancia, true, ""), "IndiceDiscor", grid3);
        PantallaPaso1.Children.Add(grid);
        PantallaPaso2.Children.Add(grid2);
        PantallaPaso2.Children.Add(grid3);
    }

    private void Button_Clicked(object sender, EventArgs e)
    {

        electre.ci = float.Parse(c1.Text);
        electre.di = float.Parse(d1.Text);
        electre.agregacion(electre.matrizNormalizada.GetLength(0), electre.matrizNormalizada.GetLength(1));
        Metodos.formatoTabla.CreateTable(Entidades.formatoAhp.formatoExcel(electre.matrizSuperacion, true, ""), "Supera", GridSupe);
    }
}