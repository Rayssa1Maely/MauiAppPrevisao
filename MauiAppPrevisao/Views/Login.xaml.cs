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

            if (resultado == null || resultado.Count == 0)
            {
                await DisplayAlert("Erro", "Email ou senha incorretos!", "OK");
                return;
            }

            App.UsuarioLogadoId = resultado.First().Id;

            await DisplayAlert("Sucesso!", $"Olá, {resultado.First().Nome}!", "OK");
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