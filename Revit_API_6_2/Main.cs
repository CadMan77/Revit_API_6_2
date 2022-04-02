// Разработать WPF-приложение, которое позволяет расставлять элементы из категории «Мебель».
// В приложении должен быть выпадающий список для выбора уровня расположения элемента.
// Расположение элемента в модели указывается с помощью запроса точки вставки.

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit_API_6_2
{
    [Transaction(TransactionMode.Manual)]

    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            MainView mainView = new MainView(commandData);
            mainView.ShowDialog();
            //TaskDialog.Show("Сообщение", "Текст");
            return Result.Succeeded;
        }
    }
}
