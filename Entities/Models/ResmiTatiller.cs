
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class ResmiTatiller : Base
    {
        [Required(ErrorMessage = "Enter the issued date.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Tarih {  get; set; }

        [StringLength(50)]
        public string Acıklama { get; set; }

        [Required(ErrorMessage = "Enter the issued date.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime IslemTarihi { get; set; }
    }
}
