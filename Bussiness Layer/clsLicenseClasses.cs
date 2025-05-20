using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer
{
    public class clsLicenseClasses
    { 
        public int LicenseClass_ID {  get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public int MinimumAllowedAge { get; set; }
        public byte ValidityLength { get; set; }
        public float ClassFees { get; set; }
         
        public static DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassesData.GetAllLicenseClass();
        }

        public clsLicenseClasses()
        {
            this.LicenseClass_ID = -1;
            this.ClassName = string.Empty;
            this.ClassDescription = string.Empty;
            this.MinimumAllowedAge = 0;
            this.ValidityLength = 0;
            this.ClassFees = 0; 
        }
        public clsLicenseClasses(int licenseClass_ID, string className, string classDescription, int minimumAllowedAge, byte validityLength, float classFees)
        {
            this.LicenseClass_ID = licenseClass_ID;
            this.ClassName = className;
            this.ClassDescription = classDescription;
            this.MinimumAllowedAge = minimumAllowedAge;
            this.ValidityLength = validityLength;
            this.ClassFees = classFees;
        }

        public static clsLicenseClasses FindLicenseClassInfoByID(int licenseClass_ID)
        {
            string ClassName = "", ClassDescription = "";
            int minimumAllowedAge = 0; byte validityLength = 0;
            float classFees = 0;
            if (clsLicenseClassesData.GetLicenseClassInfoByID(licenseClass_ID, ref ClassName, ref ClassDescription, ref minimumAllowedAge, ref validityLength, ref classFees))

                return new clsLicenseClasses(licenseClass_ID, ClassName, ClassDescription, minimumAllowedAge, validityLength, classFees);

            else
                return null;
        }
    }
}
