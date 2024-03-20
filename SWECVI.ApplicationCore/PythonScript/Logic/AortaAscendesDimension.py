# coding=utf-8

from d_code_result import DCodeResult
from enums import DCode
from parameter_names import ParameterNames
from reference_limits import ReferenceLimits
from helpers import GetBSA
from parameter_helper import Round, AddOrUpdateParameter

def AortaAscendesDimension(Parameters, References):

    dCodeResult = DCodeResult()
    dCodeResult.Result = False
    dCodeResult.PAR = Parameters
    dCodeResult.CallLeve = 11

    AOStjunctParameter = None
    AOascendensParameter = None

    AOStjunctParameterReference = None
    AOascendensParameterReference = None

    AOStjunctIndexReference = None
    AOascendensIndexReference = None


    weight = 0
    height = 0

    AOstjunctIndex = 0
    AOascendensIndex = 0

    for it in Parameters:
          if(it['Name'] == ParameterNames.AOStjunct):
             AOStjunctParameter = it['Value']
          elif(it['Name'] == ParameterNames.AOascendens):
             AOascendensParameter = it['Value']
          elif(it['Name'] == ParameterNames.PatientWeight):
             weight = it['Value']
          elif(it['Name'] == ParameterNames.PatientHeight):
             height = it['Value']


    for it in References:
          if(it['ParameterNameLogic'] == ParameterNames.AOStjunct):
             AOStjunctParameterReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.AOascendens):
             AOascendensParameterReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.AOascendensIndex):
             AOascendensIndexReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.AOStjunctIndex):
             AOStjunctIndexReference = it


    BSA = GetBSA(weight, height)

    if(BSA != None):
        if(AOStjunctParameter != None and AOascendensParameter != None):
                AOstjunctIndex = AOStjunct/BSA
                AOascendensIndex = AOascendens/BSA

                if(AOstjunctIndex >= AOStjunctIndexReference['NormalRangeUpper'] and
                   AOstjunctIndex <= AOStjunctIndexReference['NormalRangeLower'] and
                   AOascendensIndex <= AOascendensIndexReference['NormalRangeLower'] and
                   AOascendensIndex >= AOascendensIndexReference['NormalRangeUpper']):
                          dCodeResult.DCode = 0
                          return dCodeResult
                elif(AOstjunctIndex >= AOStjunctIndexReference['NormalRangeUpper'] and
                     AOstjunctIndex <= AOStjunctIndexReference['NormalRangeLower'] and
                     AOascendensIndex > AOascendensIndexReference['NormalRangeUpper']):
                          dCodeResult.DCode = 12
                          return dCodeResult
                elif(AOstjunctIndex >= AOStjunctIndexReference['NormalRangeUpper'] and
                     AOstjunctIndex <= AOStjunctIndexReference['NormalRangeLower'] and
                     AOascendensIndex < AOascendensIndexReference['NormalRangeLower']):
                          dCodeResult.DCode = 11
                          return dCodeResult
                elif(AOascendensIndex >= AOascendensIndexReference['NormalRangeUpper'] and
                     AOascendensIndex <= AOascendensIndexReference['NormalRangeLower'] and
                     AOstjunctIndex > AOStjunctIndexReference['NormalRangeUpper']):
                          dCodeResult.DCode = 12
                          return dCodeResult
                elif(AOascendensIndex >= AOascendensIndexReference['NormalRangeUpper'] and
                    AOascendensIndex <= AOascendensIndexReference['NormalRangeLower'] and
                    AOstjunctIndex < AOStjunctIndexReference['NormalRangeLower']):
                          dCodeResult.DCode = 11
                          return dCodeResult
                elif(AOstjunctIndex < AOStjunctIndexReference['NormalRangeLower'] and
                     AOascendensIndex > AOascendensIndexReference['NormalRangeUpper']):
                           dCodeResult.DCode = 13
                           return dCodeResult
                elif(AOstjunctIndex < AOStjunctIndexReference['NormalRangeLower'] and
                     AOascendensIndex < AOascendensIndexReference['NormalRangeUpper']):
                           dCodeResult.DCode = 11
                           return dCodeResult
                elif(AOstjunctIndex > AOStjunctIndexReference['NormalRangeLower'] and
                     AOascendensIndex > AOascendensIndexReference['NormalRangeUpper']):
                           dCodeResult.DCode = 12
                           return dCodeResult
                elif(AOstjunctIndex > AOStjunctIndexReference['NormalRangeLower'] and
                     AOascendensIndex < AOascendensIndexReference['NormalRangeUpper']):
                           dCodeResult.DCode = 13
                           return dCodeResult
                else:
                      dCodeResult.DCode = 700
                      return dCodeResult
        else:
             dCodeResult.DCode = GetValueByAOascendensAndAOstjunct(AOStjunctParameter,AOStjunctParameterReference, AOascendensParameter, AOascendensParameterReference)
             return dCodeResult
    else:
          dCodeResult.DCode = GetValueByAOascendensAndAOstjunct(AOStjunctParameter,AOStjunctParameterReference, AOascendensParameter, AOascendensParameterReference)
          return dCodeResult


def GetValueByAOascendensAndAOstjunct(AOStjunctParameter,AOStjunctParameterReference, AOascendensParameter, AOascendensParameterReference):
     if(AOStjunctParameter == None or
        AOStjunctParameterReference['NormalRangeLower'] == None or
        AOStjunctParameterReference['NormalRangeUpper'] == None or
        AOStjunctParameterReference['MildlyAbnormalRangeLower'] == None or
        AOStjunctParameterReference['MildlyAbnormalRangeUpper'] == None or
        AOStjunctParameterReference['ModeratelyAbnormalRangeLower'] == None or
        AOStjunctParameterReference['ModeratelyAbnormalRangeUpper'] == None):
              return GetValueByAOascendens(AOascendensParameter, AOascendensParameterReference)
     else:
           if(AOStjunctParameter == None):
               return GetValueByAOascendens(AOascendensParameter, AOascendensParameterReference)
           else:
                 if(AOStjunctParameter >= AOStjunctParameterReference['NormalRangeUpper'] and
                    AOStjunctParameter <= AOStjunctParameterReference['NormalRangeLower']):
                             return 0
                 elif(AOStjunctParameter < AOStjunctParameterReference['NormalRangeLower']):
                             return 11
                 elif(AOStjunctParameter > AOStjunctParameterReference['NormalRangeUpper']):
                             return 12
                 else:
                             return 700
               


def GetValueByAOascendens(AOascendensParameter, AOascendensParameterReference):
        if(AOascendensParameter == None or
           AOascendensParameterReference['NormalRangeLower'] == None or
           AOascendensParameterReference['NormalRangeUpper'] == None or
           AOascendensParameterReference['MildlyAbnormalRangeLower'] == None or
           AOascendensParameterReference['MildlyAbnormalRangeUpper'] == None or
           AOascendensParameterReference['ModeratelyAbnormalRangeLower'] == None or
           AOascendensParameterReference['ModeratelyAbnormalRangeUpper'] == None):
               return 700
        else:
                if(AOascendensParameter == None):
                    return 900
                else:
                    if(AOascendensParameter >= AOascendensParameterReference['NormalRangeUpper'] and
                        AOascendensParameter <= AOascendensParameterReference['NormalRangeLower']):
                                return 0
                    elif(AOascendensParameter < AOascendensParameterReference['NormalRangeLower']):
                                return 11
                    elif(AOascendensParameter > AOascendensParameterReference['NormalRangeUpper']):
                                return 12
                    else:
                                return 700