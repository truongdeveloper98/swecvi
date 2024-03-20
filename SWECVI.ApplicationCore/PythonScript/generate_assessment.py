# coding=utf-8
import sys

from enums import  DCode, AssessmentFunction, Header, POH
from helpers import GetHeader, GetAssessmentTextByLevelAndCallFunction, GetNormalSummary, GetPatientData, GetEFMethod, GetAssessmentTextEF, GetParametersByPOH, GetParameterNameValues, GetAssessmentLevelByValve, GetAssessmentCallFunctionByValve

from RVDimension import RVDimension
from AortaRegurgitation import AortaRegurgitation
from AortaStenosis import AortaStenosis
from LADimension import LADimension
from RADimension import RADimension
from LVRegionalitet import LVRegional
from MitralisStenosis import MitralisStenosis
from PulmonalisStenosis import PulmonalisStenosis
from TricuspidalisStenosis import TricuspidalisStenosis
from PulmonalisStenosis import PulmonalisStenosis
from PulmonaryArteryPressure import PulmonaryArteryPressure
from LVDiastolicFunction import LVDiastolicFunction
from RVSystolicFunction import RVSystolicFunction
from LVFunction import LVSystolicFunction
from MitalisRegurgitation import MitralRegurgitation
from PulmonalisRegurgitation import PulmonalisRegurgitation
from PulmonalisStenosis import PulmonalisStenosis
from TricuspidalisRegurgitation import TricuspidalisRegurgitation
from AortaRootDimensen import AortaRootDimension
from AortaAscendesDimension import AortaAscendesDimension



# passed variables
parametersStr = sys.strings["serializedParameters"]
studyStr = sys.strings["serializedStudy"]
assessmentTextstr = sys.strings["serializedAssessmentTexts"]
findingText = sys.strings["findingText"]
parameterList = sys.strings["parameterList"]
parameterResults = sys.strings["parameterResults"]

AssessmentText = ''
import json

