using System;
using System.Collections.Generic;

namespace DentistClinicProject;

public partial class Patient
{
    public int IdPatient { get; set; }

    public string FullName { get; set; } = null!;

    public decimal Phone { get; set; }

    public string Email { get; set; } = null!;

    public decimal PassportData { get; set; }

    public decimal Snils { get; set; }

    public DateTime BdayDate { get; set; }

    public virtual ICollection<MedicalBook> MedicalBooks { get; set; } = new List<MedicalBook>();
}
