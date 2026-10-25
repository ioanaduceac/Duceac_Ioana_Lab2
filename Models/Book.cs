using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Duceac_Ioana_Lab2.Models
{
    public class Book
    {
        public int ID { get; set; }
        [Display(Name = "Book Title")]
        public string title { get; set; }
        public int? AuthorsID { get; set; }
        [ForeignKey(nameof(AuthorsID))]
        public Authors? Authors { get; set; }

        [Column(TypeName = "decimal(6, 2)")]     
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishingDate { get; set; }
        public int? PublisherID { get; set; }
        public Publisher? Publisher { get; set; }
        public ICollection<BookCategory>? BookCategories { get; set; }
    } //navigation property 



}

