namespace SWECVI.ApplicationCore
{
    public class Enum
    {
        public enum POH
        {
            LVDimensionVolumesMass = 30, //10,
            LVSystolicFunction = 50, //20,
            LVDiastolicFunction = 40, // 30,
            RVDimension = 70, //40,
            RVFunction = 75, //45,
            LADimension = 20, //50,
            RADimension = 60,
            AortaDimension = 10, //70,
            LADRADCommon = 100,

            AortaInsufficiens = 200, //80,
            AortaStenosis = 300, //90,
            MitralisInsufficiens = 210, //100,
            MitralisStenosis = 310, //110,
            TricuspidalisInsufficiens = 220, //120,
            TricuspidalisStenosis = 320, //130,
            PulmonalisInsufficiens = 230,// 140,
            PulmonalisStenosis = 330, //150,

            OtherDopplerAndHemodynamic = 160
        }

        public enum FindingInput
        {
            Only_check_box,
            Only_radio_buttons,
            Combo_multi_select,
            Combo_one_select,
            Numeric_input,
            Text_input,
            Date_input

        }

        public enum AssessmentFunction
        {
            AortaDimension = 10,
            LADimension = 20,
            LVDimension = 30,
            LVDistolicFunction = 40,
            LVFunction = 50,
            RADimension = 60,
            RVDimension = 70,
            RVFunction = 75,

            LVRegional = 80,
            PAPressure = 90,
            LADRADCommon = 100,

            AortaInsufficiens = 200,
            MitralisInsufficiens = 210,
            TricuspidalisInsufficiens = 220,
            PulmonalisInsufficiens = 230,
            AortaStenosis = 300,
            MitralisStenosis = 310,
            TricuspidalisStenosis = 320,
            PulmonalisStenosis = 330,

            HeaderText = 0,
            BasicDataText = 400,
            Assessmenttext = 0
        }

        public enum Header
        {
            // The number is the key in the table for function = 0
            // All other is taken from the Level 

            DividerDash = 1, // 100,                // -------------------------
            DividerDashSpace = 2, // 101,         // - - - - - - - - - - - - - 
            DividerDot = 3, // 102,               // ..........................

            MainBasicData = 4, // 110,              // BASDATA
            MainMeasuredResult = 5, // 120,         // MÄTRESULTAT

            MainLeftVentricle = 6,// 130,           // VÄNSTERKAMMARE
            SubLVDiameterMass = 7, //  131,
            SubLVSystolicFunction = 8, // 132,       // - Systolisk funktion
            SubLVDiastolicFunction = 9,// 133,

            MainAtrial = 10, // 140,                 // FÖRMAK

            MainRightVentricle = 11,// 150,
            SubRVDiameter = 12, //  131,
            SubRVFunction = 13, // 132, 

            MainValves = 14,// 160,                // KLAFFAR
            SubAorta = 15,// 161,
            SubMitralis = 16, //162,
            SubTricuspidalis = 17,// 163,
            SubPulmonalis = 18, // 164,

            MainDopplerHemo = 19, // 170,

            MainAssessment = 20, // 180,            // BEDÖMNING
            MainStresstest = 21, //190,             // STRESS TEST
            MainConclusion = 22,// 200,             // SAMMANFATTNING

            MainEFMissing = 24,//  210,             // (EF, EDV och ESV har inte kunnat beräknats - mätdata saknas) 
            MainEF4D = 25,// 211,               
            MainEFTri = 26,// 212,                
            MainEFBI42 = 27,// 213,              
            MainEFBI4 = 28, //214,               
            MainEFBI2 = 29,// 215,                
            MainEFAuto42 = 30,// 216,               
            MainEFAuto4 = 31, // 217,                
            MainEFAuto2 = 32,// 218,              

