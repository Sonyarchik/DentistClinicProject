using System;
using System.Collections.Generic;

namespace DentistClinicProject;

public partial class CategoryOfService
{
    public int IdCategory { get; set; }

    public string CategoryName { get; set; } = null!;

    public string CategoryDescription { get; set; } = null!;

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual ICollection<ListOfService> ListOfServices { get; set; } = new List<ListOfService>();
}
