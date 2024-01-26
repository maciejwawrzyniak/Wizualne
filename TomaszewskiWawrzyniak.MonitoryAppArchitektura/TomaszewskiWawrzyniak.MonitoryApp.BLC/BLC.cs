using TomaszewskiWawrzyniak.MonitoryApp.Interfaces;
using System.Reflection;

namespace TomaszewskiWawrzyniak.MonitoryApp.BLC
{
    public class BLC
    {
        private IDAO dao;

        public BLC( string libraryName)
        {
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

            dao = (IDAO)Activator.CreateInstance(typeToCreate, null);
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
