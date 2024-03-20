# coding=utf-8

from d_code_result import DCodeResult
from enums import DCode
from parameter_names import ParameterNames
from reference_limits import ReferenceLimits
from helpers import GetBSA
from parameter_helper import Round, AddOrUpdateParameter


def RVSystolicFunction(Parameters, References):

    dCodeResult = DCodeResult()
    dCodeResult.Result = False
    dCodeResult.PAR = Parameters
    dCodeResult.CallLevel = 80

    NumberOfParameter = 0
    ScoreNeg = 0
    ScorePos = 0

    TAPSE = None
    RVPSVCD = None
    RVFAC = None
    RVAd = None
    RVAs = None
    RIMP = None
    RVPSVPD = None


    TAPSEReference = None
    RVPSVCDReference = None
    RVFACReference = None
    RVAdReference = None
    RVAsReference = None
    RIMPReference = None
    RVPSVPDReference = None

    for it in Parameters:
          if(it['Name'] == ParameterNames.TAPSE):
             TAPSE = it['Value']
          elif(it['Name'] == ParameterNames.RVPSVCD):
             RVPSVCD = it['Value']
          elif(it['Name'] == ParameterNames.RVPSVPD):
             RVPSVPD = it['Value']
          elif(it['Name'] == ParameterNames.RVFAC):
             RVFAC = it['Value']
          elif(it['Name'] == ParameterNames.RVAd):
             RVAd = it['Value']
          elif(it['Name'] == ParameterNames.RVAs):
             RVAs = it['Value']
          elif(it['Name'] == ParameterNames.RIMP):
             RIMP = it['Value']

    for it in References:
          if(it['ParameterNameLogic'] == ParameterNames.TAPSE):
             TAPSEReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.RVPSVCD):
             RVPSVCDReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.RVPSVPD):
             RVPSVPDReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.RVFAC):
             RVFACReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.RVAd):
             RVAdReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.RVAs):
             RVAsReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.RIMP):
             RIMPReference = it


    if((RVAd == None and RVAs == None) or RVAd == 0):
         RVFAC = None
    else:
         RVFAC = (RVAd - RVAs)/RVAd
    
    if(TAPSE != None):
       if(TAPSE != None and TAPSEReference['NormalRangeLower'] != None):
            NumberOfParameter = NumberOfParameter + 1
            
            if(TAPSE > TAPSEReference['NormalRangeLower']):
                ScoreNeg = ScoreNeg+1
            else:
                 ScorePos = ScorePos+1

    if(RIMP != None):
       if(RIMP != None and RIMPReference['NormalRangeUpper'] != None):
            NumberOfParameter = NumberOfParameter + 1
            
            if(RIMP < RIMPReference['NormalRangeUpper']):
                ScoreNeg = ScoreNeg+1
            else:
                 ScorePos = ScorePos+1

    if(RVFAC != None):
       if(RVFAC != None and RVFACReference['NormalRangeLower'] != None):
            NumberOfParameter = NumberOfParameter + 1
            
            if(RVFAC < RVFACReference['NormalRangeLower']):
                ScoreNeg = ScoreNeg+1
            else:
                 ScorePos = ScorePos+1

    if(RVPSVPD != None):
       if(RVPSVPD != None and RVPSVPDReference['NormalRangeLower'] != None):
            NumberOfParameter = NumberOfParameter + 1
            
            if(RVPSVPD > RVPSVPDReference['NormalRangeLower']):
                ScoreNeg = ScoreNeg+1
            else:
                 ScorePos = ScorePos+1
    
    if(RVPSVCD != None):
       if(RVPSVCD != None or RVPSVCDReference['NormalRangeLower'] != None):
            NumberOfParameter = NumberOfParameter + 1
            
            if(RVPSVCD > RVPSVCDReference['NormalRangeLower']):
                ScoreNeg = ScoreNeg+1
            else:
                 ScorePos = ScorePos+1
       else:
             dCodeResult.DCode = 900
             return dCodeResult
    else:
              dCodeResult.DCode = 900
              return dCodeResult

    if NumberOfParameter == 0:
        dCodeResult.DCode = 700
        return dCodeResult

    ScoreNeg = ScoreNeg / NumberOfParameter
    ScorePos = ScorePos / NumberOfParameter

    if NumberOfParameter == 1:  # 1 parameter is present
        if ScorePos == 1:
            dCodeResult.DCode = 12
            return dCodeResult  # Dilated diameter
        elif ScoreNeg == 1:
            dCodeResult.DCode = 0
            return dCodeResult  # Normal diameter
        else:
            dCodeResult.DCode = 800
            return dCodeResult
    elif NumberOfParameter == 2: # 2 parameters is present
        if ScorePos == 1:
            dCodeResult.DCode = 12
            return dCodeResult  # Dilated diameter
        elif ScoreNeg == 1:
            dCodeResult.DCode = 0
            return dCodeResult  # Normal diameter
        elif ScorePos == 0.5 and ScoreNeg == 0.5:
            dCodeResult.DCode = 11
            return dCodeResult  # Special case two measured parameters, one Pos one Neg.
        else:
            dCodeResult.DCode = 800
            return dCodeResult
    elif NumberOfParameter == 3: # 3 parameters is present
        if ScorePos >= 2/3:
            dCodeResult.DCode = 12
            return dCodeResult  # Dilated diameter
        elif ScoreNeg >= 2/3:
            dCodeResult.DCode = 0
            return dCodeResult  # Normal diameter
        else:
            dCodeResult.DCode = 800
            return dCodeResult
    elif NumberOfParameter == 4: # 4 parameters is present
        if ScorePos >= 3/4:
            dCodeResult.DCode = 12
            return dCodeResult  # Dilated diameter
        elif ScoreNeg >= 3/4:
            dCodeResult.DCode = 0
            return dCodeResult  # Normal diameter
        elif ScorePos == 0.5 and ScoreNeg == 0.5:
            dCodeResult.DCode = 11
            return dCodeResult # Special case two measured parameters, one Pos one Neg.
        else:
            dCodeResult.DCode = 800
            return dCodeResult
    else:
        dCodeResult.DCode = 900
        return dCodeResult    # NumberOfParameter = 0