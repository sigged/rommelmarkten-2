using Rommelmarkten.Api.Application.Common.Interfaces;
using Rommelmarkten.Api.Domain.Affiliates;
using Rommelmarkten.Api.Domain.Content;
using Rommelmarkten.Api.Domain.Markets;
using Rommelmarkten.Api.Domain.Users;
using Rommelmarkten.Api.Infrastructure.Persistence;
using System.Data.Common;
using System.Text;

namespace V2Importer.Importers
{

    public partial class Importer
    {
        private readonly ApplicationDbContext target;
        private readonly ICurrentUserService currentUserService;
        private readonly IDateTime dateTimeService;
        private readonly ICurrentUserService alexAdminCurrentUserService;

        private readonly IEntityRepository<UserProfile> profileRepository;

        private readonly IEntityRepository<AffiliateAd> affiliateAdRepository;
        private readonly IEntityRepository<BannerType> bannerTypeRepository;
        private readonly IEntityRepository<FAQCategory> faqCategoryRepository;
        private readonly IEntityRepository<FAQItem> faqItemRepository;
        private readonly IEntityRepository<Province> provinceRepository;
        private readonly IEntityRepository<MarketConfiguration> marketConfigRepository;
        private readonly IEntityRepository<MarketTheme> marketThemeRepository;

        private readonly IEntityRepository<Market> marketRepository;
        private readonly IEntityRepository<MarketDate> marketDateRepository;
        private readonly IEntityRepository<MarketInvoiceLine> marketInvoiceLineRepository;
        private readonly IEntityRepository<MarketInvoiceReminder> marketInvoiceReminderRepository;
        private readonly IEntityRepository<MarketInvoice> marketInvoiceRepository;
        private readonly IEntityRepository<MarketRevision> marketRevisionRepository;
        private readonly IEntityRepository<MarketRevisionWithTheme> marketRevisionWithThemeRepository;
        private readonly IEntityRepository<MarketWithTheme> marketWithThemeRepository;

        public Importer(
            ApplicationDbContext target, 
            ICurrentUserService currentUserService,
            IDateTime dateTimeService,
            
            IEntityRepository<UserProfile> profileRepository,

            IEntityRepository<AffiliateAd> affiliateAdRepository,
            IEntityRepository<BannerType> bannerTypeRepository,
            IEntityRepository<FAQCategory> faqCategoryRepository,
            IEntityRepository<FAQItem> faqItemRepository,
            IEntityRepository<Province> provinceRepository,
            IEntityRepository<MarketConfiguration> marketConfigRepository,
            IEntityRepository<MarketTheme> marketThemeRepository,

            IEntityRepository<Market> marketRepository,
            IEntityRepository<MarketDate> marketDateRepository,
            IEntityRepository<MarketInvoiceLine> marketInvoiceLineRepository,
            IEntityRepository<MarketInvoiceReminder> marketInvoiceReminderRepository,
            IEntityRepository<MarketInvoice> marketInvoiceRepository,
            IEntityRepository<MarketRevision> marketRevisionRepository,
            IEntityRepository<MarketRevisionWithTheme> marketRevisionWithThemeRepository,
            IEntityRepository<MarketWithTheme> marketWithThemeRepository
            )
        {
            this.target = target;
            this.currentUserService = currentUserService;
            this.dateTimeService = dateTimeService;

            this.profileRepository = profileRepository;

            this.affiliateAdRepository = affiliateAdRepository;
            this.bannerTypeRepository = bannerTypeRepository;
            this.faqCategoryRepository = faqCategoryRepository;
            this.faqItemRepository = faqItemRepository;
            this.provinceRepository = provinceRepository;
            this.marketConfigRepository = marketConfigRepository;
            this.marketThemeRepository = marketThemeRepository;

            this.marketRepository = marketRepository;
            this.marketDateRepository = marketDateRepository;
            this.marketInvoiceLineRepository = marketInvoiceLineRepository;
            this.marketInvoiceReminderRepository = marketInvoiceReminderRepository;
            this.marketInvoiceRepository = marketInvoiceRepository;
            this.marketRevisionRepository = marketRevisionRepository;
            this.marketRevisionWithThemeRepository = marketRevisionWithThemeRepository;
            this.marketWithThemeRepository = marketWithThemeRepository;

            alexAdminCurrentUserService = new AlexAdminCurrentUserService();
        }

        public async Task Import(DbConnection source)
        {
            await ImportUsers(source);
            await ImportRoles(source);
            await ImportUserRoles(source);

            await ImportAffiliateAds(source);
            await ImportBannerTypes(source);
            await ImportFAQCategory(source);
            await ImportFAQItems(source);
            await ImportProvinces(source);
            await ImportMarketConfigurations(source);
            await ImportMarketThemes(source);

            await ImportMarkets(source);
            await ImportMarketDates(source);
            await ImportMarketInvoices(source);
            await ImportMarketInvoiceReminders(source);
            await ImportMarketAndRevisionWithTheme(source);
        }

        private string? Normalize(string input)
        {
            return input?.Normalize(NormalizationForm.FormKD).ToLowerInvariant();
        }

        private Guid LongToGuid(long value)
        {
            byte[] bytes = new byte[16];
            var longbytes = BitConverter.GetBytes(value).Reverse().ToArray();
            longbytes.CopyTo(bytes, 0);
            return new Guid(bytes);
        }

        private long GuidToLong(Guid value)
        {
            byte[] bytes = value.ToByteArray().Reverse().ToArray();
            long result = BitConverter.ToInt64(bytes, 8);
            return result;
        }

        private Guid StringToGuid(string value)
        {
            byte[] bytes = new byte[16];
            Encoding.UTF8.GetBytes(value).Take(16).ToArray().CopyTo(bytes, 0);
            return new Guid(bytes);
        }

        private string GuidToString(Guid value)
        {
            byte[] bytes = value.ToByteArray();
            return Encoding.UTF8.GetString(bytes).Replace('\0', ' ').Trim();
        }
    }
}
