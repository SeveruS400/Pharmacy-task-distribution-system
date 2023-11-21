using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class EczaneBilgileri: Base
    {
        [StringLength(50)]
        [RegularExpression("^[a-zA-ZğüşıöçĞÜŞİÖÇ\\s]*$", ErrorMessage = "Bu alan sadece Türkçe harf ve boşluk karakterleri içermelidir.")]
        public string EczaneAdi { get; set; } = string.Empty;

        [StringLength(100)]
        public string EczaneAdresi { get; set; } = string.Empty;

        [StringLength(10)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Bu alan sadece rakam içermelidir.")]
        public string EczaneTelefon { get; set; } = string.Empty;

        [StringLength(50)]
        public string EczaneMail { get; set; } = string.Empty;

        public int BolgelerId { get; set; }
        public Bolgeler EczaneBolge { get; set; }
        public int SehirlerId { get; set; }
        public Sehirler Sehirler { get; set; }

        [StringLength(250)]
        public string EczaneAdresTarifi { get; set; } = string.Empty;


        [StringLength(50)]
        [RegularExpression("^[a-zA-ZğüşıöçĞÜŞİÖÇ\\s]*$", ErrorMessage = "Bu alan sadece Türkçe harf ve boşluk karakterleri içermelidir.")]
        public string EczaneSahibiAdi { get; set; } = string.Empty;

        [StringLength(50)]
        [RegularExpression("^[a-zA-ZğüşıöçĞÜŞİÖÇ\\s]*$", ErrorMessage = "Bu alan sadece Türkçe harf ve boşluk karakterleri içermelidir.")]
        public string EczaneSahibiSoyadi { get; set; } = string.Empty;

        [StringLength(11)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Bu alan sadece rakam içermelidir.")]
        public string EczaneSahibiTC {  get; set; } = string.Empty;

        [StringLength(10)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Bu alan sadece rakam içermelidir.")]
        public string EczaneSahibiTelefon { get; set; } = string.Empty;

        public int YasakliEzcaneId { get; set; } = 0;

        private double _eczaneKordinatLongitude;
        public double EczaneKordinatLongitude
        {
            get => _eczaneKordinatLongitude;
            set
            {
                // Türkiye'nin boylam aralığı kontrolü
                if (value >= 26.0 && value <= 45.0)
                {
                    _eczaneKordinatLongitude = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("EczaneKordinatLongitude, Türkiye'nin geçerli boylam aralığında olmalıdır.");
                }
            }
        }
        private double _eczaneKordinatLatitude;
        public double EczaneKordinatLatitude
        {
            get => _eczaneKordinatLatitude;
            set
            {
                // Türkiye'nin enlem aralığı kontrolü
                if (value >= 36.0 && value <= 42.1)
                {
                    _eczaneKordinatLatitude = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("EczaneKordinatLatitude, Türkiye'nin geçerli enlem aralığında olmalıdır.");
                }
            }
        }

        public static implicit operator EczaneBilgileri(List<EczaneBilgileri> v)
        {
            throw new NotImplementedException();
        }
    }
}
