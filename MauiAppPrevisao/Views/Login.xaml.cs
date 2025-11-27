using MauiAppPrevisao.Models;

namespace MauiAppPrevisao.Views;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            var resultado = await App.Db.Search(txt_email.Text, txt_senha.Text);
            if(resultado.Count == 0)
            {
                await DisplayAlert("Erro", "Email ou senha incorretos!", "OK");
                return;
            }

            await DisplayAlert("Sucesso!", "Olá", "OK");
            await Navigation.PushAsync(new Dashboard());

        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new Cadastro());
    }
}