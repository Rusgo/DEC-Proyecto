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
        public void ExportToExcel(List<string> sheetNames, List<float[,]> matrices, string filePath, List<string> texts)
        {
            using (var workbook = new XLWorkbook())
            {
                for (int i = 0; i < sheetNames.Count; i++)
                {
                    var worksheet = workbook.Worksheets.Add(sheetNames[i]);
                    ExportMatrixToSheet(matrices[i], worksheet);
                    AddTextboxToSheet(texts[i], worksheet, matrices[i].GetLength(0), matrices[i].GetLength(1));
                }

                workbook.SaveAs(filePath);
            }
        }

        private void ExportMatrixToSheet(float[,] matrix, IXLWorksheet worksheet)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1) + 1;

            for (int i = -1; i < rows-1 ; i++)
            {
                
                for (int j = -1; j < cols-1; j++)
                {
                    if (j == -1 && i < rows - 3 && i != -1)
                    {
                        worksheet.Cell(i + 1, j + 1).Value = "A" + (i+2).ToString();
                    }
                    else if (i == -1 && j < cols - 3)
                    {
                        worksheet.Cell(i + 1, j + 1).Value = "C" + (j + 2).ToString();
                    }
                    else if(i== cols - 2 && j == -1)
                    {
                        worksheet.Cell(i + 1, j + 1).Value = "W";
                    }
                    else
                    {
                        worksheet.Cell(i + 2, j + 2).Value = matrix[i, j];
                    }
                    
                }
            }
        }
        private void AddTextboxToSheet(string text, IXLWorksheet worksheet, int row, int col)
        {
            var cell = worksheet.Cell(row + 1, col + 1);
            cell.Value = text;
            cell.Style.Font.Bold = true;
            cell.Style.Fill.BackgroundColor = XLColor.LightGray; // Color de fondo para simular el cuadro de texto
            cell.Style.Alignment.SetWrapText(true); // Habilitar ajuste de texto automático
        }
    }
    
}
