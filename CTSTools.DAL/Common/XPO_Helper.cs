using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;
using System.Configuration;

namespace CTSTools.DAL.Common
{
    public class XPO_Helper
    {
        public static Session GetNewSession()
        {
            return new Session(DataLayer);
        }

        public static UnitOfWork GetNewUnitOfWork()
        {
            return new UnitOfWork(DataLayer);
        }

        private readonly static object lockObject = new object();

        static volatile IDataLayer fDataLayer;
        static IDataLayer DataLayer
        {
            get
            {
                if (fDataLayer == null)
                {
                    lock (lockObject)
                    {
                        if (fDataLayer == null)
                        {
                            fDataLayer = GetDataLayer();
                        }
                    }
                }
                return fDataLayer;
            }
        }

        private static IDataLayer GetDataLayer()
        {

            XpoDefault.Session = null;
            string conn = MSSqlConnectionProvider.GetConnectionString(
                ConfigurationManager.AppSettings["sql_server"],
                ConfigurationManager.AppSettings["database_user"],
                ConfigurationManager.AppSettings["database_password"],
                ConfigurationManager.AppSettings["database"]);
            XPDictionary dict = new ReflectionDictionary();
            IDataStore store = XpoDefault.GetConnectionProvider(conn, AutoCreateOption.DatabaseAndSchema);
            dict.GetDataStoreSchema(typeof(Features.AdvancedSettings.StatusManagement.StatusXPO).Assembly);
            IDataLayer dl = new ThreadSafeDataLayer(dict, store);
            return dl;
        }
    }
}
