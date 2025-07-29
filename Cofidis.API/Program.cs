using Cofidis.Application.Services;
using Cofidis.Domain.Exceptions;
using Cofidis.Domain.Services;
using Cofidis.Infra;
using Cofidis.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CofidisDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ServerConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<CreditApplicationService>();
builder.Services.AddScoped<RiskIndexCalculator>();
builder.Services.AddScoped<InterestRateCalculator>();
builder.Services.AddScoped<LoanQuoteCalculator>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<CreditEligibilityEvaluator>();
builder.Services.AddScoped<CreditLimitService>();
builder.Services.AddScoped<CreditLimitRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/application", async (CreditApplicationService creditApplicationService, CreditApplicationRequest creditApplicationRequest) =>
    {
        try
        {
            return Results.Ok(new
            {
                approved = true,
                data = await creditApplicationService.Evaluate(
                    creditApplicationRequest.RequestedAmount,
                    creditApplicationRequest.TermInMonths,
                    creditApplicationRequest.CustomerTin)
            });
        }
        catch (CreditLimitExceededException ex)
        {
            return Results.BadRequest(new
            {
                approved = false,
                error = ex.Message
            });
        }
        catch (ExcessiveEffortException ex)
        {
            return Results.BadRequest(new
            {
                approved = false,
                error = ex.Message
            });
        }
        catch (CreditDeniedException ex)
        {
            return Results.BadRequest(new
            {
                approved = false,
                error = ex.Message
            });
        }
        catch (Exception ex)
        {
            return Results.BadRequest(new
            {
                error = ex.Message
            });
        }
    })
    .WithOpenApi();

app.Run();

record CreditApplicationRequest(decimal RequestedAmount, int TermInMonths, string CustomerTin);
