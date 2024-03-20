# coding=utf-8
import sys

from d_code_result import DCodeResult
from enums import DCode
from parameter_names import ParameterNames
from reference_limits import ReferenceLimits


raw_wall_segment_dictionary = sys.strings["WallSegmentDictionary"]

import json

def LVRegional(Parameters, References):
  
    dCodeResult = DCodeResult()
    dCodeResult.Result = False
    dCodeResult.CallLevel = 60
    dCodeResult.IsLimited = False;

    WMSI = None
    WMSIReference = None

    for it in Parameters:
          if(it['Name'] == ParameterNames.WMSI):
             WMSI = it['Value']

    for it in References:
          if(it['ParameterNameLogic'] == ParameterNames.WMSI):
             WMSIReference = it
         
    
    dCodeResult.Result = False

    BullsEye = json.loads(raw_wall_segment_dictionary) if raw_wall_segment_dictionary != None else None

    # Verify if there are reference values
    if (WMSI == None):
        dCodeResult.DCode = 700
        return dCodeResult
    
        if (WMSI == WMSIReference['NormalRangeUpper']):
                dCodeResult.DCode = 0
                return dCodeResult
        else:
                if (BullsEye != None):
                    for k, v in BullsEye.items():
                        if ((k == 'AAS') and (v != '1.0') or  # Same
                            (k == 'AA') and (v != '1.0') or   # Same
                            (k == 'AL') and (v != '1.0') or   # AAL
                            (k == 'AP') and (v != '1.0') or   # AIL
                            (k == 'AI') and (v != '1.0') or   # Same
                            (k == 'AS') and (v != '1.0') or   # AIS
                            (k == 'MA') and (v != '1.0') or   # Same
                            (k == 'BA') and (v != '1.0') or   # Same
                            (k == 'MAS') and (v != '1.0') or  # Same
                            (k == 'BAS') and (v != '1.0') or  # Same
                            (k == 'MS') and (v != '1.0')):     # MIS
                            dCodeResult.DCode = 11
                            return dCodeResult
                    
                        elif ((k == 'BS') and (v != '1.0') or # BIS
                            (k == 'BI') and (v != '1.0') or      # Same
                            (k == 'MI') and (v != '1.0')):        # Same
                            dCodeResult.DCode = 12
                            return dCodeResult
                    
                        elif ((k == 'BP') and (v != '1.0') or # BIL
                            (k == 'ML') and (v != '1.0') or      # MAL
                            (k == 'MP') and (v != '1.0') or      # MIL
                            (k == 'BL') and (v != '1.0')):        # BAL
                            dCodeResult.DCode = 13
                            return dCodeResult
                    
                        else:
                            continue
                else:
                        dCodeResult.DCode = 900
                        return dCodeResult
    else:
        dCodeResult.DCode = 900
        return dCodeResult
