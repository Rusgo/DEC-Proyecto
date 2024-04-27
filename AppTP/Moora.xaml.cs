using System.Collections.ObjectModel;
using System.Security.Cryptography;
using static AppTP.MainPage;

namespace AppTP;

public partial class Moora : ContentPage
{
    public ObservableCollection<alternativa> alternativas = new ObservableCollection<alternativa>
                    {
        new alternativa(),new alternativa(), new alternativa(), new alternativa()
    };
    public List<bool> max = new List<bool>();
    public List<float> publicopeso = new List<float>();
    public Moora(ObservableCollection<alternativa> a, int criterios, List<bool> maxmin, List<float> pesos)
	{
		InitializeComponent();
        alternativas = a;
        max = maxmin;
        publicopeso = pesos;
        publicopeso.Add(float.Parse(criterios.ToString()));
        alternativa peso = new alternativa();
        peso.C1 = pesos[0].ToString();
        peso.C2 = pesos[1].ToString();
        peso.C3 = pesos[2].ToString();
        peso.C4 = pesos[3].ToString();
        peso.C5 = pesos[4].ToString();
        peso.C6 = pesos[5].ToString();
        peso.C7 = pesos[6].ToString();
        peso.Name = "W";
        alternativa moora = new alternativa();
        moora.Name = "S";
        alternativas.Add(moora);
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
            if (alternativa.Name != "W" && alternativa.Name != "S")
            {
                suma1 += (float)Math.Pow((float.Parse(alternativa.C1 ?? "0")),2);
                suma2 += (float)Math.Pow(((float.Parse(alternativa.C2 ?? "0"))), 2);
                suma3 += (float)Math.Pow((((float.Parse(alternativa.C3 ?? "0")))), 2);
                suma4 += (float)Math.Pow(((float.Parse(alternativa.C4 ?? "0"))), 2);
                suma5 += (float)Math.Pow((((float.Parse(alternativa.C5 ?? "0")))), 2);
                suma6 += (float)Math.Pow((((float.Parse(alternativa.C6 ?? "0")))), 2);
                suma7 += (float)Math.Pow((((float.Parse(alternativa.C7 ?? "0")))), 2);
            }

        }

        suma1 = (float)Math.Sqrt(suma1);
        suma2 = (float)Math.Sqrt(suma2);
        suma3 = (float)Math.Sqrt(suma3);
        suma4 = (float)Math.Sqrt(suma4);
        suma5 = (float)Math.Sqrt(suma5);
        suma6 = (float)Math.Sqrt(suma6);
        suma7 = (float)Math.Sqrt(suma7);


        foreach (alternativa alternativa in (ObservableCollection<alternativa>)alternativas)
        {
            if (alternativa.Name != "W" && alternativa.Name != "S")
            {
                alternativa.C1 = ((publicopeso[0] * float.Parse(alternativa.C1 ?? "0")) / verificar2(suma1)).ToString();
                alternativa.C2 = ((publicopeso[1] * float.Parse(alternativa.C2 ?? "0")) / verificar2(suma2)).ToString();
                alternativa.C3 = ((publicopeso[2] * (float.Parse(alternativa.C3 ?? "0"))) / verificar2(suma3)).ToString();
                alternativa.C4 = ((publicopeso[3] * (float.Parse(alternativa.C4 ?? "0"))) / verificar2(suma4)).ToString();
                alternativa.C5 = ((publicopeso[4] * (float.Parse(alternativa.C5 ?? "0"))) / verificar2(suma5)).ToString();
                alternativa.C6 = ((publicopeso[5] * (float.Parse(alternativa.C6 ?? "0"))) / verificar2(suma6)).ToString();
                alternativa.C7 = ((publicopeso[6] * (float.Parse(alternativa.C7 ?? "0"))) / verificar2(suma7)).ToString();
            }
            else if (alternativa.Name == "S")
            {
                alternativa.C1 = suma1.ToString();
                alternativa.C2 = suma2.ToString();
                alternativa.C3 = suma3.ToString();
                alternativa.C4 = suma4.ToString();
                alternativa.C5 = suma5.ToString();
                alternativa.C6 = suma6.ToString();
                alternativa.C7 = suma7.ToString();
            }


        }
        int cont = 0;
        float r1 = 0; float r2 = 0; float r3 = 0; float r4 = 0; float r5 = 0; float r6 = 0; float r7 = 0;
        foreach (alternativa alternativa in (ObservableCollection<alternativa>)alternativas)
        {
            r1=verificar(float.Parse(alternativa.C1), max[0]); r2 = verificar(float.Parse(alternativa.C2), max[1]); r3 = verificar(float.Parse(alternativa.C3), max[2]); r4 = verificar(float.Parse(alternativa.C4), max[3]); verificar(r5 = float.Parse(alternativa.C5), max[4]); r6 = verificar(float.Parse(alternativa.C6), max[5]); r7 = verificar(float.Parse(alternativa.C7), max[6]);       
            if (alternativa.Name != "W" && alternativa.Name != "S")
            {
                alternativa.U = (r1+r2+r3+r4+r5+r6+r7).ToString();
            }
            cont++;

        }

        dg.RefreshData();
    
}
    public float verificar2(float a)
    {
        if ((a != 0)) { return a; }
        return (1);
    }
    public float verificar(float a, bool b)
    {
        if (b)
        {
            return a;
        }
        return (-a);
    }
}