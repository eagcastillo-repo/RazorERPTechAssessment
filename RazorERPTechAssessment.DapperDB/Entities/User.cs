using RazorERPTechAssessment.DapperDB.Abstracts;
using RazorERPTechAssessment.DapperDB.Enums;
using System.ComponentModel.DataAnnotations;

namespace RazorERPTechAssessment.DapperDB.Entities;

public class User : BaseClass
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    public int Role { get; set; }
    public int Company { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [MaxLength(100)]
    public string Password { get; set; }
}
