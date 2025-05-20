using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Data_Access_Layer;

namespace Bussiness_Layer
{
    public class clsAppTypes
    {
        public int AppTypeID { get; set; }
        public string AppTitle { get; set; }
        public float AppFees { get; set; }
        public clsAppTypes(int ID, string Title, float Fees)        
        { 
            this.AppTypeID = ID;
            this.AppTitle = Title;
            this.AppFees = Fees;
        }
        public static DataTable GetAllAppTypes()
        {
            return (clsApplicationTypesData.GetAllAppTypes());
        }
        public static clsAppTypes Find(int ID)
        {
            string AppTitle = ""; float AppFees = 0.0f;

            if (clsApplicationTypesData.GetApplicationTypeInfoByID(ID, ref AppTitle, ref AppFees))
                return new clsAppTypes(ID, AppTitle, AppFees);
            else
                return null;
        }
        public static clsAppTypes Find(string AppTitle)
        {
            int AppTypeID = -1; float AppFees = 0.0f;

            if (clsApplicationTypesData.GetApplicationTypeInfoByTitle(AppTitle, ref AppTypeID, ref AppFees))
                return new clsAppTypes(AppTypeID, AppTitle, AppFees);
            else
                return null;
        }
        public bool UpdateAppType()
        {
            if(clsApplicationTypesData.UpdateApplicationType(this.AppTypeID, this.AppTitle, this.AppFees))
            {
                return true;
            }
            return false;
        }
    }
}
