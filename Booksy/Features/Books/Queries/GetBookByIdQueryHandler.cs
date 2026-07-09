using AutoMapper;
using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Features.Books.DTOs;
using Booksy.Models.Entities.Books;

namespace Booksy.Features.Books.Queries;

/// <summary>
/// Handler for getting a book by ID
/// </summary>
public class GetBookByIdQueryHandler : IQueryHandler<GetBookByIdQuery, BookResponse>
{
    private readonly IRepository<Book> _bookRepository;
    private readonly IMapper _mapper;

    public GetBookByIdQueryHandler(
        IRepository<Book> bookRepository,
        IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<BookResponse> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetOneAsync(b => b.Id == request.Id);
        if (book == null)
        {
            throw new NotFoundException($"Book with ID {request.Id} not found");
        }

        return _mapper.Map<BookResponse>(book);
    }
}
