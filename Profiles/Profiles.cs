using AutoMapper;
using project_service.Dtos;
using project_service.Entities;

namespace project_service.Profiles
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<Todo, TodoDto>();
            //CreateMap<Book, BookDto>()
            //    .ForMember(dest => dest.borrower, opt => opt.MapFrom(src => src.borrower != null ? src.borrower : null));
            CreateMap<Student, StudentDto>().ReverseMap()
                .ForMember(dest => dest.BorrowedBooks, opt => opt.MapFrom(src => src.BorrowedBooks));
            CreateMap<BorrowedBooks, Book>().ReverseMap();
        }
    }
}
