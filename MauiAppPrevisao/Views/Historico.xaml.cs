using MauiAppPrevisao.Models;
using Microsoft.Maui.Controls;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MauiAppPrevisao.Views
{
    public partial class Historico : ContentPage
    {
        public Historico()
        {
            InitializeComponent();

            StartDatePicker.Date = DateTime.Today.AddDays(-7);
            EndDatePicker.Date = DateTime.Today;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadHistoryAsync(StartDatePicker.Date, EndDatePicker.Date);
        }

        private async Task LoadHistoryAsync(DateTime start, DateTime end)
        {
            if (App.UsuarioLogadoId == 0)
            {
                await DisplayAlert("Erro", "Nenhum usuário logado. Retornando ao Login.", "OK");
                await Navigation.PopToRootAsync();
                return;
            }

            try
            {
                var historico = await App.Db.GetHistoricoByDateRangeAsync(App.UsuarioLogadoId, start, end);

                HistoryListView.ItemsSource = historico.ToList();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro ao Carregar Histórico", ex.Message, "OK");
            }
        }

        private async void OnFilterClicked(object sender, EventArgs e)
        {
            if (StartDatePicker.Date > EndDatePicker.Date)
            {
                await DisplayAlert("Erro de Filtro", "A Data Inicial não pode ser posterior à Data Final.", "OK");
                return;
            }

            await LoadHistoryAsync(StartDatePicker.Date, EndDatePicker.Date);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}