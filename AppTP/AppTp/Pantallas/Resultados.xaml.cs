using AppTp.Metodos;
namespace AppTp.Pantallas;

public partial class Resultados : ContentPage
{
	public Resultados(MultiCriterio obj)
	{
		InitializeComponent();

        MostrarResultados(obj.resultado);
    }

    private void MostrarResultados(float[] resultados)
    {
        // Usando LINQ para generar la salida formateada
        var resultadoFormateado = resultados
            .Select((valor, indice) => $"r{indice + 1} = {valor}");

        // Asignar los resultados al ListView
        listViewResultados.ItemsSource = resultadoFormateado;
    }


}