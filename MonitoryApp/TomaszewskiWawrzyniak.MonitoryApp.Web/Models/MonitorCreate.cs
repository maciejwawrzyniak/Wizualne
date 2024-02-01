using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using TomaszewskiWawrzyniak.MonitoryApp.Core;

namespace TomaszewskiWawrzyniak.MonitoryApp.Web.Models
{
    public class MonitorCreate
    {
        public string Name { get; set; }
        public Guid Producer { get; set; }
        [Range(1, float.MaxValue)]
        public float Diagonal { get; set; }
        public MatrixType Matrix { get; set; }

        public List<SelectListItem> Producers { get; set; } = new List<SelectListItem>();

    }
}
