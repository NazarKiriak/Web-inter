using ClosedXML.Excel;
using RESTwebAPI.Services;

namespace RESTwebAPI.Services
{
    public class ExcelService : IExcelService
    {
        public Stream GenerateExcel()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Sheet1");
                worksheet.Cell(1, 1).Value = "Hello";
                worksheet.Cell(1, 2).Value = "World";

                var stream = new MemoryStream();
                workbook.SaveAs(stream);
                stream.Seek(0, SeekOrigin.Begin);
                return stream;
            }
        }
    }
}
