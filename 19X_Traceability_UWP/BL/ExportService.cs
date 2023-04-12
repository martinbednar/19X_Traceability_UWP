using System;

namespace _19X_Traceability_UWP.BL
{
    public class ExportService
    {
        public void ExportLastKeys(string connectedDriveName)
        {
            StaticKeyService staticKeyService = new StaticKeyService();
            FoldingKeyService foldingKeyService = new FoldingKeyService();
            ControlKeyService controlKeyService = new ControlKeyService();

            staticKeyService.ExportLast(connectedDriveName);
            foldingKeyService.ExportLast(connectedDriveName);
            controlKeyService.ExportLast(connectedDriveName);
        }

        public void ExportDateKeys(DateTime from, DateTime to, string connectedDriveName)
        {
            StaticKeyService staticKeyService = new StaticKeyService();
            FoldingKeyService foldingKeyService = new FoldingKeyService();
            ControlKeyService controlKeyService = new ControlKeyService();

            staticKeyService.ExportDate(from, to, connectedDriveName);
            foldingKeyService.ExportDate(from, to, connectedDriveName);
            controlKeyService.ExportDate(from, to, connectedDriveName);
        }

        public void ExportAllKeys(string connectedDriveName) {
            StaticKeyService staticKeyService = new StaticKeyService();
            FoldingKeyService foldingKeyService = new FoldingKeyService();
            ControlKeyService controlKeyService = new ControlKeyService();

            staticKeyService.ExportAll(connectedDriveName);
            foldingKeyService.ExportAll(connectedDriveName);
            controlKeyService.ExportAll(connectedDriveName);
        }
    }
}
