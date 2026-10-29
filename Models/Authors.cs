using System.ComponentModel.DataAnnotations;

namespace Duceac_Ioana_Lab2.Models
{
    public class Authors
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
