namespace ParkingControl.ParkingRegistration;

public class ParkingRegistration
{
    public Guid Id { get; init; }
    public DateTime CheckInDate { get; private set; }
    public DateTime? CheckOutDate { get; private set; }
    public string Plate { get; private set; }
    public int DurationInSeconds { get; private set; }
    public int TimeChargedHour { get; private set; }
    public decimal PriceToPay { get; private set; }
    public decimal Price { get; private set; }


    public ParkingRegistration(DateTime checkInDate, DateTime? checkOutDate, string plate)
    {
        Id = Guid.NewGuid();
        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;
        Plate = plate;
    }

    public void UpdateCheckout(DateTime checkOutDate)
    {
        CheckOutDate = checkOutDate;
        UpdateDuration();
        UpdateTimeChargedHour();
    }

    public void UpdatePrice(decimal initialHourValue)
    {
        Price = initialHourValue;
    }

    private void UpdateDuration()
    {
        var difference = CheckOutDate - CheckInDate;
        var seconds = difference.GetValueOrDefault().TotalSeconds;
        DurationInSeconds = (int)seconds;
    }

    private void UpdateTimeChargedHour()
    {
        var difference = CheckOutDate - CheckInDate;
        var totalHours = (int)difference.GetValueOrDefault().TotalHours;
        var minutesDifference = difference.GetValueOrDefault().Minutes;

        if (DurationInSeconds > 0 && totalHours < 1)
        {
            TimeChargedHour = 1;
        }
        else if (minutesDifference > 10)
        {
            TimeChargedHour = totalHours + 1;
        }
        else
        {
            TimeChargedHour = totalHours;
        }
    }
    
    public void CalculatePriceToPay(decimal additionalHourlyValue, decimal initialHourValue)
    {
        var thirtyMinutes = 30 * 60;

        if (DurationInSeconds <= thirtyMinutes)
        {
            PriceToPay = initialHourValue / 2;
        }
        else
        {
            var durationInHour = DurationInSeconds / 3600;
            var resultPrice = additionalHourlyValue * (durationInHour - 1) + initialHourValue;

            var durationInMinutes = DurationInSeconds / 60;

            var resultTime = durationInMinutes - durationInHour * 60;


            if (resultTime > 10)
            {
                PriceToPay = additionalHourlyValue * durationInHour + initialHourValue;
            }
            else
            {
                PriceToPay = resultPrice;
            }
        }
    }
}