using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeloceWebApp_7._0_.Models
{
  public class PersonModel
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    [Required]
    [MaxLength(150)]
    public string Surname { get; set; }
    [Required]
    public DateTime Birth_Date { get; set; }
    [Required]
    public string Sex { get; set; }
    public string? Phone_Number { get; set; }
    public string? Email { get; set; }
    public int? Foot_Size { get; set; }
  }
    public class PersonViewModel
    {
        public List<SelectListItem> ls { get; set; }
        public string Selected_Person { get; set; }
    }

    public class ViewModel
    {
        public PersonModel PersonCreate { get; set; }
        public PersonModel PersonUpdate { get; set; }
    }
}

