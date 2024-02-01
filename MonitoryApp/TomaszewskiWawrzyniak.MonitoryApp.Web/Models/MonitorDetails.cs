using TomaszewskiWawrzyniak.MonitoryApp.Core;

namespace TomaszewskiWawrzyniak.MonitoryApp.Web.Models
{
    public class MonitorDetails
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ProducerName { get; set; }
        public float Diagonal { get; set; }
        public MatrixType Matrix { get; set; }
    }
}
