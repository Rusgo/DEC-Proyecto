using System.Collections.ObjectModel;
using static AppTP.MainPage;

namespace AppTP;

public partial class Topsis : ContentPage
{
    public ObservableCollection<alternativa> alternativas = new ObservableCollection<alternativa>
                    {
        new alternativa(),new alternativa(), new alternativa(), new alternativa()
    };
    public List<bool> max = new List<bool>();
    public List<float> publicopeso = new List<float>();
    public Topsis(ObservableCollection<alternativa> a, int criterios, List<bool> maxmin, List<float> pesos)
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
        alternativa mejor = new alternativa();
        alternativa peor = new alternativa();
        mejor.Name = "S+";
        peor.Name = "S-";
        alternativas.Add(mejor);
        alternativas.Add(peor);
        dg.ItemsSource = alternativas;
        alternativas.Add(peso);
        if (criterios< 7)
        {
            dg.Columns["C7"].IsVisible = false;
            if (criterios< 6)
            {
                dg.Columns["C6"].IsVisible = false;
            }
            if (criterios< 5)
            {
                dg.Columns["C5"].IsVisible = false;
                if (criterios< 4)
                {
                    dg.Columns["C4"].IsVisible = false;
                    if (criterios< 3)
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
        if (alternativa.Name != "W" && alternativa.Name != "S+" && alternativa.Name != "S-")
        {
            suma1 += (float)Math.Pow((float.Parse(alternativa.C1 ?? "0")), 2);
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

    float c1 = 0; float c2 = 0; float c3 = 0; float c4 = 0; float c5 = 0; float c6 = 0; float c7 = 0;
        float c11 = 0; float c22 = 0; float c33 = 0; float c44 = 0; float c55 = 0; float c66 = 0; float c77 = 0;
        foreach (alternativa alternativa in (ObservableCollection<alternativa>)alternativas)
    {
            if (alternativa.Name != "W" && alternativa.Name != "S+" && alternativa.Name != "S-")
            {
                alternativa.C1 = ((publicopeso[0] * float.Parse(alternativa.C1 ?? "0")) / verificar2(suma1)).ToString();
                alternativa.C2 = ((publicopeso[1] * float.Parse(alternativa.C2 ?? "0")) / verificar2(suma2)).ToString();
                alternativa.C3 = ((publicopeso[2] * (float.Parse(alternativa.C3 ?? "0"))) / verificar2(suma3)).ToString();
                alternativa.C4 = ((publicopeso[3] * (float.Parse(alternativa.C4 ?? "0"))) / verificar2(suma4)).ToString();
                alternativa.C5 = ((publicopeso[4] * (float.Parse(alternativa.C5 ?? "0"))) / verificar2(suma5)).ToString();
                alternativa.C6 = ((publicopeso[5] * (float.Parse(alternativa.C6 ?? "0"))) / verificar2(suma6)).ToString();
                alternativa.C7 = ((publicopeso[6] * (float.Parse(alternativa.C7 ?? "0"))) / verificar2(suma7)).ToString();
            }
            if (alternativa.Name == "S+")
            {
                alternativa.C1 = alternativas.Take(alternativas.Count - 3).Min(e => e.C1);
                alternativa.C2 = alternativas.Take(alternativas.Count - 3).Min(e => e.C2);
                alternativa.C3 = alternativas.Take(alternativas.Count - 3).Min(e => e.C3);
                alternativa.C4 = alternativas.Take(alternativas.Count - 3).Min(e => e.C4);
                alternativa.C5 = alternativas.Take(alternativas.Count - 3).Min(e => e.C5);
                alternativa.C6 = alternativas.Take(alternativas.Count - 3).Min(e => e.C6);
                alternativa.C7 = alternativas.Take(alternativas.Count - 3).Min(e => e.C7);
                if (max[0])
                {
                    alternativa.C1 = alternativas.Take(alternativas.Count - 3).Max(e => e.C1);
                }
                if (max[1])
                {
                    alternativa.C2 = alternativas.Take(alternativas.Count - 3).Max(e => e.C2);
                }
                if (max[2])
                {
                    alternativa.C3 = alternativas.Take(alternativas.Count - 3).Max(e => e.C3);
                }
                if (max[3])
                {
                    alternativa.C4 = alternativas.Take(alternativas.Count - 3).Max(e => e.C4);
                }
                if (max[4])
                {
                    alternativa.C5 = alternativas.Take(alternativas.Count - 3).Max(e => e.C5);
                }
                if (max[5])
                {
                    alternativa.C6 = alternativas.Take(alternativas.Count - 3).Max(e => e.C6);
                }
                if (max[6])
                {
                    alternativa.C7 = alternativas.Take(alternativas.Count - 3).Max(e => e.C7);
                }
                c1 = float.Parse(alternativa.C1);
                c2 = float.Parse(alternativa.C2);
                c3 = float.Parse(alternativa.C3);
                c4 = float.Parse(alternativa.C4);
                c5 = float.Parse(alternativa.C5);
                c6 = float.Parse(alternativa.C6);
                c7 = float.Parse(alternativa.C7);

            }
            if (alternativa.Name == "S-")
            {
                alternativa.C1 = alternativas.Take(alternativas.Count - 3).Max(e => e.C1);
                alternativa.C2 = alternativas.Take(alternativas.Count - 3).Max(e => e.C2);
                alternativa.C3 = alternativas.Take(alternativas.Count - 3).Max(e => e.C3);
                alternativa.C4 = alternativas.Take(alternativas.Count - 3).Max(e => e.C4);
                alternativa.C5 = alternativas.Take(alternativas.Count - 3).Max(e => e.C5);
                alternativa.C6 = alternativas.Take(alternativas.Count - 3).Max(e => e.C6);
                alternativa.C7 = alternativas.Take(alternativas.Count - 3).Max(e => e.C7);
                if (max[0])
                {
                    alternativa.C1 = alternativas.Take(alternativas.Count - 3).Min(e => e.C1);
                }
                if (max[1])
                {
                    alternativa.C2 = alternativas.Take(alternativas.Count - 3).Min(e => e.C2);
                }
                if (max[2])
                {
                    alternativa.C3 = alternativas.Take(alternativas.Count - 3).Min(e => e.C3);
                }
                if (max[3])
                {
                    alternativa.C4 = alternativas.Take(alternativas.Count - 3).Min(e => e.C4);
                }
                if (max[4])
                {
                    alternativa.C5 = alternativas.Take(alternativas.Count - 3).Min(e => e.C5);
                }
                if (max[5])
                {
                    alternativa.C6 = alternativas.Take(alternativas.Count - 3).Min(e => e.C6);
                }
                if (max[6])
                {
                    alternativa.C7 = alternativas.Take(alternativas.Count - 3).Max(e => e.C7);
                }
                c11 = float.Parse(alternativa.C1);
                c22 = float.Parse(alternativa.C2);
                c33 = float.Parse(alternativa.C3);
                c44 = float.Parse(alternativa.C4);
                c55 = float.Parse(alternativa.C5);
                c66 = float.Parse(alternativa.C6);
                c77 = float.Parse(alternativa.C7);

            }


            }
    int cont = 0;
    float r1 = 0; float r2 = 0; float r3 = 0; float r4 = 0; float r5 = 0; float r6 = 0; float r7 = 0;



    foreach (alternativa alternativa in (ObservableCollection<alternativa>)alternativas)
    {
        r1 = float.Parse(alternativa.C1); r2 = float.Parse(alternativa.C2); r3 = float.Parse(alternativa.C3); r4 = float.Parse(alternativa.C4); r5 = float.Parse(alternativa.C5); r6 = float.Parse(alternativa.C6); r7 = float.Parse(alternativa.C7);
        
        if (alternativa.Name != "W" && alternativa.Name != "S-" && alternativa.Name != "S+")
        {
                alternativa.SB = Math.Sqrt(((float)Math.Pow((float.Parse(alternativa.C1) - c1), 2) + (float)Math.Pow((float.Parse(alternativa.C2) - c2), 2) + (float)Math.Pow((float.Parse(alternativa.C3) - c3), 2) + (float)Math.Pow((float.Parse(alternativa.C4) - c4), 2) + (float)Math.Pow((float.Parse(alternativa.C5) - c5), 2) + (float)Math.Pow((float.Parse(alternativa.C6) - c6), 2) + (float)Math.Pow((float.Parse(alternativa.C7) - c7), 2))).ToString();
                alternativa.SM = Math.Sqrt(((float)Math.Pow((float.Parse(alternativa.C1) - c11), 2) + (float)Math.Pow((float.Parse(alternativa.C2) - c22), 2) + (float)Math.Pow((float.Parse(alternativa.C3) - c33), 2) + (float)Math.Pow((float.Parse(alternativa.C4) - c44), 2) + (float)Math.Pow((float.Parse(alternativa.C5) - c55), 2) + (float)Math.Pow((float.Parse(alternativa.C6) - c66), 2) + (float)Math.Pow((float.Parse(alternativa.C7) - c77), 2))).ToString();
            }
        cont++;

    }
    foreach (alternativa alternativa in (ObservableCollection<alternativa>)alternativas)
        {
            if (alternativa.Name != "W" && alternativa.Name != "S-" && alternativa.Name != "S+")
            {
                alternativa.U = ((float.Parse(alternativa.SM)) / (float.Parse(alternativa.SM) + float.Parse(alternativa.SB))).ToString();
            }
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