using ZANECO.API.Application.Common.Interfaces;

namespace ZANECO.API.Infrastructure.Common;

public class DateTimeFunctions : IDateTimeFunctions
{
    public int GetWorkingDays(DateTime startDate, DateTime endDate)
    {
        int workingDays = 0;
        int totalDays = (int)(endDate - startDate).TotalDays;

        for (int i = 0; i <= totalDays; i++)
        {
            var currentDay = startDate.AddDays(i);
            if (currentDay.DayOfWeek != DayOfWeek.Saturday && currentDay.DayOfWeek != DayOfWeek.Sunday)
            {
                workingDays++;
            }
        }

        return workingDays;
    }
}