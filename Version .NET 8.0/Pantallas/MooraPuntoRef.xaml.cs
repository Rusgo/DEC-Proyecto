namespace AppTp.Pantallas;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.Linq;
using static AppTp.Entidades.Alternativa;

public partial class MooraPuntoRef : ContentPage
{
    public ObservableCollection<alternativa> alternativas = new ObservableCollection<alternativa>();
    public List<bool> max = new List<bool>();
    public List<float> pesos = new List<float>();
    int criterios;
    public MooraPuntoRef(ObservableCollection<alternativa> a, int criterios, List<bool> maxmin, List<float> pesos)
    {
        InitializeComponent();
        alternativas = a;
        this.criterios = criterios;
        max = maxmin;
        this.pesos = pesos;
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

    private void ToolbarItem_Clicked(object sender, EventArgs e)
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
        Metodos.MooraPuntoRef moora = new Metodos.MooraPuntoRef(matriz, pesos, max, false);
        moora.resolver();
        Navigation.PushAsync(new Resultados(moora));
        dg.RefreshData();

    }


   

    
}