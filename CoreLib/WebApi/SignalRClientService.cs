using CoreLib.Enums;
using CoreLib.Interfaces.Hubs;
using CoreLib.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.WebApi
{
    public partial class SignalRClientService : IDisposable
    {
        private string ServerIP="https://localhost:7186/ClientChat"; //example "http://10.233.42.21/ClientHub"

        public HubConnection Connection { get; set; }

        public Action Disposing { get; set; }

        public event Action<Exception> ConnectionClosed;
        public event Action ConnectionOpened;
        public event Action<string> UpdateMessageReceived;
        //public event Action<Comment, EntityAction> UpdateCommentMessageReceived;
        public event Action<Individual, EntityAction> UpdateIndividualMessageReceived;

        public SignalRClientService()
        {
            //ServerIP = serverip;
            Disposing = () =>
            {
                Connection?.DisposeAsync();
            };
        }

        /// <summary>
        /// Creates a hubconnection to the specified ServerIP.
        /// </summary>
        /// <returns>A new hubconnection that invokes receive custom and closed events.</returns>
        public async Task<HubConnection> CreateConnection()
        {
            //Initialize connection
            HubConnection connection = new HubConnectionBuilder()
                      .AddJsonProtocol(a =>
                      {
                          a.PayloadSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                          //    a.PayloadSerializerOptions.PropertyNameCaseInsensitive = true;
                      })
                .WithUrl(ServerIP)
                .Build();
            //Map events
            connection.ServerTimeout = TimeSpan.FromSeconds(2);
            connection.On<string>(nameof(IClientHubClient.ReceiveUpdateMessage), (message) => UpdateMessageReceived?.Invoke(message));
            connection.On<Individual, EntityAction>(nameof(IClientHubClient.ReceiveIndividualUpdateMessage), (category, action) => UpdateIndividualMessageReceived?.Invoke(category, action));
            //connection.On<Comment, EntityAction>(nameof(IClientHubClient.ReceiveCommentUpdateMessage), (comment, action) => UpdateCommentMessageReceived?.Invoke(comment, action));
            //Forward invocation of inner event to service event.
            Func<Exception, Task> connectionClosed = async (e) =>
            {
                ConnectionClosed?.DynamicInvoke(e);
            };
            connection.Closed += connectionClosed;
            Disposing += () => { connection.Closed -= connectionClosed; };
            return connection;
        }

        public async Task Disconnect()
        {
            //keep trying until we manage to connect
            while (true)
            {
                try
                {
                    if (Connection != null)
                    {
                        await Connection.StopAsync().ContinueWith(task => {
                            if (task.Exception != null)
                            {
                            }
                        });
                        break; // yay! connected
                    }
                }
                catch (Exception e)
                { /* bugger! */

                }
            }
            return;
        }

        /// <summary>
        /// Attempts to connect to the server with the Connection property.
        /// *NOTE* If the <see cref="Connection"/> is null then a new connection is instantiated.
        /// </summary>
        /// <returns>Task result?</returns>
        public async Task Connect()
        {
            //keep trying until we manage to connect
            while (true)
            {
                try
                {
                    if (Connection == null)
                    {
                        Connection = await CreateConnection();
                    }
                    Connection.StartAsync().ContinueWith(task => {
                        if (task.Exception != null)
                        {
                            ConnectionClosed?.DynamicInvoke(task.Exception);
                        }
                        else
                        {
                            ConnectionOpened?.DynamicInvoke();
                        }
                    });
                    break; // yay! connected
                }
                catch (Exception e)
                { /* bugger! */

                }
            }
            return;
        }

        public async Task SendUpdateMessage(string message)
        {
            await Connection.SendAsync("SendUpdateMessage", message);
        }

        public void Dispose()
        {
            Disposing();
        }
    }
}
