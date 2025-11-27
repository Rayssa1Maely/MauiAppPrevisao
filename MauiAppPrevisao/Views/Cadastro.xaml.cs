using MauiAppPrevisao.Models;
using System;

namespace MauiAppPrevisao.Views;

public partial class Cadastro : ContentPage
{
    public Cadastro()
    {
        InitializeComponent();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            Usuario usuario = new Usuario
            {
                Nome = txt_nome.Text,
                Email = txt_email.Text,
                DataNascimento = txt_Dnas.Date.ToString("yyyy-MM-dd"),
                Senha = txt_senha.Text
            };

            await App.Db.Insert(usuario);
            App.UsuarioLogadoId = usuario.Id;

            await DisplayAlert("Sucesso!", "Usuário cadastrado com sucesso! Seja bem vindo(a)!!", "OK");

            await Navigation.PushAsync(new Dashboard());

        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new Login());
    }
}