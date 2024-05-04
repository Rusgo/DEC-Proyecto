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
            alternativa peso = new alternativa();
            dg.ItemsSource = alternativas;
            alternativas.Add(peso);
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
        float[,] matriz = new float[alternativas.Count - 1, criterios];
        int cont = 0;
            foreach (alternativa alternativa in (ObservableCollection<alternativa>)alternativas)
            {
                matriz[cont, 0] = float.Parse(alternativa.C1);
                matriz[cont, 1] = float.Parse(alternativa.C2);
                matriz[cont, 2] = float.Parse(alternativa.C3);
                matriz[cont, 3] = float.Parse(alternativa.C4);
                matriz[cont, 4] = float.Parse(alternativa.C5);
                matriz[cont, 5] = float.Parse(alternativa.C6);
                matriz[cont, 6] = float.Parse(alternativa.C7);
                cont++;
            }
            Metodos.PonderacionLineal pl = new Metodos.PonderacionLineal(matriz, pesos, max);
            pl.metodo = true;
            pl.resolver();
            dg.RefreshData();
        }
    }
