namespace ZANECO.API.Application.Common.Interfaces;

public interface IDateTimeFunctions
{
    public int GetWorkingDays(DateTime startDate, DateTime endDate);
}