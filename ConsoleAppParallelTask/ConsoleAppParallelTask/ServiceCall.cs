using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppParallelTask
{
    public class ServiceCall
    {
        static void Main(string[] args)
        {
            CallService();
            

            Console.ReadLine();
        }

        static async void CallService()
        {
            ServiceReference1.CalculatePriceRequest calculatePriceRequest = new ServiceReference1.CalculatePriceRequest()
            {
                pickupDate = "01/01/2024",
                returnDate = "02/02/2024",
                returnLocation = "Kolkata",
                vehiclePreference = "Any"
            };

            ServiceReference1.CarRentalServiceClient carRentalServiceClient = new ServiceReference1.CarRentalServiceClient();
            ServiceReference1.CalculatePriceResponse calculatePriceResponse = await carRentalServiceClient.CalculatePriceAsync(calculatePriceRequest);

            Console.WriteLine("Response: " + calculatePriceResponse.CalculatePriceResult.ToString());
        }
    }
}
