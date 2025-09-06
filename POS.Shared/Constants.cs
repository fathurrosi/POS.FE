using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Shared
{
    public class Constants
    {
        public static readonly string Allow_Create = "Create";
        public static readonly string Allow_Read = "Read";
        public static readonly string Allow_Update = "Update";
        public static readonly string Allow_Delete = "Delete";
        public static readonly string Allow_Approve = "Approve";
        public static readonly string Allow_Reject = "Reject";

        public static readonly string Role_Administrators= "Administrators";
        public static readonly string Role_Managers= "Managers";
        public static readonly string Role_Users = "Users";

        public static readonly string Cookies_Name= "POSAuthCookies";

        public const string CODE_User = "U52";
        public const string CODE_Role = "R51";
        public const string CODE_Previllage = "P45";
        public const string CODE_Menu = "M53";
        public const string CODE_File = "F01";
        public const string CODE_Master = "M02";
        public const string CODE_Transaction = "T04";
        public const string CODE_Report = "R05";
        public const string CODE_User_Management = "U07";
        public const string CODE_Product_Stock = "S03";
        public const string CODE_Neraca = "N06";
        public const string CODE_Product = "P21";
        public const string CODE_Supplier = "S22";
        public const string CODE_Customer = "C23";
        public const string CODE_Profile = "P24";
        
    }
}
