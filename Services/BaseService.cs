using AutoMapper;
using f00die_finder_be.Common.CacheService;
using f00die_finder_be.Common.CurrentUserService;
using f00die_finder_be.Common.FileService;
using f00die_finder_be.Common.MailService;
using f00die_finder_be.Data.UnitOfWork;

namespace f00die_finder_be.Services
{
    public class BaseService
    {
        protected readonly IMapper _mapper;
        protected readonly ICurrentUserService _currentUserService;
        protected readonly IConfiguration _configuration;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IFileService _fileService;
        protected readonly ICacheService _cacheService;
        protected readonly IMailService _mailService;

        public BaseService(IServiceProvider serviceProvider)
        {
            _mapper = serviceProvider.GetService<IMapper>();
            _currentUserService = serviceProvider.GetService<ICurrentUserService>();
            _cacheService = serviceProvider.GetService<ICacheService>();
            _configuration = serviceProvider.GetService<IConfiguration>();
            _unitOfWork = serviceProvider.GetService<IUnitOfWork>();
            _fileService = serviceProvider.GetService<IFileService>();
            _mailService = serviceProvider.GetService<IMailService>();
        }
    }
}
