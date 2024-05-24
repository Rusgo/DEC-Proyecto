using AppTp.Metodos;

namespace AppTp.Pantallas.Pasos;

public partial class MooraTabPage : TabbedPage
{
    MultiCriterio obj;
    public MooraTabPage(Moora moora)
    {
        InitializeComponent();
        Metodos.formatoTabla.CreateTable(moora.sinNormalizarExcel(), "Paso 1", GridMooraP1_Normalizar);
        Metodos.formatoTabla.CreateTable(moora.agregacionExcel(), "Paso 2", GridMooraP2_PondIdeal);
        resultado.mostrarResultados(moora.ordenarResultado());
        this.obj = moora;
    }
    private async void OnGenerateExcelClicked(object sender, EventArgs e)
    {
        obj.guardarExcel();
    }
}