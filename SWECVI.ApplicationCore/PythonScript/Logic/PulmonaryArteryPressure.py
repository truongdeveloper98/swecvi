# coding=utf-8

from d_code_result import DCodeResult
from enums import DCode
from parameter_names import ParameterNames
from reference_limits import ReferenceLimits
from helpers import GetBSA
from parameter_helper import Round, AddOrUpdateParameter


def PulmonaryArteryPressure(Parameters, References):

    dCodeResult = DCodeResult()
    dCodeResult.Result = False
    dCodeResult.PAR = Parameters
    dCodeResult.CallLevel = 90
    estPAP = None
    estPAPReference = None

    for it in Parameters:
          if(it['Name'] == ParameterNames.estPAP):
             estPAP = it['Value']

    for it in References:
          if(it['ParameterNameLogic'] == ParameterNames.estPAP):
             estPAPReference = it

    if(estPAPReference['NormalRangeUpper'] == None):
                dCodeResult.DCode = 700
                return dCodeResult
    elif(estPAP == None):
                dCodeResult.DCode = 900
                return dCodeResult
    elif(estPAP <= estPAPReference['NormalRangeUpper']):
                dCodeResult.DCode = 0
                return dCodeResult
    else:
                dCodeResult.DCode = 11
                return dCodeResult