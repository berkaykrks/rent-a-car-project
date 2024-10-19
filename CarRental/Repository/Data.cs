using CarRental.Models;
using System.Data.OleDb;
using System.Runtime.Versioning;
using Microsoft.AspNetCore.Hosting;
using System.Data.SqlClient;


namespace CarRental.Repository
{
	public class Data : IData
	{
		private readonly IConfiguration configuration;
		private readonly string dbcon = "";
		private readonly IWebHostEnvironment webhost;

		public Data(IConfiguration configuration, IWebHostEnvironment webhost)
		{
			this.configuration = configuration;
			dbcon = this.configuration.GetConnectionString("dbConnection");
			this.webhost = webhost;
		}

		[SupportedOSPlatform("windows")]
		public List<Rent> GetAllRents()
		{
			List<Rent> rents = new List<Rent>();
			string qry = "Select * from Rents;";

			using (SqlConnection con = new SqlConnection(dbcon))
			{
				try
				{
					con.Open();

					using (SqlCommand cmd = new SqlCommand(qry, con))
					{
						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							while (reader.Read())
							{
								Rent rent = new Rent
								{
									Id = int.Parse(reader["ID"].ToString()),
									PickUp = reader["PickUp"].ToString(),
									DropOff = reader["DropOff"].ToString(),
									PickUpDate = Convert.ToDateTime(reader["PickUpDate"].ToString()),
									DropOffDate = Convert.ToDateTime(reader["DropOffDate"].ToString()),
									TotalRun = int.Parse(reader["TotalRun"].ToString()),
									Rate = int.Parse(reader["Rate"].ToString()),
									TotalAmount = int.Parse(reader["TotalAmount"].ToString()),
									Brand = reader["Brand"].ToString(),
									Model = reader["Model"].ToString(),
									DriverId = int.Parse(reader["DriverId"].ToString()),
									CustomerName = reader["CustomerName"].ToString(),
									CustomerContact = reader["CustomerContactNo"].ToString(),
								}; 

								rents.Add(rent);
							}
						}
					}
				}
				catch (Exception ex)
				{
					// Hata loglama yapılabilir veya kullanıcıya uygun mesaj verilebilir
					throw new Exception("Veritabanından veri çekilirken bir hata oluştu.", ex);
				}
			}

			return rents;
		}

