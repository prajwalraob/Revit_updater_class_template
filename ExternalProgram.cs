using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Creation = Autodesk.Revit.Creation;
using System.Windows.Media.Imaging;
using Autodesk.Revit.UI.Events;
using GeometryCheck;


namespace UpdaterTool
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class ExternalProgram : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
        public Result OnStartup(UIControlledApplication application)
        {
            PublicVariables.UpperValue = 10000;
            PublicVariables.LowerValue = 0;
            application.CreateRibbonTab("Updater Tool");
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string dllPath = Path.Combine(appData, @"Autodesk\REVIT\Addins\2020\UpdaterTool.dll");
            string appFolderPath = Path.Combine(appData, @"Autodesk\REVIT\Addins\2020");

            RibbonPanel ribbonPanel = application.CreateRibbonPanel("Updater Tool", "Updater");

            TextBoxData textBoxData1 = new TextBoxData("textBox1");
            TextBox textBox1 = ribbonPanel.AddItem(textBoxData1) as TextBox;
            textBox1.PromptText = "Enter a Reference Value";
            textBox1.EnterPressed += TextBoxEnterClicked;
            ribbonPanel.AddSeparator();
            RadioButtonGroupData radioButtonGroupData1 = new RadioButtonGroupData("radioButtonGroup1");
            RadioButtonGroup radioButtonGroup1 = ribbonPanel.AddItem(radioButtonGroupData1) as RadioButtonGroup;
            
            ToggleButtonData toggleButton1 = new ToggleButtonData("toggleButton1","Deactivate", dllPath, "UpdaterTool.Deactivated");
            BitmapImage img1 = new BitmapImage(new Uri(Path.Combine(appFolderPath, "Stop.png")));
            toggleButton1.LargeImage = img1;

            ToggleButtonData toggleButton2 = new ToggleButtonData("toggleButton2","Activate", dllPath, "UpdaterTool.Activated");
            BitmapImage img2 = new BitmapImage(new Uri(Path.Combine(appFolderPath, "Check.png")));
            toggleButton2.LargeImage = img2;

            radioButtonGroup1.AddItem(toggleButton1);
            radioButtonGroup1.AddItem(toggleButton2);

            return Result.Succeeded;
        }

        private void TextBoxEnterClicked(object obj, TextBoxEnterPressedEventArgs args)
        {
            TextBox tb = obj as TextBox;
            double val = 0.0;
            bool status = double.TryParse(tb.Value.ToString(), out val);

            if (status)
            {
                PublicVariables.LowerValue = val;
            }
            else
            {
                tb.ToolTip = "Enter a nulmber";
            }


        }
    }

    public class AvailabilityControl : IExternalCommandAvailability
    {
        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            return true;
        }
    }
}
