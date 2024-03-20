using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWECVI.ApplicationCore.ViewModels.Hospital
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int SendingUnit { get; set; }
        public int IndexHospital { get; set; }
        public string Modality { get; set; } = default!;
        public string Location { get; set; } = default!;
    }
}
