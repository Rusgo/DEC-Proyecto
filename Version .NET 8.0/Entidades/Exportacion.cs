using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTp.Entidades
{
    public class ExcelExporter
    {
        public void ExportToExcel(List<string> sheetNames, List<string[,]> matrices, string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                for (int i = 0; i < sheetNames.Count; i++)
                {
                    var worksheet = workbook.Worksheets.Add(sheetNames[i]);
                    ExportMatrixToSheet(matrices[i], worksheet);
                }

                workbook.SaveAs(filePath);
            }
        }

        private void ExportMatrixToSheet(string[,] matrix, IXLWorksheet worksheet)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    worksheet.Cell(i + 1, j + 1).Value = matrix[i, j];
                }
            }
        }
    }
}
