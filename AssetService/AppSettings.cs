using Microsoft.Extensions.Configuration;

namespace AssetService
{
    public class AppSettings
    {
        public static string? AssetConnectionString { get; set; }

        public AppSettings(IConfiguration configuration)
        {
            AssetConnectionString = configuration.GetConnectionString("Asset");

            ValidateProperties();
        }

        private void ValidateProperties()
        {
            var properties = GetType().GetProperties();

            foreach (var property in properties)
            {
                switch (property.Name)
                {
                    case nameof(AssetConnectionString):
                        ValidateAssetConnectionString();
                        break;
                }
            }
        }

        private void ValidateAssetConnectionString()
        {
            if (AssetConnectionString == string.Empty)
                throw new InvalidOperationException("Asset ConnectionString is required in the appsettings.json file.");
        }
    }
}
