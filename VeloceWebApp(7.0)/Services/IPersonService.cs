using VeloceWebApp_7._0_.Controllers;
using VeloceWebApp_7._0_.Models;

namespace VeloceWebApp_7._0_.Services
{
    public interface IPersonService
    {
        public bool IsAny();
        public Task<IEnumerable<PersonModel>> GetAllAsync();
        public PersonModel IfIdExistsAsync(int? id);
        public bool EditRecordAsync(int id, PersonModel personModel);
        //public Task RemoveRecordAsync(PersonModel personModel);
        public void RemoveRecord(PersonModel personModel);
        public bool PersonModelExists(int id);
        public void AddRecord(PersonModel personModel);
        public List<PersonModel> SaveFile();
    }
}
