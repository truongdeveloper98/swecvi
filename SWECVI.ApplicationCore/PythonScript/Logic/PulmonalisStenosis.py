# coding=utf-8

from d_code_result import DCodeResult
from enums import DCode
from parameter_names import ParameterNames
from reference_limits import ReferenceLimits
from helpers import GetBSA
from parameter_helper import Round, AddOrUpdateParameter


def PulmonalisStenosis(Parameters, References):

    dCodeResult = DCodeResult()
    dCodeResult.Result = False
    dCodeResult.PAR = Parameters
    dCodeResult.CallLevel = 320

    PVVmax = None
    PVVmaxP = None

    PVVmaxReference = None
    PVVmaxPReference = None

    for it in Parameters:
          if(it['Name'] == ParameterNames.PVVmax):
             PVVmax = it['Value']
          elif(it['Name'] == ParameterNames.PVVmaxP):
             PVVmaxP = it['Value']

    for it in References:
          if(it['ParameterNameLogic'] == ParameterNames.PVVmax):
             PVVmaxReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.PVVmaxP):
             PVVmaxPReference = it

     
    if(PVVmaxReference['MildlyAbnormalRangeLower'] == None or
       PVVmaxReference['MildlyAbnormalRangeUpper'] == None or
       PVVmaxReference['ModeratelyAbnormalRangeLower'] == None):
                  dCodeResult.DCode = 700
                  return dCodeResult
    else:
          if(PVVmax == None and PVVmaxP != None):
                PVVmax = PVVmaxP
          else:
                if(PVVmax == None and PVVmaxP == None):
                      dCodeResult.DCode = 900
                      return dCodeResult
                else:
                      PVVmax = (PVVmax + PVVmaxP)/2

    if(PVVmax < PVVmaxReference['MildlyAbnormalRangeLower']):
            dCodeResult.DCode = 0
            return dCodeResult
    elif(PVVmax >= PVVmaxReference['MildlyAbnormalRangeLower'] and
         PVVmax < PVVmaxReference['MildlyAbnormalRangeUpper']):
            dCodeResult.DCode = 11
            return dCodeResult
    elif(PVVmax >= PVVmaxReference['MildlyAbnormalRangeUpper'] and
         PVVmax < PVVmaxReference['ModeratelyAbnormalRangeLower']):
            dCodeResult.DCode = 12
            return 
    elif(PVVmax >= PVVmaxReference['ModeratelyAbnormalRangeLower']):
            dCodeResult.DCode = 13
            return dCodeResult
    else:
            dCodeResult.DCode = 800
            return dCodeResult