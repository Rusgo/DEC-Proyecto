namespace AppTp.Pantallas;

public partial class NewPage1 : ContentPage
{
    private List<List<Entry>> entradas = new List<List<Entry>>();
    int filas;
    int columnas;
    string metodo;
    List<float> pesos;
    List<bool> maxmin;

    public NewPage1(int rows, int columns, List<bool> maxmin, List<float> pesos, string metodo)
    {
        InitializeComponent();
        this.filas = rows;
        this.columnas = columns;
        this.metodo = metodo;
        this.maxmin = maxmin;
        this.pesos = pesos; 
        ToolbarItem nextToolbarItem = new ToolbarItem
        {
            Text = "Siguiente",
            Priority = 0, // Prioridad para la posición en la barra de herramientas
            Order = ToolbarItemOrder.Primary, // Orden primario en la barra de herramientas
        };
        nextToolbarItem.Clicked += OnNextClicked;

        ToolbarItems.Add(nextToolbarItem);
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
            List<Entry> entryActual = new List<Entry>();
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
                else if (i != 0)
                {
                    Entry nueva = new Entry { VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
                    entryActual.Add(nueva);
                    grid.Add(nueva, j, i);
                }
            }
            if (i != 0) { entradas.Add(entryActual); }
                

        }
        // Agregar el Grid a tu página
        ScrollView sv = new ScrollView();
        sv.Orientation = ScrollOrientation.Vertical;
        sv.Content = grid;
        Content = sv;
    }
        private void OnNextClicked(object sender, EventArgs e)
        {
        float[,] matriz = new float[filas, columnas];
        int i = -1;
        foreach(List<Entry> Lentry in entradas)
        {
            i++;
            for(int j = 0; j < columnas; j++)
            {
                matriz[i, j] = float.Parse(Lentry[j].Text);
            }
        }
        if (metodo == "Ponderaciòn Lineal")
        {
            Metodos.PonderacionLineal pl = new Metodos.PonderacionLineal(matriz, pesos, maxmin, true);
            pl.resolver();
            Navigation.PushAsync(new Resultados(pl));
        }
        else if (metodo == "MOORA")
        {
            Metodos.Moora moora = new Metodos.Moora(matriz, pesos, maxmin, false);
            moora.resolver();
            Navigation.PushAsync(new Resultados(moora));
        }
        if (metodo == "MOORA con referencia")
        {
            Metodos.MooraPuntoRef moora = new Metodos.MooraPuntoRef(matriz, pesos, maxmin, false);
            moora.resolver();
            Navigation.PushAsync(new Resultados(moora));
        }
        if (metodo == "TOPSIS")
        {
            Metodos.Topsis tp = new Metodos.Topsis(matriz, pesos, maxmin, false);
            tp.resolver();
            Navigation.PushAsync(new Resultados(tp));
        }
    }
    
}