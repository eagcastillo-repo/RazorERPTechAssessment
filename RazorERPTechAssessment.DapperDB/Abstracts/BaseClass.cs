using System.ComponentModel.DataAnnotations;

namespace RazorERPTechAssessment.DapperDB.Abstracts;

public abstract class BaseClass
{
    [Key]
    public int Id { get; set; }
}
