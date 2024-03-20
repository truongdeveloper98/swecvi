# coding=utf-8

from d_code_result import DCodeResult
from enums import DCode
from parameter_names import ParameterNames
from reference_limits import ReferenceLimits
from helpers import GetBSA
from parameter_helper import Round, AddOrUpdateParameter


def MitralRegurgitation(Parameters,References):

    dCodeResult = DCodeResult()
    dCodeResult.Result = False
    dCodeResult.PAR = Parameters
    dCodeResult.CallLevel = 210

    ERO = None
    RV =  None

    EROReference = None
    RVReference =  None

    for it in Parameters:
          if(it['Name'] == ParameterNames.MRERO):
             ERO = it['Value']
          elif(it['Name'] == ParameterNames.MRRV):
             RV = it['Value']

    for it in References:
          if(it['ParameterNameLogic'] == ParameterNames.MRERO):
             EROReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.MRRV):
             RVReference = it
         
    if(EROReference['MildlyAbnormalRangeLower'] == None or
       EROReference['MildlyAbnormalRangeUpper'] == None or
       RVReference['MildlyAbnormalRangeLower'] == None or
       RVReference['MildlyAbnormalRangeUpper'] == None or
       RVReference['ModeratelyAbnormalRangeLower'] == None or
       EROReference['ModeratelyAbnormalRangeLower'] == None):
             dCodeResult.DCode = MitralRegurgitationMRRV(RV, RVReference)
             return dCodeResult
    else:
          if(ERO == None or RV == None):
               dCodeResult.DCode = MitralRegurgitationMRRV(RV, RVReference)
               return dCodeResult
          else:
                 if(RV == 0 and ERO == 0):
                           dCodeResult.DCode = 0
                           return dCodeResult
                 elif(RV < RVReference['MildlyAbnormalRangeLower'] and
                      ERO < EROReference['MildlyAbnormalRangeLower']):
                           dCodeResult.DCode = 11
                           return dCodeResult
                 elif(RV >= RVReference['MildlyAbnormalRangeLower'] and 
                      RV <= RVReference['MildlyAbnormalRangeUpper'] and
                      ERO >= EROReference['MildlyAbnormalRangeLower'] and 
                      ERO <= EROReference['MildlyAbnormalRangeUpper']):
                           dCodeResult.DCode = 12
                           return dCodeResult
                 elif(RV >= RVReference['MildlyAbnormalRangeUpper'] and 
                      RV < RVReference['ModeratelyAbnormalRangeLower'] and 
                      ERO >= EROReference['MildlyAbnormalRangeUpper'] and 
                      ERO < EROReference['ModeratelyAbnormalRangeLower']):
                           dCodeResult.DCode = 13
                           return dCodeResult
                 elif(RV >= RVReference['ModeratelyAbnormalRangeLower'] and
                      ERO >= EROReference['ModeratelyAbnormalRangeLower']):
                           dCodeResult.DCode = 14
                           return dCodeResult
                 else:
                           dCodeResult.DCode = 800
                           return dCodeResult




def MitralRegurgitationMRRV(RV, RVReference):
     if(RVReference['MildlyAbnormalRangeLower'] == None or
        RVReference['MildlyAbnormalRangeUpper'] == None or
        RVReference['ModeratelyAbnormalRangeLower'] == None):
             return 700
     else:
           if(RV == None):
               return 900
           else:
                 if(RV == 0):
                      return 0
                 elif(RV < RVReference['MildlyAbnormalRangeLower']):
                           return 11
                 elif(RV >= RVReference['MildlyAbnormalRangeLower'] and 
                      RV <= RVReference['MildlyAbnormalRangeUpper']):
                           return 12
                 elif(RV >= RVReference['MildlyAbnormalRangeUpper'] and 
                      RV < RVReference['ModeratelyAbnormalRangeLower']):
                           return 13
                 elif(RV >= RVReference['ModeratelyAbnormalRangeLower']):
                           return 14
                 else:
                       return 800