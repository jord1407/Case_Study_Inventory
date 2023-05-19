using Microsoft.Extensions.Configuration;

namespace InvoiceGeneratorService
{
    public class AppSettings
    {
        public static string InvoiceAPI { get; set; } = string.Empty;

        public static string InvoiceGeneratorAPI { get; set;} = string.Empty;

        public static string AssetAPI { get; set; } = string.Empty;

        public AppSettings(IConfiguration configuration)
        {
            AssetAPI = configuration.GetSection("APIs").GetChildren().Single(x => x.Key == "AssetAPI").Value ?? string.Empty;
            InvoiceAPI = configuration.GetSection("APIs").GetChildren().Single(x => x.Key == "InvoiceAPI").Value ?? string.Empty;
            InvoiceGeneratorAPI = configuration.GetSection("APIs").GetChildren().Single(x => x.Key == "InvoiceGeneratorAPI").Value ?? string.Empty;

            ValidateProperties();
        }

        private void ValidateProperties()
        {
            var properties = GetType().GetProperties();

            foreach (var property in properties)
            {
                switch (property.Name)
                {
                    case nameof(InvoiceAPI):
                        ValidateInvoiceAPI();
                        break;
                    case nameof(AssetAPI):
                        ValidateAssetAPI();
                        break;
                    case nameof(InvoiceGeneratorAPI):
                        ValidateInvoiceGeneratorAPI();
                        break;
                }
            }
        }

        private void ValidateInvoiceGeneratorAPI()
        {
            if (InvoiceGeneratorAPI == string.Empty)
                throw new InvalidOperationException("InvoiceGeneratorAPI is required in the appsettings.json file.");
        }

        private void ValidateAssetAPI()
        {
            if (AssetAPI == string.Empty)
                throw new InvalidOperationException("AssetAPI is required in the appsettings.json file.");
        }

        private void ValidateInvoiceAPI()
        {
            if (InvoiceAPI == string.Empty)
                throw new InvalidOperationException("InvoiceAPI is required in the appsettings.json file.");
        }
    }
}
