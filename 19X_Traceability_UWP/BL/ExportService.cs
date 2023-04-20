using System;
using System.Threading.Tasks;

namespace _19X_Traceability_UWP.BL
{
    public class ExportService
    {
        private StaticKeyService staticKeyService;
        private FoldingKeyService foldingKeyService;
        private ControlKeyService controlKeyService;
        private LogService logService;

        public ExportService()
        {
            staticKeyService = new StaticKeyService();
            foldingKeyService = new FoldingKeyService();
            controlKeyService = new ControlKeyService();
            logService = new LogService();
        }

        public async Task ExportLastKeys(string connectedDriveName)
        {
            (int? firstSK, int? lastSK) = await staticKeyService.ExportLast(connectedDriveName);
            (int? firstFK, int? lastFK) = await foldingKeyService.ExportLast(connectedDriveName);
            (int? firstKK, int? lastKK) = await controlKeyService.ExportLast(connectedDriveName);
            await logService.logExport(LogService.ExportType.New, firstSK, lastSK, firstFK, lastFK, firstKK, lastKK);
        }

        public async Task ExportDateKeys(DateTime from, DateTime to, string connectedDriveName)
        {
            (int? firstSK, int? lastSK) = await staticKeyService.ExportDate(from, to, connectedDriveName);
            (int? firstFK, int? lastFK) = await foldingKeyService.ExportDate(from, to, connectedDriveName);
            (int? firstKK, int? lastKK) = await controlKeyService.ExportDate(from, to, connectedDriveName);
            await logService.logExport(LogService.ExportType.Date, firstSK, lastSK, firstFK, lastFK, firstKK, lastKK);
        }

        public async Task ExportAllKeys(string connectedDriveName)
        {
            (int? firstSK, int? lastSK) = await staticKeyService.ExportAll(connectedDriveName);
            (int? firstFK, int? lastFK) = await foldingKeyService.ExportAll(connectedDriveName);
            (int? firstKK, int? lastKK) = await controlKeyService.ExportAll(connectedDriveName);
            await logService.logExport(LogService.ExportType.All, firstSK, lastSK, firstFK, lastFK, firstKK, lastKK);
        }
    }
}
