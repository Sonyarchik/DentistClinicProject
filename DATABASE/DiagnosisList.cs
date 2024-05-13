using System;
using System.Collections.Generic;

namespace DentistClinicProject;

public partial class DiagnosisList
{
    public int IdDiagnosis { get; set; }

    public string DiagnosisName { get; set; } = null!;

    public string DiagnosisDescription { get; set; } = null!;

    public virtual ICollection<MedicalBook> MedicalBooks { get; set; } = new List<MedicalBook>();
}
