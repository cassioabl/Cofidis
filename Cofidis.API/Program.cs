using Cofidis.Application.Services;
using Cofidis.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.MapGet("/simulation", (string customerTin) =>
//     {
//         var creditApplicationService = new CreditApplicationService(
//             new RiskIndexCalculator(),
//             new InterestRateCalculator(),
//             new LoanQuoteCalculator(),
//             new CustomerService(),
//             new CreditEligibilityEvaluator(),
//             new CreditLimitService()
//         );
//
//         return Results.Ok(creditApplicationService.Evaluate(
//             creditApplicationRequest.requestedAmount,
//             creditApplicationRequest.termInMonths,
//             creditApplicationRequest.customerTin));
//     })
//     .WithName("GetWeatherForecast")
//     .WithOpenApi();

app.MapPost("/application", (CreditApplicationRequest creditApplicationRequest) =>
    {
        var creditApplicationService = new CreditApplicationService(
            new RiskIndexCalculator(),
            new InterestRateCalculator(),
            new LoanQuoteCalculator(),
            new CustomerService(),
            new CreditEligibilityEvaluator(),
            new CreditLimitService()
        );

        return Results.Ok(creditApplicationService.Evaluate(
            creditApplicationRequest.requestedAmount,
            creditApplicationRequest.termInMonths,
            creditApplicationRequest.customerTin));
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();

record CreditApplicationRequest(decimal requestedAmount, int termInMonths, string customerTin);
