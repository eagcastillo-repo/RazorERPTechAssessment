using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RazorERPTechAssessment.DapperDB.Enums;

public enum Role
{
    [Display(Name = "Admin")]
    Admin,
    [Display(Name = "User")]
    User
}

public enum Company
{
    CompanyOne,
    CompanyTwo,
    CompanyThree,
    CompanyFour,
    CompanyFive
}
