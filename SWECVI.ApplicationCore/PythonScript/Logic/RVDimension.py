# coding=utf-8

from d_code_result import DCodeResult
from enums import DCode
from parameter_names import ParameterNames
from helpers import GetBSA
from parameter_helper import Round, AddOrUpdateParameter
from reference_limits import ReferenceLimits


def RVDimension(Parameters, References):
   
    dCodeResult = DCodeResult()
    dCodeResult.Result = False
    dCodeResult.DCode = 0
    dCodeResult.CallLevel = 70
    
    NumberOfParameter = 0
    ScorePos = 0
    ScoreNeg = 0
    weight = 0
    height = 0
    
    RVAd = None
    RVD1 = None
    RVD2 = None
    RVOTDd = None
    RVAdIndex = None

    RVAdReference = None
    RVD1Reference = None
    RVD2Reference = None
    RVOTDdReference = None
    RVAdIndexReference = None


    for it in Parameters:
          if(it['Name'] == ParameterNames.RVAd):
             RVAd = it['Value']
          elif(it['Name'] == ParameterNames.RVD1):
             RVD1 = it['Value']
          elif(it['Name'] == ParameterNames.RVD2):
             RVD2 = it['Value']
          elif(it['Name'] == ParameterNames.RVOTDd):
             RVOTDd = it['Value']
          elif(it['Name'] == ParameterNames.RVAdIndex):
             RVOTDd = it['Value']
          elif(it['Name'] == ParameterNames.PatientWeight):
             weight = it['Value']
          elif(it['Name'] == ParameterNames.PatientHeight):
             height = it['Value']

    for it in References:
          if(it['ParameterNameLogic'] == ParameterNames.RVAd):
             RVAdReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.RVD1):
             RVD1Reference = it
          elif(it['ParameterNameLogic'] == ParameterNames.RVD2):
             RVD2Reference = it
          elif(it['ParameterNameLogic'] == ParameterNames.RVOTDd):
             RVOTDdReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.RVOTDd):
             RVOTDdReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.RVAdIndex):
             RVAdIndexReference = it

    BSA = GetBSA(weight,height)
    
    if RVAd != None and BSA != None: 
         RVAdIndex = round(RVAd / BSA, 0) 

    if(RVD1 !=  None):
        if RVD1Reference['NormalRangeUpper'] != None:
            NumberOfParameter += 1

    if(RVD2 !=  None):
        if RVD2Reference['NormalRangeUpper'] != None:
            NumberOfParameter += 1
    
    if RVAd != None:
        if RVAdReference['NormalRangeUpper'] != None:
            NumberOfParameter += 1

    if(RVOTDd !=  None):
        if RVOTDdReference['NormalRangeUpper'] != None:
            NumberOfParameter += 1

    if NumberOfParameter == 0:
        dCodeResult.DCode = 700
        return dCodeResult

    if(RVD1 !=  None):
        if RVD1Reference['NormalRangeUpper'] != None:
            if RVD1 < RVD1Reference['NormalRangeUpper']:
                ScoreNeg += 1 
            else:
                ScorePos += 1 

    if(RVD2 !=  None):
        if RVD2Reference['NormalRangeUpper'] != None:
            if RVD2 < RVD2Reference['NormalRangeUpper']:
                ScoreNeg += 1 
            else:
                ScorePos += 1 

    if(RVAd !=  None):
        if RVAdReference['NormalRangeUpper'] != None:
           if(RVAd != None):
              RVAd = round(RVAd, 0) 
              if RVAd < RVAdReference['NormalRangeUpper']:
                  ScoreNeg += 1
              else:
                  ScorePos += 1 

    if(RVAdIndex !=  None):
        if RVAdIndexReference['NormalRangeUpper'] != None:
           if(RVAdIndex != None):
              if RVAdIndex < RVAdReference['NormalRangeUpper']:
                  ScoreNeg += 1
              else:
                  ScorePos += 1 

    if(RVOTDd !=  None):
        if RVOTDdReference['NormalRangeUpper'] != None:
            if RVOTDd < RVOTDdReference['NormalRangeUpper']:
                ScoreNeg += 1 
            else:
                ScorePos += 1 
    

    ScoreNeg = ScoreNeg / NumberOfParameter
    ScorePos = ScorePos / NumberOfParameter

    if NumberOfParameter == 1:  
        if ScorePos == 1:
            dCodeResult.DCode = 12
            return dCodeResult 
        elif ScoreNeg == 1:
            dCodeResult.DCode = 0
            return dCodeResult  
        else:
            dCodeResult.DCode = 800
            return dCodeResult
    elif NumberOfParameter == 2:
        if ScorePos == 1:
            dCodeResult.DCode = 12
            return dCodeResult 
        elif ScoreNeg == 1:
            dCodeResult.DCode = 0
            return dCodeResult  
        elif ScorePos == 0.5 and ScoreNeg == 0.5:
            dCodeResult.DCode = 11
            return dCodeResult 
        else:
            dCodeResult.DCode = 800
            return dCodeResult
    elif NumberOfParameter == 3: 
        if ScorePos >= 2/3:
            dCodeResult.DCode = 12
            return dCodeResult 
        elif ScoreNeg >= 2/3:
            dCodeResult.DCode = 0
            return dCodeResult  
        else:
            dCodeResult.DCode = 800
            return dCodeResult
    elif NumberOfParameter == 4: 
        if ScorePos >= 3/4:
            dCodeResult.DCode = 12
            return dCodeResult  
        elif ScoreNeg >= 3/4:
            dCodeResult.DCode = 0
            return dCodeResult  
        elif ScorePos == 0.5 and ScoreNeg == 0.5:
            dCodeResult.DCode = 11
            return dCodeResult
        else:
            dCodeResult.DCode = 800
            return dCodeResult
    else:
        dCodeResult.DCode = 900
        return dCodeResult   
