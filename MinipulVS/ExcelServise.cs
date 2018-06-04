using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace MinipulVS
{
    public interface IExcelServise
    {
        void PointToFile(IEnumerable<RobotPosition> points);
    }
    class ExcelServise : IExcelServise
    {
        public void PointToFile(IEnumerable<RobotPosition> points)
        {
            //****Создание экземпляра файла Excel****
            Excel.Application excelApp = new Excel.Application();
            // Создаём экземпляр рабочий книги Excel
            Excel.Workbook workBook;
            // Создаём экземпляр листа Excel
            Excel.Worksheet workSheet;
            workBook = excelApp.Workbooks.Add();
            workSheet = (Excel.Worksheet)workBook.Worksheets.get_Item(1);

            //Внесение в книгу первых строк констант
            workSheet.Name = "data1";
            workSheet.Range["A2:D15000"].NumberFormat = "0.00E+00"; // установка формата ячеек
            workSheet.Cells[1, 1] = "t";
            workSheet.Cells[1, 2] = "x";
            workSheet.Cells[1, 3] = "y";
            workSheet.Cells[1, 4] = "z";
            workSheet.Cells[2, 1] = 0 * 1e-3;
            workSheet.Cells[2, 4] = 0;
            workSheet.Cells[2, 2] = 390;
            workSheet.Cells[2, 3] = 686.6;

            int COUNT = 3; // начальное положение для заполнения

            //Заполнение данными
            foreach (var position in points)
            {
                workSheet.Cells[COUNT, 1] = position.time;
                workSheet.Cells[COUNT, 4] = position.z;
                workSheet.Cells[COUNT, 2] = position.x;
                workSheet.Cells[COUNT, 3] = position.y;
                COUNT++;
            }

            //Обработка ошибки сохранения
            try
            {
                string outpath = Environment.CurrentDirectory + "/";
                workBook.SaveAs(@outpath + "end_position.xlsx");
                workBook.Close(false);
                excelApp.Quit();
                excelApp = null;
                workBook = null;
                workSheet = null;
                GC.Collect();// обнуляются ссылки, процесс удалится сборщиком мусора
            }
            catch (Exception ex)
            {
                workBook.Close(false);
                excelApp.Quit();
                excelApp = null;
                workBook = null;
                workSheet = null;
                GC.Collect();// обнуляются ссылки, процесс удалится сборщиком мусора
                System.Windows.Forms.MessageBox.Show(ex.Message + "Ошибка сохранения. Файл остался в прежнем состоянии. Ресурсы освобождены.");
            }
        }
    }
}
