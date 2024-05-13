using System;
using System.Collections.Generic;

namespace DentistClinicProject;

public partial class MedicineRegistrator
{
    public int IdMedicineReg { get; set; }

    public string FullName { get; set; } = null!;

    public decimal Phone { get; set; }

    public string Email { get; set; } = null!;

    public DateTime BdayDate { get; set; }

    public int IdUser { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;
}
