namespace EzcaneBilgiSistemi.Settings
{
    public class MesafeHesaplayici
    {
        private const double EarthRadius = 6371; // Dünya'nın yarıçapı in kilometers

        public static double Haversine(double lat1, double lon1, double lat2, double lon2)
        {
            // Latitude ve longitude değerlerini radyan cinsinden al
            double dLat = ToRadians(lat2 - lat1);
            double dLon = ToRadians(lon2 - lon1);

            // Haversine formülü
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return EarthRadius * c; // Mesafe, km cinsinden
        }

        private static double ToRadians(double degree)
        {
            return degree * (Math.PI / 180);
        }
    }

}
