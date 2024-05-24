

using AppTp.Metodos;

namespace AppTp.Pantallas.Pasos;

public partial class Moora_Resultados : ContentPage
{
    MultiCriterio obj;
    public Moora_Resultados()
    {
        InitializeComponent();
    }
    public void mostrarResultados(string[] resultados)
    {
        List<string> alternativas = new List<string>();
        foreach (string alter in resultados)
        {
            alternativas.Add(alter);
        }
        // Asignar las primeras tres alternativas al podio
        Alternativa1Label.Text = alternativas.Count > 0 ? alternativas[0] : string.Empty;
        Alternativa2Label.Text = alternativas.Count > 1 ? alternativas[1] : string.Empty;
        Alternativa3Label.Text = alternativas.Count > 2 ? alternativas[2] : string.Empty;

        // Limpiar cualquier contenido previo en el StackLayout
        MedallasStackLayout.Children.Clear();

        // Crear las vistas para las medallas restantes
        for (int i = 3; i < alternativas.Count; i++)
        {
            var alternativaLabel = new Label
            {
                Text = alternativas[i],
                FontSize = 14,
                TextColor = Colors.Black,
                HorizontalOptions = LayoutOptions.Center
            };

            var medallaImage = new Image
            {
                Source = $"medalla{i + 1}.png", // imágenes existan con estos nombres
                WidthRequest = 50,
                HeightRequest = 50,
                HorizontalOptions = LayoutOptions.Center
            };

            var stack = new VerticalStackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                Children = { alternativaLabel, medallaImage }
            };

            MedallasStackLayout.Children.Add(stack);
        }
    }
}


