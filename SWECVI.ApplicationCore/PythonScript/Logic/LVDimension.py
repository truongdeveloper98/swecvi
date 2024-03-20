# coding=utf-8

from d_code_result import DCodeResult
from enums import DCode
from parameter_names import ParameterNames
from reference_limits import ReferenceLimits
from helpers import GetBSA
from parameter_helper import Round, AddOrUpdateParameter
import math 


def LVDimension(Parameters, References):

    dCodeResult = DCodeResult()
    dCodeResult.Result = False
    dCodeResult.PAR = Parameters
    dCodeResult.CallLevel = 30

    LVEDV = None
    LVMass = None
    LVIDd = None
    LVPWd = None
    LVIVSd = None

    LVEDVReference = None
    LVMassReference = None
    LVIDdReference = None
    LVPWdReference = None
    LVIVSdReference = None

    weight = 0
    height = 0

    for it in Parameters:
          if(it['Name'] == ParameterNames.LAESV):
             LAESV = it['Value']
          elif(it['Name'] == ParameterNames.LVMass):
             LVMass = it['Value']
          elif(it['Name'] == ParameterNames.LVIDd):
             LVIDd = it['Value']
          elif(it['Name'] == ParameterNames.LVPWd):
             LVPWd = it['Value']
          elif(it['Name'] == ParameterNames.LVIVSd):
             LVIVSd = it['Value']
          elif(it['Name'] == ParameterNames.PatientWeight):
             weight = it['Value']
          elif(it['Name'] == ParameterNames.PatientHeight):
             height = it['Value']

     for it in References:
          if(it['ParameterNameLogic'] == ParameterNames.LAESV):
             LAESVReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.LVMass):
             LVMassReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.LVIDd):
             LVIDdReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.LVPWd):
             LVPWdReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.LVIVSd):
             LVIVSdReference = it


    BSA = GetBSA(weight, height)

    if(BSA != None):
         if (LVEDV == None or LVMass == None or LVIDd == None or LVPWd == None):
             if (LVIDd != None and LVPWd != None and LVIVSd != None):
                 if(LVMass == None):
                    LVMass = CalculateLVMASS(BSA, LVIVSd, LVIDd, LVPWd)
                 
                 LVIDdIndex = math.round(LVIDd / BSA, 0); # No decimal
                 LVMassIndex = math.round(LVMass / BSA, 0); # No decimal


                       



def CalculateLVMASS(BSA, LVIVSd, LVIDd, LVPWd):
    LVSum = LVIVSd + LVIDd + LVPWd;
    return (0.8 * (1.04 * (math.pow(LVSum, 3) - math.pow(LVIDd, 3))) + 0.6)/1000;
