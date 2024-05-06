using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using static AppTp.Entidades.Alternativa;
namespace AppTp.Pantallas;

    public partial class PonderacionLineal : ContentPage
    {
    public ObservableCollection<alternativa> alternativas;
    int criterios;
    List<float> pesos;
  
        public List<bool> max = new List<bool>();
        public List<float> publicopeso = new List<float>();
        public PonderacionLineal(ObservableCollection<alternativa> a, int criterios, List<bool> maxmin, List<float> pesos)
        {
            this.pesos = pesos;
            this.criterios = criterios;
            alternativas = a;
            InitializeComponent();
            max = maxmin;
            publicopeso = pesos;
            publicopeso.Add(float.Parse(criterios.ToString()));
            
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


        private void TextColumn_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

        }

        private async void dg_PullToRefresh(object sender, EventArgs e)
        {

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
                if(aux< criterios)
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
            Metodos.PonderacionLineal pl = new Metodos.PonderacionLineal(matriz, pesos, max, true);
            pl.metodo = true;
            pl.resolver();
            Navigation.PushAsync(new Resultados(pl));
            dg.RefreshData();
        }
    }
