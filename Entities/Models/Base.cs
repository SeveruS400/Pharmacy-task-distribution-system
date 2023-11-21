

using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Base
    {
        //[ForeignKey("Id")]
        public int Id { get; set; }
        //public DateTime Create { get; set; } = DateTime.Now;
        //public bool isActive { get; set; }
    }
}
