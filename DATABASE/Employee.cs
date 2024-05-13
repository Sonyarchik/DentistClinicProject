using System;
using System.Collections.Generic;

namespace DentistClinicProject;

public partial class Employee
{
    public int IdEmployee { get; set; }

    public int IdJobTitle { get; set; }

    public int IdRole { get; set; }

    public decimal PhoneNumber { get; set; }

    public string Email { get; set; } = null!;

    public virtual JobTitle IdJobTitleNavigation { get; set; } = null!;

    public virtual Role IdRoleNavigation { get; set; } = null!;
}
