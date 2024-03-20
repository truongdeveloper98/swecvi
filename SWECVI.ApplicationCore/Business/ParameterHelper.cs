using static SWECVI.ApplicationCore.Enum;
using SWECVI.ApplicationCore.Entities;
using SWECVI.ApplicationCore.Solution;
using SWECVI.ApplicationCore.ViewModels;

namespace SWECVI.ApplicationCore.Business
{
    public static class ParameterHelper
    {
        public static decimal? Round(decimal? value, int? displayDecimal)
        {
            if (!value.HasValue)
            {
                return null;
            }

            return (decimal)Math.Round(value.Value, displayDecimal.HasValue ? displayDecimal.Value: 2, MidpointRounding.AwayFromZero);
        }

        public static void RoundResultValue(ParameterViewModel parameter)
        {
            if (parameter == null || !parameter.ResultValue.HasValue)
            {
                return;
            }

            parameter.ResultValue = (float?)Round((decimal?)parameter.ResultValue, parameter.DisplayDecimal);
        }

        public static decimal RoundResultValueTest(double resultValue, int displayDecimal)
        {
            
            return (decimal)Round((decimal?)resultValue, displayDecimal);
        }

        public static ParameterReference MatchReference(this ParameterViewModel parameter, int? age, Gender gender)
        {
            if (parameter?.AvailableReferences != null)
            {
                return parameter.AvailableReferences.FirstOrDefault(x => x.MinAge <= age && age <= x.MaxAge && x.Gender == gender);
            }

            return new ParameterReference();
        }

        public static void AddOrUpdateParameter(this ParameterDictionary parameterDictionary, ParameterViewModel parameter)
        {
            if (parameterDictionary.value.ContainsKey(parameter.ParameterName))
            {
                parameterDictionary.value[parameter.ParameterName] = (double?)parameter.ResultValue;
            }
            else
            {
                parameterDictionary.value.Add(parameter.ParameterName, (double?)parameter.ResultValue);
            }
        }
    }
}
