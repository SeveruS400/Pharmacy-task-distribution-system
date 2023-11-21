
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class Nobetler : Base
    {
        public int EczaneBilgileriId { get; set; }
        public EczaneBilgileri EczaneBilgileri { get; set; }
        public string NobetTuru { get; set; }
        //public int NobetTuruId { get; set; }
        //public NobetTuru NobetTuru { get; set; }

        [Required(ErrorMessage = "Enter the issued date.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Tarih { get; set; }
        public string Acıklama { get; set; }=string.Empty;

    }
}