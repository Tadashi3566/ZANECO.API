using ZANECO.API.Domain.CAD;

namespace ZANECO.API.Application.CAD.PowerRates;

public class PowerRateCreateRequest : RequestExtension<PowerRateCreateRequest>, IRequest<Guid>
{
    public DefaultIdType GroupId { get; private set; } = default!;
    public string Code { get; set; } = default!;

    public string BillMonth { get; set; } = default!;
    public decimal GenerationCharge { get; set; }
    public decimal HostCommCharge { get; set; }
    public decimal ForexICERADAACharge { get; set; }
    public decimal TransmissionDemandCharge { get; set; }
    public decimal TransmissionCharge { get; set; }
    public decimal SystemLossCharge { get; set; }
    public decimal DistributionSystemCharge { get; set; }
    public decimal DistributionDemandCharge { get; set; }
    public decimal SupplySystemCharge { get; set; }
    public decimal SupplyRetailCharge { get; set; }
    public decimal MeteringSystemCharge { get; set; }
    public decimal MeteringRetailCharge { get; set; }
    public decimal UniChargeStrandedDebt { get; set; }
    public decimal UniChargeStrandedContractNPC { get; set; }
    public decimal UniChargeStrandedContractDU { get; set; }
    public decimal UniChargeME { get; set; }
    public decimal UniChargeEqalTaxRoyalties { get; set; }
    public decimal UniChargeEnvironmental { get; set; }
    public decimal UniChargeCrossSubsidyRemoval { get; set; }
    public decimal InterclassCrossSubsidyCharge { get; set; }
    public decimal PowerActReductionCharge { get; set; }
    public decimal LifelineDiscountSubsidyCharge { get; set; }
    public decimal LoanCondonationCharge { get; set; }
    public decimal GRAMICERADAACharge { get; set; }
    public decimal LoanCondonationCustomerPerMonth { get; set; }
    public decimal PrevYearPowerCostAdjustment { get; set; }
    public decimal SystemLossUnderRecovery { get; set; }
    public decimal DistributionVAT { get; set; }
    public decimal GenerationVAT { get; set; }
    public decimal TransmissionVAT { get; set; }
    public decimal SystemLossVAT { get; set; }
    public decimal DifferentialBillPerKWH { get; set; }
    public decimal DifferentialBillPerKW { get; set; }
    public decimal DifferentialBillCustomerPerMonth { get; set; }
    public decimal RFSCCAPEXCharge { get; set; }
    public decimal SeniorCitizenDiscount { get; set; }
    public decimal SeniorCitizenDiscountKWH { get; set; }
    public decimal SeniorCitizenDiscountPercentage { get; set; }
    public decimal SeniorCitizenSubsidyKWH { get; set; }
    public decimal WheelingCharge { get; set; }
    public decimal OtherGenerationCharge { get; set; }
    public decimal OtherTransmissionCharge { get; set; }
    public decimal OtherTransmissionDemandCharge { get; set; }
    public decimal OtherSystemLossCharge { get; set; }
    public decimal OtherLifelineCharge { get; set; }
    public decimal OtherSeniorCitizenCharge { get; set; }
    public decimal FeedInTariffAllowance { get; set; }
    public decimal PowerActRecoveryCharge { get; set; }
    public decimal FinalVAT { get; set; }
    public decimal WithholdingTaxServices { get; set; }
    public decimal WithholdingTaxRental { get; set; }
}

public class CreatePowerRateRequestValidator : CustomValidator<PowerRateCreateRequest>
{
    public CreatePowerRateRequestValidator(IReadRepository<PowerRate> powerRateRepo, IStringLocalizer<CreatePowerRateRequestValidator> localizer)
    {
        RuleFor(p => p.Code)
            .NotEmpty()
            .MustAsync(async (code, ct) => await powerRateRepo.FirstOrDefaultAsync(new PowerRateByCodeSpec(code), ct) is null)
            .WithMessage((_, PowerRate) => string.Format(localizer["PowerRate already exists."], PowerRate));

        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(1024)
            .MustAsync(async (name, ct) => await powerRateRepo.FirstOrDefaultAsync(new PowerRateByNameSpec(name), ct) is null)
            .WithMessage((_, PowerRate) => string.Format(localizer["PowerRate already exists."], PowerRate));
    }
}

public class PowerRateCreateRequestHandler : IRequestHandler<PowerRateCreateRequest, Guid>
{
    private readonly IRepositoryWithEvents<PowerRate> _repository;

    public PowerRateCreateRequestHandler(IRepositoryWithEvents<PowerRate> repository) => _repository = repository;

    public async Task<Guid> Handle(PowerRateCreateRequest request, CancellationToken cancellationToken)
    {
        var powerRate = new PowerRate(request.GroupId, request.Code, request.Name, request.BillMonth, request.GenerationCharge, request.HostCommCharge, request.ForexICERADAACharge, request.TransmissionDemandCharge, request.TransmissionCharge, request.SystemLossCharge, request.DistributionSystemCharge, request.DistributionDemandCharge, request.SupplySystemCharge, request.SupplyRetailCharge, request.MeteringSystemCharge, request.MeteringRetailCharge, request.UniChargeStrandedDebt, request.UniChargeStrandedContractNPC, request.UniChargeStrandedContractDU, request.UniChargeME, request.UniChargeEqalTaxRoyalties, request.UniChargeEnvironmental, request.UniChargeCrossSubsidyRemoval, request.InterclassCrossSubsidyCharge, request.PowerActReductionCharge, request.LifelineDiscountSubsidyCharge, request.LoanCondonationCharge, request.GRAMICERADAACharge, request.LoanCondonationCustomerPerMonth, request.PrevYearPowerCostAdjustment, request.SystemLossUnderRecovery, request.DistributionVAT, request.GenerationVAT, request.TransmissionVAT, request.SystemLossVAT, request.DifferentialBillPerKWH, request.DifferentialBillPerKW, request.DifferentialBillCustomerPerMonth, request.RFSCCAPEXCharge, request.SeniorCitizenDiscount, request.SeniorCitizenDiscountKWH, request.SeniorCitizenDiscountPercentage, request.SeniorCitizenSubsidyKWH, request.WheelingCharge, request.OtherGenerationCharge, request.OtherTransmissionCharge, request.OtherTransmissionDemandCharge, request.OtherSystemLossCharge, request.OtherLifelineCharge, request.OtherSeniorCitizenCharge, request.FeedInTariffAllowance, request.PowerActRecoveryCharge, request.FinalVAT, request.WithholdingTaxServices, request.WithholdingTaxRental, request.Description, request.Notes);

        await _repository.AddAsync(powerRate, cancellationToken);

        return powerRate.Id;
    }
}