using System.Collections.ObjectModel;
using bguallasaminS6.Modelo;
using Newtonsoft.Json;

namespace bguallasaminS6.Vista;

public partial class vCrud : ContentPage
{	private const string url = "http://localhost:7878/api/v1/personas";
	private HttpClient client = new HttpClient();
	private ObservableCollection<Persona> _personaGrid;
    public vCrud()
	{
		InitializeComponent();
		MostrarPersonas();
    }
	public async void MostrarPersonas()
    {
        var contenido = await client.GetStringAsync(url);
		List<Persona> listaPersona = JsonConvert.DeserializeObject<List<Persona>>(contenido);
        _personaGrid = new ObservableCollection<Persona>(listaPersona);
		listView.ItemsSource = _personaGrid;
    }

    private void btnAgregarPersona_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new vAgregar());
    }

    private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var objPersona = (Persona)e.SelectedItem;
        Navigation.PushAsync(new vActEli(objPersona));
    }
}