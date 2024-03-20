# coding=utf-8

from decimal import *
from reference_limits import ReferenceLimits

def Round(value, displayDecimal = 2):
    if value == None:
        return None
    return round(value, displayDecimal)


def AddOrUpdateParameter(parameterDictionary, parameter):
    parameterDictionary['value'][parameter['ParameterName']] = float(parameter['ResultValue']) if parameter['ResultValue'] != None else None 


def AddOrUpdateReference(referenceDictionary, parameter):
    if (parameter.Reference != None):
        refMinName = parameter['ParameterName'] + ReferenceLimits.Min
        refMaxName = parameter['ParameterName'] + ReferenceLimits.Max
        
        referenceDictionary['value'][refMinName] = float(parameter.Reference.Min)

        referenceDictionary['value'][refMaxName] = float(parameter.Reference.Max)

        refLowName = parameter.ParameterName + ReferenceLimits.Low
        
        referenceDictionary['value'][refLowName] = float(parameter.Reference.Low)

        normalLowerName = parameter.ParameterName + ReferenceLimits.NormalLower
        normalUpperName = parameter.ParameterName + ReferenceLimits.NormalUpper
        mildlyLowerName = parameter.ParameterName + ReferenceLimits.MildlyLower
        mildlyUpperName = parameter.ParameterName + ReferenceLimits.MildlyUpper
        moderatelyLowerName = parameter.ParameterName + ReferenceLimits.ModeratelyLower
        moderatelyUpperName = parameter.ParameterName + ReferenceLimits.ModeratelyUpper
        severelyLimitName = parameter.ParameterName + ReferenceLimits.SeverelyLimit

        referenceDictionary['value'][normalLowerName] = float(parameter.Reference.NormalLower)
    
        referenceDictionary['value'][normalUpperName] = float(parameter.Reference.NormalUpper)
    
        referenceDictionary['value'][mildlyLowerName] = float(parameter.Reference.MildlyLower)
    
        referenceDictionary['value'][mildlyUpperName] = float(parameter.Reference.MildlyUpper)

        referenceDictionary['value'][moderatelyLowerName] = float(parameter.Reference.ModeratelyLower)
        
        referenceDictionary['value'][moderatelyUpperName] = float(parameter.Reference.ModeratelyUpper)
        
        referenceDictionary['value'][severelyLimitName] = float(parameter.Reference.SeverelyLimit)
