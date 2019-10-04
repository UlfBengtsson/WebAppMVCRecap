using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppMVCRecap.Models
{
    public class CarsRepository : ICarsRepository
    {
        private readonly AppDbContext _dbContext;

        public CarsRepository(AppDbContext appDbContext)
        {
            this._dbContext = appDbContext;
        }

        public List<Car> AllCars()
        {
            return _dbContext.Cars.ToList();
        }

        public Car Create(string brand, string name)
        {
            if (String.IsNullOrWhiteSpace(brand) || String.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            Car car = new Car() { Brand = brand, Name = name };

            _dbContext.Add(car);
            _dbContext.SaveChanges();

            return car;
        }

        public bool Delete(int id)
        {
            Car car = FindById(id);

            if (car == null)
            {
                return false;
            }

            _dbContext.Cars.Remove(car);
            _dbContext.SaveChanges();

            return true;
        }

        public Car FindById(int id)
        {
            return _dbContext.Cars.SingleOrDefault(c => c.Id == id);
        }

        public Car Update(Car car)
        {
            Car dbCar = FindById(car.Id);

            dbCar.Name = car.Name;
            dbCar.Brand = car.Brand;

            _dbContext.SaveChanges();

            return dbCar;
        }
    }
}
