using System.Net;
using bguallasaminS6.Modelo;

namespace bguallasaminS6.Vista;

public partial class vAgregar : ContentPage
{
	public vAgregar()
	{
		InitializeComponent();
	}

    private async void btnAgregar_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
            string.IsNullOrWhiteSpace(txtApellido.Text) ||
            string.IsNullOrWhiteSpace(txtEdad.Text))
        {
            await DisplayAlert("Validación", "Todos los campos son obligatorios.", "OK");
            return;
        }

        try
        {
            var persona = new
            {
                nombre = txtNombre.Text,
                apellido = txtApellido.Text,
                edad = int.Parse(txtEdad.Text)
            };

            string json = System.Text.Json.JsonSerializer.Serialize(persona);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                var response = await client.PostAsync("http://localhost:7878/api/v1/personas", content);

                if (response.IsSuccessStatusCode)
                {
                    await Navigation.PushAsync(new vCrud());
                }
                else
                {
                    await DisplayAlert("Error", $"Error del servidor: {response.StatusCode}", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "No se pudo agregar la persona: " + ex.Message, "OK");
        }
    }

}