		[SupportedOSPlatform("windows")]
		public List<string> GetModel(string brand) 
		{
			List<string> model = new List<string>();
			string qry = "Select distinct Model from Cars where Brand= '"+brand+"'";

			using (SqlConnection con = new SqlConnection(dbcon))
			{
				try
				{
					con.Open();
					using (SqlCommand cmd = new SqlCommand(qry, con))
					{
						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							while (reader.Read())
							{
								// Brand değerini al ve listeye ekle
								model.Add(reader["Model"].ToString());
							}
						}
					}
				}
				catch (Exception ex)
				{
					// Hata loglama yapılabilir veya kullanıcıya uygun mesaj verilebilir
					throw new Exception("Veritabanından veri çekilirken bir hata oluştu.", ex);
				}
				finally
				{
					con.Close();
				}
			}
			return model;
		}
		[SupportedOSPlatform("windows")]
		public List<string> GetBrand()
		{
			List<string> brand = new List<string>();
			string qry = "Select distinct Brand from Cars";

			using (SqlConnection con = new SqlConnection(dbcon))
			{
				try
				{
					con.Open();
					using (SqlCommand cmd = new SqlCommand(qry, con))
					{
						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							while (reader.Read())
							{
								// Brand değerini al ve listeye ekle
								brand.Add(reader["Brand"].ToString());
							}
						}
					}
				}
				catch (Exception ex)
				{
					// Hata loglama yapılabilir veya kullanıcıya uygun mesaj verilebilir
					throw new Exception("Veritabanından veri çekilirken bir hata oluştu.", ex);
				}
				finally
				{
					con.Close();
				}
			}

			return brand;

		}
		[SupportedOSPlatform("windows")]
		public bool BookingNow(Rent rent) 
		{
			bool isSaved = false;
			using (SqlConnection con = new SqlConnection(dbcon)) // SqlConnection kullan
			{
				try
				{
					con.Open();
					rent.TotalAmount = rent.TotalRun * rent.Rate;

					string qry = "INSERT INTO Rents (PickUp, DropOff, PickUpDate,DropOffDate,TotalRun,Rate,TotalAmount,Brand, Model, DriverId,CustomerName,CustomerContactNo) " +
								 "VALUES (@PickUp, @DropOff, @PickUpDate, @DropOffDate, @TotalRun, @Rate, @TotalAmount, @Brand, @Model, @DriverId, @CustomerName, @CustomerContactNo)";

					using (SqlCommand cmd = new SqlCommand(qry, con))
					{
						// Parametreleri ekleyin
						cmd.Parameters.AddWithValue("@PickUp", rent.PickUp);
						cmd.Parameters.AddWithValue("@DropOff", rent.DropOff);
						cmd.Parameters.AddWithValue("@PickUpDate", rent.PickUpDate);
						cmd.Parameters.AddWithValue("@DropOffDate", rent.DropOffDate);
						cmd.Parameters.AddWithValue("@TotalRun", rent.TotalRun);
						cmd.Parameters.AddWithValue("@Rate", rent.Rate);
						cmd.Parameters.AddWithValue("@TotalAmount", rent.TotalAmount);
						cmd.Parameters.AddWithValue("@Brand", rent.Brand);
						cmd.Parameters.AddWithValue("@Model", rent.Model);
						cmd.Parameters.AddWithValue("@DriverId", rent.DriverId);
						cmd.Parameters.AddWithValue("@CustomerName", rent.CustomerName);
						cmd.Parameters.AddWithValue("@CustomerContactNo", rent.CustomerContact);

						cmd.ExecuteNonQuery();
						isSaved = true;
					}
				}
				catch (Exception ex)
				{
					// Hata günlüğü kaydedebilir veya özel bir hata işleme yapabilirsiniz
					throw;
				}
				finally
				{
					con.Close();
				}
			}
			return isSaved;
		}

		[SupportedOSPlatform("windows")]
		public List<Driver> GetAllDrivers()
		{
			List<Driver> drivers = new List<Driver>();
			string qry = "Select * from Drivers";

			using (SqlConnection con = new SqlConnection(dbcon))
			{
				try
				{
					con.Open();

					using (SqlCommand cmd = new SqlCommand(qry, con))
					{
						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							while (reader.Read())
							{
								Driver drive = new Driver
								{
									Id = int.Parse(reader["ID"].ToString()),
									DriverName = reader["DriverName"].ToString(),
									Address = reader["Address"].ToString(),
									MobileNo = reader["MobileNo"].ToString(),
									Age = int.Parse(reader["Age"].ToString()),
									Experience = int.Parse(reader["Experience"].ToString()),
									ImagePath = reader["ImagePath"].ToString(),
									
								};

								drivers.Add(drive);
							}
						}
					}
				}
				catch (Exception ex)
				{
					// Hata loglama yapılabilir veya kullanıcıya uygun mesaj verilebilir
					throw new Exception("Veritabanından veri çekilirken bir hata oluştu.", ex);
				}
			}

			return drivers;

		}

		[SupportedOSPlatform("windows")]
		public bool AddDriver(Driver newdriver)
		{
			bool isSaved = false;
			using (SqlConnection con = new SqlConnection(dbcon)) // SqlConnection kullan
			{
				try
				{
					con.Open();
					newdriver.ImagePath = SaveImage(newdriver.DriverImage, "drivers");

					string qry = "INSERT INTO Drivers (DriverName, Address, MobileNo,Age,Experience,ImagePath) " +
								 "VALUES (@DriverName, @Address, @MobileNo, @Age, @Experience, @ImagePath)";

					using (SqlCommand cmd = new SqlCommand(qry, con))
					{
						// Parametreleri ekleyin
						cmd.Parameters.AddWithValue("@DriverName", newdriver.DriverName);
						cmd.Parameters.AddWithValue("@Address", newdriver.Address);
						cmd.Parameters.AddWithValue("@MobileNo", newdriver.MobileNo);
						cmd.Parameters.AddWithValue("@Age", newdriver.Age);
						cmd.Parameters.AddWithValue("@Experience", newdriver.Experience);						
						cmd.Parameters.AddWithValue("@ImagePath", newdriver.ImagePath);						

						cmd.ExecuteNonQuery();
						isSaved = true;
					}
				}
				catch (Exception ex)
				{
					// Hata günlüğü kaydedebilir veya özel bir hata işleme yapabilirsiniz
					throw;
				}
				
			}
			return isSaved;
		}

		[SupportedOSPlatform("windows")]
		public List<Car> GetAllCars() {
            List<Car> cars = new List<Car>();
            string qry = "Select * from Cars";

            using (SqlConnection con = new SqlConnection(dbcon))
            {
                try
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(qry, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Car car = new Car
                                {
                                    Id = int.Parse(reader["Id"].ToString()),
                                    Brand = reader["Brand"].ToString(),
                                    Model = reader["Model"].ToString(),
                                    PassingYear = int.Parse(reader["PassingYear"].ToString()),
                                    Engine = reader["Engine"].ToString(),
                                    FuelType = reader["FuelType"].ToString(),
                                    ImagePath = reader["ImagePath"].ToString(),
                                    CarNumber = reader["CarNumber"].ToString(),
                                    SeatingCapacity = int.Parse(reader["SeatingCapacity"].ToString())
                                };

                                cars.Add(car);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Hata loglama yapılabilir veya kullanıcıya uygun mesaj verilebilir
                    throw new Exception("Veritabanından veri çekilirken bir hata oluştu.", ex);
                }
            }

            return cars;

        }
        [SupportedOSPlatform("windows")]

		private SqlDataReader GetConnection(string qry, SqlConnection con) 
		{
			SqlDataReader reader = null;
			try
			{
				SqlCommand cmd = new SqlCommand(qry, con);
				reader= cmd.ExecuteReader();
			}
			catch (Exception)
			{

				throw;
			}
			return reader;
		}
		[SupportedOSPlatform("windows")]
		public bool AddNewCar(Car newcar)
		{
			bool isSaved = false;
			using (SqlConnection con = new SqlConnection(dbcon)) // SqlConnection kullan
			{
				try
				{
					con.Open();
					newcar.ImagePath = SaveImage(newcar.CarImage, "cars");

					string qry = "INSERT INTO Cars (Brand, Model, PassingYear, CarNumber, Engine, FuelType, ImagePath, SeatingCapacity) " +
								 "VALUES (@Brand, @Model, @PassingYear, @CarNumber, @Engine, @FuelType, @ImagePath, @SeatingCapacity)";

					using (SqlCommand cmd = new SqlCommand(qry, con))
					{
						// Parametreleri ekleyin
						cmd.Parameters.AddWithValue("@Brand", newcar.Brand);
						cmd.Parameters.AddWithValue("@Model", newcar.Model);
						cmd.Parameters.AddWithValue("@PassingYear", newcar.PassingYear);
						cmd.Parameters.AddWithValue("@CarNumber", newcar.CarNumber);
						cmd.Parameters.AddWithValue("@Engine", newcar.Engine);
						cmd.Parameters.AddWithValue("@FuelType", newcar.FuelType);
						cmd.Parameters.AddWithValue("@ImagePath", newcar.ImagePath);
						cmd.Parameters.AddWithValue("@SeatingCapacity", newcar.SeatingCapacity);

						cmd.ExecuteNonQuery();
						isSaved = true;
					}
				}
				catch (Exception ex)
				{
					// Hata günlüğü kaydedebilir veya özel bir hata işleme yapabilirsiniz
					throw;
				}
				finally
				{
					con.Close();
				}
			}
			return isSaved;
		}

		private string SaveImage(IFormFile file, string folderName)
		{
			string imagepath = "";
			try
			{
				string uploadfolder = Path.Combine(webhost.WebRootPath, "assets/" + folderName);
				imagepath = Guid.NewGuid().ToString() + "_" + file.FileName;
				string filepath = Path.Combine(uploadfolder, imagepath);
				using (var filestream = new FileStream(filepath, FileMode.Create))
				{
					file.CopyTo(filestream);
				}
			}
			catch (Exception ex)
			{
				// Hata günlüğü kaydedebilir veya özel bir hata işleme yapabilirsiniz
				throw;
			}
			return imagepath;
		}
	}
}
