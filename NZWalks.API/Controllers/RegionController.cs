using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        public RegionController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Hardcoded values
        [HttpGet]
        public IActionResult GetAll()
        {
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "AuckLand Region",
                    Code = "AKL",
                    RegionImageUrl = "https://unsplash.com/photos/zNN6ubHmruI",
                },
                 new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "AuckLand Region",
                    Code = "AKL",
                    RegionImageUrl = "https://unsplash.com/photos/VlH2eHyE_50",
                }

            };

            return Ok(regions);
        }

        [HttpGet("GetAllRegionsSample")]

        public IActionResult GetAllRegionsSample()
        {
            var regionSample = dbContext.Regions.ToList();
            return Ok(regionSample);
        }


        // 1. Get  Region By Id Using Find Method
        //[HttpGet("GetRegionByIdSample/{id:Guid}")]
        //public IActionResult GetRegionByIdSample([FromRoute] Guid id)
        //{
        //    var regionById = dbContext.Regions.Find(id);

        //    if(regionById == null) 
        //    {
        //        return NotFound();
        //    }

        //    return Ok(regionById);

        //}

        // 2. Get  Region By Id Using FisrtorDefault() which is Linq Method
        [HttpGet("GetRegionByIdSample/{id:Guid}")]
        public IActionResult GetRegionByIdSample([FromRoute] Guid id)
        {
            var regionById = dbContext.Regions.FirstOrDefault(regionId => regionId.Id == id);

            if (regionById == null)
            {
                return NotFound();
            }

            return Ok(regionById);

        }

        [HttpGet("GetAllRegions")]
        public async Task<ActionResult> GetAllRegions()
        {
            //Get Data From Database - Domain models
            var regionsDomain = await dbContext.Regions.ToListAsync();

            //Map Domain Models to DTOs
            var regionsDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });

            }

            //Return DTOs
            return Ok(regionsDto);
        }

        [HttpGet("GetRegionById/{id:Guid}")]

        public async Task<ActionResult> GetRegionById([FromRoute] Guid id)
        {
            //var region = await dbContext.Regions.FindAsync(id);

            //Get Region Domain Model From Database
            //By using Linq Method FirstOrDefault we can get single region
            var region = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (region == null)
            {
                return NotFound();
            }

            //Map/Convert Region Domain Model to Region DTO

            var regionDto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            //Return DTO back to Client
            return Ok(regionDto);
        }

        [HttpGet("GetAllRegionsSampleUsingDto")]
        public IActionResult GeGetAllRegionsSampleUsingDto()
        {
            var regionsDomain = dbContext.Regions.ToList();

            var regionsDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }
            return Ok(regionsDto);

        }

        [HttpGet("GetAllRegionsByIdUsingDto/{id}")]
        public IActionResult GetAllRegionsByIdUsingDto([FromRoute] Guid id)
        {
            var getRegionDomainModel = dbContext.Regions.FirstOrDefault(regionId => regionId.Id == id);

            if (getRegionDomainModel == null)
            {
                return NotFound();
            }

            var regionDto = new RegionDto
            {
                Id = getRegionDomainModel.Id,
                Code = getRegionDomainModel.Code,
                Name = getRegionDomainModel.Name,
                RegionImageUrl = getRegionDomainModel.RegionImageUrl
            };
            return Ok(regionDto);

        }


        //Post To Create New Region
        [HttpPost]
        public async Task<IActionResult> CreateNewRegion([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map or Convert DTO to Domain Model

            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            //Use Domain Model to create Region

            await dbContext.Regions.AddAsync(regionDomainModel);
            await dbContext.SaveChangesAsync();

            //Map Domain Model back to DTO

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetRegionById), new { id = regionDto.Id }, regionDto);

        }

        [HttpPost("CreateNewRegionSample")]

        public IActionResult CreateNewRegionSample([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomain = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            dbContext.Regions.Add(regionDomain);
            dbContext.SaveChanges();

            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetRegionById), new { id = regionDomain.Id }, regionDto);
        }

        [HttpPut("{id:Guid}")]
        public IActionResult UpdateRegionById([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //check if region exists
            var regionDomain = dbContext.Regions.FirstOrDefault(regionId => regionId.Id == id);

            if (regionDomain == null)
            {
                NotFound();
            }

            //Map DTO to Domain Model
            regionDomain.Code = updateRegionRequestDto.Code;
            regionDomain.Name = updateRegionRequestDto.Name;    
            regionDomain.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            dbContext.SaveChanges();

            //Convert domain model to DTO

            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return Ok(regionDto);
        }

        [HttpDelete("{id:Guid}")]
        public IActionResult DeleteRegionById([FromRoute] Guid id)
        {
            var regionDomainModel = dbContext.Regions.FirstOrDefault(regId => regId.Id == id);
            if (regionDomainModel == null)
            {
                NotFound();
            }

            //Delete Region

           dbContext.Regions.Remove(regionDomainModel);
           dbContext.SaveChanges();

            //Return Deleted Region back
            //Map Domain Model to DTO

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

             return Ok(regionDto);

        }

       
    }
}
