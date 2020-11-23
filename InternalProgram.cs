using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;
using GeometryCheck;
using Creation = Autodesk.Revit.Creation;

namespace UpdaterTool
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    class Activated : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            PublicVariables.SetUiApplication(commandData.Application);
            PublicVariables.SetUiDocument(commandData.Application.ActiveUIDocument);

            UpdaterClass updater = new UpdaterClass(commandData.Application.ActiveAddInId);
            UpdaterRegistry.RegisterUpdater(updater);
            UpdaterRegistry.EnableUpdater(updater.GetUpdaterId());
            UpdaterRegistry.AddTrigger(updater.GetUpdaterId(), new ElementClassFilter(typeof(FamilyInstance)), Element.GetChangeTypeAny());
            UpdaterRegistry.AddTrigger(updater.GetUpdaterId(), new ElementClassFilter(typeof(FamilyInstance)), Element.GetChangeTypeElementAddition());
            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    class Deactivated : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UpdaterClass updater = new UpdaterClass(commandData.Application.ActiveAddInId);
            UpdaterRegistry.RemoveAllTriggers(updater.GetUpdaterId());
            UpdaterRegistry.DisableUpdater(updater.GetUpdaterId());
            UpdaterRegistry.UnregisterUpdater(updater.GetUpdaterId());
            return Result.Succeeded;
        }
    }
}
