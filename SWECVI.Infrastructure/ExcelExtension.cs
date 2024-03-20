using IronXL;
using Microsoft.IdentityModel.Tokens;
using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.ViewModels;
using System.Data;

namespace SWECVI.Infrastructure
{
    public static class ExcelExtension
    {

        public static List<ManufacturerDicomParameters> GetValueFromExcel(IEnumerable<DicomTags> dicomTags)
        {
            WorkBook workbook = WorkBook.Load("C:\\Users\\LENOVO\\Downloads\\Parameter.xlsx");
            WorkSheet sheet = workbook.GetWorkSheet("Data");
            DataTable dt = sheet.ToDataTable(true);

            List<ManufacturerDicomParameters> reuslt = new List<ManufacturerDicomParameters>();

            if (dt.Rows.Count > 0)
            {
                
                string findingCSD = string.Empty;
                string findingCV = string.Empty;

                string imageModeCSD = string.Empty;
                string imageModeCV = string.Empty;

                string imageViewCSD = string.Empty;
                string imageViewCV = string.Empty;

                string cardiacPhaseCSD = string.Empty;
                string cardiacPhaseCV = string.Empty;


                string measurementMethodCSD = string.Empty;
                string measurementMethodCV = string.Empty;

                foreach (DataRow row in dt.Rows)
                {
                    findingCSD = row[13] == DBNull.Value ? string.Empty : row[13].ToString().Trim();
                    findingCV = row[14] == DBNull.Value ? string.Empty : row[14].ToString().Trim();
                    var finding = dicomTags.Where(x => x.CSD == findingCSD && x.CV == findingCV).FirstOrDefault();

                    imageModeCSD = row[16] == DBNull.Value ? string.Empty : row[16].ToString().Trim();
                    imageModeCV = row[17] == DBNull.Value ? string.Empty : row[17].ToString().Trim();
                    var imageMode = dicomTags.Where(x => x.CSD == imageModeCSD && x.CV == imageModeCV).FirstOrDefault();

                    imageViewCSD = row[19] == DBNull.Value ? string.Empty : row[19].ToString().Trim();
                    imageViewCV = row[20] == DBNull.Value ? string.Empty : row[20].ToString().Trim();
                    var imageView = dicomTags.Where(x => x.CSD == imageViewCSD && x.CV == imageViewCV).FirstOrDefault();

                    cardiacPhaseCSD = row[22] == DBNull.Value ? string.Empty : row[22].ToString().Trim();
                    cardiacPhaseCV = row[23] == DBNull.Value ? string.Empty : row[23].ToString().Trim();
                    var cardiacPhase = dicomTags.Where(x => x.CSD == cardiacPhaseCSD && x.CV == cardiacPhaseCV).FirstOrDefault();

                    measurementMethodCSD = row[25] == DBNull.Value ? string.Empty : row[25].ToString().Trim();
                    measurementMethodCV = row[26] == DBNull.Value ? string.Empty : row[26].ToString().Trim();
                    var measurementMethod = dicomTags.Where(x => x.CSD == measurementMethodCSD && x.CV == measurementMethodCV).FirstOrDefault();

                    var model = new ManufacturerDicomParameters()
                    {
                        ProviderId = "GE",
                        ProviderParameterId = "GE" +  row[1].ToString(),
                        ParameterId = row[1] == DBNull.Value ? string.Empty : row[1].ToString().Trim(),
                        ProviderParameterShortName = row[2] == DBNull.Value ? string.Empty : row[2].ToString().Trim(),
                        ParameterNameLogic = row[2] == DBNull.Value ? string.Empty : row[2].ToString().Trim(),
                        MeasurementCSD = row[10] == DBNull.Value ? string.Empty : row[10].ToString().Trim(),
                        MeasurementCV = row[11] == DBNull.Value ? string.Empty : row[11].ToString().Trim(),
                        MeasurementCM = row[12] == DBNull.Value ? string.Empty : row[12].ToString().Trim(),
                        FindingSite = finding != null ? finding.Id : null,
                        ImageMode = imageMode != null ? imageMode.Id : null,
                        ImageView = imageView != null ? imageView.Id : null,
                        CardiacPhase = cardiacPhase != null ? cardiacPhase.Id : null,
                        MeasurementMethod = measurementMethod != null ? measurementMethod.Id : null,
                    };

                    reuslt.Add(model);
                }
            }


            return reuslt;
        }

