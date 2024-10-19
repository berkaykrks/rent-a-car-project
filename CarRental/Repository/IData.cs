using CarRental.Models;
namespace CarRental.Repository
{
    public interface IData
    {
        bool AddNewCar(Car car);
        
        List<Car> GetAllCars();

        bool AddDriver(Driver newdriver);

		List<Driver> GetAllDrivers();

        bool BookingNow(Rent rent);

        List<string> GetBrand();
        List<string> GetModel(string brand);

        List<Rent> GetAllRents();
	}
}
