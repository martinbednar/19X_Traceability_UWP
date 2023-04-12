namespace _19X_Traceability_UWP.BL
{
    internal class StaticKeyService : KeyService
    {
        protected override string SelectQuery { get; } = @"SELECT
                SK.id,
                dateTime,
                SN,
                SKP,
                custNr,
                keyCode,
                result AS resultId,
                text AS resultDescription,
                pcInSet,
                actCnt,
                forceKey,
                forcePress,
                forcePull
                FROM SK LEFT OUTER JOIN alarmSK ON alarmSK.id = SK.result";

        protected override string OrderByQuery { get; } = " ORDER BY SK.id ASC;";

        protected override string OutputFileName { get; } = "StaticKey";
    }
}
