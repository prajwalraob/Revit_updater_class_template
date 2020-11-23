using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Macros;
using GeometryCheck;
using GeometryCheck.Models;

namespace UpdaterTool
{
    class UpdaterClass : IUpdater
    {
        private static AddInId addInId;
        private static UpdaterId updaterId;

        public UpdaterClass(AddInId addInId)
        {
            UpdaterClass.addInId = addInId;
            updaterId = new UpdaterId(UpdaterClass.addInId, new Guid("FBFBF6B2-4C06-42d4-97C1-D1B4EB593EFF"));
        }
        public void Execute(UpdaterData data)
        {
            Document Doc = data.GetDocument();

            using (SubTransaction str = new SubTransaction(Doc))
            {
                try
                {
                    var addedElementIds = data.GetAddedElementIds();
                    var modifiedElementIds = data.GetModifiedElementIds();
                    var deletedElementIds = data.GetDeletedElementIds();

                    // Your Code Goes Here
                }
                catch
                {
                    str.RollBack();
                }

            }
        }

        public UpdaterId GetUpdaterId()
        {
            return updaterId;
        }

        public ChangePriority GetChangePriority()
        {
            return ChangePriority.Views;
        }

        public string GetUpdaterName()
        {
            return "CustomMeasurementUpdater";
        }

        public string GetAdditionalInformation()
        {
            return "CustomMeasurementUpdater";
        }
    }
}
