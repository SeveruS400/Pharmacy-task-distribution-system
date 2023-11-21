
namespace Entities.Models
{
    public class NobetDagilim : Base
    {
        public int EczaneId { get; set; }
        public int YasaklıEczaneId { get; set; } = 0;
        public int NobetTuruId { get; set; }
        public NobetTurleri NobetTuru { get; set; }
        public int NobetSayisi { get; set; }= 0;
    }
}
