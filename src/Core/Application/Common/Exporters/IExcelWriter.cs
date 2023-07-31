namespace ZANECO.API.Application.Common.Exporters;

public interface IExcelWriter : ITransientService
{
    Stream WriteToStream<T>(IList<T> data);

    IList<T> ReadFromStream<T>(Stream stream)
        where T : new();
}