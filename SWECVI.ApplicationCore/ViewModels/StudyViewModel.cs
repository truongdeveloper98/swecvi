using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.ViewModels.MirthConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWECVI.ApplicationCore.ViewModels
{
    public class StudyViewModel: Study
    {
        public Dictionary<string, ParameterViewModel> ParameterViewModels { get; set; }

        public PatientViewModel PatientViewModel { get; set; }
        public int? Age { get; set; }
    }
}
