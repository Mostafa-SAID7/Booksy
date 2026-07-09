using AutoMapper;
using Booksy.Core.Interfaces;
using Booksy.Features.Authentication.DTOs;
using Booksy.Repositories.IRepositories;

namespace Booksy.Features.Authors.Queries
{
    /// <summary>
    /// Handler for retrieving an author by slug
    /// </summary>
    public class GetAuthorBySlugQueryHandler : IQueryHandler<GetAuthorBySlugQuery, AuthorResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAuthorBySlugQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AuthorResponse> Handle(GetAuthorBySlugQuery request, CancellationToken cancellationToken)
        {
            var author = await _unitOfWork.Authors.GetOneAsync(
                a => a.Slug.ToLower() == request.Slug.ToLower() && !a.IsDeleted);

            if (author == null)
            {
                throw new KeyNotFoundException($"Author with slug '{request.Slug}' not found");
            }

            return _mapper.Map<AuthorResponse>(author);
        }
    }
}
