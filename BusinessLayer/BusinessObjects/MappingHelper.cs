using AutoMapper;
using BusinessLayer.Managers;
using DataLayer.DTO;
using DataLayer.Repositories;
using Microsoft.VisualBasic.FileIO;

namespace BusinessLayer.BusinessObjects
{
    public static class MappingHelper
    {
        private static IMapper _mapper;

        public static void SetMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public static UserDto ToDto(this User user)
        {
            return _mapper.Map<UserDto>(user);
        }

        public static User ToBo(this UserDto user)
        {
            return _mapper.Map<User>(user);
        }

        public static BookDto ToDto(this Book user)
        {
            return _mapper.Map<BookDto>(user);
        }

        public static Book ToBo(this BookDto user)
        {
            return _mapper.Map<Book>(user);
        }

        public static BorrowingDto ToDto(this Borrowing user)
        {
            return _mapper.Map<BorrowingDto>(user);
        }

        public static Borrowing ToBo(this BorrowingDto user)
        {
            return _mapper.Map<Borrowing>(user);
        }

        public static FindTypeDb ToDto(this FindType findType)
        {
            return _mapper.Map<FindTypeDb>(findType);
        }

        public static FindType ToBo(this FindTypeDb findType)
        {
            return _mapper.Map<FindType>(findType);
        }
    }
}
