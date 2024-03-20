from d_code_result import DCodeResult
from enums import DCode
from parameter_names import ParameterNames
from reference_limits import ReferenceLimits
from helpers import GetBSA
import math


def AortaRegurgitation(Parameters, References):

    dCodeResult = DCodeResult()
    dCodeResult.Result = False
    dCodeResult.CallLevel = 200

    ARERO = None
    ARRV = None

    AREROReference = None
    ARRVReference = None


    for it in Parameters:
          if(it['Name'] == ParameterNames.ARERO):
             ARERO = it['Value']
          elif(it['Name'] == ParameterNames.ARRV):
             ARRV = it['Value']

    for it in References:
          if(it['ParameterNameLogic'] == ParameterNames.ARERO):
             AREROReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.ARRV):
             ARRVReference = it


    if(ARERO != None and ARRV != None):
        if(AREROReference['MildlyAbnormalRangeLower'] != None and
           AREROReference['MildlyAbnormalRangeUpper'] != None and
           AREROReference['ModeratelyAbnormalRangeLower'] != None and
           ARRVReference['MildlyAbnormalRangeLower'] != None and
           ARRVReference['MildlyAbnormalRangeUpper'] != None and
           ARRVReference['ModeratelyAbnormalRangeLower'] != None):
                    if(ARERO != None and ARRV != None):
                           if(ARERO == 0 and ARRV == 0):
                                     dCodeResult.DCode = 0
                                     return dCodeResult
                           elif(ARERO < AREROReference['MildlyAbnormalRangeLower'] and
                                ARRV < ARRVReference['MildlyAbnormalRangeLower']):
                                     dCodeResult.DCode = 11
                                     return dCodeResult
                           elif(ARERO >= AREROReference['MildlyAbnormalRangeLower'] and
                                ARERO <= AREROReference['MildlyAbnormalRangeUpper'] and
                                ARRV >= ARRVReference['MildlyAbnormalRangeLower'] and
                                ARRV <= ARRVReference['MildlyAbnormalRangeUpper']):
                                     dCodeResult.DCode = 12
                                     return dCodeResult
                           elif(ARERO > AREROReference['MildlyAbnormalRangeUpper'] and
                                ARERO <= AREROReference['ModeratelyAbnormalRangeLower'] and
                                ARRV > ARRVReference['MildlyAbnormalRangeUpper'] and
                                ARRV <= ARRVReference['ModeratelyAbnormalRangeLower']):
                                     dCodeResult.DCode = 13
                                     return dCodeResult
                           elif(ARERO > AREROReference['ModeratelyAbnormalRangeLower'] and
                                ARRV > ARRVReference['ModeratelyAbnormalRangeLower']):
                                     dCodeResult.DCode = 14
                                     return dCodeResult
                           else:
                                     dCodeResult.DCode = 800
                                     return dCodeResult   
                    else:
                            dCodeResult.DCode = AortaRegurgitationWithARRV(ARRV, ARRVReference)
                            return dCodeResult 
        else:
                dCodeResult.DCode = AortaRegurgitationWithARRV(ARRV, ARRVReference)
                return dCodeResult 
    else:
                dCodeResult.DCode = AortaRegurgitationWithARRV(ARRV, ARRVReference)
                return dCodeResult 


def AortaRegurgitationWithARRV(ARRV, ARRVReference):
     if(ARRV == None):
        return 700
     else:
         if(ARRVReference['MildlyAbnormalRangeLower'] == None or
            ARRVReference['MildlyAbnormalRangeUpper'] == None or
            ARRVReference['ModeratelyAbnormalRangeLower'] == None):
                      return 700
         else:
               if(ARRV == None):
                      return 900
               else: 
                     if(ARRV == 0):
                                return 0
                     elif(ARRV < ARRVReference['MildlyAbnormalRangeLower']):
                                 return 11
                     elif(ARRV >= ARRVReference['MildlyAbnormalRangeLower'] and
                          ARRV <= ARRVReference['MildlyAbnormalRangeUpper']):
                                 return 12
                     elif(ARRV > ARRVReference['MildlyAbnormalRangeUpper'] and
                          ARRV <= ARRVReference['ModeratelyAbnormalRangeLower']):
                                 return 13
                     elif(ARRV > ARRVReference['ModeratelyAbnormalRangeLower']):
                                 return 14
                     else:
                                 return 800   
                  
                 