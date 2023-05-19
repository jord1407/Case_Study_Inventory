using Microsoft.Extensions.Configuration;

namespace InvoiceService
{
    public class AppSettings
    {
        public static string InvoiceConnectionString { get; set; } = string.Empty;

        public AppSettings(IConfiguration configuration)
        {
            InvoiceConnectionString = configuration.GetConnectionString("Invoice") ?? string.Empty;

            ValidateProperties();
        }

        private void ValidateProperties()
        {
            var properties = GetType().GetProperties();

            foreach (var property in properties)
            {
                switch (property.Name)
                {
                    case nameof(InvoiceConnectionString):
                        ValidateInvoiceConnectionString();
                        break;
                }
            }
        }

        private void ValidateInvoiceConnectionString()
        {
            if (InvoiceConnectionString == string.Empty)
                throw new InvalidOperationException("Invoice ConnectionString is required in the appsettings.json file.");
        }
    }
}
