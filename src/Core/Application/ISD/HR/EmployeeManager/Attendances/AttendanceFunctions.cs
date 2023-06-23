namespace ZANECO.API.Application.ISD.HR.EmployeeManager.Attendances;

public class AttendanceFunctions
{
    public int GetLate(DateTime tsScheduleTimIn, DateTime tsActualTimeIn)
    {
        if (!tsActualTimeIn.Equals(TimeSpan.Zero) && !tsScheduleTimIn.Equals(TimeSpan.Zero))
        {
            var tsLateMinutes = tsActualTimeIn - tsScheduleTimIn;
            if (tsLateMinutes.TotalMinutes > 0)
                return (int)tsLateMinutes.TotalMinutes;
        }

        return 0;
    }

    public int GetUnderTime(DateTime tsScheduleTimOut, DateTime tsActualTimeOut)
    {
        if (!tsActualTimeOut.Equals(TimeSpan.Zero) && !tsScheduleTimOut.Equals(TimeSpan.Zero))
        {
            var tsLateUndertime = tsScheduleTimOut - tsActualTimeOut;

            if (tsLateUndertime.TotalMinutes > 0)
                return (int)tsLateUndertime.TotalMinutes;
        }

        return 0;
    }

    public double GetWorkingHours(DateTime tsScheduleTimeIn, DateTime tsActualTimeIn, DateTime tsScheduleTimOut, DateTime tsActualTimeOut, double late = 0, double undertime = 0, bool isOverTime = false)
    {
        if (isOverTime)
        {
            return (tsActualTimeOut - tsActualTimeIn).TotalHours;
        }

        if (!tsActualTimeOut.Equals(TimeSpan.Zero))
        {
            DateTime dtTimeIn;
            DateTime dtTimeOut;

            if (late > 0)
                dtTimeIn = tsActualTimeIn;
            else
                dtTimeIn = tsScheduleTimeIn;

            if (undertime > 0)
                dtTimeOut = tsActualTimeOut;
            else
                dtTimeOut = tsScheduleTimOut;

            return tsScheduleTimeIn.Equals(TimeSpan.Zero) || tsScheduleTimOut.Equals(TimeSpan.Zero)
                ? (tsActualTimeOut - tsActualTimeIn).TotalHours
                : (dtTimeOut - dtTimeIn).TotalHours;
        }

        return 0;
    }
}