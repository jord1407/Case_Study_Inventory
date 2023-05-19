using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNetCore.SignalR.Client;
using Shared;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace InvoiceGeneratorService.Models
{
    public abstract class BaseInvoice<T> : IInvoice
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.MinValue;

        [Required]
        public int Year
        {
            get
            {
                return Date.Year;
            }
        }

        [Required]
        public int Month
        {
            get
            {
                return Date.Month;
            }
        }

        public InvoiceTypes Type { get; }

        [Required]
        public decimal Total { get; set; } = decimal.MinValue;

        public BaseInvoice(InvoiceTypes type)
        {
            Type = type;
        }

        public abstract void GenerateInvoiceDetails(DateTime invoiceDate);


        public virtual async void NotifyClients()
        {
            //var connection = new HubConnection(AppSettings.InvoiceGeneratorAPI);
            //IHubProxy myHub = connection.CreateHubProxy("generatorhub");

            var connection = new HubConnectionBuilder()
            .WithUrl($"{AppSettings.InvoiceGeneratorAPI}/generatorhub", (opts) =>
            {
                opts.HttpMessageHandlerFactory = (message) =>
                {
                    if (message is HttpClientHandler clientHandler)
                        // always verify the SSL certificate
                        clientHandler.ServerCertificateCustomValidationCallback +=
                            (sender, certificate, chain, sslPolicyErrors) => { return true; };
                    return message;
                };
            })
            .Build();

            await connection.StartAsync();
            await connection.InvokeAsync("notifyCompletion");
            await connection.StopAsync();
        }
    }
}
