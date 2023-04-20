namespace _19X_Traceability_UWP.BL
{
    internal class FoldingKeyService : KeyService
    {
        protected override string SelectQuery { get; } = @"SELECT
                FK.id,
                dateTime,
                SN,
                SKP,
                custNr,
                keyCode,
                result AS resultId,
                text AS resultDescription,
                pcInSet,
                actCnt,
                home,
                ins,
                keyPress,
                spirollPress,
                takeOut,
                forceKey,
                forceSpiroll,
                notEnough,
                tooMuch,
                spirolDepth
                FROM FK LEFT OUTER JOIN alarmFK ON alarmFK.id = FK.result";

        protected override string OrderByQuery { get; } = " ORDER BY FK.id ASC;";

        protected override string WhereQueryExportLast { get; } = " WHERE FK.id > (ISNULL((SELECT TOP (1) lastFK FROM exportLog WHERE exportType = 1 AND lastFK IS NOT NULL ORDER BY id DESC),0))";

        protected override string OutputFileName { get; } = "FoldingKey";
    }
}
