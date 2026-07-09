using AutoMapper;
using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Features.Authentication.DTOs;
using Booksy.Models.Entities.Books;
using Booksy.Repositories.IRepositories;

namespace Booksy.Features.Authors.Queries;

/// <summary>
/// Handler for getting an author by ID
/// </summary>
public class GetAuthorByIdQueryHandler : IQueryHandler<GetAuthorByIdQuery, AuthorResponse>
{
    private readonly IRepository<Author> _repository;
    private readonly IMapper _mapper;

    public GetAuthorByIdQueryHandler(
        IRepository<Author> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<AuthorResponse> Handle(
        GetAuthorByIdQuery request,
        CancellationToken cancellationToken)
    {
        var author = await _repository.GetByIdAsync(request.Id);
        if (author == null)
        {
            throw new NotFoundException($"Author with ID {request.Id} not found");
        }

        return _mapper.Map<AuthorResponse>(author);
    }
}

