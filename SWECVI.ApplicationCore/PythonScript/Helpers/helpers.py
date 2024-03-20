# coding=utf-8
import sys

from exam_helper import CalculateBSA
from parameter_names import ParameterNames
from enums import AssessmentKey, EFMethod, Header, DCode, AssessmentFunction
from report_resources import ReportResources
from resources import Resources
from ef_database_names import EFDatabaseNames

# global vars
raw_patient_data_list = sys.strings["PatientDataList"]
raw_assessments = sys.strings["Assessments"]


import json

def GetBSA(weight, height):
    return CalculateBSA(weight, height)


def GetHeader(header):
    c_assessments = json.loads(raw_assessments)
    assessment = c_assessments[str(int(header))] if c_assessments.has_key(str(int(header))) else ''
    return assessment['Text'] if assessment.has_key('Text') else ''
    
def GetAssessmentTextByLevelAndCallFunction(level, function):
    c_assessments = json.loads(raw_assessments)
    for assessment in c_assessments.values():
        if (type(assessment) is dict and (assessment['Level'] if assessment.has_key('Level') else None) == int(level) and (assessment['CallFunction'] if assessment.has_key('CallFunction') else None) == int(function)):
            return assessment['Text'] 
    return ''

def GetNormalSummary():
    c_assessments = json.loads(raw_assessments)
    for it in c_assessments.values():
        if(type(it) is dict):
            if(it['Id'] == int(AssessmentKey.NormalSummary)):
                return it['Text'] if it.has_key('Text') else ''
    return ''

def GetPatientData(PAR, Exam):
    c_patient_data_list = json.loads(raw_patient_data_list)
    result = ''
    if (Exam != None):
        age = Exam['Age'] if Exam.has_key('Age') else None
        if (age):
            result += '%s: %s' %(ReportResources.Age, age)
        result += ' '

        blood_pressure = Exam['BloodPressure'] if Exam.has_key('BloodPressure') else None
        if blood_pressure != None:
            result += 'ReportResources.BloodPressure: blood_pressure mmHg. '

        if (c_patient_data_list):
            for it in c_patient_data_list:
                name = it['Name'] if it.has_key('Name') else None
                if name == Resources.Rytm:
                    value = it['Value'] if it.has_key('Value') else ''
                    result += '%s: %s' %(name, value)
                    break

        if (PAR != None):
            bpm = PAR['value'][ParameterNames.LVOTHR] if PAR['value'].has_key(ParameterNames.LVOTHR) else None
            if(bpm != None):
                result += '%s: %s BPM. ' %(ReportResources.HeartRate, bpm)
                

        height = Exam['HeightInCm'] if Exam.has_key('HeightInCm') else 0
        result += '%s: %s cm. ' %(ReportResources.Height, height)
        
        weight = Exam['Weight'] if Exam.has_key('Weight') else 0
        result += '%s: %s kg. ' %(ReportResources.Weight, weight)
        
        formattedBSA = Exam['FormattedBSA'] if Exam.has_key('FormattedBSA') else 0
        result += '%s: %s m2. ' %(ReportResources.BSA, formattedBSA)
        

    if c_patient_data_list != None:
        i = 0
        for it in c_patient_data_list:
            if (type(it) is dict):
                if ((it['Name'] if it.has_key('Name') else None) == Resources.Rytm):
                    continue
                i += 1
                name = it['Name'] if it.has_key('Name') else None
                value = it['Value'] if it.has_key('Value') else None
                if name and value:
                    result += 'name: value. '
                    result += '%s: %s. ' %(name, value)
    return result


def GetParametersByPOH(Exam, POH):
    def func(it):
        if type(it) is not dict: return False
        poh = it['PartOfHeart'] if it.has_key('PartOfHeart') else None
        if poh == None: return False
        if len(poh) > 0 and str(int(POH)) in poh:
            return True
        else:
            return False
            
    if (Exam != None and Exam['Parameters'] != None):
        examParameters = filter(func, Exam['Parameters'].values())
        # Count 4D parameters
        if (examParameters != None):
            for it in examParameters:
                if (it['Is4D'] == False):
                    break
        return examParameters
    return None

