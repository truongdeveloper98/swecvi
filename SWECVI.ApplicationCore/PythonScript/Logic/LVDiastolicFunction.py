# coding=utf-8

from d_code_result import DCodeResult
from enums import DCode
from parameter_names import ParameterNames
from reference_limits import ReferenceLimits
from parameter_helper import Round, AddOrUpdateParameter

from helpers import GetBSA

def LVDiastolicFunction(Parameters, References):

    
    dCodeResult = DCodeResult();
    dCodeResult.Result = False
    dCodeResult.CallLevel = 40

    MVEARatio = None
    MVEVelocity = None
    TRVmax = None
    TRVmaxP = None
    LVEEPrimeAvg = None
    LVEprimesept = None
    LVEprimelat = None
    LVEmlat = None
    LVEF = None
    LAESV = None
    LAESVIndex = None


    MVEARatioReference = None
    MVEVelocityReference = None
    TRVmaxReference = None
    TRVmaxPReference = None
    LVEEPrimeAvgReference = None
    LVEprimeseptReference = None
    LVEprimelatReference = None
    LVEmlatReference = None
    LVEFReference = None
    LAESVReference = None
    LAESVIndexReference = None

    N = 0
    Score = 0
    SR = 0
    weight = 0
    height = 0


    for it in Parameters:
          if(it['Name'] == ParameterNames.MVEARatio):
             MVEARatio = it['Value']
          elif(it['Name'] == ParameterNames.MVEVelocity):
             MVEVelocity = it['Value']
          elif(it['Name'] == ParameterNames.TRVmax):
             TRVmax = it['Value']
          elif(it['Name'] == ParameterNames.TRVmaxP):
             TRVmaxP = it['Value']
          elif(it['Name'] == ParameterNames.LVEEPrimeAvg):
             LVEEPrimeAvg = it['Value']
          elif(it['Name'] == ParameterNames.LVEprimesept):
             LVEprimesept = it['Value']
          elif(it['Name'] == ParameterNames.LVEmlat):
             LVEmlat = it['Value']
          elif(it['Name'] == ParameterNames.LVEF):
             LVEF = it['Value']
          elif(it['Name'] == ParameterNames.LAESV):
             LAESV = it['Value']
          elif(it['Name'] == ParameterNames.LVEprimelat):
             LVEprimelat = it['Value']
          elif(it['Name'] == ParameterNames.PatientWeight):
             weight = it['Value']
          elif(it['Name'] == ParameterNames.PatientHeight):
             height = it['Value']
             
    for it in References:
          if(it['ParameterNameLogic'] == ParameterNames.MVEARatio):
             MVEARatioReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.MVEVelocity):
             MVEVelocityReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.TRVmax):
             TRVmaxReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.TRVmaxP):
             TRVmaxPReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.LVEEPrimeAvg):
             LVEEPrimeAvgReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.LVEprimesept):
             LVEprimeseptReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.LVEmlat):
             LVEmlatReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.LVEF):
             LVEFReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.LAESV):
             LAESVReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.LVEprimelat):
             LVEprimelatReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.LAESVIndex):
             LAESVIndexReference = it
        

    BSA = GetBSA(weight, height)


     
    if(LVEF == None):
        dCodeResult.DCode = GetValueByMultipleParam(TRVmax,TRVmaxP,MVEARatio,MVEVelocity,LVEEprimeAvg,LVEprimesept,LVEprimelat,LAESV,BSA,N,Score,SN, TRVmaxReference,TRVmaxPReference,MVEARatioReference,MVEVelocityReference,LVEEprimeAvgReference,LVEprimeseptReference,LVEprimelatReference,LAESVReference,LAESVIndexReference)
        return dCodeResult
    else:
          if(LVEFReference['NormalRangeUpper'] != None):
                if(LVEF < LVEFReference['NormalRangeLower']):
                      if(MVEARatioReference['MildlyAbnormalRangeLower'] == None or
                         MVEARatioReference['NormalRangeLower'] == None or
                         MVEVelocityReference['MildlyAbnormalRangeLower'] == None or
                         LVEEprimeAvgReference['MildlyAbnormalRangeLower'] == None or
                         TRVmaxReference['MildlyAbnormalRangeLower'] == None):
                                dCodeResult.DCode = 700
                                return dCodeResult
                      else:
                                dCodeResult.DCode = GetValueByTRVmax(TRVmax,TRVmaxP,MVEARatio,MVEVelocity,BSA,LAESV,LVEEprimeAvg, N, Score,SR, TRVmaxReference,TRVmaxPReference,MVEARatioReference,MVEVelocityReference,LAESVReference,LVEEprimeAvgReference,LAESVIndexReference)
                                return dCodeResult
                else:
                       dCodeResult.DCode = 800
                       return dCodeResult
          else:
                dCodeResult.DCode = GetValueByMultipleParam(TRVmax,TRVmaxP,MVEARatio,MVEVelocity,LVEEprimeAvg,LVEprimesept,LVEprimelat,LAESV,BSA,N,Score,SN, TRVmaxReference,TRVmaxPReference,MVEARatioReference,MVEVelocityReference,LVEEprimeAvgReference,LVEprimeseptReference,LVEprimelatReference,LAESVReference,LAESVIndexReference)
                return dCodeResult


