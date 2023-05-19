using AutoMapper;
using InvoiceService.Models;
using System.Text;
using System.Text.Json;

namespace InvoiceService
{
    internal class InvoiceProfile : Profile
    {
        public InvoiceProfile() : base()
        {
            CreateMap<Models.ServiceInvoice, InvoiceRepository.DataAccess.Invoice>()
                .ForMember(vm => 
                    vm.Data, m => m.MapFrom((u, _) => Encoding.ASCII.GetBytes(JsonSerializer.Serialize(u.Assets))));

            CreateMap<InvoiceRepository.DataAccess.Invoice, Models.ServiceInvoice>()
                .ForMember(vm =>
                    vm.Assets, m => m.MapFrom((u, _) => JsonSerializer.Deserialize<List<Asset>>(Encoding.ASCII.GetString(u.Data ?? new byte[0]))));
        }
    }
}
