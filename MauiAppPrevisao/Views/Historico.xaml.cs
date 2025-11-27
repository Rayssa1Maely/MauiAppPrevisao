using Android.SE.Omapi;
using MauiAppPrevisao.Models;
using MauiAppPrevisao.Services;
using MauiAppPrevisao.Models;
using MauiAppPrevisao.Views;
using Microsoft.Maui.Controls;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MauiAppPrevisao.Views
{
    public partial class Cadastro : ContentPage
    {
        private readonly UserService _userService;

        public Cadastro(UserService userService)
        {
            InitializeComponent();
            _userService = userService;
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            var newUser = new Usuario
            {
                Nome = NameEntry.Text,     
                Email = EmailEntry.Text,   
                DataNascimento = BirthDatePicker.Date.ToString("yyyy-MM-dd") 
            };

            string rawPassword = PasswordEntry.Text; 

            try
            {
               
                await _userService.RegisterUser(newUser, rawPassword);

             
                await DisplayAlert("Sucesso", "Usuário cadastrado com sucesso! Você será redirecionado para o Login.", "OK");

               
                await Shell.Current.GoToAsync($"//{nameof(Login)}");
            }
            
            catch (ArgumentException ex)
            {
                await DisplayAlert("Erro de Validação", ex.Message, "OK");
            }
          
            catch (InvalidOperationException ex)
            {
                await DisplayAlert("Erro de Cadastro", ex.Message, "OK");
            }
            catch (Exception)
            {
                await DisplayAlert("Erro", "Ocorreu um erro inesperado ao cadastrar. Verifique sua conexão.", "OK");
            }
        }

      
        private async void OnLoginTapped(object sender, TappedEventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(Login)}");
        }
    }
}