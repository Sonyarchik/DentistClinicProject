using System;
using System.Collections.Generic;

namespace DentistClinicProject;

public partial class Doctor
{
    public int IdDoctor { get; set; }

    public string FullName { get; set; } = null!;

    public int IdCategory { get; set; }

    public int IdJobTitle { get; set; }

    public decimal Phone { get; set; }

    public string Email { get; set; } = null!;

    public DateTime BdayDate { get; set; }

    public int IdUser { get; set; }

    public virtual CategoryOfService IdCategoryNavigation { get; set; } = null!;

    public virtual JobTitle IdJobTitleNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;

    public virtual ICollection<MedicalBook> MedicalBooks { get; set; } = new List<MedicalBook>();
}
