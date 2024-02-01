using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Extensions;
using TomaszewskiWawrzyniak.MonitoryApp.Core;
using TomaszewskiWawrzyniak.MonitoryApp.Web.Models;

namespace TomaszewskiWawrzyniak.MonitoryApp.Web.Controllers
{
    public class MonitorsController : Controller
    {
        private readonly BLC.BLC _blc;

        public MonitorsController(BLC.BLC blc)
        {
            _blc = blc;
        }

        // GET: Monitors
        public async Task<IActionResult> Index(string searchName, string producer, float minDiagonal, float maxDiagonal, string matrix)
        {
            ViewData["SearchName"] = searchName;
            ViewData["MinDiagonal"] = minDiagonal;
            ViewData["MaxDiagonal"] = maxDiagonal;
            
            IEnumerable<SelectListItem> producers = _blc.GetProducers()
                .Select( p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() })
                .Prepend(new SelectListItem() { Text = "All", Value = "" })
                .ToList();
            foreach (SelectListItem _producer in producers)
            {
                if(_producer.Value  == producer)
                {
                    _producer.Selected = true;
                }
                else
                {
                    _producer.Selected = false;
                }
            }
            ViewData["Producers"] = producers;

            IEnumerable<SelectListItem> matrixes = Enum.GetValues(typeof(MatrixType))
                .Cast<MatrixType>()
                .Select(m => new SelectListItem() { Text = m.ToString(), Value = m.ToString() })
                .Prepend(new SelectListItem() { Text = "All", Value = "" })
                .ToList();
            foreach (SelectListItem m in matrixes)
            {
                if (m.Value == matrix)
                {
                    m.Selected = true;
                }
                else
                {
                    m.Selected = false;
                }
            }
            ViewData["Matrixes"] = matrixes;

            if(maxDiagonal == 0)
            {
                return View(_blc.FilterMonitors(searchName, matrix, minDiagonal, float.MaxValue, producer).Select(m => new MonitorDetails()
                {
                    Id = m.Id,
                    Name = m.Name,
                    ProducerName = m.Producer.Name,
                    Diagonal = m.Diagonal,
                    Matrix = m.Matrix
                }));
            }
            else
            {
                return View(_blc.FilterMonitors(searchName, matrix, minDiagonal, maxDiagonal, producer).Select(m => new MonitorDetails()
                {
                    Id = m.Id,
                    Name = m.Name,
                    ProducerName = m.Producer.Name,
                    Diagonal = m.Diagonal,
                    Matrix = m.Matrix
                }));
            }
        }

        // GET: Monitors/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monitor = _blc.GetMonitor(id.Value);
            if (monitor == null)
            {
                return NotFound();
            }
            MonitorDetails monitorDetails = new MonitorDetails()
            {
                Id = monitor.Id,
                Name = monitor.Name,
                ProducerName = monitor.Producer.Name,
                Diagonal = monitor.Diagonal,
                Matrix = monitor.Matrix
            };
            return View(monitorDetails);
        }

        // GET: Monitors/Create
        public IActionResult Create()
        {
            var producers = _blc.GetProducers();
            MonitorCreate monitor = new MonitorCreate();
            monitor.Producers = producers.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();
            return View(monitor);
        }

        // POST: Monitors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Producer,Diagonal,Matrix,Producers")] MonitorCreate monitor)
        {
            if (ModelState.IsValid)
            {
                _blc.CreateNewMonitor(monitor.Name, monitor.Producer, monitor.Diagonal, monitor.Matrix);
                return RedirectToAction(nameof(Index));
            }
            var producers = _blc.GetProducers();
            monitor.Producers = producers.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();
            return View(monitor);
        }

        // GET: Monitors/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monitor = _blc.GetMonitor(id.Value);
            if (monitor == null)
            {
                return NotFound();
            }
            var producers = _blc.GetProducers();
            MonitorEdit monitorEdit = new MonitorEdit()
            {
                Id = monitor.Id,
                Name = monitor.Name,
                Producer = monitor.Producer.Id,
                Diagonal = monitor.Diagonal,
                Matrix = monitor.Matrix,
                Producers = producers.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList()
            };
            return View(monitorEdit);
        }

        // POST: Monitors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Producer,Diagonal,Matrix,Producers")] MonitorEdit monitor)
        {
            if (id != monitor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _blc.EditMonitor(monitor.Id, monitor.Name, monitor.Producer, monitor.Diagonal, monitor.Matrix);
                return RedirectToAction(nameof(Index));
            }
            var producers = _blc.GetProducers();
            monitor.Producers = producers.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() }).ToList();
            return View(monitor);
        }

        // GET: Monitors/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monitor = _blc.GetMonitor(id.Value);
            if (monitor == null)
            {
                return NotFound();
            }
            MonitorDetails monitorDelete = new MonitorDetails()
            {
                Id = monitor.Id,
                Name = monitor.Name,
                ProducerName = monitor.Producer.Name,
                Diagonal = monitor.Diagonal,
                Matrix = monitor.Matrix
            };
            return View(monitorDelete);
        }

        // POST: Monitors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _blc.DeleteMonitor(id);
            return RedirectToAction(nameof(Index));
        }

        private bool MonitorExists(Guid id)
        {
            if (_blc.GetMonitor(id) == null)
                return false;
            else
                return true;
        }
    }
}
