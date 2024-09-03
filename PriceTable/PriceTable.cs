namespace ParkingControl.PriceTable;

public class PriceTable
{
    public Guid Id { get; init; }
    public DateTime ValidityStartPeriod { get; private set; }
    public DateTime ValidityFinalPeriod { get; private set; }
    public decimal InitialHourValue { get; private set; }
    public decimal AdditionalHourlyValue { get; private set; }

    public PriceTable(PayloadPriceTable payloadPriceTable)
    {
        InitialHourValue = payloadPriceTable.InitialHourValue;
        AdditionalHourlyValue = payloadPriceTable.AdditionalHourlyValue;
        ValidityStartPeriod = new DateTime(payloadPriceTable.ValidityStartPeriod.Year,
            payloadPriceTable.ValidityStartPeriod.Month, payloadPriceTable.ValidityStartPeriod.Day, 0, 0, 0);
        ValidityFinalPeriod = new DateTime(payloadPriceTable.ValidityFinalPeriod.Year,
            payloadPriceTable.ValidityFinalPeriod.Month, payloadPriceTable.ValidityFinalPeriod.Day, 0, 0, 0);
    }
    

    public PriceTable(DateTime validityStartPeriod, DateTime validityFinalPeriod, decimal initialHourValue,
        decimal additionalHourlyValue)
    {
        Id = new Guid();
        ValidityStartPeriod = new DateTime(validityStartPeriod.Year,
            validityStartPeriod.Month, validityStartPeriod.Day, 0, 0, 0);
        ValidityFinalPeriod = new DateTime(validityFinalPeriod.Year,
            validityFinalPeriod.Month, validityFinalPeriod.Day, 0, 0, 0);
        InitialHourValue = initialHourValue;
        AdditionalHourlyValue = additionalHourlyValue;
    }
    
    public void UpdatePriceTable(PayloadPriceTable payload, bool periodInvalidToAddPriceTable )
    {
        var validStartPeriod = payload.ValidityStartPeriod < payload.ValidityFinalPeriod;
        
        if (!validStartPeriod || periodInvalidToAddPriceTable) return;
        
        if (payload.ValidityStartPeriod != null)
            ValidityStartPeriod = new DateTime(payload.ValidityStartPeriod.Year,
                payload.ValidityStartPeriod.Month, payload.ValidityStartPeriod.Day, 0, 0, 0);

        if (payload.ValidityFinalPeriod != null)
            ValidityFinalPeriod = new DateTime(payload.ValidityFinalPeriod.Year,
                payload.ValidityFinalPeriod.Month, payload.ValidityFinalPeriod.Day, 0, 0, 0);

        if (payload.InitialHourValue != null)
            InitialHourValue = payload.InitialHourValue;

        if (payload.AdditionalHourlyValue != null)
            AdditionalHourlyValue = payload.AdditionalHourlyValue;
    }
}