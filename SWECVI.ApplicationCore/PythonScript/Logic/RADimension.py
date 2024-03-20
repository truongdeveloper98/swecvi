# coding=utf-8

from d_code_result import DCodeResult
from enums import DCode
from parameter_names import ParameterNames
from reference_limits import ReferenceLimits
from helpers import GetBSA

from parameter_helper import Round, AddOrUpdateParameter


def RADimension(Parameters, References):

    dCodeResult = DCodeResult()
    dCodeResult.Result = False
    dCodeResult.PAR = Parameters
    dCodeResult.CallLevel = 60

    RAESV = None
    RAESVIndex = None

    RAESVReference = None
    RAESVIndexReference = None


    weight = 0
    height = 0

    for it in Parameters:
          if(it['Name'] == ParameterNames.RAESV):
             RAESV = it['Value']
          elif(it['Name'] == ParameterNames.RAESVIndex):
             RAESV = it['Value']
          elif(it['Name'] == ParameterNames.PatientWeight):
             weight = it['Value']
          elif(it['Name'] == ParameterNames.PatientHeight):
             height = it['Value']

    for it in References:
        if(it['ParameterNameLogic'] == ParameterNames.RAESV):
            RAESVReference = it
        if(it['ParameterNameLogic'] == ParameterNames.RAESVIndex):
            RAESVIndexReference = it



    BSA = GetBSA(weight,height)


    if(BSA == None):
           if(RAESVReference['NormalRangeLower'] == None or
              RAESVReference['NormalRangeUpper'] == None):
                      dCodeResult.DCode = 700
                      return dCodeResult
           else:
                 if(RAESV == None):
                      dCodeResult.DCode = 900
                      return dCodeResult
                 else:
                       if(RAESV >= RAESVReference['NormalRangeLower'] and
                          RAESV <= RAESVReference['NormalRangeUpper']):
                               dCodeResult.DCode = 0
                               return dCodeResult
                       elif(RAESV > RAESVReference['NormalRangeUpper']):
                               dCodeResult.DCode = 11
                               return dCodeResult
                       else:
                             dCodeResult.DCode = 14
                             return dCodeResult
    else:
            if(RAESVIndexReference['NormalRangeLower'] == None or
               RAESVIndexReference['NormalRangeUpper'] == None):
                       if(RAESVReference['NormalRangeLower'] == None or
                          RAESVReference['NormalRangeUpper'] == None):
                                  dCodeResult.DCode = 700
                                  return dCodeResult
                       else:
                             if(RAESV == None):
                                  dCodeResult.DCode = 900
                                  return dCodeResult
                             else:
                                   if(RAESV >= RAESVReference['NormalRangeLower'] and
                                      RAESV <= RAESVReference['NormalRangeUpper']):
                                           dCodeResult.DCode = 0
                                           return dCodeResult
                                   elif(RAESV > RAESVReference['NormalRangeUpper']):
                                           dCodeResult.DCode = 11
                                           return dCodeResult
                                   else:
                                         dCodeResult.DCode = 14
                                         return dCodeResult
            else:
                  if(RAESV == None):
                          if(RAESVReference['NormalRangeLower'] == None or
                             RAESVReference['NormalRangeUpper'] == None):
                                  dCodeResult.DCode = 700
                                  return dCodeResult
                          else:
                                if(RAESV == None):
                                     dCodeResult.DCode = 900
                                     return dCodeResult
                                else:
                                      if(RAESV >= RAESVReference['NormalRangeLower'] and
                                         RAESV <= RAESVReference['NormalRangeUpper']):
                                              dCodeResult.DCode = 0
                                              return dCodeResult
                                      elif(RAESV > RAESVReference['NormalRangeUpper']):
                                              dCodeResult.DCode = 11
                                              return dCodeResult
                                      else:
                                            dCodeResult.DCode = 14
                                            return dCodeResult
                  else:
                        RAESVIndex =  RAESV / BSA

                        if(RAESVIndex >= RAESVIndexReference['NormalRangeLower'] and
                           RAESVIndex <= RAESVIndexReference['NormalRangeUpper']):
                                dCodeResult.DCode = 0
                                return dCodeResult
                        elif(RAESVIndex > RAESVIndexReference['NormalRangeUpper']):
                                dCodeResult.DCode = 11
                                return dCodeResult
                        else:
                                dCodeResult.DCode = 14
                                return dCodeResult