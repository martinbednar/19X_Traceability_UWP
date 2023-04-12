namespace _19X_Traceability_UWP.BL
{
    internal class ControlKeyService : KeyService
    {
        protected override string SelectQuery { get; } = @"SELECT
                KK.id,
                dateTime,
                SN,
                SKP,
                custNr,
                keyCode,
                result AS resultId,
                text AS resultDescription
                FROM KK LEFT OUTER JOIN alarmKK ON alarmKK.id = KK.result";

        protected override string OrderByQuery { get; } = " ORDER BY KK.id ASC;";


        protected override string OutputFileName { get; } = "ControlKey";
    }
}
