using Data_Access_Layer;
using System.Data;

namespace Bussiness_Layer
{
    public class clsCountry
    {
        public int ID { set; get; }
        public string CountryName { set; get; }

        public clsCountry()
        {
            this.ID = -1;
            this.CountryName = "";
        }
        private clsCountry(int ID, string CountryName)
        {
            this.ID = ID;
            this.CountryName = CountryName;
        }

        public static DataTable GetCountries()
        {
            return clsCountriesData.GetCountries();
        }

        public static clsCountry Find(int ID)
        {
            string CountryName = "";

            if (clsCountriesData.GetCountryInfoByID(ID, ref CountryName))
                return new clsCountry(ID, CountryName);
            else
                return null;
        }

        public static clsCountry Find(string CountryName)
        {
            int ID = -1;

            if (clsCountriesData.GetCountryInfoByName(CountryName, ref ID))

                return new clsCountry(ID, CountryName);
            else
                return null;
        }
    }
}
