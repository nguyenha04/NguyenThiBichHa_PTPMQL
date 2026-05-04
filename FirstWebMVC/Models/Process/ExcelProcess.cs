using OfficeOpenXml;
using System.Data;

namespace FirstWebMVC.Models.Process
{
    public class ExcelProcess
    {
        public DataTable ExcelToDataTable(string filePath)
        {
            var dt = new DataTable();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;

                // Lấy header (dòng 1)
                for (int col = 1; col <= colCount; col++)
                {
                    dt.Columns.Add(worksheet.Cells[1, col].Text.Trim());
                }

                // Lấy dữ liệu (từ dòng 2)
                for (int row = 2; row <= rowCount; row++)
                {
                    var newRow = dt.NewRow();

                    for (int col = 1; col <= colCount; col++)
                    {
                        newRow[col - 1] = worksheet.Cells[row, col].Text;
                    }

                    dt.Rows.Add(newRow);
                }
            }

            return dt;
        }
    }
}