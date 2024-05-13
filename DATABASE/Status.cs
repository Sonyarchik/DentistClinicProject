using System;
using System.Collections.Generic;

namespace DentistClinicProject;

public partial class Status
{
    public int IdStatus { get; set; }

    public string StatusName { get; set; } = null!;

    public string StatusDescription { get; set; } = null!;

    public virtual ICollection<MedicalBook> MedicalBooks { get; set; } = new List<MedicalBook>();
}
