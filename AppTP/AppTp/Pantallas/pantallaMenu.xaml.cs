using AppTp.Metodos;
using System.Collections.ObjectModel;
using AppTp.Entidades;
using static AppTp.Entidades.Alternativa;
using System.Runtime.CompilerServices;
namespace AppTp.Pantallas;

public partial class pantallaMenu : ContentPage
{
    string metodo;
    bool otro;
	public pantallaMenu(string metodo)
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
        if (metodo == "M�todo AHP")
        {
            c1.IsVisible = false; c2.IsVisible = false;
            otro = false;
        }
        else
        {
            otro = true;
        }
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
        if (metodo == "M�todo AHP")
        {
            List<AHP> tablasGlobal = new List<AHP>();
            Navigation.PushAsync(new Ahp(tablasGlobal, int.Parse(criterios.Text), alternativas.Count, 0, maxmin));
        }
        else if (DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            Navigation.PushAsync(new NewPage1(alternativas.Count, int.Parse(criterios.Text), maxmin, peso, metodo));
        }
        else
        {
            
                if (metodo == "  Ponderaci�n Lineal")
                {
                    Navigation.PushAsync(new PonderacionLineal(alternativas, int.Parse(criterios.Text), maxmin, peso));
                }
                else if (metodo == "M�todo MOORA")
                {
                    Navigation.PushAsync(new Moora(alternativas, int.Parse(criterios.Text), maxmin, peso));
                }
                if (metodo == "          MOORA Punto de Referencia")
                {
                    Navigation.PushAsync(new MooraPuntoRef(alternativas, int.Parse(criterios.Text), maxmin, peso));
                }
                if (metodo == "M�todo TOPSIS")
                {
                    Navigation.PushAsync(new Topsis(alternativas, int.Parse(criterios.Text), maxmin, peso));
                }
                if (metodo == "M�todo TOPSIS")
                {
                    Navigation.PushAsync(new Topsis(alternativas, int.Parse(criterios.Text), maxmin, peso));
                }
                if (metodo == "M�todo AHP")
                {
                    List<AHP> tablasGlobal = new List<AHP>();
                    Navigation.PushAsync(new Ahp(tablasGlobal, int.Parse(criterios.Text), int.Parse(Alternativas.Text), 0, maxmin));
                }
            
            


        }

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