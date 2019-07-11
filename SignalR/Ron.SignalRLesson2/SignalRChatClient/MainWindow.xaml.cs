#region snippet_MainWindowClass
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.AspNetCore.SignalR.Client;
using SignalRChatClient.Code;

namespace SignalRChatClient
{
    public partial class MainWindow : Window
    {
        HubConnection connection;
        string token;
        public MainWindow()
        {
            InitializeComponent();
            connection = new HubConnectionBuilder().WithUrl("http://localhost:57286/wechatHub").Build();


            //connection = new HubConnectionBuilder()
            //    .WithUrl("http://localhost:57286/wechatHub", options =>
            //    {
            //        options.Headers.Add("huarui", "jwtToken");
            //        options.AccessTokenProvider = () => Task.FromResult(token);
            //    })
            //    .Build();

            #region snippet_ClosedRestart
            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };
            #endregion
        }

        private async void connectButton_Click(object sender, RoutedEventArgs e)
        {
            // token = JwtBearerAuthentication.BuildJwtToken(userTextBox.Text);

            #region snippet_ConnectionOn
            connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    var newMessage = $"{user}: {message}";
                    messagesList.Items.Add(newMessage);
                });
            });

            connection.On<ChatMessage>("Recv", (body) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    var newMessage = $"{body.UserName}: {body.Content}";
                    messagesList.Items.Add(newMessage);
                });
            });
            #endregion

            try
            {
                await connection.StartAsync();
                messagesList.Items.Add("Connection started");
                connectButton.IsEnabled = false;
                sendButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                messagesList.Items.Add(ex.Message);
            }
        }

        private async void sendButton_Click(object sender, RoutedEventArgs e)
        {
            #region snippet_ErrorHandling
            try
            {
                #region snippet_InvokeAsync
                await connection.InvokeAsync("SendMessage",
                    userTextBox.Text, messageTextBox.Text);
                #endregion
            }
            catch (Exception ex)
            {
                messagesList.Items.Add(ex.Message);
            }
            #endregion
        }
    }
}
#endregion
