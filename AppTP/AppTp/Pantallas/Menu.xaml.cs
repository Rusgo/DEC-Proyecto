namespace AppTp.Pantallas;

using AppTp.Metodos;
using System.Collections.ObjectModel;
using static AppTp.Entidades.Alternativa;

public partial class Menu : ContentPage
{
    public Menu()
    {
        InitializeComponent();
        c1.IsVisible = false;
        c2.IsVisible = false;
        c3.IsVisible = false;
        c4.IsVisible = false;
        c5.IsVisible = false;
        c6.IsVisible = false;
        c7.IsVisible = false;
    }

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (criterios.SelectedItem.ToString() == "7")
        {
            c1.IsVisible = true;
            c2.IsVisible = true;
            c3.IsVisible = true;
            c4.IsVisible = true;
            c5.IsVisible = true;
            c6.IsVisible = true;
            c7.IsVisible = true;
        }
        else if (criterios.SelectedItem.ToString() == "6")
        {
            c1.IsVisible = true;
            c2.IsVisible = true;
            c3.IsVisible = true;
            c4.IsVisible = true;
            c5.IsVisible = true;
            c6.IsVisible = true;
            c7.IsVisible = false;
        }
        else if (criterios.SelectedItem.ToString() == "5")
        {
            c1.IsVisible = true;
            c2.IsVisible = true;
            c3.IsVisible = true;
            c4.IsVisible = true;
            c5.IsVisible = true;
            c7.IsVisible = false;
            c6.IsVisible = false;
        }
        else if (criterios.SelectedItem.ToString() == "4")
        {
            c1.IsVisible = true;
            c2.IsVisible = true;
            c3.IsVisible = true;
            c4.IsVisible = true;
            c7.IsVisible = false;
            c6.IsVisible = false;
            c5.IsVisible = false;
        }
        else if (criterios.SelectedItem.ToString() == "3")
        {
            c1.IsVisible = true;
            c2.IsVisible = true;
            c3.IsVisible = true;
            c7.IsVisible = false;
            c6.IsVisible = false;
            c5.IsVisible = false;
            c4.IsVisible = false;
        }
        else if (criterios.SelectedItem.ToString() == "2")
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

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        ObservableCollection<alternativa> alternativas = new ObservableCollection<alternativa>();
        for (int i = 0; i < int.Parse(numeroalter.Text); i++)
        {
            alternativas.Add(new alternativa());
        }
        List<bool> maxmin = new List<bool> { maxc1.IsChecked, maxc2.IsChecked, maxc3.IsChecked, maxc4.IsChecked, maxc5.IsChecked, maxc6.IsChecked, maxc7.IsChecked };
        List<float> peso = new List<float> { float.Parse(peso1.Text ?? "0"), float.Parse(peso2.Text ?? "0"), float.Parse(peso3.Text ?? "0"), float.Parse(peso4.Text ?? "0"), float.Parse(peso5.Text ?? "0"), float.Parse(peso6.Text ?? "0"), float.Parse(peso7.Text ?? "0") };
        if (metodo.SelectedItem.ToString() == "AHP")
        {
            List<AHP> tablasGlobal = new List<AHP>();
            Navigation.PushAsync(new Ahp(tablasGlobal, int.Parse(criterios.SelectedItem.ToString()), int.Parse(numeroalter.Text), 0, maxmin));
        }
        else if (DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            Navigation.PushAsync(new NewPage1(alternativas.Count, int.Parse(criterios.SelectedItem.ToString()), maxmin, peso, metodo.SelectedItem.ToString())); 
        }
        else
        {
            if (metodo.SelectedItem.ToString() == "Ponderaciòn Lineal")
            {
                Navigation.PushAsync(new PonderacionLineal(alternativas, int.Parse(criterios.SelectedItem.ToString()), maxmin, peso));
            }
            else if (metodo.SelectedItem.ToString() == "MOORA")
            {
                Navigation.PushAsync(new Moora(alternativas, int.Parse(criterios.SelectedItem.ToString()), maxmin, peso));
            }
            if (metodo.SelectedItem.ToString() == "MOORA con referencia")
            {
                Navigation.PushAsync(new MooraPuntoRef(alternativas, int.Parse(criterios.SelectedItem.ToString()), maxmin, peso));
            }
            if (metodo.SelectedItem.ToString() == "TOPSIS")
            {
                Navigation.PushAsync(new Topsis(alternativas, int.Parse(criterios.SelectedItem.ToString()), maxmin, peso));
            }
            
        }

    }
}
