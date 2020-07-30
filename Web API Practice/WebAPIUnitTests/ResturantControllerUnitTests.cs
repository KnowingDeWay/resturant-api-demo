using System.Linq;
using Xunit;
using Moq;
using Web_API_Practice.Interfaces.Controllers;
using Web_API_Practice.Interfaces.Models;
using Web_API_Practice.DataAccess;
using Web_API_Practice.Models;
using Web_API_Practice.Controllers;
using Microsoft.Extensions.Logging;
using System.Text;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace WebAPIUnitTests
{
    public class ResturantControllerUnitTests
    {
        private readonly IResturantController _resturantController;
        private readonly ILoggerFactory _loggerFactory;
        private readonly Random _random;

        public ResturantControllerUnitTests()
        {
            _loggerFactory = new Mock<ILoggerFactory>().Object;
            _resturantController = new ResturantController(_loggerFactory);
            _random = new Random();
        }

        [Fact]
        public void AddResturant()
        {
            bool result = false;
            IResturant resturantModel = null;
            try
            {
                var resturant = new Mock<IResturant>();
                var address = new FormalAddress();
                resturant.SetupAllProperties();
                resturant.SetupProperty(x => x.ResturantAddress, address);
                resturantModel = resturant.Object;
                var addressModel = resturant.Object.ResturantAddress;
                _resturantController.AddResturant(resturantModel.ResturantName, addressModel.AddressApartmentNumber,
                    addressModel.AddressBuildingNumber, addressModel.AddressStreetName, addressModel.AddressSuburbName,
                    addressModel.AddressCityName, addressModel.AddressStateName, addressModel.AddressCountryName, addressModel.AddressPostcode,
                    resturantModel.ResturantOwnerName, resturantModel.ResturantDescription, (int)resturantModel.ResturantType, null);

                Resturant dbResturantModel;
                using (var dbContext = new ResturantReviewDBContext())
                {
                    dbResturantModel = dbContext.Resturants.Where(x => x.DuplicateEquals(resturantModel)).FirstOrDefault();
                    result = dbResturantModel != null;
                    dbContext.Resturants.Remove(dbResturantModel);
                    dbContext.SaveChanges();
                    // Ensures test object is removed
                    if (!result && resturantModel != null)
                    {
                        dbContext.Resturants.Remove(Resturant.ConvertToEntity(resturantModel));
                        dbContext.SaveChanges();
                    }
                }
            }
            catch(Exception)
            {
                using (var dbContext = new ResturantReviewDBContext())
                {
                    dbContext.Resturants.Remove(Resturant.ConvertToEntity(resturantModel));
                    dbContext.SaveChanges();
                }
            }

            Assert.True(result);
        }
        
        [Fact]
        public void ListResturants()
        {
            int recordCount = -1;

            using (var dbContext = new ResturantReviewDBContext())
            {
                recordCount = dbContext.Resturants.Count();
            }
            bool result = _resturantController.GetResturants(30).Count == recordCount;

            Assert.True(result);
        }

        [Fact]
        public void SearchForResturant()
        {
            bool result = false;
            IResturant resturantModel = null;
            try
            {
                int size = _random.Next(0, 30);
                string desiredName = GenerateResturantName(size);
                resturantModel = SetupAddedResturant(desiredName);
                var searchResults = _resturantController.SearchResturant(resturantModel.ResturantName);
                result = CheckSearchResultsList(resturantModel.ResturantName, searchResults);
                using (var dbContext = new ResturantReviewDBContext())
                {
                    dbContext.Resturants.Remove(Resturant.ConvertToEntity(resturantModel));
                    dbContext.SaveChanges();
                }
            }
            catch(Exception)
            {
                using (var dbContext = new ResturantReviewDBContext())
                {
                    dbContext.Resturants.Remove(Resturant.ConvertToEntity(resturantModel));
                    dbContext.SaveChanges();
                }
            }

            Assert.True(result);
        }

        [Fact]
        public void UpdateResturantDetails()
        {
            bool result = false;
            IResturant resturantModel = null;
            try
            {
                IResturant updatedResturant = null;
                resturantModel = SetupAddedResturant();
                int size = _random.Next(0, 30);
                string name = GenerateResturantName(size);
                resturantModel.ResturantName = name;
                _resturantController.UpdateResturantDetails(JsonConvert.SerializeObject(resturantModel));
                using (var dbContext = new ResturantReviewDBContext())
                {
                    updatedResturant = dbContext.Resturants.Where(x => x.ResturantName.Equals(name)).FirstOrDefault();
                    result = updatedResturant != null;
                    dbContext.Resturants.Remove(Resturant.ConvertToEntity(resturantModel));
                    dbContext.SaveChanges();
                }
            }
            catch(Exception)
            {
                using (var dbContext = new ResturantReviewDBContext())
                {
                    dbContext.Resturants.Remove(Resturant.ConvertToEntity(resturantModel));
                    dbContext.SaveChanges();
                }
            }

            Assert.True(result);
        }

        [Fact]
        public void DeleteResturant()
        {
            bool result = false;
            IResturant resturantModel = null;
            try
            {
                Resturant deletedResturant = null;
                resturantModel = SetupAddedResturant("testModel");
                using (var _dbContext = new ResturantReviewDBContext())
                {
                    _resturantController.DeleteResturant(resturantModel.ResturantId);
                    deletedResturant = _dbContext.Resturants.Where(x => x.ResturantName.Equals("testModel")).FirstOrDefault();
                    result = deletedResturant == null;
                    if (!result)
                    {
                        _dbContext.Resturants.Remove(Resturant.ConvertToEntity(resturantModel));
                        _dbContext.SaveChanges();
                    }
                }
            }
            catch(Exception)
            {
                try
                {
                    using (var dbContext = new ResturantReviewDBContext())
                    {
                        dbContext.Resturants.Remove(Resturant.ConvertToEntity(resturantModel));
                        dbContext.SaveChanges();
                    }
                }
                catch(DbUpdateConcurrencyException)
                {

                }
            }

            Assert.True(result);
        }

        private string GenerateResturantName(int size)
        {
            var stringBuilder = new StringBuilder();
            for(int i = 0; i < size; i++)
            {
                var randChar = (char)_random.Next(65, 122);
                stringBuilder.Append(randChar);
            }
            return stringBuilder.ToString();
        }

        // Adds a test resturant to the database
        private IResturant SetupAddedResturant(string desiredName = "")
        {
            IResturant resturant = new Resturant();
            var address = new FormalAddress();
            IResturant resturantModel = null;
            resturant.ResturantAddress = address;
            if(!string.IsNullOrEmpty(desiredName))
            {
                resturant.ResturantName = desiredName;
            }
            using (var dbContext = new ResturantReviewDBContext())
            {
                dbContext.Resturants.Add(Resturant.ConvertToEntity(resturant));
                dbContext.SaveChanges();
                resturantModel = dbContext.Resturants.Where(x => x.ResturantName.Equals(resturant.ResturantName)).FirstOrDefault();
            }
            return resturantModel;
        }

        private bool CheckSearchResultsList(string resturantName, List<IResturant> searchResults)
        {
            foreach(var resturant in searchResults)
            {
                if(!resturant.ResturantName.Contains(resturantName))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
