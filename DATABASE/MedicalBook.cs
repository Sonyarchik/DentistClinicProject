using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DentistClinicProject;

public partial class MedicalBook
{
    public int IdMedicalBook { get; set; }

    public int IdDoctor { get; set; }

    public int IdPatient { get; set; }

    public int IdService { get; set; }

    public decimal TotalPayment { get; set; }

    public DateTime DataAppointment { get; set; }

    public TimeSpan TimeAppointment { get; set; }

    public int IdDiagnosis { get; set; }

    public string Treatment { get; set; } = null!;

    public int IdStatus { get; set; }

    public virtual DiagnosisList IdDiagnosisNavigation { get; set; } = null!;

    public virtual Doctor IdDoctorNavigation { get; set; } = null!;

    public virtual Patient IdPatientNavigation { get; set; } = null!;

    public virtual ListOfService IdServiceNavigation { get; set; } = null!;

    public virtual Status IdStatusNavigation { get; set; } = null!;
}
