namespace TomaszewskiWawrzyniak.MonitoryApp.Web.Models
{
    public class ProducerDetails
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CountryFrom { get; set; }
        public List<MonitorDetails> Monitors {get;set;}
    }
}
