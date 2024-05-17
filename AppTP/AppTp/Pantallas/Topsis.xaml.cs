using System.Collections.ObjectModel;
using System.Drawing;
using static AppTp.Entidades.Alternativa;
namespace AppTp.Pantallas;

public partial class Topsis : ContentPage
{
    public ObservableCollection<alternativa> alternativas = new ObservableCollection<alternativa>();
    public List<bool> max = new List<bool>();
    public List<float> pesos = new List<float>();
    int criterios;
    public Topsis(ObservableCollection<alternativa> a, int criterios, List<bool> maxmin, List<float> pesos)
    {
        InitializeComponent();

        alternativas = a;
        max = maxmin;
        this.pesos = pesos;
        this.criterios = criterios;
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
            Metodos.Topsis tp = new Metodos.Topsis(matriz, pesos, max, false);
            tp.resolver();
            dg.RefreshData();
            Navigation.PushAsync(new Resultados(tp));
        }
        catch
        {
            await DisplayAlert("Errore en la carga de datos", "Solo se pueden ingresar numeros en la tabla", "OK");
        }
        

    }
}