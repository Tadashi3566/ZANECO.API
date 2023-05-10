using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ZANECO.API.Application.Common.Events;
using ZANECO.API.Application.Common.Interfaces;
using ZANECO.API.Domain.AGMA;
using ZANECO.API.Domain.App;
using ZANECO.API.Domain.CAD;
using ZANECO.API.Domain.Catalog;
using ZANECO.API.Domain.ISD;
using ZANECO.API.Domain.ISD.HR.EmployeeManager;
using ZANECO.API.Domain.ISD.HR.PayrollManager;
using ZANECO.API.Domain.SMS;
using ZANECO.API.Domain.Surveys;
using ZANECO.API.Infrastructure.Persistence.Configuration;

namespace ZANECO.API.Infrastructure.Persistence.Context;

public class ApplicationDbContext : BaseDbContext
{
    public ApplicationDbContext(ITenantInfo currentTenant, DbContextOptions options, ICurrentUser currentUser, ISerializerService serializer, IOptions<DatabaseSettings> dbSettings, IEventPublisher events)
        : base(currentTenant, options, currentUser, serializer, dbSettings, events)
    {
    }

    public DbSet<Group> Groups => Set<Group>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<TicketProgress> TicketProgress => Set<TicketProgress>();

    public DbSet<Contact> Contacts => Set<Contact>();
    public DbSet<MessageTemplate> MessageTemplates => Set<MessageTemplate>();
    public DbSet<MessageIn> MessageIn => Set<MessageIn>();
    public DbSet<MessageOut> MessageOut => Set<MessageOut>();
    public DbSet<MessageLog> MessageLog => Set<MessageLog>();

    public DbSet<Raffle> Raffles => Set<Raffle>();
    public DbSet<Prize> Prizes => Set<Prize>();
    public DbSet<Winner> Winners => Set<Winner>();

    public DbSet<Rate> Rates => Set<Rate>();
    public DbSet<RatingTemplate> RatingTemplates => Set<RatingTemplate>();
    public DbSet<Rating> Ratings => Set<Rating>();

    public DbSet<Salary> Salaries => Set<Salary>();
    public DbSet<Adjustment> Adjustments => Set<Adjustment>();
    public DbSet<EmployeeAdjustment> EmployeeAdjustments => Set<EmployeeAdjustment>();

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Employer> Employers => Set<Employer>();
    public DbSet<Designation> Designations => Set<Designation>();
    public DbSet<Dependent> Dependents => Set<Dependent>();
    public DbSet<Powerbill> PowerBills => Set<Powerbill>();
    public DbSet<Document> Documents => Set<Document>();

    public DbSet<Schedule> Schedules => Set<Schedule>();
    public DbSet<ScheduleDetail> ScheduleDetails => Set<ScheduleDetail>();
    public DbSet<Calendar> Calendars => Set<Calendar>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<Attendance> Attendance => Set<Attendance>();
    public DbSet<TimeLog> TimeLogs => Set<TimeLog>();
    public DbSet<Contribution> Contributions => Set<Contribution>();
    public DbSet<Payroll> Payrolls => Set<Payroll>();
    public DbSet<PayrollAdjustment> PayrollAdjustments => Set<PayrollAdjustment>();
    public DbSet<EmployeePayroll> EmployeePayrolls => Set<EmployeePayroll>();
    public DbSet<EmployeePayrollDetail> EmployeePayrollDetails => Set<EmployeePayrollDetail>();
    public DbSet<Loan> Loans => Set<Loan>();

    public DbSet<Area> Areas => Set<Area>();
    public DbSet<Barangay> Barangays => Set<Barangay>();
    public DbSet<Route> Routes => Set<Route>();

    public DbSet<PowerRate> PowerRates => Set<PowerRate>();
    public DbSet<PowerConsumption> PowerConsumptions => Set<PowerConsumption>();
    public DbSet<RemoteCollection> RemoteCollections => Set<RemoteCollection>();
    public DbSet<Member> Members => Set<Member>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Ledger> Ledgers => Set<Ledger>();

    public DbSet<Barcode> Barcodes => Set<Barcode>();
    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Discount> Discounts => Set<Discount>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Sale> Sales => Set<Sale>();
    public DbSet<SaleItem> SaleItems => Set<SaleItem>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(SchemaNames.ZANECO);
    }
}