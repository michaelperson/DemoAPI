using MahApps.Metro.Controls;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfSignalR.Models;

namespace WpfSignalR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public string Token { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Configuration du client http 
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://localhost:5001/api/");
                //Configurer la requête
                //Construire le body
                LoginModel lm = new LoginModel() { Email = this.Login, Password = this.Password };
                string dataJson = JsonSerializer.Serialize(lm);
                //Quel méthod(verb)
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "Account/Login");
                //Ajout du body
                request.Content = new StringContent(dataJson, Encoding.UTF8);
                //Content-type
                request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                //L'appel réel de l'api
                HttpResponseMessage response = client.SendAsync(request).Result; //!!! appel bloquand d'une méthode async!!!!!
                response.EnsureSuccessStatusCode(); //Throw une exception si ce n'est pas une 200

                string strreponse = response.Content.ReadAsStringAsync().Result;
                Token = strreponse;

                MeConnecterASignalr();
            }
            catch (Exception ex)
            {
                Token = "Erreur de récupération";
            }
           

        }

        private async void MeConnecterASignalr()
        {
            //Prépartion du Uri avec le token
            string url =  $"https://localhost:5001/messagehub?access_token={this.Token}";


            HubConnection connection = new HubConnectionBuilder()
                                        .WithAutomaticReconnect()
                                        .WithUrl(url, HttpTransportType.ServerSentEvents)
                                        .Build();

            //Ecouter le serveur
            connection.On<string,string>("FnClientMessage", receiveMessage);

            //Connection et déconnection
            connection.Closed += Connection_Closed;
            connection.Reconnecting += Connection_Reconnecting;
            connection.Reconnected += Connection_Reconnected;

            //LAncer la connexion
            await connection.StartAsync();
             MessageBox.Show("Connecté");

                //Communiquer vers le serveur


              await  connection.InvokeAsync("SendMessage", this.Login, "Coucou");
            
            



        }

        private Task receiveMessage(string user, string message)
        {
              Debug.WriteLine($"{user}: {message}");
            return Task.FromResult(0);
        }

        private Task Connection_Reconnected(string arg)
        {
            Debug.WriteLine("Connecté");
            return Task.FromResult(0);
        }

        private Task Connection_Reconnecting(Exception arg)
        {
            Debug.WriteLine("Connexion en cours");
            return Task.FromResult(0);
        }

        private Task Connection_Closed(Exception arg)
        {
            Debug.WriteLine("Déconnecté");
            return Task.FromResult(0);
        }
    }
}
