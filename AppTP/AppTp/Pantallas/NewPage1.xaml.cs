namespace AppTp.Pantallas;

public partial class NewPage1 : ContentPage
{
	public NewPage1(int rows, int columns)
	{
		InitializeComponent();

        Grid grid = new Grid();
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.5, GridUnitType.Star) }); // Columna 2
        for (int j = 0; j < columns; j++)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.15, GridUnitType.Star) }); // Columna i

        }
        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.15, GridUnitType.Star) }); // fila i
        for (int i = 0; i <= rows; i++)
        {
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.15, GridUnitType.Star) }); // fila i
            
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // Agregar una fila
                for (int j = 0; j <= columns; j++)
                {
                    if (j != 0 && i == 0)
                    {
                        grid.Add(new Label { Text = "C" + (j).ToString(), VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, j, 0);
                    }
                    else if (j == 0 && i != 0)
                    {
                        grid.Add(new Label { Text = "A" + (i).ToString(), VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, 0, i);
                    }
                    else if(i!=0)
                    {
                        grid.Add(new Entry { VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center }, j, i);
                    }
                }
            
        }
        // Agregar el Grid a tu página
        ScrollView sv = new ScrollView();
        sv.Orientation = ScrollOrientation.Vertical;
        sv.Content = grid;
        Content = sv;
    }
}