def GetParameterNameValues(parameters):
    result = ''    
    parameters.sort(key=lambda k: k['OrderInAssessment'])
    
    for it in parameters:
        if (it['ParameterName'] != None and it['ShowInAssessmentText'] == True and it['ResultValue'] != None):
            asterisk =  '*' if it['IsOutsideReferenceRange'] else ''
            unitName = it['UnitName'] if it.has_key('UnitName') else ''
            referenceRange = ''
            if(it['Reference'] != None and it['Reference']['ReferenceRange'] != None):
                referenceRange = '(' + it['Reference']['ReferenceRange'] + ') '
            if (it['SuppressReference'] != None):
                result += '%s: %s%s %s ' % (it['TextFriendlyName'], it['ResultValue'], asterisk, unitName)
            else:
                result += '%s: %s%s %s %s' % (it['TextFriendlyName'], it['ResultValue'], asterisk, unitName, referenceRange)
    return result

def GetAssessmentTextEF(efMethod):
    if efMethod == EFMethod.EF4D:
        return GetHeader(Header.MainEF4D)
    elif efMethod == EFMethod.TRIPLANE:
        return GetHeader(Header.MainEFTri)
    elif efMethod == EFMethod.BIPLANE4CH2CH:
        return GetHeader(Header.MainEFBI42)
    elif efMethod == EFMethod.BIPLANE4CH:
        return GetHeader(Header.MainEFBI4)
    elif efMethod == EFMethod.BIPLANE2CH:
        return GetHeader(Header.MainEFBI2)
    elif efMethod == EFMethod.AUTO4CH2CH:
        return GetHeader(Header.MainEFAuto42)
    elif efMethod == EFMethod.AUTO4CH:
        return GetHeader(Header.MainEFAuto4)
    elif efMethod == EFMethod.AUTO2CH:
        return GetHeader(Header.MainEFAuto2)
    else:
        return GetHeader(Header.MainEFMissing)


def GetEFMethod(exam):
    parameter = exam['Parameters'][ParameterNames.LVEF]
    if (parameter == None or parameter['ResultValue'] == None):
        return EFMethod.UNKNOWN
    if (parameter['EPParameterName'] == EFDatabaseNames.LVEF_4D):
        return EFMethod.EF4D
    elif (parameter['EPParameterName'] == EFDatabaseNames.LVEF_Geom):
        return EFMethod.TRIPLANE
    elif (parameter['EPParameterName'] == EFDatabaseNames.LVEF_MOD_A2C):
        return EFMethod.BIPLANE2CH  # return EFMethod.BIPLANE4CH2CH
    elif (parameter['EPParameterName'] == EFDatabaseNames.LVEF_MOD_A4C):
        return EFMethod.BIPLANE4CH
    elif (parameter['EPParameterName'] == EFDatabaseNames.LVEF_MOD_BP_03):
        return EFMethod.BIPLANE4CH2CH
    elif (parameter['EPParameterName'] == EFDatabaseNames.Auto2DEF_LVEF_2Ch_Q):
        return EFMethod.AUTO2CH    # return EFMethod.AUTO4CH2CH
    elif (parameter['EPParameterName'] == EFDatabaseNames.Auto2DEF_LVEF_4Ch_Q):
        return EFMethod.AUTO4CH
    elif (parameter['EPParameterName'] == EFDatabaseNames.Auto2DEF_LVEF_BiP_Q):
        return EFMethod.AUTO4CH2CH  # return EFMethod.AUTO2CH
    else:
        return EFMethod.UNKNOWN
    
    
def GetAssessmentLevelByValve(valve):
    if (valve == Resources.No):
        return DCode.Normal
    elif (valve == Resources.Mild):
        return DCode.Level12
    elif (valve == Resources.MildToModerate):
        return DCode.Level13
    elif (valve == Resources.Moderate):
        return DCode.Level14
    elif (valve == Resources.ModerateToSevere):
        return DCode.Level15
    elif (valve == Resources.Severe):
        return DCode.Level16
    return None

