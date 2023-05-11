using ZANECO.API.Application.Identity.Roles;
using ZANECO.API.Application.Identity.Users;
using ZANECO.API.Application.ISD.HR.EmployeeManager.Employees;
using ZANECO.API.Application.SMS.MessageLogs;
using ZANECO.API.Application.SMS.MessageTemplates;
using ZANECO.API.Domain.CAD;
using ZANECO.API.Domain.ISD;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;
using ZANECO.API.Domain.SMS;

namespace ZANECO.API.Application.Dashboard;

public class GetStatsRequest : IRequest<StatsDto>
{
    //public Guid Id { get; set; }

    //public GetStatsRequest(Guid id) => Id = id;
}

public class GetStatsRequestHandler : IRequestHandler<GetStatsRequest, StatsDto>
{
    //private readonly ICurrentUser _currentUser;
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;

    private readonly IReadRepository<Employee> _repoEmployee;

    private readonly IReadRepository<Contact> _repoContact;
    private readonly IReadRepository<MessageLog> _repoSMSLog;
    private readonly IReadRepository<MessageTemplate> _repoSMSTemplate;

    private readonly IReadRepository<Member> _repoMember;
    private readonly IReadRepository<Account> _repoAccount;

    private readonly IReadRepository<Brand> _repoBrand;
    private readonly IReadRepository<Product> _repoProduct;

    public GetStatsRequestHandler(
        //ICurrentUser currentUser,
        IUserService userService,
        IRoleService roleService,
        IReadRepository<Employee> repoEmployee,
        IReadRepository<Contact> repoContact,
        IReadRepository<MessageLog> repoSMSLog,
        IReadRepository<MessageTemplate> repoSMSTemplate,
        IReadRepository<Member> repoMember,
        IReadRepository<Account> repoAccount,
        IReadRepository<Brand> repoBrand,
        IReadRepository<Product> repoProduct)
    {
        //_currentUser = currentUser;
        _userService = userService;
        _roleService = roleService;

        _repoEmployee = repoEmployee;

        _repoContact = repoContact;
        _repoSMSLog = repoSMSLog;
        _repoSMSTemplate = repoSMSTemplate;

        _repoMember = repoMember;
        _repoAccount = repoAccount;

        _repoBrand = repoBrand;
        _repoProduct = repoProduct;
    }

    public async Task<StatsDto> Handle(GetStatsRequest request, CancellationToken cancellationToken)
    {
        var stats = new StatsDto
        {
            UserCount = await _userService.GetCountAsync(cancellationToken),
            RoleCount = await _roleService.GetCountAsync(cancellationToken),

            ContactCount = await _repoContact.CountAsync(cancellationToken),
            SMSLogCount = await _repoSMSLog.CountAsync(cancellationToken),
            SMSTemplateCount = await _repoSMSTemplate.CountAsync(new MessageTemplateByScheduleSpec(), cancellationToken),

            ProductCount = await _repoProduct.CountAsync(cancellationToken),
            BrandCount = await _repoBrand.CountAsync(cancellationToken),

            MemberCount = await _repoMember.CountAsync(cancellationToken),
            AccountCount = await _repoAccount.CountAsync(cancellationToken),
        };

        var birthdays = await _repoEmployee.ListAsync(new EmployeeByBirthdaySpec(), cancellationToken);
        var anniversaries = await _repoEmployee.ListAsync(new EmployeeByAnniversarySpec(), cancellationToken);

        int selectedYear = DateTime.Now.Year;
        double[] brandsFigure = new double[13];
        double[] productsFigure = new double[13];
        double[] smsLogFigure = new double[13];
        double[] smsLogsPerDay = new double[32];

        for (int i = 1; i <= 12; i++)
        {
            int month = i;
            var filterStartDate = new DateTime(selectedYear, month, 01);
            var filterEndDate = new DateTime(selectedYear, month, DateTime.DaysInMonth(selectedYear, month), 23, 59, 59); // Monthly Based

            var brandSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Brand>(filterStartDate, filterEndDate);
            var productSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Product>(filterStartDate, filterEndDate);
            var smsLogSpec = new AuditableEntitiesByCreatedOnBetweenSpecInt<MessageLog>(filterStartDate, filterEndDate);

            brandsFigure[i - 1] = await _repoBrand.CountAsync(brandSpec, cancellationToken);
            productsFigure[i - 1] = await _repoProduct.CountAsync(productSpec, cancellationToken);
            smsLogFigure[i - 1] = await _repoSMSLog.CountAsync(smsLogSpec, cancellationToken);
        }

        stats.BarChartSandurot.Add(new ChartSeries { Name = "Brands", Data = brandsFigure });
        stats.BarChartSandurot.Add(new ChartSeries { Name = "Products", Data = productsFigure });
        stats.BarChartSMSPerMonth.Add(new ChartSeries { Name = "SMS per Month", Data = smsLogFigure });

        int currentYear = DateTime.Today.Year;
        int currentMonth = DateTime.Today.Month;

        for (int day = 1; day <= DateTime.DaysInMonth(currentYear, currentMonth); day++)
        {
            DateTime date = new DateTime(currentYear, currentMonth, day);
            var result = _repoSMSLog.CountAsync(new MessageLogBySendTimeSpec(new DateTime(currentYear, currentMonth, day, 0, 0, 0), new DateTime(currentYear, currentMonth, day, 23, 59, 59)), cancellationToken);
            smsLogsPerDay[day - 1] = result.Result;
        }

        stats.BarChartSMSPerDay.Add(new ChartSeries { Name = "SMS per Day", Data = smsLogsPerDay });

        return stats;
    }
}