using Microsoft.EntityFrameworkCore;
using ParkingControl.Data;

namespace ParkingControl.PriceTable;

public static class PriceTableController
{
    public static void MapPriceTableController(this WebApplication app)
    {
        var priceTablesRoutes = app.MapGroup("/prices");

        priceTablesRoutes.MapGet("", async (AppDbContext context) => await context.PricesTable.ToListAsync());

        priceTablesRoutes.MapGet("/{id:guid}", (Guid id, AppDbContext context) =>

        {
            var existingPrice = context.PricesTable.SingleOrDefault(table => table.Id == id);

            return existingPrice != null
                ? Results.Ok(existingPrice)
                : Results.NotFound("Nenhuma tabela de preços encontrada");
        });

        priceTablesRoutes.MapPost("", async (PayloadPriceTable payloadPriceTable, AppDbContext context) =>
        {
            var periodInvalidToAddPriceTable = await context.PricesTable.AnyAsync((table) =>
                payloadPriceTable.ValidityStartPeriod >= table.ValidityStartPeriod &&
                payloadPriceTable.ValidityStartPeriod < table.ValidityFinalPeriod ||
                payloadPriceTable.ValidityFinalPeriod > table.ValidityStartPeriod &&
                payloadPriceTable.ValidityFinalPeriod <= table.ValidityFinalPeriod);


            var validStartPeriod = payloadPriceTable.ValidityStartPeriod < payloadPriceTable.ValidityFinalPeriod;

            var isValidPriceTable = payloadPriceTable.ValidityStartPeriod != null &&
                                    payloadPriceTable.ValidityFinalPeriod != null &&
                                    payloadPriceTable.AdditionalHourlyValue != null &&
                                    payloadPriceTable.InitialHourValue != null;

            if (isValidPriceTable && !periodInvalidToAddPriceTable && validStartPeriod)
            {
                var newPriceTable = new PriceTable(payloadPriceTable);

                await context.PricesTable.AddAsync(newPriceTable);
                await context.SaveChangesAsync();

                return Results.Ok($"Regra de preço registrada com sucesso");
            }
            else if (periodInvalidToAddPriceTable)
            {
                return Results.UnprocessableEntity($"Já existe uma regra de preço com esse período cadastrado");
            }
            else if (!validStartPeriod)
            {
                return Results.UnprocessableEntity($"A data inicial tem que ser menor que a data final");
            }
            else
            {
                return Results.UnprocessableEntity($"Regra de preço inválida, preencha todos os campos obrigatórios");
            }
        });

        priceTablesRoutes.MapPatch("/{id:guid}", async (Guid id, PayloadPriceTable payload, AppDbContext context) =>
        {
            var existingPriceTable = context.PricesTable.SingleOrDefault((table) => table.Id == id);
            
            if (existingPriceTable == null)
                return Results.NotFound($"Nenhuma tabela de preços encontrada com id {id}");
            
            var periodInvalidToAddPriceTable = await context.PricesTable.AnyAsync((table) =>
                payload.ValidityStartPeriod >= table.ValidityStartPeriod && table.Id != id &&
                payload.ValidityStartPeriod < table.ValidityFinalPeriod ||
                payload.ValidityFinalPeriod > table.ValidityStartPeriod && table.Id != id &&
                payload.ValidityFinalPeriod <= table.ValidityFinalPeriod);
            
            existingPriceTable.UpdatePriceTable(payload, periodInvalidToAddPriceTable);

            await context.SaveChangesAsync();
            return Results.Ok($"Regra de preço atualizada com sucesso");
        });

        priceTablesRoutes.MapDelete("/{id:guid}", async (Guid id, AppDbContext context) =>
        {
            var existingPriceTable = context.PricesTable.SingleOrDefault((table) => table.Id == id);

            if (existingPriceTable == null)
            {
                return Results.NotFound($"Nenhuma tabela de preços encontrada com id {id}");
            }
            else
            {
                context.PricesTable.Remove(existingPriceTable);
                await context.SaveChangesAsync();

                return Results.Ok($"Regra de preço excluída com sucesso");
            }
        });
    }
}