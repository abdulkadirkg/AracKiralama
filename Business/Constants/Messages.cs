using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    //static sürekli newlemeye gerek kalmıyor
    public static class Messages
    {
        public static string CarAdded = "Araç Eklendi";
        public static string ColorAdded = "Renk Eklendi";
        public static string BrandAdded = "Marka Eklendi";
        public static string CarUpdated = "Araç Güncellendi";
        public static string ColorUpdated = "Renk Güncellendi";
        public static string BrandUpdated = "Marka Güncellendi";
        public static string CarDeleted = "Araç Silindi";
        public static string ColorDeleted = "Renk Silindi";
        public static string BrandDeleted = "Marka Silindi";
        public static string AuthorizationDenied = "Yetkisiz Erişim";
    }
}
