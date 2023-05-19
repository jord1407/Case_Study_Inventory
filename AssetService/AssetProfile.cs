using AutoMapper;

namespace AssetService
{
    internal class AssetProfile : Profile
    {
        public AssetProfile() : base()
        {
            CreateMap<Models.Asset, AssetRepository.DataAccess.Asset>().ReverseMap();
        }
    }
}
