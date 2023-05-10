namespace ZANECO.API.Domain.Common;

public static class DateTimeFunctions
{
    public static int Years(DateTime startDate, DateTime endDate)
    {
        int yearsPassed = endDate.Year - startDate.Year;

        // Are we before the birth date this year? If so subtract one year from the mix
        if (endDate.Month < startDate.Month || (endDate.Month == startDate.Month && endDate.Day < startDate.Day))
        {
            yearsPassed--;
        }

        return yearsPassed;
    }
}