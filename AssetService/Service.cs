using AssetRepository;
using AssetService.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace AssetService
{
    public class Service : IService
    {
        private AssetContext _context { get; set; }

        private IMapper _mapper;

        public Service(DbContext context)
        {
            _context = (AssetContext)context;

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AssetProfile>();
            });

            _mapper = configuration.CreateMapper();
        }

        public List<Asset> ReadAll()
        {
            return _context.Assets
                .Select(_mapper.Map<AssetRepository.DataAccess.Asset, Asset>)
                .ToList();
        }

        public Asset Read(int id)
        {
            return _context.Assets
                .Where(x => x.Id == id)
                .Select(_mapper.Map<AssetRepository.DataAccess.Asset, Asset>)
                .Single();
        }

        /// <summary>
        /// Get All eligible services that matches the criteria:
        /// - Valid since the beginning
        /// - Valid as from the date passed as argument (inclusive)
        /// - Valid forever
        /// - Valid until the date passed as argument (inclusive)
        /// </summary>
        /// <param name="effectiveDate">Effective Date</param>
        /// <returns></returns>
        public List<Asset> GetEligibleAssets(DateTime effectiveDate)
        {
            return _context.Assets
                .Where(x =>
                    // Get all services valid as from
                    (
                        x.ValidFrom == null || // the beginning
                        x.ValidFrom <= effectiveDate // the date passed as argument
                    ) &&
                    // Get all services valid until
                    (
                        x.ValidTo == null || //forever
                        x.ValidTo >= effectiveDate // the date passed as argument
                    ))
                .Select(_mapper.Map<AssetRepository.DataAccess.Asset, Asset>)
                .ToList();
        }

        public Asset Create(Asset asset)
        {
            AssetRepository.DataAccess.Asset assetDb = _mapper.Map<AssetRepository.DataAccess.Asset>(asset);

            _context.Assets.Add(assetDb);
            _context.SaveChanges();

            return asset;
        }

        public Asset Modify(Asset asset)
        {
            AssetRepository.DataAccess.Asset assetDb = _context.Assets.Single(x => x.Id == asset.Id);

            _mapper.Map(asset, assetDb);
            _context.SaveChanges();

            return asset;
        }

        public void Delete(int id)
        {
            AssetRepository.DataAccess.Asset assetDb = _context.Assets.Single(x => x.Id == id);

            _context.Assets.Remove(assetDb);
            _context.SaveChanges();
        }
    }
}
