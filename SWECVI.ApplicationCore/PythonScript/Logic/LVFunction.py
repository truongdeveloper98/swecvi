# coding=utf-8

from d_code_result import DCodeResult
from enums import DCode
from parameter_names import ParameterNames
from reference_limits import ReferenceLimits
from helpers import LVAVPDAvgMM, LVAVPDAvgTT


def LVSystolicFunction(Parameters, References):

    dCodeResult = DCodeResult()
    dCodeResult.Result = False
    dCodeResult.PAR = Parameters
    dCodeResult.CallLevel = 50

    LVEF = None
    GLS = None

    LVEFReference = None
    GLSReference = None

    for it in Parameters:
          if(it['Name'] == ParameterNames.LVEF):
             LVEF = it['Value']
          elif(it['Name'] == ParameterNames.LVGLS):
             GLS = it['Value']

    for it in References:
          if(it['ParameterNameLogic'] == ParameterNames.LVEF):
             LVEFReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.LVGLS):
             GLSReference = it


    if(LVEFReference['MildlyAbnormalRangeLower'] != None and
        LVEFReference['MildlyAbnormalRangeUpper'] != None and
        LVEFReference['ModeratelyAbnormalRangeLower'] != None and
        LVEFReference['ModeratelyAbnormalRangeUpper'] != None):
                for it in References:
                    if(it['ParameterId'] == 'EF 4D'):
                        LVEFReference = it
                    elif(it['ParameterId'] == 'Auto2DEF/LVEF_BiP_Q'):
                        LVEFReference = it
                    elif(it['ParameterId'] == 'EF(Biplane)_03'):
                        LVEFReference = it
                    elif(it['ParameterId'] == 'Auto2DEF/LVEF_4Ch_Q'):
                        LVEFReference = it
                    elif(it['ParameterId'] == 'Auto2DEF/LVEF_2Ch_Q'):
                        LVEFReference = it
                    elif(it['ParameterId'] == 'EF(MOD A4C)'):
                        LVEFReference = it
                    elif(it['ParameterId'] == 'EF(MOD A2C)'):
                        LVEFReference = it


                if(LVEF != None or LVEF != None):
                    if(LVEF < LVEFReference['ModeratelyAbnormalRangeUpper']):
                             dCodeResult.DCode = 11
                             return dCodeResult
                    elif(LVEF >= LVEFReference['ModeratelyAbnormalRangeUpper'] and
                         LVEF <= LVEFReference['MildlyAbnormalRangeLower']):
                             dCodeResult.DCode = 12
                             return dCodeResult
                    elif(LVEF > LVEFReference['ModeratelyAbnormalRangeUpper'] and
                         LVEF < LVEFReference['MildlyAbnormalRangeLower']):
                             dCodeResult.DCode = 12
                             return dCodeResult
                    else:
                           dCodeResult.DCode = 0
                           return dCodeResult
                else:
                       dCodeResult.DCode = GetValueByLVGLS(GLS, GLSReference)
                       return dCodeResult 
    else:
           dCodeResult.DCode = GetValueByLVGLS(GLS, GLSReference)
           return dCodeResult 



def GetValueByLVGLS(GLS, GLSReference):
      if(GLSReference['NormalRangeLower'] == None):
            return 700
      else:
            if(GLS == None):
               return 900
            else:
                  if(GLS <= GLSReference['NormalRangeLower']):
                        return 0
                  elif(GLS > GLSReference['NormalRangeLower']):
                        return 13
                  else:
                        return 800