            MainTAPSETT = 33, // 220,               // (TAPSE har beräknats med: TT
            MainTAPSE2D = 34,// 221,                // (TAPSE har beräknats med: 2D
            MainTAPSEMM = 35,// 222,                // (TAPSE har beräknats med: MM
            MainTAPSEMissing = 36,// 223,

            TextStandardReply = 38,// 10,           // Standard reply
            TextStressReply = 39, //11,             // Stress seplay
            TextConclusionNormal = 23,               // SAMMANFATTNING NORMAL
            TextConclusionAbnormal = 23,            // SAMMANFATTNING EJ NORMAL

            MainNoParameters = 37 // 300 
                                  // Be aware the key number over 100 if add rows this must be updated.
        }
        public enum DCode
        {
            #region DCode
            Normal = 0,
            Level11 = 11,
            Level12 = 12,
            Level13 = 13,
            Level14 = 14,
            Level15 = 15,
            Level16 = 16,
            Level17 = 17,
            Level18 = 18,
            Level19 = 19,
            Level20 = 20,
            Level21 = 21,
            Level22 = 22,
            Level23 = 23,
            Level24 = 24,
            Level25 = 25,
            Level26 = 26,
            Level27 = 27,
            Level28 = 28,
            Level29 = 29,
            Level30 = 30,
            Level31 = 31,
            Level32 = 32,
            Level33 = 33,
            Level34 = 34,
            Level35 = 35,
            Level36 = 36,
            Level37 = 37,
            Level38 = 38,
            Level39 = 39,
            Level40 = 40,
            Level41 = 41,
            Level42 = 42,
            Level43 = 43,
            Level44 = 44,
            Level45 = 45,
            Level46 = 46,
            Level47 = 47,
            Level48 = 48,
            Level49 = 49,
            Level50 = 50,
            Level51 = 51,
            Level52 = 52,
            Level53 = 53,
            Level54 = 54,
            Level55 = 55,
            Level56 = 56,
            Level57 = 57,
            Level58 = 58,
            Level59 = 59,
            Level60 = 60,
            Level61 = 61,
            Level62 = 62,
            Level63 = 63,
            Level64 = 64,
            Level65 = 65,
            Level66 = 66,
            Level67 = 67,
            Level68 = 68,
            Level69 = 69,
            Level70 = 70,
            Level71 = 71,
            Level72 = 72,
            Level73 = 73,

            Level350 = 350,
            Level360 = 360,

            DataHasNoReference = 700,
            DataIsOutsideRange = 800,
            DataIsMissing = 900

            #endregion DCode
        }
        public enum DStatus
        {
            Normal = 100,
            Limit = 200,
            // DataIsOutsideRange = 800,
            // DataIsMissing = 900
        }
        public enum Gender
        {
            Male = 1,
            Female = 2,
            Unknown = 3,
        }

        public enum FunctionSelector
        {
            Min = 1,
            Avg = 2,
            Max = 3,
            Latest  = 4
        }
        public enum EFMethod
        {
            EF4D = 1,
            TRIPLANE = 2,
            BIPLANE4CH2CH = 3,
            BIPLANE4CH = 4,
            BIPLANE2CH = 5,
            AUTO4CH2CH = 6,
            AUTO4CH = 7,
            AUTO2CH = 8,
            UNKNOWN = 9
        }
        public enum TAPSEMethod
        {
            TAPSE_TT = 1,
            TAPSE_2D = 2,
            TAPSE_MM = 3,
            UNKNOWN = 9
        }

        public enum PatientFilter
        {
            OneYearAgo = 1,
            Today = 2,
            Yesterday = 3,
            Week = 4
        }

        public enum ParameterGridTab
        {
            ParameterTab = 0,
            ChartTab = 1,
            ExamBaseDataTab = 2,
            WMSIChartTab = 3
        }

        public enum AssessmentKey
        {
            NormalSummary = 38,                 // Normal summary: Normalstor vänsterkammare utan hypertrofi.
            AbnormalSummary = 41                // Abnormal summary: Patologiska resultat.
        }
    }
}
