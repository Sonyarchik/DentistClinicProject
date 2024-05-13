using System;
using System.Collections.Generic;

namespace DentistClinicProject;

public partial class JobTitle
{
    public int IdJobTitle { get; set; }

    public string JobTitleName { get; set; } = null!;

    public string JobTitleDescription { get; set; } = null!;

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
