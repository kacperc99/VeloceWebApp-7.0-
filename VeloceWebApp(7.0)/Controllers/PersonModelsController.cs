using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using VeloceWebApp_7._0_.Data;
using VeloceWebApp_7._0_.Models;
using System.Web;
using VeloceWebApp_7._0_.Services;
using System.Text.RegularExpressions;

namespace VeloceWebApp_7._0_.Controllers
{
    public class PersonModelsController : Controller
    {
        private readonly IPersonService _service;

        public PersonModelsController(IPersonService service)
        {
            _service = service;
        }

        // GET: PersonModels
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            EditModel.instance.IsRecordBeingEdited = false;
            return _service.IsAny() ?
                        View(data) :
                        Problem("Entity set 'VeloceWebApp_7_0_Context.PersonModel'  is null.");
        }

        // GET: PersonModels/Create
        public IActionResult Create()
        {
            return View();
        }


        // GET: PersonModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var personModel = _service.IfIdExistsAsync(id);
            if (personModel == null)
            {
                return NotFound();
            }
            return View(personModel);
        }

        // POST: PersonModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Birth_Date,Sex,Phone_Number,Email,Foot_Size")] PersonModel personModel)
        {
            if (id != personModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                if (_service.EditRecordAsync(id, personModel))
                return RedirectToAction(nameof(Index));
            }
            return View(personModel);
        }

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personModel = _service.IfIdExistsAsync(id);
            if (personModel != null)
            {
                _service.RemoveRecord(personModel);
            }

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> CreateOrEdit(int? id)
        {
            if (id == null || !_service.IsAny())
            {
                return View();
            }

            var personModel = _service.IfIdExistsAsync(id);
            if (personModel == null)
            {
                return NotFound();
            }
            EditModel.instance.IsRecordBeingEdited = true;
            return View(personModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit(int id, [Bind("Id,Name,Surname,Birth_Date,Sex,Phone_Number,Email,Foot_Size")] PersonModel personModel)
        {
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!EditModel.instance.IsRecordBeingEdited)
            {
                Regex regex = new Regex(emailPattern);
                bool mail_result = true;
                bool number_result = true;
                if (personModel.Email!=null)
                    mail_result = regex.IsMatch(personModel.Email);
                if(personModel.Phone_Number!=null)
                    number_result = personModel.Phone_Number.Any(x => char.IsNumber(x));
                if (ModelState.IsValid && 
                    DateTime.Compare(personModel.Birth_Date, DateTime.Now) < 0 && 
                    (personModel.Sex=="Male" || personModel.Sex=="Female") && 
                    ((personModel.Phone_Number==null|| personModel.Phone_Number.Length==9) 
                    && number_result) && mail_result && ((personModel.Foot_Size>30 && personModel.Foot_Size < 50) || personModel.Foot_Size==null))
                {
                    _service.AddRecord(personModel);
                    return RedirectToAction(nameof(Index));
                }
            }
            else if(EditModel.instance.IsRecordBeingEdited)
            {
                if (id != personModel.Id)
                {
                    return NotFound();
                }
                Regex regex = new Regex(emailPattern);
                bool mail_result = true;
                bool number_result = true;
                if (personModel.Email != null)
                    mail_result = regex.IsMatch(personModel.Email);
                if (personModel.Phone_Number != null)
                    number_result = personModel.Phone_Number.Any(x => char.IsNumber(x));
                if (ModelState.IsValid &&
                    DateTime.Compare(personModel.Birth_Date, DateTime.Now) < 0 &&
                    (personModel.Sex == "Male" || personModel.Sex == "Female") &&
                    ((personModel.Phone_Number == null || personModel.Phone_Number.Length == 9)
                    && number_result) && mail_result && ((personModel.Foot_Size > 30 && personModel.Foot_Size < 50) || personModel.Foot_Size == null))
                {

                    if (_service.EditRecordAsync(id, personModel))
                        return RedirectToAction(nameof(Index));
                }
            }
            return View(personModel);
        }
        public async Task<IActionResult> SaveAsFile()
        {
            var data = _service.SaveFile();
            string filename = DateTime.Now.ToString() + ".txt";
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            foreach(var p in data)
            {
                //int age = DateTime.Compare(p.Birth_Date, DateTime.Now);
                var today = DateTime.Now;
                var age = today - p.Birth_Date;
                if (p.Sex == "Male")
                    writer.Write("Mr ");
                else if (p.Sex=="Female")
                    writer.Write("Mrs ");
                writer.WriteLine(p.Name + " " + p.Surname + " " + p.Birth_Date + " " + age + " " + p.Sex + " " + p.Phone_Number + " " + p.Email + " " + p.Foot_Size);
            }
            writer.Flush();
            stream.Position = 0;
            return File(stream, "text/csv", filename);
        }
    }
}
