using Microsoft.EntityFrameworkCore;
using ParkingControl.Data;

namespace ParkingControl.ParkingRegistration;

public static class ParkingRegistrationController
{
    public static void MapParkingRegistrationController(this WebApplication app)
    {
        var parkingRegistrationsRoutes = app.MapGroup("/parking-registrations");

        parkingRegistrationsRoutes.MapGet("",
            async (AppDbContext context) => await context.ParkingRegistrations.ToListAsync());

        parkingRegistrationsRoutes.MapPost("/checkin", async (PayloadCheckIn payload, AppDbContext context) =>
        {
            var plate = payload.Plate;

            if (!string.IsNullOrEmpty(plate))
            {
                var existingRegistration = context.ParkingRegistrations.SingleOrDefault(r => r.Plate == plate);

                if (existingRegistration != null)
                {
                    var message = $"Placa '{plate}' já registrada";

                    return Results.Conflict(message);
                }
                else
                {
                    var registration = new ParkingRegistration(DateTime.Now, null, plate);

                    var checkIn = registration.CheckInDate;

                    var priceTable = context.PricesTable.FirstOrDefault((item)=>
                        (checkIn >= item.ValidityStartPeriod) && (checkIn <= item.ValidityFinalPeriod));

                    if (priceTable != null)
                        registration.UpdatePrice(priceTable.InitialHourValue);
                    else
                        return Results.NotFound($"Não é possível realizar o checkin pois não há tabela de preço cadastrada");
                    
                    await context.ParkingRegistrations.AddAsync(registration);
                    await context.SaveChangesAsync();

                    var message = $"Checkin registrado com sucesso";

                    return Results.Ok(message);
                }
            }
            else
            {
                var message = $"Placa '{plate}' inválida";

                return Results.UnprocessableEntity(message);
            }
        });

        parkingRegistrationsRoutes.MapPatch("/checkout", async (PayloadCheckOut payload, AppDbContext context) =>
        {
            var plate = payload.Plate;

            if (!string.IsNullOrEmpty(plate))
            {
                var existingRegistration = context.ParkingRegistrations.SingleOrDefault(r => r.Plate == plate);

                if (existingRegistration != null)
                {
                    if (existingRegistration.CheckOutDate != null)
                    {
                       return Results.Conflict($"Não é possível realizar o checkout pois já foi realizado");
                    }
                    
                    var checkIn = existingRegistration.CheckInDate;

                    var priceTable = context.PricesTable.FirstOrDefault((item) =>
                        (checkIn >= item.ValidityStartPeriod) && (checkIn <= item.ValidityFinalPeriod));

                    if (priceTable != null)
                        existingRegistration.CalculatePriceToPay(priceTable.AdditionalHourlyValue,
                            priceTable.InitialHourValue);
                    else
                        return Results.NotFound($"Não é possível realizar o checkout pois não há tabela de preço cadastrada");

                    existingRegistration.UpdateCheckout(DateTime.Now);

                    await context.SaveChangesAsync();

                    return Results.Ok($"Checkout registrado com sucesso");
                }
                else
                {
                    var message = $"Veículo com a placa '{plate}' não encontrado";

                    return Results.NotFound(message);
                }
            }
            else
            {
                var message = $"Placa '{plate}' inválida";

                return Results.UnprocessableEntity(message);
            }
        });
        
        
        parkingRegistrationsRoutes.MapDelete("/{id:guid}", async (Guid id, AppDbContext context) =>
        {
            var existingParkingRegistration = context.ParkingRegistrations.SingleOrDefault((parkingRegistrations) => parkingRegistrations.Id == id);

            if (existingParkingRegistration == null)
            {
                return Results.NotFound($"Nenhum registro de estacionamento encontrado com id {id}");
            }
            else
            {
                context.ParkingRegistrations.Remove(existingParkingRegistration);
                await context.SaveChangesAsync();

                return Results.Ok($"Registro de estacionamento excluído com sucesso");
            }
        });
    }
}