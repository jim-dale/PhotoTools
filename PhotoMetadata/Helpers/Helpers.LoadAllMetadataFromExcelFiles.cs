using System.Collections.Generic;
using System.Linq;
using Ganss.Excel;
using NPOI.SS.UserModel;

namespace PhotoMetadata
{
    internal static partial class Helpers
    {
        public static List<InputMetadata> LoadAllMetadataFromExcelFiles(string[] inputs)
        {
            var result = new List<InputMetadata>();

            foreach (var input in inputs)
            {
                var workbook = WorkbookFactory.Create(input);

                var mapper = new ExcelMapper(workbook);

                foreach (var sheet in workbook)
                {
                    var items = mapper.Fetch<InputMetadata>(sheet.SheetName);

                    var filtered = from item in items
                                   where item.IsEmpty().Equals(false)
                                   select item;

                    result.AddRange(filtered);
                }

                workbook.Close();
            }

            return result;
        }
    }
}
