namespace SWECVI.ApplicationCore.Common
{
    public static class UnitExtension
    {
        public static decimal ConvertFromSI(decimal valueInSI, string targetUnit)
        {
            switch (targetUnit)
            {
                case "mm":
                case "g":
                case "g/m2":
                case "mm/m2":
                case "ms":
                    return valueInSI * 1000;
                case "cm":
                case "%":
                case "cm/s":
                case "cm/s2":
                    return valueInSI * 100;
                case "cm2":
                    return valueInSI * 100 * 100;
                case "l/min":
                case "l/minm2":
                case "l/min/m2":
                    return valueInSI * 1000 * 60;  // return (valueInSI * 1000) / 60;
                case "ml/m2":
                case "ml/s":
                case "ml":
                    return valueInSI * 1000 * 1000;
                case "cm2/m2":
                    return valueInSI * 100 * 100;
                case "m/s2":
                case "m/s":
                case "BPM":
                case "kg":
                case "m2":
                case "mmHg":
                default:
                    return valueInSI;
            }
        }
    }
}
