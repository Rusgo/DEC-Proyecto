using AppTp.Metodos;

namespace AppTp.Pantallas.Pasos;

public partial class TOPSISTabPage : TabbedPage
{
    MultiCriterio obj;
	public TOPSISTabPage(Topsis tp)
	{
		InitializeComponent();
        Metodos.formatoTabla.CreateTable(tp.ponderarExcel(), "Paso 1", GridTOPSIS1_NormAgregacion);
        Metodos.formatoTabla.CreateTable(tp.tablaSenExcel(0), "Paso 2", GridTOPSIS2_PondIdealAntiIdeal);
        Metodos.formatoTabla.CreateTable(tp.tablaSenExcel(1), "Paso 3", GridTOPSIS3_DistanciasIdealAntiideal);
        resultado.mostrarResultados(tp.ordenarResultado());
        this.obj = tp;
    }

    private async void OnGenerateExcelClicked(object sender, EventArgs e)
    {
        obj.guardarExcel();
    }
    }