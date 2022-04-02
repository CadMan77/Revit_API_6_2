using Autodesk.Revit;
//using Autodesk.Revit.Creation;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit_API_6_2
{
    internal class MainViewViewModel
    {
        private ExternalCommandData _commandData;
        private UIDocument uidoc;
        private Document doc;

        public List<FamilySymbol> ProjectFurnitureTypes { get; } = new List<FamilySymbol>();
        public List<Level> ProjectBaseConstraints { get; } = new List<Level>();
        public FamilySymbol SelectedFurnitureType { get; set; }
        public Level SelectedBaseConstraint { get; set; }

        public DelegateCommand ApplyCommand { get; }

        XYZ insertPoint = null;

        public MainViewViewModel(ExternalCommandData commandData)
        {
            _commandData = commandData;
            uidoc = _commandData.Application.ActiveUIDocument;
            doc = uidoc.Document;

            ApplyCommand = new DelegateCommand(OnApplyCommand);

            insertPoint = uidoc.Selection.PickPoint("Выберите точку вставки:");

            List<FamilySymbol> projectFurnitureTypes = new FilteredElementCollector(doc)
                .WhereElementIsElementType()
                .OfCategory(BuiltInCategory.OST_Furniture)
                .Cast<FamilySymbol>()
                .ToList();

            ProjectFurnitureTypes = projectFurnitureTypes;

            //TaskDialog.Show("projectFurnitureTypes", $"{projectFurnitureTypes.Count}");

            List<Level> projectBaseConstraints = new FilteredElementCollector(doc)
                .OfClass(typeof(Level))
                .Cast<Level>()
                .ToList();

            ProjectBaseConstraints = projectBaseConstraints;

            //TaskDialog.Show("ProjectBaseConstraints", $"{ProjectBaseConstraints.Count}");            
        }

        private void OnApplyCommand()
        {
            if (insertPoint == null || SelectedFurnitureType == null || SelectedBaseConstraint == null)
            {
                return;
            }

            using (Transaction ts = new Transaction(doc, "Place Family Instance Transaction"))
            {
                ts.Start();

                //doc.Create.NewFamilyInstance();

                ts.Commit();
            }
            RaiseCloseRequest();
        }

        public EventHandler CloseRequest;

        public void RaiseCloseRequest()
        {
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }
    }
}