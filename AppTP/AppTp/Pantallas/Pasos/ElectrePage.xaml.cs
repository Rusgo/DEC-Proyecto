using AppTp.Metodos;

namespace AppTp.Pantallas.Pasos;

public partial class ElectrePage : TabbedPage
{
	public ElectrePage(Electre electre)
	{
		InitializeComponent();
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
}