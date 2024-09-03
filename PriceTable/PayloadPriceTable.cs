namespace ParkingControl.PriceTable;

public record PayloadPriceTable(
    DateTime ValidityStartPeriod,
    DateTime ValidityFinalPeriod,
    decimal InitialHourValue,
    decimal AdditionalHourlyValue);