using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWECVI.ApplicationCore.ViewModels
{
    public class DicomResultMirthViewModel
    {
        public string PatientName { get; set; } = default!;
        public string PatientSex { get; set; } = default!;
        public string PatientDate { get; set; } = default!;
        public string PatientId { get; set; } = default!;
        public string StudyDate { get; set; } = default!;
        public string StudyTime { get; set; } = default!;
        public string StudyDescription { get; set; } = default!;
        public string StudyInstanceUID { get; set; } = default!;
        public string StudyId { get; set; } = default!;
        public string InstitutionName { get; set; } = default!;
        public string SOPClassUID { get; set; } = default!;
        public string SOPInstanceUID { get; set; } = default!;
        public string AccessionNumber { get; set; } = default!;
        public string InstitutionalDepartmentName { get; set; } = default!;
        public string StudyWeight { get; set; } = default!;
        public string StudyHeight { get; set; } = default!;
        public string BSA { get; set; } = default!;
        public string Manufacture { get; set; } = default!;
        public string ManufactureName { get; set; } = default!;
        public string ModalitiesInStudy { get; set; } = default!;
        public string Tag60051010 { get; set; } = default!;
        public string Tag60051030 { get; set; } = default!;
        public List<MirthParameter> Parameters { get; set; } = default!;

    }

    public class HospitalPatientViewModel
    {
        public string PatientName { get; set; } = default!;
        public string PatientSex { get; set; } = default!;
        public string PatientDate { get; set; } = default!;
        public string PatientId { get; set; } = default!;
    }

    public class HospitalStudyViewModel
    {
        public string PatientId { get; set; } = default!;
        public string StudyDate { get; set; } = default!;
        public string StudyTime { get; set; } = default!;
        public string StudyDescription { get; set; } = default!;
        public string StudyInstanceUID { get; set; } = default!;
        public string StudyId { get; set; } = default!;
        public string InstitutionName { get; set; } = default!;
        public string SOPClassUID { get; set; } = default!;
        public string SOPInstanceUID { get; set; } = default!;
        public string AccessionNumber { get; set; } = default!;
        public string InstitutionalDepartmentName { get; set; } = default!;
        public string StudyWeight { get; set; } = default!;
        public string BSA { get; set; } = default!;
        public string StudyHeight { get; set; } = default!;
    }


    public class HospitalParameterViewModel
    {
        public float ResultValue { get; set; }
        public int FindingSite{ get; set; }
        public int ImageView{ get; set; }
        public int ImageMode{ get; set; }
        public int CardiacCyclePoint{ get; set; }
        public int RespiratoryCyclePoint{ get; set; }
        public int MeasurementMethod{ get; set; }
        public int IndexMeasurementUnit{ get; set; }
        public int Derivation{ get; set; }
        public int SelectionStatus{ get; set; }
        public int DirectionOfFlow{ get; set; }
    }

    public class MirthParameter
    {
        public string Name { get; set; } = default!;
        public string NameCode { get; set; } = default!;
        public string Value { get; set; } = default!;
        public string ValueUnit { get; set; } = default!;
        public string ValueUnitCode { get; set; } = default!;
        public List<MirthParameterDetail> ParameterDetails { get; set; } = default!;
    }

    public class MirthParameterDetail
    {
        public string NameDetail { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string NameCode { get; set; } = default!;
        public string Value { get; set; } = default!;
        public string ValueCode { get; set; } = default!;
    }
}
