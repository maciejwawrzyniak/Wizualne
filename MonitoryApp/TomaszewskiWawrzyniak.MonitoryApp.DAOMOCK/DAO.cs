using Microsoft.EntityFrameworkCore;
using TomaszewskiWawrzyniak.MonitoryApp.Core;
using TomaszewskiWawrzyniak.MonitoryApp.DAOMOCK.BO;
using TomaszewskiWawrzyniak.MonitoryApp.Interfaces;
using static System.Reflection.Metadata.BlobBuilder;

namespace TomaszewskiWawrzyniak.MonitoryApp.DAOSQL1
{
    public class DAO : DbContext, IDAO
    {
        List<IMonitor> monitors;
        List<IProducer> producers;

        public DAO()
        {
            monitors = new List<IMonitor>();
            producers = new List<IProducer>();

            IProducer producer = new Producer()
            {
                Id = Guid.NewGuid(),
                Name = "Asus",
                CountryFrom = "Tajwan",
                Monitors = new List<IMonitor>()
            };

            IMonitor monitor = new DAOMOCK.BO.Monitor()
            {
                Id = Guid.NewGuid(),
                Name = "XQ123",
                Diagonal = 20,
                Matrix = MatrixType.TN,
                Producer = producer
            };

            producer.Monitors.Add(monitor);

            producers.Add(producer);
            monitors.Add(monitor);

            producer = new Producer()
            {
                Id = Guid.NewGuid(),
                Name = "HP",
                CountryFrom = "USA",
                Monitors = new List<IMonitor>()
            };

            monitor = new DAOMOCK.BO.Monitor()
            {
                Id = Guid.NewGuid(),
                Name = "V24i",
                Diagonal = 24,
                Matrix = MatrixType.IPS,
                Producer = producer
            };

            producer.Monitors.Add(monitor);

            producers.Add(producer);
            monitors.Add(monitor);
        }

        public IMonitor CreateNewMonitor(string name, Guid producer, float diagonal, MatrixType matrixType)
        {
            Producer? p = (Producer)GetProducer(producer);
            if (p == null)
            {
                throw new ArgumentException("Producer not found.");
            }
            DAOMOCK.BO.Monitor monitor = new DAOMOCK.BO.Monitor() 
            {
                Id = Guid.NewGuid(),
                Name = name,
                Diagonal = diagonal,
                Matrix = matrixType,
                Producer = p
            };
            monitors.Add(monitor);
            p.Monitors.Add(monitor);
            return monitor;
        }

        public IProducer CreateNewProducer(string name, string countryFrom)
        {
            Producer producer = new Producer()
            {
                Id = Guid.NewGuid(),
                Name = name,
                CountryFrom = countryFrom,
                Monitors = new List<IMonitor>()
            };
            producers.Add(producer);
            return producer;
        }

        public void DeleteMonitor(Guid id)
        {
            DAOMOCK.BO.Monitor? monitor = (DAOMOCK.BO.Monitor?)GetMonitor(id);
            if (monitor == null)
            {
                throw new ArgumentException("Monitor not found.");
            }
            monitors.Remove(monitor);
        }

        public void DeleteProducer(Guid id)
        {
            Producer? producer = (Producer?)GetProducer(id);
            if (producer == null)
            {
                throw new ArgumentException("Producer not found.");
            }
            monitors.RemoveAll(monitor => monitor.Producer.Id == producer.Id);
            producers.Remove(producer);
        }

        public IMonitor EditMonitor(Guid id, string name, Guid producer, float diagonal, MatrixType matrixType)
        {
            DAOMOCK.BO.Monitor? monitor = (DAOMOCK.BO.Monitor?)GetMonitor(id);
            if(monitor == null)
            {
                throw new ArgumentException("Monitor not found.");
            }
            Producer? p = (Producer?)GetProducer(producer);
            if (p == null)
            {
                throw new ArgumentException("Producer not found");
            }
            monitor.Name = name;
            monitor.Producer = p;
            monitor.Diagonal = diagonal;
            monitor.Matrix = matrixType;
            return monitor;
        }

        public IProducer EditProducer(Guid id, string name, string countryFrom)
        {
            Producer? producer = (Producer?)GetProducer(id);
            if (producer == null)
            {
                throw new ArgumentException("Producer not found.");
            }
            producer.Name = name;
            producer.CountryFrom = countryFrom;
            return producer;
        }

        public IEnumerable<IMonitor> GetAllMonitors()
        {
            return monitors;
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return producers;
        }

        public IMonitor? GetMonitor(Guid id)
        {
            for (int i = 0; i < monitors.Count(); i++)
            {
                if (monitors[i].Id.Equals(id))
                {
                    return monitors[i];
                }
            }
            return null;
        }

        public IProducer? GetProducer(Guid id)
        {
            for (int i = 0; i < producers.Count(); i++)
            {
                if (producers[i].Id.Equals(id))
                {
                    return producers[i];
                }
            }
            return null;
        }
    }
}
