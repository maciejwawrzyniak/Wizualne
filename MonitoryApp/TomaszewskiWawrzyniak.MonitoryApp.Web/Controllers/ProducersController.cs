﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TomaszewskiWawrzyniak.MonitoryApp.Web.Models;

namespace TomaszewskiWawrzyniak.MonitoryApp.Web.Controllers
{
    public class ProducersController : Controller
    {
        private readonly BLC.BLC _blc;

        public ProducersController(BLC.BLC blc)
        {
            _blc = blc;
        }

        // GET: Producers
        public async Task<IActionResult> Index(string searchName, string countryFrom)
        {
            ViewData["SearchName"] = searchName;
            IEnumerable<SelectListItem> countriesFrom = _blc.GetAllProducersCountries()
                .Select(x => new SelectListItem() { Text = x.ToString(), Value = x.ToString() })
                .Prepend(new SelectListItem() { Text = "All", Value = "" })
                .ToList();
            foreach (SelectListItem country in countriesFrom)
            {
                if(country.Value == countryFrom)
                {
                    country.Selected = true;
                    break;
                }
            }
            ViewData["CountriesFrom"] = countriesFrom;
            return View(_blc.FilterProducers(searchName, countryFrom).Select(m => new ProducerDetails()
            {
                Id= m.Id,
                Name = m.Name,
                CountryFrom = m.CountryFrom
            }));
        }

        // GET: Producers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producer = _blc.GetProducer(id.Value);
            if (producer == null)
            {
                return NotFound();
            }
            if (producer.Monitors == null)
            {
                ProducerDetails details = new ProducerDetails()
                {
                    Id = producer.Id,
                    Name = producer.Name,
                    CountryFrom = producer.CountryFrom,
                    Monitors = new List<MonitorDetails>()
                };
                return View(details);
            }
            else
            {
                ProducerDetails details = new ProducerDetails()
                {
                    Id = producer.Id,
                    Name = producer.Name,
                    CountryFrom = producer.CountryFrom,
                    Monitors = producer.Monitors.Select(m => new MonitorDetails()
                    {
                        Id = m.Id,
                        Name = m.Name,
                        ProducerName = producer.Name,
                        Diagonal = m.Diagonal,
                        Matrix = m.Matrix,
                    }).ToList()
                };
                return View(details);
            }
        }

        // GET: Producers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,CountryFrom")] ProducerCreate producer)
        {
            if (ModelState.IsValid)
            {
                _blc.CreateNewProducer(producer.Name, producer.CountryFrom);
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }

        // GET: Producers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producer = _blc.GetProducer(id.Value);
            if (producer == null)
            {
                return NotFound();
            }
            ProducerEdit producerEdit = new ProducerEdit()
            {
                Id = producer.Id,
                Name = producer.Name,
                CountryFrom = producer.CountryFrom
            };
            return View(producerEdit);
        }

        // POST: Producers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,CountryFrom")] ProducerEdit producer)
        {
            if (id != producer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _blc.EditProducer(producer.Id, producer.Name, producer.CountryFrom);
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }

        // GET: Producers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producer = _blc.GetProducer(id.Value);
            if (producer == null)
            {
                return NotFound();
            }
            ProducerDetails producerDetails = new ProducerDetails()
            {
                Id = producer.Id,
                Name = producer.Name,
                CountryFrom = producer.CountryFrom
            };

            return View(producerDetails);
        }

        // POST: Producers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var producer = _blc.GetProducer(id);
            if (producer != null)
            {
                _blc.DeleteProducer(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProducerExists(Guid id)
        {
            if (_blc.GetProducer(id) == null) return false;
            return true;
        }
    }
}
