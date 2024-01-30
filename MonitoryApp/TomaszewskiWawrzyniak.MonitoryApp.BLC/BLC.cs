using TomaszewskiWawrzyniak.MonitoryApp.Interfaces;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace TomaszewskiWawrzyniak.MonitoryApp.BLC
{
    public class BLC
    {
        private IDAO dao;

        public BLC(IConfiguration configuration)
        {
            string libraryName = System.Configuration.ConfigurationManager.AppSettings["DBLibraryName"]!;
            Type? typeToCreate = null;
            Assembly assembly = Assembly.UnsafeLoadFrom(libraryName);
            foreach( Type type in assembly.GetTypes() )
            {
                if( type.IsAssignableTo(typeof( IDAO )) ) 
                {
                    typeToCreate = type;
                    break;
                }
            }
            ConstructorInfo? constructor = typeToCreate.GetConstructor(new[] { typeof(IConfiguration) });
            if ( constructor != null )
            {
                dao = (IDAO)constructor.Invoke(new object[] { configuration });
            }
            else
            {
                dao = (IDAO)Activator.CreateInstance(typeToCreate, null);
            }
        }

        public BLC(IDAO dao) 
        {
            this.dao = dao;
        }

        public IEnumerable<IProducer> GetProducers()
        {
            return dao.GetAllProducers();
        }
        public IEnumerable<IMonitor> GetMonitors()
        {
            return dao.GetAllMonitors();
        }
    }
}
