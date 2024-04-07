from d_code_result import DCodeResult
from enums import DCode
from parameter_names import ParameterNames
from reference_limits import ReferenceLimits
from helpers import GetBSA
import math


def AortaStenosis(Parameters, References):

    dCodeResult = DCodeResult()
    dCodeResult.Result = False
    dCodeResult.CallLevel = 300

    AVAVTI = None
    AVMeanPG = None
    AVVmax = None
    AVVmaxP = None

    AVAVTIReference = None
    AVMeanPGReference = None
    AVVmaxReference = None
    AVVmaxPReference = None

    for it in Parameters:
          if(it['Name'] == ParameterNames.AVAVTI):
             AVAVTI = it['Value']
          elif(it['Name'] == ParameterNames.AVMeanPG):
             AVMeanPG = it['Value']
          elif(it['Name'] == ParameterNames.AVVmax):
             AVVmax = it['Value']
          elif(it['Name'] == ParameterNames.AVVmaxP):
             AVVmaxP = it['Value']

    for it in References:
          if(it['ParameterNameLogic'] == ParameterNames.AVAVTI):
             AVAVTIReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.AVMeanPG):
             AVMeanPGReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.AVVmax):
             AVVmaxReference = it
          elif(it['ParameterNameLogic'] == ParameterNames.AVVmaxP):
             AVVmaxPReference = it

    if(AVVmaxReference['NormalRangeLower'] == None or
       AVVmaxReference['NormalRangeUpper'] == None or
       AVVmaxReference['MildlyAbnormalRangeLower'] == None or
       AVMeanPGReference['NormalRangeLower'] == None or
       AVMeanPGReference['NormalRangeUpper'] == None or
       AVMeanPGReference['MildlyAbnormalRangeLower'] == None or
       AVAVTIReference['NormalRangeLower'] == None or
       AVAVTIReference['NormalRangeUpper'] == None or
       AVAVTIReference['MildlyAbnormalRangeLower'] == None):
                dCodeResult.DCode = 700
                return dCodeResult
    else:
          if(AVVmax == None):
                 if(AVVmaxP == None):
                      dCodeResult.DCode = 900
                      return dCodeResult
                 else:
                       AVVmax = AVVmaxP
                       if(AVAVTI == None or AVMeanPG == None):
                            dCodeResult.DCode = 900
                            return dCodeResult
                       else:
                               if(AVVmax < AVVmaxReference['NormalRangeLower'] and
                                  AVMeanPG < AVMeanPGReference['NormalRangeUpper'] and
                                  AVAVTI > AVAVTIReference['NormalRangeLower']):
                                        dCodeResult.DCode = 0
                                        return dCodeResult
                               elif(AVVmax >= AVVmaxReference['NormalRangeLower'] and
                                    AVVmax <= AVVmaxReference['NormalRangeUpper'] and
                                    AVMeanPG < AVMeanPGReference['NormalRangeUpper'] and
                                    AVAVTI > AVAVTIReference['NormalRangeLower']):
                                        dCodeResult.DCode = 12
                                        return dCodeResult
                               elif(AVVmax >= AVVmaxReference['NormalRangeUpper'] and
                                    AVVmax <= AVVmaxReference['MildlyAbnormalRangeLower'] and
                                    AVMeanPG >= AVMeanPGReference['NormalRangeLower'] and
                                    AVMeanPG <= AVMeanPGReference['NormalRangeUpper'] and
                                    AVAVTI > AVAVTIReference['NormalRangeLower'] and
                                    AVAVTI <= AVAVTIReference['NormalRangeUpper']):
                                        dCodeResult.DCode = 14
                                        return dCodeResult
                               elif(AVVmax > AVVmaxReference['MildlyAbnormalRangeLower'] and
                                    AVMeanPG > AVMeanPGReference['NormalRangeUpper'] and
                                    AVAVTI < AVAVTIReference['NormalRangeUpper']):
                                        dCodeResult.DCode = 16
                                        return dCodeResult
                               else:
                                      dCodeResult.DCode = 800
                                      return dCodeResult
          else:
                 if(AVVmaxP == None):
                      dCodeResult.DCode = 900
                      return dCodeResult

                 AVVmax = (AVVmax + AVVmaxP)/2
                 if(AVAVTI == None or AVMeanPG == None):
                      dCodeResult.DCode = 900
                      return dCodeResult
                 else:
                       if(AVVmax < AVVmaxReference['NormalRangeLower'] and
                          AVMeanPG < AVMeanPGReference['NormalRangeUpper'] and
                          AVAVTI > AVAVTIReference['NormalRangeLower']):
                                dCodeResult.DCode = 0
                                return dCodeResult
                       elif(AVVmax >= AVVmaxReference['NormalRangeLower'] and
                            AVVmax <= AVVmaxReference['NormalRangeUpper'] and
                            AVMeanPG < AVMeanPGReference['NormalRangeUpper'] and
                            AVAVTI > AVAVTIReference['NormalRangeLower']):
                                dCodeResult.DCode = 12
                                return dCodeResult
                       elif(AVVmax >= AVVmaxReference['NormalRangeUpper'] and
                            AVVmax <= AVVmaxReference['MildlyAbnormalRangeLower'] and
                            AVMeanPG >= AVMeanPGReference['NormalRangeLower'] and
                            AVMeanPG <= AVMeanPGReference['NormalRangeUpper'] and
                            AVAVTI > AVAVTIReference['NormalRangeLower'] and
                            AVAVTI <= AVAVTIReference['NormalRangeUpper']):
                                dCodeResult.DCode = 14
                                return dCodeResult
                       elif(AVVmax > AVVmaxReference['MildlyAbnormalRangeLower'] and
                            AVMeanPG > AVMeanPGReference['NormalRangeUpper'] and
                            AVAVTI < AVAVTIReference['NormalRangeUpper']):
                                dCodeResult.DCode = 16
                                return dCodeResult
                       else:
                              dCodeResult.DCode = 800
                              return dCodeResult