using AppTp.Metodos;
using System.Collections.ObjectModel;
using AppTp.Entidades;
using static AppTp.Entidades.Alternativa;
using System.Runtime.CompilerServices;
namespace AppTp.Pantallas;

public partial class pantallaMenuPromethe : ContentPage
{
    string metodo;
    bool otro = true;
	public pantallaMenuPromethe(string metodo)
	{
		InitializeComponent();
        this.metodo = metodo;
        c3.IsVisible = false;
        c4.IsVisible = false;
        c5.IsVisible = false;
        c6.IsVisible = false;
        c7.IsVisible = false;
        Alternativas.Text = "2";
        criterios.Text = "2";
    }
    private void cambioCriterio(object sender, EventArgs e)
    {

    }
    private void btnMenosAlter(object sender, EventArgs e)
    {
        if(int.Parse(Alternativas.Text) <= 15 && int.Parse(Alternativas.Text) > 2) 
        {
            int num = int.Parse(Alternativas.Text) - 1;
            Alternativas.Text = num.ToString();
        }

    }
    private void btnMasAlter(object sender, EventArgs e)
    {
        if (int.Parse(Alternativas.Text) < 15 && int.Parse(Alternativas.Text) >= 2)
        {
            int num = int.Parse(Alternativas.Text) + 1;
            Alternativas.Text = num.ToString();
        }

    }
    private void btnMenosCri(object sender, EventArgs e)
    {
        if (int.Parse(criterios.Text) <= 7 && int.Parse(criterios.Text) > 2)
        {
            int num = int.Parse(criterios.Text) - 1;
            criterios.Text = num.ToString();
        }

    }
    private void btnMasCri(object sender, EventArgs e)
    {
        if (int.Parse(criterios.Text) < 7 && int.Parse(criterios.Text) >= 2)
        {
            int num = int.Parse(criterios.Text) + 1;
            criterios.Text = num.ToString();
        }

    }
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        ObservableCollection<alternativa> alternativas = new ObservableCollection<alternativa>();
        for (int i = 0; i < int.Parse(Alternativas.Text); i++)
        {
            alternativas.Add(new alternativa(i+1));
        }
        List<bool> maxmin = new List<bool> { maxc1.IsChecked, maxc2.IsChecked, maxc3.IsChecked, maxc4.IsChecked, maxc5.IsChecked, maxc6.IsChecked, maxc7.IsChecked };
        List<float> peso = new List<float> { float.Parse(peso1.Text ?? "0"), float.Parse(peso2.Text ?? "0"), float.Parse(peso3.Text ?? "0"), float.Parse(peso4.Text ?? "0"), float.Parse(peso5.Text ?? "0"), float.Parse(peso6.Text ?? "0"), float.Parse(peso7.Text ?? "0") };
        List<string> funciones = new List<string>();
        List<float> p = new List<float>();
        List<float> q = new List<float>();
        List<float> o = new List<float>();
        funciones.Add(p1.SelectedItem.ToString() ?? "1");
        funciones.Add(p2.SelectedItem.ToString() ?? "1");
        p.Add(float.Parse(pp1.Text ?? "0"));
        p.Add(float.Parse(pp2.Text ?? "0"));
        q.Add(float.Parse(q1.Text ?? "0"));
        q.Add(float.Parse(q2.Text ?? "0"));
        o.Add(float.Parse(o1.Text ?? "0"));
        o.Add(float.Parse(o2.Text ?? "0"));
        if (int.Parse(criterios.Text) == 3)
        {
            funciones.Add(p3.SelectedItem.ToString() ?? "1");
            p.Add(float.Parse(pp3.Text ?? "0"));
            q.Add(float.Parse(q3.Text ?? "0")); o.Add(float.Parse(o3.Text ?? "0"));
        }
        if (int.Parse(criterios.Text) == 4)
        {
            funciones.Add(p4.SelectedItem.ToString() ?? "1");
            p.Add(float.Parse(pp4.Text ?? "0"));
            q.Add(float.Parse(q4.Text ?? "0")); o.Add(float.Parse(o3.Text ?? "0"));
        }
        if (int.Parse(criterios.Text) == 5)
        {
            funciones.Add(p5.SelectedItem.ToString() ?? "1");
            p.Add(float.Parse(pp5.Text ?? "0"));
            q.Add(float.Parse(q5.Text ?? "0")); o.Add(float.Parse(o5.Text ?? "0"));
        }
        if (int.Parse(criterios.Text) == 6)
        {
            funciones.Add(p6.SelectedItem.ToString() ?? "1");
            p.Add(float.Parse(pp6.Text ?? "0"));
            q.Add(float.Parse(q6.Text ?? "0")); o.Add(float.Parse(o6.Text ?? "0"));
        }
        if (int.Parse(criterios.Text) == 7)
        {
            funciones.Add(p7.SelectedItem.ToString() ?? "1");
            p.Add(float.Parse(pp7.Text ?? "0"));
            q.Add(float.Parse(q7.Text ?? "0")); o.Add(float.Parse(o7.Text ?? "0"));

        }

        Navigation.PushAsync(new NewPage2(alternativas.Count, int.Parse(criterios.Text), maxmin, peso, metodo, funciones,p,q,o));
        
        

    }

    private void criterios_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (otro)
        {
            if (criterios.Text == "7")
            {
                c1.IsVisible = true;
                c2.IsVisible = true;
                c3.IsVisible = true;
                c4.IsVisible = true;
                c5.IsVisible = true;
                c6.IsVisible = true;
                c7.IsVisible = true;
            }
            else if (criterios.Text == "6")
            {
                c1.IsVisible = true;
                c2.IsVisible = true;
                c3.IsVisible = true;
                c4.IsVisible = true;
                c5.IsVisible = true;
                c6.IsVisible = true;
                c7.IsVisible = false;
            }
            else if (criterios.Text == "5")
            {
                c1.IsVisible = true;
                c2.IsVisible = true;
                c3.IsVisible = true;
                c4.IsVisible = true;
                c5.IsVisible = true;
                c7.IsVisible = false;
                c6.IsVisible = false;
            }
            else if (criterios.Text == "4")
            {
                c1.IsVisible = true;
                c2.IsVisible = true;
                c3.IsVisible = true;
                c4.IsVisible = true;
                c7.IsVisible = false;
                c6.IsVisible = false;
                c5.IsVisible = false;
            }
            else if (criterios.Text == "3")
            {
                c1.IsVisible = true;
                c2.IsVisible = true;
                c3.IsVisible = true;
                c7.IsVisible = false;
                c6.IsVisible = false;
                c5.IsVisible = false;
                c4.IsVisible = false;
            }
            else if (criterios.Text == "2")
            {
                c1.IsVisible = true;
                c2.IsVisible = true;
                c7.IsVisible = false;
                c6.IsVisible = false;
                c5.IsVisible = false;
                c4.IsVisible = false;
                c3.IsVisible = false;
            }
        }

    }
}