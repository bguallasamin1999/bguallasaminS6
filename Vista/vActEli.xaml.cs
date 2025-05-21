using bguallasaminS6.Modelo;

namespace bguallasaminS6.Vista;

public partial class vActEli : ContentPage
{
	public vActEli(Persona datos)
	{
		InitializeComponent();
        txtId.Text = datos.id.ToString();
        txtNombre.Text = datos.nombre;
        txtApellido.Text = datos.apellido;
        txtEdad.Text = datos.edad.ToString();
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtId.Text))
        {
            await DisplayAlert("Validación", "Debe ingresar un ID para eliminar.", "OK");
            return;
        }

        try
        {
            string id = txtId.Text;
            using (HttpClient client = new HttpClient())
            {
                var response = await client.DeleteAsync($"http://localhost:7878/api/v1/personas/{id}");

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Éxito", "Persona eliminada correctamente.", "OK");
                    await Navigation.PushAsync(new vCrud());
                }
                else
                {
                    await DisplayAlert("Error", $"No se pudo eliminar: {response.StatusCode}", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Ocurrió un error: " + ex.Message, "OK");
        }
    }
    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtId.Text) ||
            string.IsNullOrWhiteSpace(txtNombre.Text) ||
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
                id = int.Parse(txtId.Text),
                nombre = txtNombre.Text,
                apellido = txtApellido.Text,
                edad = int.Parse(txtEdad.Text)
            };

            string json = System.Text.Json.JsonSerializer.Serialize(persona);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                var response = await client.PutAsync($"http://localhost:7878/api/v1/personas/{persona.id}", content);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Éxito", "Persona actualizada correctamente.", "OK");
                    await Navigation.PushAsync(new vCrud());
                }
                else
                {
                    await DisplayAlert("Error", $"No se pudo actualizar: {response.StatusCode}", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Error al actualizar: " + ex.Message, "OK");
        }
    }

}