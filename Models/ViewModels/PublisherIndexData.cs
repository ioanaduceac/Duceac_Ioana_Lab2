using Duceac_Ioana_Lab2.Models;

namespace Duceac_Ioana_Lab2.Models.ViewModels
{
    public class PublisherIndexData
    {
        public IEnumerable<Publisher> Publishers { get; set; } 
        public IEnumerable<Book> Books { get; set; }
    }
}
