from d_code_result import DCodeResult
from enums import DCode
from parameter_names import ParameterNames
from reference_limits import ReferenceLimits
from helpers import GetBSA
import math


def AortaRootDimension(Parameters, References):

    dCodeResult = DCodeResult()
    dCodeResult.Result = False
    dCodeResult.CallLevel = 10

    AOsinus = None
    AOsinusIndex = None

    AOsinusReference = None
    AOsinusIndexReference = None

    weight = 0
    height = 0

    for it in Parameters:
          if(it['Name'] == ParameterNames.AOsinus):
             AOsinus = it['Value']
          elif(it['Name'] == ParameterNames.PatientWeight):
             weight = it['Value']
          elif(it['Name'] == ParameterNames.PatientHeight):
             height = it['Value']

    for it in References:
          if(it['ParameterNameLogic'] == ParameterNames.AOsinus):
             AOsinusReference = it
          if(it['ParameterNameLogic'] == ParameterNames.AOsinusIndex):
             AOsinusIndexReference = it

    BSA = GetBSA(weight, height)

    if(BSA != None):
         if(AOsinusIndexReference['NormalRangeLower'] != None and
            AOsinusIndexReference['NormalRangeUpper'] != None):
                  if(AOsinus != None):
                        AOsinusIndex =  AOsinus/BSA
                        if(AOsinusIndex < AOsinusIndexReference['NormalRangeLower']):
                               dCodeResult.DCode = 0
                               return dCodeResult
                        elif(AOsinusIndex >= AOsinusIndexReference['NormalRangeLower'] and
                             AOsinusIndex <= AOsinusIndexReference['NormalRangeUpper']):
                                 dCodeResult.DCode = 11
                                 return 
                        elif(AOsinusIndex > AOsinusIndexReference['NormalRangeUpper']):
                                 dCodeResult.DCode = 12
                                 return dCodeResult
                        else:
                                 dCodeResult.DCode = 800
                                 return dCodeResult   

                  else:
                        dCodeResult.DCode = AortaRootDimensionAOsinus(AOsinus, AOsinusReference)
                        return dCodeResult 
         else:
                dCodeResult.DCode = AortaRootDimensionAOsinus(AOsinus, AOsinusReference)
                return dCodeResult 
    else:
            dCodeResult.DCode = AortaRootDimensionAOsinus(AOsinus, AOsinusReference)
            return dCodeResult 


def AortaRootDimensionAOsinus(AOsinus, AOsinusReference):
     if(AOsinusReference['NormalRangeLower'] != None and
        AOsinusReference['NormalRangeUpper'] != None):
            if(AOsinus != None):
                if(AOsinus < AOsinusReference['NormalRangeLower']):
                            return 0
                elif(AOsinus >= AOsinusReference['NormalRangeLower'] and
                     AOsinus <= AOsinusReference['NormalRangeUpper']):
                            return 11
                elif(AOsinus > AOsinusReference['NormalRangeUpper']):
                            return 12
                else:
                            return 800  
            else:
                   return 900
     else:
            return 700  