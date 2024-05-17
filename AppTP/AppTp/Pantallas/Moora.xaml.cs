namespace AppTp.Pantallas;

using System.Collections.ObjectModel;
using static AppTp.Entidades.Alternativa;

public partial class Moora : ContentPage
{
    public ObservableCollection<alternativa> alternativas = new ObservableCollection<alternativa>();
    public List<bool> max = new List<bool>();
    public List<float> pesos = new List<float>();
    int criterios;
    public Moora(ObservableCollection<alternativa> a, int criterios, List<bool> maxmin, List<float> pesos)
    {
        InitializeComponent();
        this.criterios = criterios;
        alternativas = a;
        max = maxmin;
        this.pesos = pesos;
        this.pesos.Add(float.Parse(criterios.ToString()));
        dg.ItemsSource = alternativas;
        if (criterios < 7)
        {
            dg.Columns["C7"].IsVisible = false;
            if (criterios < 6)
            {
                dg.Columns["C6"].IsVisible = false;
            }
            if (criterios < 5)
            {
                dg.Columns["C5"].IsVisible = false;
                if (criterios < 4)
                {
                    dg.Columns["C4"].IsVisible = false;
                    if (criterios < 3)
                    {
                        dg.Columns["C3"].IsVisible = false;
                    }
                }
            }
        }
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            float[,] matriz = new float[alternativas.Count, criterios];
            int cont = 0;
            int aux = -1;
            foreach (alternativa alternativa in (ObservableCollection<alternativa>)alternativas)
            {
                aux = 0;
                matriz[cont, aux] = float.Parse(alternativa.C1);
                aux++;
                matriz[cont, aux] = float.Parse(alternativa.C2);
                aux++;
                if (aux < criterios)
                    matriz[cont, aux] = float.Parse(alternativa.C3);
                aux++;
                if (aux < criterios)
                    matriz[cont, aux] = float.Parse(alternativa.C4);
                aux++;
                if (aux < criterios)
                    matriz[cont, aux] = float.Parse(alternativa.C5);
                aux++;
                if (aux < criterios)
                    matriz[cont, aux] = float.Parse(alternativa.C6);
                aux++;
                if (aux < criterios)
                    matriz[cont, aux] = float.Parse(alternativa.C7);
                cont++;
            }
            Metodos.Moora moora = new Metodos.Moora(matriz, pesos, max, false);
            moora.resolver();
            Navigation.PushAsync(new Resultados(moora));
            dg.RefreshData();
        }
        catch 
        {
            await DisplayAlert("Error en la carga de datos", "Solo se pueden ingresar numeros en la tabla", "OK");
        }
        

    }

}