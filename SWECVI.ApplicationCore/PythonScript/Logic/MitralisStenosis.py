# coding=utf-8

from d_code_result import DCodeResult
from enums import DCode
from parameter_names import ParameterNames
from reference_limits import ReferenceLimits
from helpers import GetBSA
from parameter_helper import Round, AddOrUpdateParameter


def MitralisStenosis(Parameters, References):

    dCodeResult = DCodeResult()
    dCodeResult.Result = False
    dCodeResult.PAR = Parameters

    MVA = None
    MVVTI = None
    MVmeanPG = None
    LVOTVmax = None
    LVOTD = None
    LVOTVTI = None



    MVAReference = None
    MVVTIReference = None
    MVmeanPGReference = None
    LVOTVmaxReference = None
    LVOTDReference = None
    LVOTVTIReference = None

    for it in Parameters:
          if(it['Name'] == ParameterNames.MVA):
             MVA = it['Value']
          elif(it['Name'] == ParameterNames.MVVTI):
             MVVTI = it['Value']
          elif(it['Name'] == ParameterNames.MVmeanPG):
             MVmeanPG = it['Value']
          elif(it['Name'] == ParameterNames.LVOTVmax):
             LVOTVmax = it['Value']
          elif(it['Name'] == ParameterNames.LVOTD):
             LVOTD = it['Value']
          elif(it['Name'] == ParameterNames.LVOTVTI):
             LVOTVTI = it['Value']

    for it in References:
          if(it['ParameterNameLogic'] == ParameterNames.MVA):
             MVAReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.MVVTI):
             MVVTIReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.MVmeanPG):
             MVmeanPGReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.LVOTVmax):
             LVOTVmaxReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.LVOTD):
             LVOTDReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.LVOTVTI):
             LVOTVTIReference = it

    if(MVAReference['NormalRangeLower'] == None or
       MVAReference['NormalRangeUpper'] == None or
       MVAReference['MildlyAbnormalRangeLower'] == None):
            dCodeResult.DCode = GetValueByMVmeanPGs(MVA,MVmeanPG,LVOTVmax,LVOTD,MVVTI,LVOTVTI, MVAReference,MVmeanPGReference,LVOTVmaxReference,LVOTDReference,MVVTIReference,LVOTVTIReference)
            return dCodeResult
    else:
        if(MVA == None):
            dCodeResult.DCode = GetValueByMVmeanPGs(MVA,MVmeanPG,LVOTVmax,LVOTD,MVVTI,LVOTVTI, MVAReference,MVmeanPGReference,LVOTVmaxReference,LVOTDReference,MVVTIReference,LVOTVTIReference)
            return dCodeResult
        else:
                if(MVA > MVAReference['MildlyAbnormalRangeLower']):
                             dCodeResult.DCode = 0
                             return dCodeResult
                elif(MVA > MVAReference['NormalRangeUpper'] and
                     MVA <= MVAReference['MildlyAbnormalRangeLower']):
                             dCodeResult.DCode = 12
                             return dCodeResult
                elif(MVA >= MVAReference['NormalRangeLower'] and
                     MVA <= MVAReference['NormalRangeUpper']):
                             dCodeResult.DCode = 13
                             return dCodeResult
                elif(MVA < MVAReference['NormalRangeLower']):
                             dCodeResult.DCode = 14
                             return dCodeResult
                else:
                             dCodeResult.DCode = 800
                             return dCodeResult



def GetValueByMVmeanPG(MVmeanPG, MVmeanPGReference):
     if(MVmeanPGReference['NormalRangeLower'] == None or
        MVmeanPGReference['NormalRangeUpper'] == None or
        MVmeanPGReference['MildlyAbnormalRangeLower'] == None):
             return 700
     else:
           if(MVmeanPG == None):
                return 900
           else:
                 if(MVmeanPG < MVmeanPGReference['NormalRangeLower']):
                        return 0
                 elif(MVmeanPG >= MVmeanPGReference['NormalRangeLower'] and
                      MVmeanPG <= MVmeanPGReference['NormalRangeUpper']):
                              return 12
                 elif(MVmeanPG >= MVmeanPGReference['NormalRangeUpper'] and
                      MVmeanPG <= MVmeanPGReference['MildlyAbnormalRangeLower']):
                              return 13
                 elif(MVmeanPG > MVmeanPGReference['MildlyAbnormalRangeLower']):
                              return 14
                 else:
                       return 800



def GetValueByMVmeanPGs(MVA,MVmeanPG,LVOTVmax,LVOTD,MVVTI,LVOTVTI, MVAReference,MVmeanPGReference,LVOTVmaxReference,LVOTDReference,MVVTIReference,LVOTVTIReference):
     if(MVAReference['NormalRangeLower'] == None or
        MVAReference['NormalRangeUpper'] == None or
        MVAReference['MildlyAbnormalRangeLower'] == None):
             return 700
     else:
           if(LVOTVmax == None or LVOTVmax == None or LVOTD == None or MVVTI == None or LVOTVTI == None):
                return GetValueByMVmeanPG(MVmeanPG, MVmeanPGReference)
           else:
                 MVA = LVOTVTI * (LVOTD * LVOTD * math.PI/4)/MVVTI

                 if(MVA > MVAReference['MildlyAbnormalRangeLower']):
                        return 0
                 elif(MVA > MVAReference['NormalRangeUpper'] and
                      MVA <= MVAReference['MildlyAbnormalRangeLower']):
                              return 12
                 elif(MVA >= MVAReference['NormalRangeLower'] and
                      MVA <= MVAReference['NormalRangeUpper']):
                              return 13
                 elif(MVA < MVAReference['NormalRangeLower']):
                              return 14
                 else:
                       return 800