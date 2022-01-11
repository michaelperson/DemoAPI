using MahApps.Metro.Controls;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfSignalR.Tools.Infrastructures.Constants;
using WpfSignalR.Tools.Infrastructures.Interfaces;

namespace WpfSignalR.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        public MainWindow(IRegionManager regionManager)
        {
            InitializeComponent();

            if (regionManager != null)
            {
                SetRegionManager(regionManager, this.leftWindowCommandsRegion, RegionNames.LeftWindowCommandsRegion);
                SetRegionManager(regionManager, this.rightWindowCommandsRegion, RegionNames.RightWindowCommandsRegion);
                SetRegionManager(regionManager, this.flyoutsControlRegion, RegionNames.FlyoutRegion);
            }
 

        }

        #region Private
        private void SetRegionManager(IRegionManager regionManager, DependencyObject regionTarget, string regionName)
        {
            RegionManager.SetRegionName(regionTarget, regionName);
            RegionManager.SetRegionManager(regionTarget, regionManager);
        }
        #endregion

        #region Obsolete
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        //Configuration du client http 
        //        HttpClient client = new HttpClient();
        //        client.BaseAddress = new Uri("https://localhost:5001/api/");
        //        //Configurer la requête
        //        //Construire le body
        //        LoginModel lm = new LoginModel() { Email = this.Login, Password = this.Password };
        //        string dataJson = JsonSerializer.Serialize(lm);
        //        //Quel méthod(verb)
        //        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "Account/Login");
        //        //Ajout du body
        //        request.Content = new StringContent(dataJson, Encoding.UTF8);
        //        //Content-type
        //        request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        //        //L'appel réel de l'api
        //        HttpResponseMessage response = client.SendAsync(request).Result; //!!! appel bloquand d'une méthode async!!!!!
        //        response.EnsureSuccessStatusCode(); //Throw une exception si ce n'est pas une 200

        //        string strreponse = response.Content.ReadAsStringAsync().Result;
        //        Token = strreponse;

        //        MeConnecterASignalr();
        //    }
        //    catch (Exception ex)
        //    {
        //        Token = "Erreur de récupération";
        //    }


        //}

        //private async void MeConnecterASignalr()
        //{
        //    //Prépartion du Uri avec le token
        //    string url = $"https://localhost:5001/messagehub?access_token={this.Token}";


        //    HubConnection connection = new HubConnectionBuilder()
        //                                .WithAutomaticReconnect()
        //                                .WithUrl(url, HttpTransportType.ServerSentEvents)
        //                                .Build();

        //    //Ecouter le serveur
        //    connection.On<string, string>("FnClientMessage", receiveMessage);

        //    //Connection et déconnection
        //    connection.Closed += Connection_Closed;
        //    connection.Reconnecting += Connection_Reconnecting;
        //    connection.Reconnected += Connection_Reconnected;

        //    //LAncer la connexion
        //    await connection.StartAsync();
        //    MessageBox.Show("Connecté");

        //    //Communiquer vers le serveur


        //    await connection.InvokeAsync("SendMessage", this.Login, "Coucou");





        //}

        //private Task receiveMessage(string user, string message)
        //{
        //    Debug.WriteLine($"{user}: {message}");
        //    return Task.FromResult(0);
        //}

        //private Task Connection_Reconnected(string arg)
        //{
        //    Debug.WriteLine("Connecté");
        //    return Task.FromResult(0);
        //}

        //private Task Connection_Reconnecting(Exception arg)
        //{
        //    Debug.WriteLine("Connexion en cours");
        //    return Task.FromResult(0);
        //}

        //private Task Connection_Closed(Exception arg)
        //{
        //    Debug.WriteLine("Déconnecté");
        //    return Task.FromResult(0);
        //} 
        #endregion
    }
}
