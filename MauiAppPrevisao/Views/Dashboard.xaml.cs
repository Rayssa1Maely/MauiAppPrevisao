using MauiAppPrevisao.Models;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;

namespace MauiAppPrevisao.Views;

public class WeatherResponse
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("weather")]
    public List<WeatherCondition> Weather { get; set; }

    [JsonPropertyName("main")]
    public MainData Main { get; set; }
}

public class WeatherCondition
{
    [JsonPropertyName("description")]
    public string Description { get; set; }
}

public class MainData
{
    [JsonPropertyName("temp")]
    public double Temp { get; set; }
}

public partial class Dashboard : ContentPage
{
    private const string API_KEY = "96beddadbba90961a69471838c56a770";
    private readonly HttpClient _httpClient;

    public Dashboard()
    {
        InitializeComponent();
        _httpClient = new HttpClient();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Historico());
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        string cidade = txt_cidade.Text?.Trim();
 
        if (string.IsNullOrWhiteSpace(cidade))
        {
            await DisplayAlert("Erro", "Por favor, insira a cidade.", "OK");
            return;
        }

        try
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={cidade}&appid={API_KEY}&units=metric&lang=pt";

            var response = await _httpClient.GetStringAsync(url);
            var weatherData = JsonSerializer.Deserialize<WeatherResponse>(response);

            if (weatherData != null)
            {
                string temp = $"{weatherData.Main.Temp:F1}°C";
                string desc = weatherData.Weather.FirstOrDefault()?.Description ?? "N/A";
                string previsaoDetalhes = $"Temperatura: {temp}\nCondição: {desc}";

                lblPrevisao.Text = $"Previsão para {weatherData.Name}:\n{previsaoDetalhes}";
                lblPrevisao.TextColor = Colors.DarkGreen;

                Consulta historico = new Consulta
                {
                    UsuarioId = App.UsuarioLogadoId,
                    Cidade = weatherData.Name,
                    PrevisaoDetalhes = previsaoDetalhes,
                    DataConsulta = DateTime.Now 
                };

                await App.Db.SaveHistoricoAsync(historico);
            }
            else
            {
                lblPrevisao.Text = "Não foi possível obter a previsão para a cidade.";
                lblPrevisao.TextColor = Colors.Red;
            }
        }
        catch (HttpRequestException)
        {
            lblPrevisao.Text = "Cidade não encontrada ou erro de conexão (verifique sua chave API ou o nome da cidade).";
            lblPrevisao.TextColor = Colors.Red;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Erro inesperado: {ex.Message}", "OK");
        }
    }

    private async void Button_Clicked_2(object sender, EventArgs e)
    {
        App.UsuarioLogadoId = 0;
        await Navigation.PopToRootAsync();
    }

}