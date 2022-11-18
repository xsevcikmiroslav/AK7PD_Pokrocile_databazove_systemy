using AutoMapper;
using BusinessLayer.BusinessObjects;
using BusinessLayer.Managers;
using DataLayer.DTO;
using DataLayer.Repositories;
using MongoDB.Bson;

namespace BusinessLayer.Automapper
{
    public class AutomapperConfiguration : Profile
    {
        public AutomapperConfiguration()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest._id, opt => opt.MapFrom(src => GetObjectIdStringOrDefault(src._id)))
                .ReverseMap()
                .ForMember(dest => dest._id, opt => opt.MapFrom(src => src._id.ToString()));
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest._id, opt => opt.MapFrom(src => GetObjectIdStringOrDefault(src._id)))
                .ReverseMap()
                .ForMember(dest => dest._id, opt => opt.MapFrom(src => src._id.ToString()));
            CreateMap<Borrowing, BorrowingDto>()
                .ForMember(dest => dest._id, opt => opt.MapFrom(src => GetObjectIdStringOrDefault(src._id)))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => GetObjectIdStringOrDefault(src.UserId)))
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => GetObjectIdStringOrDefault(src.BookId)))
                .ReverseMap()
                .ForMember(dest => dest._id, opt => opt.MapFrom(src => src._id.ToString()))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId.ToString()))
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId.ToString()));
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<FindType, DbFindType>().ReverseMap();
        }

        private ObjectId GetObjectIdStringOrDefault(string id)
        {
            return
                string.IsNullOrEmpty(id)
                ? ObjectId.Empty
                : ObjectId.Parse(id);
        }
    }
}
