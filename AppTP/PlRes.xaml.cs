using System.Collections.ObjectModel;
using static AppTP.MainPage;

namespace AppTP;

public partial class PlRes : ContentPage
{
	public PlRes(ObservableCollection<alternativa> a, int criterios)
	{
		InitializeComponent();
        dg.ItemsSource = a;
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
}