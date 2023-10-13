using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using VeloceWebApp_7._0_.Data;
using VeloceWebApp_7._0_.Models;
using Microsoft.AspNetCore.Mvc;
using VeloceWebApp_7._0_.Controllers;

namespace VeloceWebApp_7._0_.Services
{
    public class PersonService : IPersonService
    {
        private readonly VeloceWebApp_7_0_Context _context;
        public PersonService(VeloceWebApp_7_0_Context context)
        {
            _context = context;
        }
        public bool IsAny()
        {
            return _context.PersonModel != null ? true : false;
        }
        public async Task<IEnumerable<PersonModel>> GetAllAsync()
        {
            return await _context.PersonModel.ToListAsync();
        }
        public PersonModel IfIdExistsAsync(int? id)
        {
            if (id == null || _context.PersonModel == null)
            {
                return null;
            }

            var personModel = _context.PersonModel.Find(id);
            return personModel;
        }
        public bool EditRecordAsync(int id, PersonModel personModel)
        {
            try
            {
                _context.Update(personModel);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
                {
                if (PersonModelExists(personModel.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }
        /*public async Task RemoveRecordAsync(PersonModel personModel)
        {
            _context.PersonModel.Remove(personModel);
            await _context.SaveChangesAsync();
        }*/
        public void RemoveRecord(PersonModel personModel)
        {
                _context.PersonModel.Remove(personModel);
                _context.SaveChangesAsync();
            
        }
        public void AddRecord(PersonModel personModel)
        {
            _context.Add(personModel);
            _context.SaveChanges();
        }
        public bool PersonModelExists(int id)
        {
            return (_context.PersonModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public List<PersonModel> SaveFile()
        {
            return _context.PersonModel.ToList();
        }
    }
}
