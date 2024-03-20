using SWECVI.ApplicationCore.ViewModels;
using SWECVI.ApplicationCore.Entities;

namespace SWECVI.ApplicationCore.Interfaces.Services
{
    public interface IParameterService
    {
        Task<List<ParameterViewModel>> GetParametersWithReferences();
        Task<PagedResponseDto<ParameterDto.ParameterValue>> GetParameterValues(int currentPage, int pageSize, string? sortColumnDirection, string? sortColumnName, string? textSearch);
        //Dictionary<string, ParameterViewModel> GetExamParametersFromMultipleInstances(Exam exam, IEnumerable<DicomSRDto> instances);
        Task<List<ParameterDto.ParameterValuesChartByPatientId>> GetValuesByParametersForPatient(int[] ids, int patientId);
        //select y value
        Task<List<ParameterDto.ParameterSelector>> GetParameterNames();
        //select x value
        Task<List<string>> GetXValueSelector();
        Task<List<ParameterDto.EnumFunctionSelectorModel>> GetFunctionNameSelector();
        Task UpdateParameterValue(int id, ParameterDto.ParameterValue model);
        Task<List<ParameterDto.ParameterStaticChart>> GetValuesByParameters(int ySelectorId, string xValueSelector, string? period);
        Dictionary<string, ParameterViewModel> GetExamParameters(Study exam, List<ParameterViewModel> parametersWithReference);
    }
}