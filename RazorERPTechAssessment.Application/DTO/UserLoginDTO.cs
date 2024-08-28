using RazorERPTechAssessment.DapperDB.Enums;
using System.ComponentModel.DataAnnotations;

namespace RazorERPTechAssessment.Application.DTO;

public class UserLoginDTO
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [MaxLength(100)]
    public string Password { get; set; }

    public Role Role { get; set; }
}
