using AppTp.Metodos;

namespace AppTp.Pantallas.Pasos;

public partial class MooraPRTabPage : TabbedPage
{
    MultiCriterio obj;
    public MooraPRTabPage(MooraPuntoRef moora)
	{
		InitializeComponent();
        Metodos.formatoTabla.CreateTable(moora.sinNormalizarExcel(), "Paso 1", GridPR1_NormAgregacion);
        Metodos.formatoTabla.CreateTable(moora.mejorAlternativaExcel(), "Paso 2", GridMooraPR2_PondIdeal);
        Metodos.formatoTabla.CreateTable(moora.agregacionExcel(), "Paso 3", GridMooraPR3_Distancias);
        resultado.mostrarResultados(moora.ordenarResultado());
        this.obj = moora;
    }
    private async void OnGenerateExcelClicked(object sender, EventArgs e)
    {
        obj.guardarExcel();
    }
}