class Assessment:

    parameters = json.loads(parametersStr)
    study = json.loads(studyStr)
    parameterResult = json.loads(parameterResults)
    assessmentTexts = json.loads(assessmentTextstr)
    parameterReferences = json.loads(parameterList)
    findingResult = json.loads(findingText)
    assessment_text = ''
    divider_dash = ''



    valve_normal = False
    count_no_4d_parameters = 0
                    
    def create_assessment(self):
        # process data
        has_limited_assessment = False
        Pathology = False
        dCodeResult = 0;

        for text in self.assessmentTexts:
              if(int(text['ACode']) == -7 and int(text['DCode']) == -7):
                  self.divider_dash += text['DescriptionReportText'] + '\n'

        for text in self.assessmentTexts:
              if(int(text['ACode']) == -1 and int(text['DCode']) == -1):
                  self.assessment_text += text['DescriptionReportText'] + '\n'

        self.assessment_text += str(self.study)
        self.assessment_text += self.divider_dash + '\n'
        for text in self.assessmentTexts:
              if(int(text['ACode']) == -2 and int(text['DCode']) == -2):
                  self.assessment_text += text['DescriptionReportText'] + '\n'
        self.assessment_text += self.divider_dash + '\n'

        ra_dimension_code = int(RADimension(self.parameters, self.parameterReferences).DCode)

        rv_dimension_code = int(RVDimension(self.parameters, self.parameterReferences).DCode)

        la_dimension_code = int(LADimension(self.parameters, self.parameterReferences).DCode)

        lv_systolic_function_code = int(LVSystolicFunction(self.parameters, self.parameterReferences).DCode)

        rv_systolic_function_code = int(RVSystolicFunction(self.parameters, self.parameterReferences).DCode)

        pulmonary_artery_pressure_code = int(PulmonaryArteryPressure(self.parameters, self.parameterReferences).DCode)

        aorta_regurgitation_code = int(AortaRegurgitation(self.parameters, self.parameterReferences).DCode)

        aorta_stenosis_code = int(AortaStenosis(self.parameters, self.parameterReferences).DCode)

        mitral_regurgitation_code = int(MitralRegurgitation(self.parameters, self.parameterReferences).DCode)

        mitralis_stenosis_code = int(MitralisStenosis(self.parameters, self.parameterReferences).DCode)

        pulmonalis_regurgitation_code = int(PulmonalisRegurgitation(self.parameters, self.parameterReferences).DCode)

        pulmonalis_stenosis_code = int(PulmonalisStenosis(self.parameters, self.parameterReferences).DCode)

        tricuspidalis_regurgitation_code = int(TricuspidalisRegurgitation(self.parameters, self.parameterReferences).DCode)

        tricuspidalis_stenosis_code = int(TricuspidalisStenosis(self.parameters, self.parameterReferences).DCode)

        aorta_root_dimension_code = int(AortaRootDimension(self.parameters, self.parameterReferences).DCode)

        aorta_ascendes_dimension_code = int(AortaAscendesDimension(self.parameters, self.parameterReferences).DCode)
        

        aCode = 0

        if(rv_dimension_code == 0):
              aCode = 70
              Pathology = True
        elif(la_dimension_code == 0):
              aCode = 20
              Pathology = True
        elif(ra_dimension_code == 0):
              aCode = 60
              Pathology = True
        elif(lv_systolic_function_code == 0):
              aCode = 50
              Pathology = True
        elif(rv_systolic_function_code == 0):
              aCode = 80
              Pathology = True
        elif(pulmonary_artery_pressure_code == 0):
              aCode = 90
              Pathology = True
        elif(aorta_regurgitation_code == 0):
              aCode = 200
              Pathology = True
        elif(aorta_stenosis_code == 0):
              aCode = 300
              Pathology = True
        elif(mitral_regurgitation_code == 0):
              aCode = 210
              Pathology = True
        elif(mitralis_stenosis_code == 0):
              aCode = 310
              Pathology = True
        elif(pulmonalis_regurgitation_code == 0):
              aCode = 220
              Pathology = True
        elif(pulmonalis_stenosis_code == 0):
              aCode = 320
              Pathology = True
        elif(tricuspidalis_regurgitation_code == 0):
              aCode = 230
              Pathology = True
        elif(tricuspidalis_stenosis_code == 0):
              aCode = 330
              Pathology = True
        elif(aorta_root_dimension_code == 0):
              aCode = 10
              Pathology = True
        elif(aorta_ascendes_dimension_code == 0):
            aCode = 11
            Pathology = True
        else:
              aCode =  100

       
        text_overview = ''
        for text in self.assessmentTexts:
              if(int(text['ACode']) == aCode and int(text['DCode']) == 100):
                  text_overview = text['DescriptionReportText'] + '\n'

        if(text_overview == ''):
                for text in self.assessmentTexts:
                      if(int(text['ACode']) == -8 and int(text['DCode']) == -8):
                            self.assessment_text += text['DescriptionReportText'] + '\n'
        else:
              self.assessment_text += text_overview + '\n'

        
        self.assessment_text += self.findingResult + '\n'
        self.assessment_text += self.divider_dash + '\n'
        for text in self.assessmentTexts:
              if(int(text['ACode']) == -3 and int(text['DCode']) == -3):
                  self.assessment_text += text['DescriptionReportText'] + '\n'
        self.assessment_text += self.divider_dash + '\n'

        if(rv_dimension_code != 0):
            for text in self.assessmentTexts:
              if(int(text['ACode']) == 70 and int(text['DCode']) == rv_dimension_code):
                  self.assessment_text += text['ReportTextSE'] + '\n'

        if(la_dimension_code != 0):
            for text in self.assessmentTexts:
              if(int(text['ACode']) == 20 and int(text['DCode']) == la_dimension_code):
                  self.assessment_text += text['ReportTextSE'] + '\n'

        if(ra_dimension_code != 0):
            for text in self.assessmentTexts:
              if(int(text['ACode']) == 60 and int(text['DCode']) == ra_dimension_code):
                  self.assessment_text += text['ReportTextSE'] + '\n'
 
        if(lv_systolic_function_code != 0):
            for text in self.assessmentTexts:
              if(int(text['ACode']) == 50 and int(text['DCode']) == lv_systolic_function_code):
                  self.assessment_text += text['ReportTextSE'] + '\n'
        
        if(rv_systolic_function_code != 0):
           for text in self.assessmentTexts:
              if(int(text['ACode']) == 80 and int(text['DCode']) == rv_systolic_function_code):
                  self.assessment_text += text['ReportTextSE'] + '\n'

        if(pulmonary_artery_pressure_code != 0):
           for text in self.assessmentTexts:
              if(int(text['ACode']) == 90 and int(text['DCode']) == pulmonary_artery_pressure_code):
                  self.assessment_text += text['ReportTextSE'] + '\n'

        if(aorta_regurgitation_code != 0):
           for text in self.assessmentTexts:
              if(int(text['ACode']) == 200 and int(text['DCode']) == aorta_regurgitation_code):
                  self.assessment_text += text['ReportTextSE'] + '\n'

        if(aorta_stenosis_code != 0):
           for text in self.assessmentTexts:
              if(int(text['ACode']) == 300 and int(text['DCode']) == aorta_stenosis_code):
                  self.assessment_text += text['ReportTextSE'] + '\n'

        if(mitral_regurgitation_code != 0):
           for text in self.assessmentTexts:
              if(int(text['ACode']) == 210 and int(text['DCode']) == mitral_regurgitation_code):
                  self.assessment_text += text['ReportTextSE'] + '\n'

        if(mitralis_stenosis_code != 0):
           for text in self.assessmentTexts:
              if(int(text['ACode']) == 310 and int(text['DCode']) == mitralis_stenosis_code):
                  self.assessment_text += text['ReportTextSE'] + '\n'

        if(pulmonalis_regurgitation_code != 0):
           for text in self.assessmentTexts:
              if(int(text['ACode']) == 220 and int(text['DCode']) == pulmonalis_regurgitation_code):
                  self.assessment_text += text['ReportTextSE'] + '\n'

        if(pulmonalis_stenosis_code != 0):
           for text in self.assessmentTexts:
              if(int(text['ACode']) == 320 and int(text['DCode']) == pulmonalis_stenosis_code):
                  self.assessment_text += text['ReportTextSE'] + '\n'

        if(tricuspidalis_regurgitation_code != 0):
           for text in self.assessmentTexts:
              if(int(text['ACode']) == 230 and int(text['DCode']) == tricuspidalis_regurgitation_code):
                  self.assessment_text += text['ReportTextSE'] + '\n'

        if(tricuspidalis_stenosis_code != 0):
           for text in self.assessmentTexts:
              if(int(text['ACode']) == 330 and int(text['DCode']) == tricuspidalis_stenosis_code):
                  self.assessment_text += text['ReportTextSE'] + '\n'

        if(aorta_root_dimension_code != 0):
           for text in self.assessmentTexts:
              if(int(text['ACode']) == 10 and int(text['DCode']) == aorta_root_dimension_code):
                  self.assessment_text += text['ReportTextSE'] + '\n'

        if(aorta_ascendes_dimension_code != 0):
           for text in self.assessmentTexts:
              if(int(text['ACode']) == 11 and int(text['DCode']) == aorta_ascendes_dimension_code):
                  self.assessment_text += text['ReportTextSE'] + '\n'
        
        self.assessment_text += '\n'

        self.assessment_text += self.parameterResult

        return self.assessment_text
                    
assessment = Assessment()
AssessmentText = assessment.create_assessment()
