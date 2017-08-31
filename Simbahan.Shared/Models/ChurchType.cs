namespace Simbahan.Models
{
    public class ChurchType
    {
        private const int CHURCH = 1;
        private const int CATHEDRAL = 2;
        private const int BASILICA = 3;
        private const int SHRINE = 4;
        private const int CHAPEL = 5;

        public static string ParseInt(int? churchTypeId)
        {
            switch (churchTypeId)
            {
                case CHURCH:
                    return "Church";

                case CATHEDRAL:
                    return "Cathedral";

                case BASILICA:
                    return "Basilica";

                case SHRINE:
                    return "Shrine";

                case CHAPEL:
                    return "Chapel";

                default: return "";
            }
        }
    }
}