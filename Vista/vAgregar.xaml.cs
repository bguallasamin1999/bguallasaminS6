using System.Net;
using bguallasaminS6.Modelo;

namespace bguallasaminS6.Vista;

public partial class vAgregar : ContentPage
{
	public vAgregar()
	{
		InitializeComponent();
	}

    private void btnAgregar_Clicked(object sender, EventArgs e)
    {
		try
		{
            WebClient client = new WebClient();
			var parametros = new System.Collections.Specialized.NameValueCollection();
			parametros.Add("nombre", txtNombre.Text);
			parametros.Add("apellido", txtApellido.Text);
			client.UploadValues("http://localhost:7878/api/v1/personas", "POST", parametros);
			Navigation.PushAsync(new vCrud());
        }
		catch (Exception ex)
		{

			DisplayAlert("Error", "No se pudo agregar la persona: " + ex.Message, "OK");
        }
    }
}