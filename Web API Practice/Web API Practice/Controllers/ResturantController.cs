using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Web_API_Practice.DataAccess;
using Web_API_Practice.Enums;
using Web_API_Practice.Interfaces.Controllers;
using Web_API_Practice.Interfaces.Models;
using Web_API_Practice.Models;

namespace Web_API_Practice.Controllers
{
    [Route("api/Resturant")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public class ResturantController : ControllerBase, IResturantController
    {
        private readonly ILogger _logger;

        public ResturantController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("Web_API_Practice.Controllers.ResturantController");
        }

        [Route("AddResturant/{resturantName}/{unitNumber}/{buildingNumber}/{streetName}/{suburbName}/{cityName}/{stateName}/{countryName}/{postcode}/{ownerName}/{description}/{resturantType}/{image?}")]
        public bool AddResturant([FromRoute] string resturantName, [FromRoute] string unitNumber, [FromRoute] string buildingNumber,
            [FromRoute] string streetName, [FromRoute] string suburbName, [FromRoute] string cityName, [FromRoute] string stateName,
            [FromRoute] string countryName, [FromRoute] string postcode, [FromRoute] string ownerName, [FromRoute] string description,
            [FromRoute] int resturantType, [FromRoute] byte[] image = null)
        {
            try
            {
                // If a whitespace is given for the address then it was meant to be null
                if (string.IsNullOrWhiteSpace(unitNumber))
                {
                    unitNumber = null;
                }
                FormalAddress resturantAddress = new FormalAddress()
                {
                    AddressApartmentNumber = unitNumber,
                    AddressBuildingNumber = buildingNumber,
                    AddressStreetName = streetName,
                    AddressSuburbName = suburbName,
                    AddressCityName = cityName,
                    AddressStateName = stateName,
                    AddressCountryName = countryName,
                    AddressPostcode = postcode
                };
                Resturant resturant = new Resturant()
                {
                    ResturantName = resturantName,
                    ResturantAddress = resturantAddress,
                    ResturantImage = image,
                    ResturantOwnerName = ownerName,
                    ResturantDescription = description,
                    ResturantType = (ResturantType)resturantType,
                    DateAdded = DateTime.Now
                };

                using (var _dbContext = new ResturantReviewDBContext())
                {
                    _dbContext.Add(resturant);
                    _dbContext.SaveChanges();
                }

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        [Route("DeleteResturant/{resturantId}")]
        public bool DeleteResturant([FromRoute] int resturantId)
        {
            IResturant resturant;
            try
            {
                using (var _dbContext = new ResturantReviewDBContext())
                {
                    resturant = _dbContext.Resturants.Where(x => x.ResturantId == resturantId).FirstOrDefault();
                    if(resturant != null)
                    {
                        _dbContext.Entry(resturant).State = EntityState.Detached;
                        _dbContext.Resturants.Remove(Resturant.ConvertToEntity(resturant));
                        _dbContext.SaveChanges();
                    }
                }
                return true;
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        [Route("GetResturants/{resultLimit?}")]
        public List<IResturant> GetResturants([FromRoute] int resultLimit = 30)
        {
            try
            {
                List<IResturant> resturants = new List<IResturant>();

                using (var _dbContext = new ResturantReviewDBContext())
                {
                    resturants = _dbContext.Resturants.Take(resultLimit).ToList().ConvertAll(x => (IResturant)x);
                }

                return resturants;
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        [Route("SearchResturants/{resturantName}/{resultLimit?}")]
        public List<IResturant> SearchResturant([FromRoute] string resturantName, [FromRoute] int resultLimit = 30)
        {
            List<IResturant> searchResults = new List<IResturant>();
            try
            {
                using (var _dbContext = new ResturantReviewDBContext())
                {
                    searchResults.AddRange(_dbContext.Resturants.Where(x => x.ResturantName.Contains(resturantName)).Take(resultLimit));
                }
                return searchResults;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        [Route("UpdateResturantDetails/{resturant}")]
        public bool UpdateResturantDetails([FromRoute] dynamic resturantJson)
        {
            IResturant resturant = null;
            try
            {
                resturant = JsonConvert.DeserializeObject<Resturant>(resturantJson);
                using (var _dbContext = new ResturantReviewDBContext())
                {
                    if(resturant != null)
                    {
                        _dbContext.Resturants.Update(Resturant.ConvertToEntity(resturant));
                        _dbContext.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }
    }
}