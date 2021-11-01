using Google.Apis.Books.v1;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class GoogleBookApiService
    {
        private readonly BooksService _booksService;
        public GoogleBookApiService()
        {
            _booksService = new BooksService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyBaSYsg_m - ZiP0HzGaezNy6UBq6Hc8GsvM",
                ApplicationName = this.GetType().ToString()
            });
        }
        public string ValueChanger(IList<string> ListOfElements)
        {
            string stringList = "";
            if (ListOfElements != null) {
                stringList = string.Join(",", ListOfElements);
                
            }else
            {
                return stringList;
            }
            return stringList;

        }
        public List<Book> Search( int offset)
        {
            var listquery = _booksService.Volumes.List("Subject: ART");
            listquery.MaxResults = 10;
            listquery.StartIndex = offset;
            var res = listquery.Execute();
            var books = res.Items.Select(b => new Book
            {
                Id = b.Id,
                Name = b.VolumeInfo.Title,
                Author = ValueChanger(b.VolumeInfo.Authors),
                PublishedDate = b.VolumeInfo.PublishedDate,
                Description = b.VolumeInfo.Description,
                
            }).ToList();
            return new List<Book>(books);
        }
    }
}
