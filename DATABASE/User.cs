using System;
using System.Collections.Generic;

namespace DentistClinicProject;

public partial class User
{
    public int IdUser { get; set; }

    public string Login { get; set; } = null!;

    public decimal Password { get; set; }

    public int IdRole { get; set; }

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual Role IdRoleNavigation { get; set; } = null!;

    public virtual ICollection<MedicineRegistrator> MedicineRegistrators { get; set; } = new List<MedicineRegistrator>();
}
