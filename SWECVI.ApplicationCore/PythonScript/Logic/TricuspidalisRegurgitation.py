# coding=utf-8

from d_code_result import DCodeResult
from enums import DCode
from parameter_names import ParameterNames
from reference_limits import ReferenceLimits
from helpers import GetBSA
from parameter_helper import Round, AddOrUpdateParameter


def TricuspidalisRegurgitation(Parameters,References):

    dCodeResult = DCodeResult()
    dCodeResult.Result = False
    dCodeResult.PAR = Parameters
    dCodeResult.CallLevel = 230

    ERO = None
    RV =  None

    EROReference = None
    RVReference =  None

    for it in Parameters:
          if(it['Name'] == ParameterNames.TRERO):
             ERO = it['Value']
          elif(it['Name'] == ParameterNames.TRRV):
             RV = it['Value']

    for it in References:
          if(it['ParameterNameLogic'] == ParameterNames.TRERO):
             EROReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.TRRV):
             RVReference = it

    if(ERO ==  None or RV == None or
       EROReference['NormalRangeLower'] == None or
       EROReference['NormalRangeUpper'] == None or
       EROReference['MildlyAbnormalRangeLower'] == None or
       EROReference['MildlyAbnormalRangeUpper'] == None or
       EROReference['ModeratelyAbnormalRangeLower'] == None or
       EROReference['ModeratelyAbnormalRangeUpper'] == None or
       RVReference['NormalRangeLower'] == None or
       RVReference['NormalRangeUpper'] == None or
       RVReference['MildlyAbnormalRangeUpper'] == None or
       RVReference['ModeratelyAbnormalRangeLower'] == None or
       RVReference['ModeratelyAbnormalRangeUpper'] == None or
       RVReference['MildlyAbnormalRangeLower']):
               if(RVReference['NormalRangeLower'] == None or
                  RVReference['NormalRangeUpper'] == None or
                  RVReference['MildlyAbnormalRangeUpper'] == None or
                  RVReference['ModeratelyAbnormalRangeLower'] == None or
                  RVReference['ModeratelyAbnormalRangeUpper'] == None or
                  RVReference['MildlyAbnormalRangeLower']):
                             dCodeResult.DCode = 700
                             return dCodeResult
               else:
                     if(RV == None):
                         dCodeResult.DCode = 900
                         return dCodeResult
    else:
         if(ERO == None or RV == None):
            if(RVReference['NormalRangeLower'] == None or
               RVReference['NormalRangeUpper'] == None or
               RVReference['MildlyAbnormalRangeUpper'] == None or
               RVReference['ModeratelyAbnormalRangeLower'] == None or
               RVReference['ModeratelyAbnormalRangeUpper'] == None or
               RVReference['MildlyAbnormalRangeLower']):
                             dCodeResult.DCode = 700
                             return dCodeResult
            else:
                  if(RV == None):
                      dCodeResult.DCode = 900
                      return dCodeResult

    if(RV == 0 and ERO == 0):
              dCodeResult.DCode = 0
              return dCodeResult
    elif(RV  < RVReference['MildlyAbnormalRangeLower'] and 
         ERO < EROReference['MildlyAbnormalRangeLower']):
              dCodeResult.DCode = 11
              return dCodeResult
    elif(RV  >= RVReference['MildlyAbnormalRangeLower'] and 
         RV <= RVReference['MildlyAbnormalRangeUpper'] and
         ERO >= EROReference['MildlyAbnormalRangeLower'] and
         ERO <= EROReference['MildlyAbnormalRangeUpper']):
              dCodeResult.DCode = 12
              return dCodeResult
    elif(RV  > RVReference['MildlyAbnormalRangeUpper'] and 
         RV <= RVReference['ModeratelyAbnormalRangeLower'] and
         ERO > EROReference['MildlyAbnormalRangeUpper'] and
         ERO <= EROReference['ModeratelyAbnormalRangeLower']):
              dCodeResult.DCode = 13
              return dCodeResult
    elif(RV  > RVReference['ModeratelyAbnormalRangeLower'] and 
         ERO > EROReference['ModeratelyAbnormalRangeLower']):
              dCodeResult.DCode = 14
              return dCodeResult
    else:
          dCodeResult.DCode = 800
          return dCodeResult