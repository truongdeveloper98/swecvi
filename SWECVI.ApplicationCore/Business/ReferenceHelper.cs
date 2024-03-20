//using SWECVI.ApplicationCore.Solution;
//using SWECVI.ApplicationCore.ViewModels;

//namespace SWECVI.ApplicationCore.Business
//{
//    public static class ReferenceHelper
//    {
//        public static void AddOrUpdateReference(this ReferenceDictionary referenceDictionary, ParameterViewModel parameter)
//        {
//            if (parameter.Reference != null)
//            {
//                var refMinName = parameter.ParameterName + ReferenceLimits.Min;
//                var refMaxName = parameter.ParameterName + ReferenceLimits.Max;
//                if (referenceDictionary.value.ContainsKey(refMinName))
//                {
//                    referenceDictionary.value[refMinName] = (double?)parameter.Reference.ReferenceMin;
//                }
//                else
//                {
//                    referenceDictionary.value.Add(refMinName, (double?)parameter.Reference.ReferenceMin);
//                }

//                if (referenceDictionary.value.ContainsKey(refMaxName))
//                {
//                    referenceDictionary.value[refMaxName] = (double?)parameter.Reference.ReferenceMax;
//                }
//                else
//                {
//                    referenceDictionary.value.Add(refMaxName, (double?)parameter.Reference.ReferenceMax);
//                }

//                var refLowName = parameter.ParameterName + ReferenceLimits.Low;
//                if (referenceDictionary.value.ContainsKey(refLowName))
//                {
//                    referenceDictionary.value[refLowName] = (double?)parameter.Reference.ReferenceLow;
//                }
//                else
//                {
//                    referenceDictionary.value.Add(refLowName, (double?)parameter.Reference.ReferenceLow);
//                }

//                var normalLowerName = parameter.ParameterName + ReferenceLimits.NormalLower;
//                var normalUpperName = parameter.ParameterName + ReferenceLimits.NormalUpper;
//                var mildlyLowerName = parameter.ParameterName + ReferenceLimits.MildlyLower;
//                var mildlyUpperName = parameter.ParameterName + ReferenceLimits.MildlyUpper;
//                var moderatelyLowerName = parameter.ParameterName + ReferenceLimits.ModeratelyLower;
//                var moderatelyUpperName = parameter.ParameterName + ReferenceLimits.ModeratelyUpper;
//                var severelyLimitName = parameter.ParameterName + ReferenceLimits.SeverelyLimit;

//                if (referenceDictionary.value.ContainsKey(normalLowerName))
//                {
//                    referenceDictionary.value[normalLowerName] = (double?)parameter.Reference.NormalRangeLower;
//                }
//                else
//                {
//                    referenceDictionary.value.Add(normalLowerName, (double?)parameter.Reference.NormalRangeLower);
//                }

//                if (referenceDictionary.value.ContainsKey(normalUpperName))
//                {
//                    referenceDictionary.value[normalUpperName] = (double?)parameter.Reference.NormalRangeUpper;
//                }
//                else
//                {
//                    referenceDictionary.value.Add(normalUpperName, (double?)parameter.Reference.NormalRangeUpper);
//                }

//                if (referenceDictionary.value.ContainsKey(mildlyLowerName))
//                {
//                    referenceDictionary.value[mildlyLowerName] = (double?)parameter.Reference.MildlyAbnormalRangeLower;
//                }
//                else
//                {
//                    referenceDictionary.value.Add(mildlyLowerName, (double?)parameter.Reference.MildlyAbnormalRangeLower);
//                }

//                if (referenceDictionary.value.ContainsKey(mildlyUpperName))
//                {
//                    referenceDictionary.value[mildlyUpperName] = (double?)parameter.Reference.MildlyAbnormalRangeUpper;
//                }
//                else
//                {
//                    referenceDictionary.value.Add(mildlyUpperName, (double?)parameter.Reference.MildlyAbnormalRangeUpper);
//                }

//                if (referenceDictionary.value.ContainsKey(moderatelyLowerName))
//                {
//                    referenceDictionary.value[moderatelyLowerName] = (double?)parameter.Reference.ModeratelyAbnormalRangeLower;
//                }
//                else
//                {
//                    referenceDictionary.value.Add(moderatelyLowerName, (double?)parameter.Reference.ModeratelyAbnormalRangeLower);
//                }

//                if (referenceDictionary.value.ContainsKey(moderatelyUpperName))
//                {
//                    referenceDictionary.value[moderatelyUpperName] = (double?)parameter.Reference.ModeratelyAbnormalRangeUpper;
//                }
//                else
//                {
//                    referenceDictionary.value.Add(moderatelyUpperName, (double?)parameter.Reference.ModeratelyAbnormalRangeUpper);
//                }

//                if (referenceDictionary.value.ContainsKey(severelyLimitName))
//                {
//                    referenceDictionary.value[severelyLimitName] = (double?)parameter.Reference.SeverelyAbnormalRangeMoreThan;
//                }
//                else
//                {
//                    referenceDictionary.value.Add(severelyLimitName, (double?)parameter.Reference.SeverelyAbnormalRangeLessThan);
//                }
//            }
//        }
//    }
//}
