using AutoMapper;
using LibrarySystem.Application.Dtos.Members;
using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Application.Exceptions;

namespace LibrarySystem.Persistence.Implementations.Services
{
    internal class MemberService : IMemberService
    {
        private readonly IMemberRepository _repository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public MemberService(
            IMemberRepository repository,
            IMapper mapper,
            IBookRepository bookRepository
            )
        {
            _repository = repository;
            _mapper = mapper;
            _bookRepository = bookRepository;
        }

        public async Task<IReadOnlyList<GetAllMemberDto>> GetAllMembers()
        {
            IReadOnlyList<Member> members = await _repository.GetAllAsync();
            return _mapper.Map<IReadOnlyList<GetAllMemberDto>>(members);
        }

        public async Task<GetByIdMemberDto> GetByIdMember(long id)
        {
            Member? member = await _repository.GetByIdAsync(id, "BookMembers.Book");
            if (member is null) throw new NotFoundException("Entity not found");

            return _mapper.Map<GetByIdMemberDto>(member);
        }

        public async Task PostMember(PostMemberDto memberDto)
        {
            bool resultName = await _repository.AnyAsync(m => m.Name == memberDto.Name);
            if (resultName) throw new ConflictException("Name already exists");

            IReadOnlyList<Book> existingBooks = await _bookRepository.GetAllAsync(filter: s => memberDto.BookIds.Contains(s.Id));
            if (existingBooks.Count != memberDto.BookIds.Count)
                throw new NotFoundException("BookIds does not exists");

            _repository.Add(_mapper.Map<Member>(memberDto));
            await _repository.SaveChangesAsync();
        }

        public async Task PutMember(long id, PutMemberDto memberDto)
        {
            Member? member = await _repository.GetByIdAsync(id);
            if (member is null) throw new NotFoundException("Entity not found");

            bool resultName = await _repository.AnyAsync(m => m.Name == memberDto.Name && m.Id != id);
            if (resultName) throw new ConflictException("Name already exists");

            IReadOnlyList<Book> existingBooks = await _bookRepository.GetAllAsync(filter: s => memberDto.BookIds.Contains(s.Id));
            if (existingBooks.Count != memberDto.BookIds.Count)
                throw new NotFoundException("BookIds does not exists");

            member = _mapper.Map(memberDto, member);

            _repository.Update(member);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteMember(long id)
        {
            Member? member = await _repository.GetByIdAsync(id);
            if (member is null) throw new NotFoundException("Entity not found");

            _repository.Delete(member);
            await _repository.SaveChangesAsync();
        }

    }


}