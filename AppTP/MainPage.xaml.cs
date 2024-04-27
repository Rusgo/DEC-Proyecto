using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;

namespace AppTP
{
    public partial class MainPage : ContentPage
    {
            public ObservableCollection<alternativa> alternativas = new ObservableCollection<alternativa>
                    {
        new alternativa(),new alternativa(), new alternativa(), new alternativa()
    };
        public List<bool> max = new List<bool>();
        public List<float> publicopeso = new List<float>();
        public MainPage(ObservableCollection<alternativa> a, int criterios, List<bool> maxmin, List<float> pesos)
            {
                alternativas = a;
                InitializeComponent();
                max = maxmin;
                publicopeso = pesos;
                publicopeso.Add(float.Parse(criterios.ToString()));
                alternativa peso = new alternativa();
                peso.C1 = pesos[0].ToString();
                peso.C2 = pesos[1].ToString();
                peso.C3= pesos[2].ToString();
                peso.C4 = pesos[3].ToString();
                peso.C5 = pesos[4].ToString();
                peso.C6 = pesos[5].ToString();
                peso.C7 = pesos[6].ToString();
                peso.Name = "W";
                dg.ItemsSource = alternativas;
                alternativas.Add(peso);
            if (criterios < 7 ) 
            {
                dg.Columns["C7"].IsVisible = false;
                if(criterios < 6 ) 
                {
                    dg.Columns["C6"].IsVisible = false;
                }
                if(criterios < 5 ) { dg.Columns["C5"].IsVisible = false;
                    if (criterios < 4) {
                        dg.Columns["C4"].IsVisible = false;
                        if(criterios < 3)
                        {
                            dg.Columns["C3"].IsVisible = false;
                        }
                    }
                }
            }
            
            }

            public class alternativa
            {
                static int id = 1;
                public alternativa()
                {
                    Name = "A" + id.ToString();
                    id++;
                }
                public string Name { get; set; }
                public string C1 { get; set; }
                public string C2 { get; set; }

                public string C3 { get; set; }
                public string C4 { get; set; }

                public string C5 { get; set; }
                public string C6 { get; set; }
                public string C7 { get; set; }
            public string U { get; set; }
            public string SB { get; set; }
            public string SM { get; set; }




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
        public float verificar(float a, bool b)
        {
            if (b)
            {
                return a;
            }
            else if (a == 0)
            {
                return 0;
            }
                return (1 / a);
            
        }
        public float verificar2(float a)
        {
            if((a != 0) ) { return a; }
            return (1);
        }
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            float suma1 = 0;
            float suma2 = 0;
            float suma3 = 0;
            float suma4 = 0;
            float suma5 = 0;
            float suma6 = 0;
            float suma7 = 0;
            foreach (alternativa alternativa in (ObservableCollection<alternativa>)alternativas)
            {
                if (alternativa.Name != "W")
                {
                    suma1 += verificar(float.Parse(alternativa.C1 ?? "0"), max[0]);
                    suma2 += verificar((float.Parse(alternativa.C2 ?? "0")), max[1]);
                    suma3 += verificar(((float.Parse(alternativa.C3 ?? "0"))), max[2]);
                    suma4 += verificar((float.Parse(alternativa.C4 ?? "0")), max[3]);
                    suma5 += verificar(((float.Parse(alternativa.C5 ?? "0"))), max[4]);
                    suma6 += verificar(((float.Parse(alternativa.C6 ?? "0"))), max[5]);
                    suma7 += verificar(((float.Parse(alternativa.C7 ?? "0"))), max[6]);
                }

            }



            foreach (alternativa alternativa in (ObservableCollection<alternativa>)alternativas)
            {
                if (alternativa.Name != "W")
                {
                    alternativa.C1 = (verificar(float.Parse(alternativa.C1), max[0]) / verificar2(suma1)).ToString();
                    alternativa.C2 = (verificar((float.Parse(alternativa.C2 ?? "0")), max[1]) / verificar2(suma2)).ToString();
                    alternativa.C3 = (verificar(((float.Parse(alternativa.C3 ?? "0"))), max[2]) / verificar2(suma3)).ToString();
                    alternativa.C4 = (verificar(((float.Parse(alternativa.C4 ?? "0"))), max[3]) / verificar2(suma4)).ToString();
                    alternativa.C5 = (verificar(((float.Parse(alternativa.C5 ?? "0"))), max[4]) / verificar2(suma5)).ToString();
                    alternativa.C6 = (verificar(((float.Parse(alternativa.C6 ?? "0"))), max[5]) / verificar2(suma6)).ToString();
                    alternativa.C7 = (verificar(((float.Parse(alternativa.C7 ?? "0"))), max[6]) / verificar2(suma7)).ToString();
                }


            }
            int cont = 0;
            foreach (alternativa alternativa in (ObservableCollection<alternativa>)alternativas)
            {
                if (alternativa.Name != "W")
                {

                    alternativa.U = ((float.Parse(alternativa.C1) * publicopeso[0] + float.Parse(alternativa.C2) * publicopeso[1] + float.Parse(alternativa.C3) * publicopeso[2] + float.Parse(alternativa.C4) * publicopeso[3] + float.Parse(alternativa.C5) * publicopeso[4] + float.Parse(alternativa.C6) * publicopeso[5] + float.Parse(alternativa.C7) * publicopeso[6])).ToString();
                }
                cont++;

            }

            dg.RefreshData();
        }
    }
    }