def GetValueByTRVmax(TRVmax,TRVmaxP,MVEARatio,MVEVelocity,BSA,LAESV,LVEEprimeAvg, N, Score,SR, TRVmaxReference,TRVmaxPReference,MVEARatioReference,MVEVelocityReference,LAESVReference,LVEEprimeAvgReference,LAESVIndexReference):
        if(TRVmax == None):
            TRVmax = TRVmaxP
        
        if(MVEARatio == None or MVEVelocity == None):
                return 16
        elif(MVEARatio <= MVEARatioReference['NormalRangeUpper'] and
             MVEVelocity <= MVEARatioReference['MildlyAbnormalRangeLower']):
                return 12
        elif(MVEVelocity > MVEARatioReference['MildlyAbnormalRangeLower']):
                return 14
        else:
              N = 0

              if(BSA != None and LAESV != None):
                    LAESVIndex = LAESV/BSA

              if((MVEARatio <= MVEARatioReference['NormalRangeLower'] and
                  MVEVelocity > MVEVelocityReference['MildlyAbnormalRangeLower']) or
                 (MVEARatio > MVEARatioReference['NormalRangeLower'] and
                  MVEVelocity < MVEVelocityReference['MildlyAbnormalRangeLower'])):
                       return 700
              else:
                     if(LVEEprimeAvg != None):
                          N = N + 1
                     if(TRVmax != None):
                          N = N + 1
                     if(LAESVIndex != None):
                          N = N + 1
                     if(LAESV != None):
                          N = N + 1

                     if(N == 0):
                         return 900
                     else:
                           if(LVEEprimeAvg != None):
                               if(LVEEprimeAvg > LVEEprimeAvgReference['MildlyAbnormalRangeLower']):
                                      SN = SN + 1
                               else:
                                    Score = Score + 1

                           if(TRVmax != None):
                               if(TRVmax > TRVmaxReference['MildlyAbnormalRangeLower']):
                                      SN = SN + 1
                               else:
                                    Score = Score + 1
                           
                           if(LAESVIndex != None):
                               if(LAESVIndex > LAESVIndexReference['MildlyAbnormalRangeLower']):
                                      SN = SN + 1
                               else:
                                    Score = Score + 1

                           if(LAESV != None):
                               if(LAESV > LAESVReference['MildlyAbnormalRangeLower']):
                                       SN = SN + 1
                               else:
                                   Score = Score + 1

                           if(N == 3):
                                if(SP >= 2):
                                    return 13
                                elif(SN >= 2):
                                    return 12
                                else:
                                      return 800
                           elif(N == 2):
                                if(SP == 2):
                                    return 13
                                elif(SN == 2):
                                    return 12
                                else:
                                      return 15
                           else:
                                 if(SP == 1):
                                     return 15
                                 elif(SN == 1):
                                     return 12
                                 else:
                                       return 800


def GetValueByMultipleParam(TRVmax,TRVmaxP,MVEARatio,MVEVelocity,LVEEprimeAvg,LVEprimesept,LVEprimelat,LAESV,BSA,N,Score,SN, TRVmaxReference,TRVmaxPReference,MVEARatioReference,MVEVelocityReference,LVEEprimeAvgReference,LVEprimeseptReference,LVEprimelatReference,LAESVReference,LAESVIndexReference):
     if(LVEEprimeAvgReference['MildlyAbnormalRangeLower'] == None or
        LVEprimeseptReference['MildlyAbnormalRangeLower'] == None or
        LVEprimelatReference['MildlyAbnormalRangeLower'] == None or
        TRVmaxReference['MildlyAbnormalRangeLower'] == None or
        LAESVReference['MildlyAbnormalRangeLower'] == None or
        LAESVReference['MildlyAbnormalRangeLower'] == None):
                  return 700
     else:
           if(BSA != None and LAESV != None):
                LAESVIndex = LAESV/BSA
                if(LVEEprimeAvg != None):
                     N = N + 1
                if(LVEprimeSep == None  or
                   LVEprimeLat == None):
                     N = N + 1
                if(TRVmax != None):
                    N = N + 1
                if(LAESVIndex != None):
                   N = N + 1
                if(LAESV != None):
                   N = N + 1

                if(LVEEprimeAvg != None and 
                   LVEEprimeAvg > VEEprimeAvgReference['MildlyAbnormalRangeLower']):
                       Score = Score + 1
                if(LVEprimeLat != None and 
                   LVEprimeLat > LVEprimeLatReference['MildlyAbnormalRangeLower']):
                       Score = Score + 1
                if(LVEprimeSept != None and 
                   LVEprimeSept > LVEprimeSeptReference['MildlyAbnormalRangeLower']):
                       Score = Score + 1
                if(TRVmax != None and 
                   TRVmax > TRVmaxReference['MildlyAbnormalRangeLower']):
                       Score = Score + 1
                if(LAESVIndex != None and 
                   LAESVIndex > LAESVIndexReference['NormalRangeUpper']):
                       Score = Score + 1
                if(LAESV != None and 
                   LAESV > LAESVReference['NormalRangeUpper']):
                       Score = Score + 1

                if(N == 0):
                    return 900

                SR = (SR/N)*100

                if(SR < 50):
                    return 0
                elif(SR == 50):
                    return 11
                else:
                     return GetValueByTRVmax(TRVmax,TRVmaxP,MVEARatio,MVEVelocity,BSA,LAESV,LAESVIndex,LVEEprimeAvg, N, Score,SR, TRVmaxReference,TRVmaxPReference,MVEARatioReference,MVEVelocityReference,LAESVReference,LAESVReference,LVEEprimeAvgReference, LAESVIndexReference)

