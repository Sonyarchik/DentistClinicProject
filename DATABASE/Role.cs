using System;
using System.Collections.Generic;

namespace DentistClinicProject;

public partial class Role
{
    public int IdRole { get; set; }

    public string RoleName { get; set; } = null!;

    public string RoleDescription { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
