using TomaszewskiWawrzyniak.MonitoryApp.Interfaces;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using TomaszewskiWawrzyniak.MonitoryApp.Core;
using static System.Reflection.Metadata.BlobBuilder;

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
            ConstructorInfo? constructor = typeToCreate!.GetConstructor(new[] { typeof(IConfiguration) });
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
        public IProducer? GetProducer(Guid Id)
        {
            return dao.GetProducer(Id);
        }

        public IMonitor? GetMonitor(Guid Id)
        {
            return dao.GetMonitor(Id);
        }
        public IProducer CreateNewProducer(string name, string countryFrom)
        {
            return dao.CreateNewProducer(name, countryFrom);
        }

        public IMonitor CreateNewMonitor(string name, Guid producer, float diagonal, MatrixType matrixType) 
        {
            return dao.CreateNewMonitor(name, producer, diagonal, matrixType);
        }

        public void EditProducer(Guid id, string name, string countryFrom)
        {
            dao.EditProducer(id, name, countryFrom);
        }
        public void EditMonitor(Guid id, string name, Guid producer, float diagonal, MatrixType matrixType )
        {
            dao.EditMonitor(id, name, producer, diagonal, matrixType);
        }
        public void DeleteProducer(Guid id)
        {
            dao.DeleteProducer(id);
        }
        public void DeleteMonitor(Guid id)
        {
            dao.DeleteMonitor(id);
        }

        public IEnumerable<IProducer> FilterProducers(string name, string countryFrom) 
        {
            IEnumerable<IProducer> producers = dao.GetAllProducers();
            if (!string.IsNullOrEmpty(name))
            {
                producers = producers.Where(p => p.Name.ToLower().Contains(name.ToLower()));
            }
            if (!string.IsNullOrEmpty(countryFrom))
            {
                producers = producers.Where(p => p.CountryFrom.ToLower().Contains(countryFrom.ToLower()));
            }
            return producers;
        }
        public IEnumerable<IMonitor> FilterMonitors(string name, string matrixType, float minDiagonal, float maxDiagonal, string publisher)
        {
            IEnumerable<IMonitor> monitors = dao.GetAllMonitors();
            if (!string.IsNullOrEmpty(name))
            {
                monitors = monitors.Where(m => m.Name.ToLower().Contains(name.ToLower()));
            }
            if (!string.IsNullOrEmpty(matrixType))
            {
                monitors = monitors.Where(m => Enum.Parse<MatrixType>(matrixType) == m.Matrix);
            }
            if (!string.IsNullOrEmpty(publisher))
            {
                monitors = monitors.Where(m => m.Producer.Id == Guid.Parse(publisher));
            }
            if (float.IsPositive(minDiagonal))
            {
                monitors = monitors.Where(m => m.Diagonal > minDiagonal);
            }
            if (float.IsPositive(maxDiagonal))
            {
                monitors = monitors.Where(m => m.Diagonal < maxDiagonal);
            }

            return monitors;
        }
    }
}
