using System.Configuration;
/// <summary>
/// Repository for configuration settings
/// </summary>

 
    public static class seoWebAppConfiguration
    {  
        private readonly static string storeName;
        // Caches the connection string
        private readonly static string storeDesc;
        // Caches the connection string
        private readonly static string storeKeywords;
        // Caches the connection string
        private readonly static string storeTitle; 
        // Caches the connection string
        private readonly static string storeAddress;
        private readonly static string storeZip;
        private readonly static string storeCity;
        // Caches the connection string
        private readonly static string storeState;
        // Caches the connection string
        private readonly static string storePhone;
        // Caches the connection string 
        private readonly static string storeImgLogo;
        
        // Caches the connection string
        private readonly static string superUser;
        // Caches the connection string
        private readonly static string superPassword;
        // Caches the connection string
        private readonly static bool useDocumentDb;
        // Caches the connection string
        private readonly static bool useRavenDb;
        // Caches the connection string
        private readonly static bool useMongoDb;
        // Caches the connection string
        private readonly static bool useSqlServerDb;
        // Caches the connection string
        private readonly static string dbConnectionString;
        // Caches the data provider name
        private readonly static string dbProviderName;
        // Store the number of products per page
        private readonly static int productsPerPage;
        // Store the product description length for product lists
        private readonly static int productDescriptionLength;
        // Store the product description length for product lists
        private readonly static int idWebstore;
        // Store the name of your shop
        private readonly static string siteName;
        private readonly static string googleUrl;
        private readonly static string twitterUrl;
        private readonly static string facebookUrl;
        private readonly static string facebookAppId;
        private readonly static string storeUrl;
        private readonly static string paypalClientId;
        // Caches the connection string
        private readonly static string paypalClientSecret;
        private readonly static string paypalReturnUrl;
        // Caches the connection string
        private readonly static string paypalCancelUrl;
        static seoWebAppConfiguration()
        { 
            storeUrl = ConfigurationManager.AppSettings["StoreUrl"];
            facebookAppId = ConfigurationManager.AppSettings["FacebookAppId"];
            storeName = ConfigurationManager.AppSettings["StoreName"];
            storeImgLogo = ConfigurationManager.AppSettings["StoreImgLogo"];
            storePhone = ConfigurationManager.AppSettings["Phone"];
            storeZip = ConfigurationManager.AppSettings["StoreZip"];
            storeState = ConfigurationManager.AppSettings["StoreState"];
            storeCity = ConfigurationManager.AppSettings["StoreCity"];
            storeAddress = ConfigurationManager.AppSettings["StoreAddress"];
            storeTitle = ConfigurationManager.AppSettings["StoreTitle"];
            storeKeywords = ConfigurationManager.AppSettings["StoreKeywords"];
            storeDesc = ConfigurationManager.AppSettings["StoreDescr"]; 

            superUser = ConfigurationManager.AppSettings["SuperUser"];
            superPassword = ConfigurationManager.AppSettings["SuperPassword"];  

            useDocumentDb = System.Boolean.Parse(ConfigurationManager.AppSettings["UseDocumentDb"]);
            useRavenDb = System.Boolean.Parse(ConfigurationManager.AppSettings["UseRavenDb"]);
            useMongoDb = System.Boolean.Parse(ConfigurationManager.AppSettings["UseMongoDb"]);
            useSqlServerDb = System.Boolean.Parse(ConfigurationManager.AppSettings["UseSqlServerDb"]);
            
            idWebstore = System.Int32.Parse(ConfigurationManager.AppSettings["idSeoWebstore"]);
            dbConnectionString = ConfigurationManager.ConnectionStrings["SeoWebAppConnString"].ConnectionString;
            dbProviderName = ConfigurationManager.ConnectionStrings["SeoWebAppConnString"].ProviderName;
            productsPerPage = System.Int32.Parse(ConfigurationManager.AppSettings["ProductsPerPage"]);
            productDescriptionLength = System.Int32.Parse(ConfigurationManager.AppSettings["ProductDescriptionLength"]);
            siteName = ConfigurationManager.AppSettings["SiteName"];

            googleUrl = ConfigurationManager.AppSettings["GoogleUrl"];
            twitterUrl = ConfigurationManager.AppSettings["TwitterUrl"];
            facebookUrl = ConfigurationManager.AppSettings["FacebookUrl"];

            paypalClientId = ConfigurationManager.AppSettings["PaypalClientId"];
            paypalClientSecret = ConfigurationManager.AppSettings["PaypalClientSecret"];
            paypalReturnUrl = ConfigurationManager.AppSettings["PaypalReturnUrl"];
            paypalCancelUrl = ConfigurationManager.AppSettings["PaypalCancelUrl"];
        }
        public static string PaypalReturnUrl
        {
            get
            {
                return paypalReturnUrl;
            }
        }
        public static string PaypalCancelUrl
        {
            get
            {
                return paypalCancelUrl;
            }
        }

        public static string PaypalClientId
        {
            get
            {
                return paypalClientId;
            }
        }
        public static string PaypalClientSecret
        {
            get
            {
                return paypalClientSecret;
            }
        }
        public static string StoreUrl
        {
            get
            {
                return storeUrl;
            }
        }
        public static string FacebookAppId
        {
            get
            {
                return facebookAppId;
            }
        }
        public static string GoogleUrl
        {
            get
            {
                return googleUrl;
            }
        }

        public static string TwitterUrl
        {
            get
            {
                return twitterUrl;
            }
        }

        public static string FacebookUrl
        {
            get
            {
                return facebookUrl;
            }
        }
 public static string StoreName
        {
            get
            {
                return storeName;
            }
        }
 public static string StoreImgLogo
        {
            get
            {
                return storeImgLogo;
            }
        }
 public static string StorePhone
        {
            get
            {
                return storePhone;
            }
        }
 public static string StoreZip
        {
            get
            {
                return storeZip;
            }
        }
 public static string StoreState
        {
            get
            {
                return storeState;
            }
        }
 public static string StoreCity
        {
            get
            {
                return storeCity;
            }
        }
 public static string StoreAddress
        {
            get
            {
                return storeAddress;
            }
        }
 public static string StoreTitle
        {
            get
            {
                return storeTitle;
            }
        }
 public static string StoreKeywords
        {
            get
            {
                return storeKeywords;
            }
        }
 public static string StoreDesc
        {
            get
            {
                return storeDesc;
            }
        }
        public static bool UseDocumentDb
        {
            get
            {
                return useDocumentDb;
            }
        }
        public static bool UseRavenDb
        {
            get
            {
                return useRavenDb;
            }
        }
        public static bool UseMongoDb
        {
            get
            {
                return useMongoDb;
            }
        }
        public static bool UseSqlServerDb
        {
            get
            {
                return useSqlServerDb;
            }
        }
        public static int IdWebstore
        {
            get
            {
                return idWebstore;
            }
        }
        public static string SuperUser
        {
            get
            {
                return superUser;
            }
        }
        // Returns the data provider name
        public static string SuperPassword
        {
            get
            {
                return superPassword;
            }
        }
        public static string DbConnectionString
        {
            get
            {
                return dbConnectionString;
            }
        }
        // Returns the data provider name
        public static string DbProviderName
        {
            get
            {
                return dbProviderName;
            }
        }
        // Returns the maximum number of products to be displayed on a page
        public static int ProductsPerPage
        {
            get
            {
                return productsPerPage;
            }
        }
        // Returns the length of product descriptions in products lists
        public static int ProductDescriptionLength
        {
            get
            {
                return productDescriptionLength;
            }
        }
        // Returns the length of product descriptions in products lists
        public static string SiteName
        {
            get
            {
                return siteName;
            }
        }

        // Returns the address of the mail server
        public static string MailServer
        {
            get
            {
                return ConfigurationManager.AppSettings["MailServer"];
            }
        }
        // Returns the email username
        public static string MailUsername
        {
            get
            {
                return ConfigurationManager.AppSettings["MailUsername"];
            }
        }
        // Returns the email password
        public static string MailPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["MailPassword"];
            }
        }
        // Returns the email password
        public static string MailFrom
        {
            get
            {
                return ConfigurationManager.AppSettings["MailFrom"];
            }
        }
        // Send error log emails?
        public static bool EnableErrorLogEmail
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings
                ["EnableErrorLogEmail"]);
            }
        }
        // Returns the email address where to send error reports
        public static string ErrorLogEmail
        {
            get
            {
                return ConfigurationManager.AppSettings["ErrorLogEmail"];
            }
        }

        // The PayPal shopping cart URL
        public static string PaypalUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["PaypalUrl"];
            }
        }
        // The PayPal email account
        public static string PaypalEmail
        {
            get
            {
                return ConfigurationManager.AppSettings["Email"];
            }
        }
        // Currency code (such as USD)
        public static string PaypalCurrency
        {
            get
            {
                return ConfigurationManager.AppSettings["PaypalCurrency"];
            }
        }
        

        // Returns the number of days for shopping cart expiration
        public static int CartPersistDays
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["CartPersistDays"]);
            }
        }

        // Returns the email address for customers to contact the site
        public static string CustomerServiceEmail
        {
            get
            {
                return
                ConfigurationManager.AppSettings["CustomerServiceEmail"];
            }
        }
        // The "from" address for auto-generated order processor emails
        public static string OrderProcessorEmail
        {
            get
            {
                return
                ConfigurationManager.AppSettings["OrderProcessorEmail"];
            }
        }

        // The email address to use to contact the supplier
        public static string SupplierEmail
        {
            get
            {
                return ConfigurationManager.AppSettings["SupplierEmail"];
            }
        }
    }
 