def LVAVPDAvgMM(PAR):
    # Parameter name: LVAVPD
    meanCount = 4
    AVPD1 = 0
    AVPD2 = 0
    AVPD3 = 0
    AVPD4 = 0
    
    AVPDlatMM = PAR['value'][ParameterNames.AVPDlatMM] if PAR['value'].has_key(ParameterNames.AVPDlatMM) else None
    AVPDinfMM = PAR['value'][ParameterNames.AVPDinfMM] if PAR['value'].has_key(ParameterNames.AVPDinfMM) else None
    AVPDantMM = PAR['value'][ParameterNames.AVPDantMM] if PAR['value'].has_key(ParameterNames.AVPDantMM) else None
    AVPDseptMM = PAR['value'][ParameterNames.AVPDseptMM] if PAR['value'].has_key(ParameterNames.AVPDseptMM) else None

    if (AVPDlatMM != None or AVPDinfMM != None or AVPDantMM != None or AVPDseptMM != None):
        if (AVPDlatMM == None):
            AVPD1 = 0
            meanCount -= 1
        else:
            AVPD1 = float(AVPDlatMM)
        if (AVPDinfMM == None):
            AVPD2 = 0
            meanCount -= 1
        else:
            AVPD2 = float(AVPDinfMM)
        if (AVPDantMM == None):
            AVPD3 = 0
            meanCount -= 1
        else:
            AVPD3 = float(AVPDantMM)
        if (AVPDseptMM == None):
            AVPD4 = 0
            meanCount -= 1
        else:
            AVPD4 = float(AVPDseptMM)

        return (AVPD1 + AVPD2 + AVPD3 + AVPD4) / meanCount
    else:
        return None
    
def LVAVPDAvgTT(PAR):
    meanCount = 4
    AVPD1 = 0
    AVPD2 = 0
    AVPD3 = 0
    AVPD4 = 0

    AVPDlatTT = PAR['value'][ParameterNames.AVPDlatTT]
    AVPDinfTT = PAR['value'][ParameterNames.AVPDinfTT]
    AVPDantTT = PAR['value'][ParameterNames.AVPDantTT]
    AVPDseptTT = PAR['value'][ParameterNames.AVPDseptTT]

    if (AVPDlatTT != None or AVPDinfTT != None or AVPDantTT != None or AVPDseptTT != None):
        if (AVPDlatTT == None):
            AVPD1 = 0
            meanCount -= 1
        else:
            AVPD1 = AVPDlatTT
        if (AVPDinfTT == None):
            AVPD2 = 0
            meanCount -= 1
        else:
            AVPD2 = AVPDinfTT
        if (AVPDantTT == None):
            AVPD3 = 0
            meanCount -= 1
        else:
            AVPD3 = AVPDantTT
        if (AVPDseptTT == None):
            AVPD4 = 0
            meanCount -= 1
        else:
            AVPD4 = AVPDseptTT
        return (AVPD1 + AVPD2 + AVPD3 + AVPD4) / meanCount
    else:
        return None
    
def GetAssessmentCallFunctionByValve(name):
    if (name == Resources.AR):
        return AssessmentFunction.AortaInsufficiens
    elif (name == Resources.MR):
        return AssessmentFunction.MitralisInsufficiens
    elif (name == Resources.TR):
        return AssessmentFunction.TricuspidalisInsufficiens
    elif (name == Resources.PR):
        return AssessmentFunction.PulmonalisInsufficiens
    elif (name == Resources.AS):
        return AssessmentFunction.AortaStenosis
    elif (name == Resources.MS):
        return AssessmentFunction.MitralisStenosis
    elif (name == Resources.TS):
        return AssessmentFunction.TricuspidalisStenosis
    elif (name == Resources.PS):
        return AssessmentFunction.PulmonalisStenosis
    return None

def LVEEPrime(MVEVelocity, MVEEprimeLat, MVEEprimeSept):
     LVEEPrime = 0
     LVEprimeavg = 0

     if(MVEVelocity['Value'] == None):
          LVEEPrime = None
          return LVEEPrime
     elif(MVEEprimeLat['Value'] != None and MVEEprimeSept['Value'] == None):
            LVEprimeavg = MVEEprimeLat['Value']
     elif(MVEEprimeLat['Value'] == None and MVEEprimeSept['Value'] != None):
            LVEprimeavg = MVEEprimeSept['Value']
     elif(MVEEprimeLat['Value'] != None and MVEEprimeSept['Value'] != None):
            LVEprimeavg = (MVEEprimeLat['Value']  + MVEEprimeSept['Value']) / 2
     else:
           LVEEPrime = None
           return LVEEPrime

     LVEEPrime = (MVEVelocity['Value'] / LVEprimeavg)

     return LVEEPrime

