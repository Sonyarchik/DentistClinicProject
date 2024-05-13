using System;
using System.Collections.Generic;

namespace DentistClinicProject;

public partial class ListOfService
{
    public int IdService { get; set; }

    public int IdCategory { get; set; }

    public string NameOfService { get; set; } = null!;

    public decimal Price { get; set; }

    public string Description { get; set; } = null!;

    public virtual CategoryOfService IdCategoryNavigation { get; set; } = null!;

    public virtual ICollection<MedicalBook> MedicalBooks { get; set; } = new List<MedicalBook>();
}
