using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Xml.Linq;
using TomaszewskiWawrzyniak.MonitoryApp.Core;
using TomaszewskiWawrzyniak.MonitoryApp.DAOSQL1.BO;
using TomaszewskiWawrzyniak.MonitoryApp.Interfaces;

namespace TomaszewskiWawrzyniak.MonitoryApp.DAOSQL1
{
    public class DAO : DbContext, IDAO
    {
        private IConfiguration _configuration;
        public DAO(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("Sqlite"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producer>()
                .HasMany(p => (ICollection<BO.Monitor>)p.Monitors)
                .WithOne(b => (Producer)b.Producer);
        }
        public virtual DbSet<Producer> Producers { get; set; }
        public virtual DbSet<BO.Monitor> Monitors { get; set; }

        public IMonitor CreateNewMonitor(string name, Guid producer, float diagonal, MatrixType matrixType)
        {
            Producer? _producer = Producers.Find(producer);
            if (_producer == null)
            {
                throw new ArgumentException("Producer not found.");
            }
            else
            {
                BO.Monitor monitor = new BO.Monitor
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Producer = _producer,
                    Diagonal = diagonal,
                    Matrix = matrixType
                };
                Monitors.Add(monitor);
                SaveChanges();
                return monitor;
            }


        }

        public IProducer CreateNewProducer(string name, string countryFrom)
        {
            Producer _producer = new Producer
            {
                Id = Guid.NewGuid(),
                Name = name,
                CountryFrom = countryFrom
            };
            Producers.Add(_producer);
            SaveChanges();
            return _producer;
        }

        public void DeleteMonitor(Guid id)
        {
            BO.Monitor? monitor = Monitors.Find(id);
            if (monitor == null)
            {
                throw new ArgumentException("Monitor not found.");
            }
            else
            {
                Monitors.Remove(monitor);
                SaveChanges();
            }
        }

        public void DeleteProducer(Guid id)
        {
            Producer? producer = Producers.Find(id);
            if (producer == null)
            {
                throw new ArgumentException("Producer not found.");
            }
            else
            {
                Producers.Remove(producer);
                SaveChanges();
            }
        }

        public IMonitor EditMonitor(Guid id, string name, Guid producer, float diagonal, MatrixType matrixType)
        {
            BO.Monitor? monitor = Monitors.Find(id);
            if (monitor == null)
            {
                throw new ArgumentException("Monitor not found.");
            }
            else
            {
                Producer? _producer = Producers.Find(producer);
                if (_producer == null)
                {
                    throw new ArgumentException("Producer not found.");
                }
                else
                {
                    monitor.Producer = _producer;
                    monitor.Name = name;
                    monitor.Diagonal = diagonal;
                    monitor.Matrix = matrixType;
                    SaveChanges();
                    return monitor;
                }
            }

        }

        public IProducer EditProducer(Guid id, string name, string countryFrom)
        {
            Producer? producer = Producers.Find(id);
            if (producer == null)
            {
                throw new ArgumentException("Producer not found.");
            }
            else
            {
                producer.Name = name;
                producer.CountryFrom = countryFrom;
                SaveChanges();
                return producer;
            }
        }
        public IEnumerable<IMonitor> GetAllMonitors()
        {
            return Monitors.Include(m => m.Producer);
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return Producers;
        }

        public IMonitor? GetMonitor(Guid id)
        {
            return Monitors.Find(id);
        }

        public IProducer? GetProducer(Guid id)
        {
            return Producers.Find(id);
        }
    }
}
