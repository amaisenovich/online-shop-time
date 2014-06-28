using CloudinaryDotNet;
using System.Web;

namespace OnlineShopTime.Utils
{
    public class Cloud
    {
        static Cloudinary m_cloudinary;

        public static Api Api
        {
            get
            {
                LazyInit();
                return m_cloudinary.Api;
            }
        }

        public static IHtmlString GetCloudinaryJsConfig() {
            LazyInit();
            return m_cloudinary.GetCloudinaryJsConfig();
        }

        private static void LazyInit()
        {
            if (m_cloudinary == null)
            {
                Account acc = new Account(
                Properties.Settings.Default.CloudName,
                Properties.Settings.Default.ApiKey,
                Properties.Settings.Default.ApiSecret);

                m_cloudinary = new Cloudinary(acc);
            }
        }


    }
}