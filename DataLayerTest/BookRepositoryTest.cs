using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;
using DataLayer.DTO;

namespace DataLayerTest
{
    [TestClass]
    public class BookRepositoryTest : BaseRepositoryTest<BookDto>
    {
        protected override IRepository<BookDto> getRepository()
        {
            return new BookRepository(CONNECTION_STRING);
        }

        protected override BookDto GetEntity()
        {
            return new BookDto
            {
                Author = "Test Author Insert",
                NumberOfBorrowed = 0,
                NumberOfLicences = 5,
                NumberOfPages = 200,
                Title = "Test Title Insert",
                YearOfPublication = 2022
            };
        }

        [TestMethod]
        public void BookRepository_FindExisting_ReturnsEntities()
        {
            var book = GetEntity();
            repository.Add(book);

            var result = ((IBookRepository)repository).Find(DbFindType.AND, string.Empty, "Ins", 0, null);

            Assert.IsTrue(result.Any());
        }

        protected override void updateEntity(BookDto entity)
        {
            entity.Author = "Test Author Update";
            entity.YearOfPublication = 2010;
        }

        protected override void assertAfterUpdate(BookDto entity)
        {
            Assert.AreEqual("Test Author Update", entity.Author);
            Assert.AreEqual(2010, entity.YearOfPublication);
        }
    }
}
