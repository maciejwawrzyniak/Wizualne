using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TomaszewskiWawrzyniak.MonitoryApp.DAOSQL1;
using TomaszewskiWawrzyniak.MonitoryApp.DAOSQL1.BO;
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
        public async Task<IActionResult> Index()
        {
            return View(_blc.GetMonitors());
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
                Producer = monitor.Producer.Id,
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
                Producer = monitor.Producer.Id,
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
