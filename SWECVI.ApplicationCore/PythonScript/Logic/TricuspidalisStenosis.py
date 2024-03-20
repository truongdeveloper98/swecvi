# coding=utf-8

from d_code_result import DCodeResult
from enums import DCode
from parameter_names import ParameterNames
from reference_limits import ReferenceLimits
from helpers import GetBSA
from parameter_helper import Round, AddOrUpdateParameter


def TricuspidalisStenosis(Parameters, References):

    dCodeResult = DCodeResult()
    dCodeResult.Result = False
    dCodeResult.PAR = Parameters
    dCodeResult.CallLevel = 330

    TVmeanPG =  None
    TVVmax = None
    TVmeanPGReference =  None
    TVVmaxReference = None

    for it in Parameters:
          if(it['Name'] == ParameterNames.TVmeanPG):
             TVmeanPG = it['Value']
          elif(it['Name'] == ParameterNames.TVVmax):
             TVVmax = it['Value']

    for it in References:
          if(it['ParameterNameLogic'] == ParameterNames.TVmeanPG):
             TVmeanPGReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.TVVmax):
             TVVmaxReference = it


    if(TVmeanPGReference['NormalRangeLower'] == None or
        TVVmaxReference['NormalRangeLower'] == None):
                dCodeResult.DCode = 700
                return dCodeResult
    else:
            if(TVmeanPG == None or TVVmax == None):
                    dCodeResult.DCode = 900
                    return dCodeResult
            elif(TVmeanPG >= TVmeanPGReference['NormalRangeLower']):
                    dCodeResult.DCode = 11
                    return dCodeResult
            elif(TVmeanPG < TVmeanPGReference['NormalRangeLower']):
                    dCodeResult.DCode = 800
                    return dCodeResult
            elif(TVVmax > TVVmaxReference['NormalRangeLower']):
                    dCodeResult.DCode = 12
                    return dCodeResult
            else:
                dCodeResult.DCode = 0
                return dCodeResult