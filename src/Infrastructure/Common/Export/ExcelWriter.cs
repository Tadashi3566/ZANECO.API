using ClosedXML.Excel;
using System.ComponentModel;
using System.Data;
using ZANECO.API.Application.Common.Exporters;

namespace ZANECO.API.Infrastructure.Common.Export;

public class ExcelWriter : IExcelWriter
{
    public Stream WriteToStream<T>(IList<T> data)
    {
        var properties = TypeDescriptor.GetProperties(typeof(T));
        var table = new DataTable("table", "table");
        foreach (PropertyDescriptor prop in properties)
            table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        foreach (var item in data)
        {
            var row = table.NewRow();
            foreach (PropertyDescriptor prop in properties)
                row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
            table.Rows.Add(row);
        }

        using var wb = new XLWorkbook();
        wb.Worksheets.Add(table);
        Stream stream = new MemoryStream();
        wb.SaveAs(stream);
        stream.Seek(0, SeekOrigin.Begin);
        return stream;
    }

    public IList<T> ReadFromStream<T>(Stream stream)
        where T : new()
    {
        using var wb = new XLWorkbook(stream);
        var dataTable = wb.Worksheets.Worksheet(1).Table("table");
        var properties = TypeDescriptor.GetProperties(typeof(T));

        IList<T> resultList = new List<T>();

        foreach (DataRow row in dataTable.DataRange.Rows().Skip(1).Cast<DataRow>())
        {
            T item = new T();
            foreach (PropertyDescriptor prop in properties)
            {
                string propertyName = prop.Name;
                object value = row.Field<object>(propertyName)!;
                Type propertyType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                if (value != null && propertyType != typeof(string))
                {
                    value = Convert.ChangeType(value, propertyType);
                }

                prop.SetValue(item, value);
            }

            resultList.Add(item);
        }

        return resultList;
    }
}