        public static List<DicomtagParameterViewModel> ReadExcel()
        {
            string fileName = "C:\\Users\\LENOVO\\Downloads\\Parameters.xlsx";
            WorkBook workbook = WorkBook.Load(fileName);
         
            WorkSheet sheet = workbook.DefaultWorkSheet;
          
            DataTable data = sheet.ToDataTable(true);

            List<DicomtagParameterViewModel> parameters = new List<DicomtagParameterViewModel>();

            foreach (DataRow item in data.Rows)
            {
                DicomtagParameterViewModel model = new DicomtagParameterViewModel()
                {
                    ParameterID = item[1] == DBNull.Value ? string.Empty : item[1].ToString(),
                    ParameterShortName = item[2] == DBNull.Value ? string.Empty : item[2].ToString(),
                    MeasurementConceptCSD = item[3] == DBNull.Value ? string.Empty : item[3].ToString(),
                    MeasurementConceptCV = item[4] == DBNull.Value ? string.Empty : item[4].ToString(),
                    MeasurementConceptCM = item[5] == DBNull.Value ? string.Empty : item[5].ToString(),
                    FindingSiteCSD = item[6] == DBNull.Value ? string.Empty : item[6].ToString(),
                    FindingsSiteCV = item[7] == DBNull.Value ? string.Empty : item[7].ToString(),
                    FindingsSiteCM = item[8] == DBNull.Value ? string.Empty : item[8].ToString(),
                    ImageModeCSD = item[9] == DBNull.Value ? string.Empty : item[9].ToString(),
                    ImageModeCV = item[10] == DBNull.Value ? string.Empty : item[10].ToString(),
                    ImageModeCM = item[11] == DBNull.Value ? string.Empty : item[11].ToString(),
                    ImageViewCSD = item[12] == DBNull.Value ? string.Empty : item[12].ToString(),
                    ImageViewCV = item[13] == DBNull.Value ? string.Empty : item[13].ToString(),
                    ImageViewCM = item[14] == DBNull.Value ? string.Empty : item[14].ToString(),
                    CardiacPhaseCSD = item[15] == DBNull.Value ? string.Empty : item[15].ToString(),
                    CardiacPhaseCV = item[16] == DBNull.Value ? string.Empty : item[16].ToString(),
                    CardiacPhaseCM = item[17] == DBNull.Value ? string.Empty : item[17].ToString(),
                    MeausermentMethodCSD = item[18] == DBNull.Value ? string.Empty : item[18].ToString(),
                    MeausermentMethodCV = item[19] == DBNull.Value ? string.Empty : item[19].ToString(),
                    MeausermentMethodCM = item[20] == DBNull.Value ? string.Empty : item[20].ToString(),
                    FlowDirectionCSD = item[21] == DBNull.Value ? string.Empty : item[21].ToString(),
                    FlowDirectionCV = item[22] == DBNull.Value ? string.Empty : item[22].ToString(),
                    FlowDirectionCM = item[23] == DBNull.Value ? string.Empty : item[23].ToString(),
                    AnatomicalSiteCSD = item[24] == DBNull.Value ? string.Empty : item[24].ToString(),
                    AnatomicalSiteCV = item[25] == DBNull.Value ? string.Empty : item[25].ToString(),
                    AnatomicalSiteCM = item[26] == DBNull.Value ? string.Empty : item[26].ToString(),
                };

                parameters.Add(model);
            }

            return parameters;
        }

    }
}
