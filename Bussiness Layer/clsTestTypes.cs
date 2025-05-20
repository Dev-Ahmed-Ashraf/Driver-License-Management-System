using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer
{
    public class clsTestTypes
    {
        public enum enTestType {VisionTest = 1, WrittenTest = 2, StreetTest = 3};
        public int TestTypeID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Fees { get; set; }

        public clsTestTypes(int testTypeID, string title, string description, float fees)
        {
            this.Title = title;
            this.Description = description;
            this.Fees = fees;
            this.TestTypeID = testTypeID;
        }

        public static DataTable GetAllTestTypes()
        {
            return (clsTestTypesData.GetAllTestTypes());
        }
        public static clsTestTypes Find(int ID)
        {
            string TestTitle = "", TestDescription = ""; float TestFees = 0.0f;

            if (clsTestTypesData.GetTestTypeInfoByID(ID, ref TestTitle, ref TestDescription, ref TestFees))
                return new clsTestTypes(ID, TestTitle, TestDescription, TestFees);
            else
                return null;
        }
        public bool UpdateTestType()
        {
            if (clsTestTypesData.UpdateTestType(this.TestTypeID, this.Title, this.Description, this.Fees))
            {
                return true;
            }
            return false;
        }
    }
}
