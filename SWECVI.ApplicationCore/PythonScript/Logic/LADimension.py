# coding=utf-8

from d_code_result import DCodeResult
from enums import DCode
from parameter_names import ParameterNames
from reference_limits import ReferenceLimits
from helpers import GetBSA
from parameter_helper import Round, AddOrUpdateParameter


def LADimension(Parameters, References):

    dCodeResult = DCodeResult()
    dCodeResult.Result = False
    dCodeResult.PAR = Parameters
    dCodeResult.CallLevel = 20

    LAESV = None
    LAAs = None
    LADs = None
    LADsIndex = None
    LAESVIndex = None
    LAAsIndex = None
    weight = 0
    height = 0

    LAESVIndexReference = None
    LAAsIndexReference = None
    LADsIndexReference = None

    for it in Parameters:
          if(it['Name'] == ParameterNames.LAESV):
             LAESV = it['Value']
          elif(it['Name'] == ParameterNames.LAAs):
             LAAs = it['Value']
          elif(it['Name'] == ParameterNames.LADs):
             LADs = it['Value']
          elif(it['Name'] == ParameterNames.PatientWeight):
             weight = it['Value']
          elif(it['Name'] == ParameterNames.PatientHeight):
             height = it['Value']

    for it in References:
          if(it['ParameterNameLogic'] == ParameterNames.LAESV):
             LAESVReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.LAAs):
             LAAsReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.LADs):
             LADsReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.LAAsIndex):
             LAAsIndexReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.LADsIndex):
             LADsIndexReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.LAESVIndex):
             LAESVIndexReference = it

    
    BSA = GetBSA(weight, height)

    if(BSA == None):
        dCodeResult.DCode = GetValueByLAAs(LAAs, LAAsReference)
        return dCodeResult
    else:
          if(LAAs != None):
                    LAAsIndex =  LAAs/BSA
                    if(LAAsIndex >= LAAsIndexReference['NormalRangeLower'] and
                       LAAsIndex <= LAAsIndexReference['NormalRangeUpper']):
                                dCodeResult.DCode = 0
                                return dCodeResult
                    elif(LAAsIndex > LAAsIndexReference['NormalRangeUpper'] and
                         LAAsIndex <= LAAsIndexReference['MildlyAbnormalRangeLower']):
                                dCodeResult.DCode = 13
                                return dCodeResult
                    elif(LAAsIndex > LAAsIndexReference['MildlyAbnormalRangeLower']):
                                dCodeResult.DCode = 14
                                return dCodeResult
                    else:
                                dCodeResult.DCode = 800
                                return dCodeResult
          else:
                    if (LAESVReference['NormalRangeLower'] == None or
                        LAESVReference['NormalRangeUpper'] == None or
                        LAESVReference['MildlyAbnormalRangeLower'] == None or
                        LAESVReference['MildlyAbnormalRangeUpper'] == None or
                        LAESVReference['ModeratelyAbnormalRangeLower'] == None or
                        LAESVReference['ModeratelyAbnormalRangeUpper'] == None or
                        LAESVReference['SeverelyAbnormalRangeMoreThan'] == None):
                                dCodeResult.DCode = GetValueByLAAsIndex(LADs, BSA, LADsReference)
                                return dCodeResult
                    else:
                            if(LAESV == None):
                                dCodeResult.DCode = GetValueByLAAsIndex(LADs, BSA, LADsReference)
                                return dCodeResult
                            else:
                                LAESVIndex =  LAESV / BSA
                                if(LAESVIndex >= LAESVIndexReference['NormalRangeLower'] and
                                   LAESVIndex <= LAESVIndexReference['NormalRangeUpper']):
                                            dCodeResult.DCode = 0
                                            return dCodeResult
                                elif(LAESVIndex >= LAESVIndexReference['NormalRangeUpper'] and
                                     LAESVIndex <= LAESVIndexReference['MildlyAbnormalRangeLower']):
                                            dCodeResult.DCode = 11
                                            return dCodeResult
                                elif(LAESVIndex > LAESVIndexReference['MildlyAbnormalRangeLower'] and
                                     LAESVIndex <= LAESVIndexReference['MildlyAbnormalRangeLower']):
                                            dCodeResult.DCode = 12
                                            return dCodeResult
                                elif(LAESVIndex > LAESVIndexReference['ModeratelyAbnormalRangeLower'] and
                                     LAESVIndex <= LAESVIndexReference['ModeratelyAbnormalRangeUpper']):
                                            dCodeResult.DCode = 13
                                            return dCodeResult
                                elif(LAESVIndex > LAESVIndexReference['ModeratelyAbnormalRangeUpper']):
                                            dCodeResult.DCode = 14
                                            return dCodeResult
                                else:
                                            dCodeResult.DCode = 800
                                            return dCodeResult
                                   


def GetValueByLAAs(LAAs, LAAsReference):
    if (LAAsReference['NormalRangeLower'] != None and
        LAAsReference['MildlyAbnormalRangeLower'] != None and
        LAAsReference['NormalRangeUpper'] != None):
            if(LAAs != None):
                if(LAAs >= LAAsReference['NormalRangeLower'] and
                   LAAs <= LAAsReference['NormalRangeUpper']):
                        return 0
                elif(LAAs > LAAsReference['NormalRangeUpper'] and
                     LAAs <= LAAsReference['MildlyAbnormalRangeLower']):
                        return 13
                elif(LAAs > LAAsReference['MildlyAbnormalRangeLower']):
                        return 14
                else:
                        return 800
            else:
                  return 900
    else:
          return 700




def GetValueByLAAsIndex(LADs, BSA, LADsIndexReference):
    if (LADsReference['NormalRangeLower'] != None and
        LADsReference['NormalRangeUpper'] != None):
            if(LADs != None):
                LADsIndex =  LADs/BSA
                if(LADsIndex >= LADsIndexReference['NormalRangeLower'] and
                   LADsIndex <= LADsIndexReference['NormalRangeUpper']):
                        return 0
                elif(LADsIndex >= LADsIndexReference['NormalRangeUpper'] and
                     LADsIndex <= LADsIndexReference['MildlyAbnormalRangeLower']):
                        return 11
                elif(LADsIndex > LADsIndexReference['MildlyAbnormalRangeLower'] and
                     LADsIndex <= LADsIndexReference['MildlyAbnormalRangeUpper']):
                        return 12
                elif(LADsIndex > LADsIndexReference['ModeratelyAbnormalRangeLower'] and
                     LADsIndex <= LADsIndexReference['ModeratelyAbnormalRangeUpper']):
                        return 13
                elif(LADsIndex > LADsIndexReference['ModeratelyAbnormalRangeUpper']):
                        return 14
                else:
                        return 800
            else:
                  return 900
    else:
          return 700