using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWECVI.ApplicationCore.Entities
{
    public class ManufacturerDicomParameters : BaseEntity
    {
        public string? ProviderId { get; set; }
        public string? ProviderParameterId { get; set; }
        public string? ParameterId { get; set; }
        public string? ProviderParameterShortName { get; set; }
        public string? ParameterNameLogic { get; set; }
        public string? MeasurementCSD { get; set; }
        public string? MeasurementCV { get; set; }
        public string? MeasurementCM { get; set; }
        public int? FindingSite { get; set; }
        public int? ImageMode { get; set; }
        public int? ImageView { get; set; }
        public int? CardiacPhase { get; set; }
        public int? MeasurementMethod { get; set; }
        public int? FlowDirection { get; set; }
        public int? AnatomicalSite { get; set; }
        public virtual DicomTags DicomTags1 { get; set; }
        public virtual DicomTags DicomTags2 { get; set; }
        public virtual DicomTags DicomTags3 { get; set; }
        public virtual DicomTags DicomTags4 { get; set; }
        public virtual DicomTags DicomTags5 { get; set; }
        public virtual DicomTags DicomTags6 { get; set; }
    }
}
