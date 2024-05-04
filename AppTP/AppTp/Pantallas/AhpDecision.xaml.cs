using AppTp.Metodos;

namespace AppTp.Pantallas;

public partial class AhpDecision : ContentPage
{
	public AhpDecision()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		Navigation.PushAsync(new Ahp(new List<AHP>(),int.Parse(criterios.Text),int.Parse(alternativas.Text),0));
    }
}