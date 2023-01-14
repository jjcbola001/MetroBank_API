using Metrobank.DataAccessLibrary.DataAccess;
using Metrobank.Interface.Interfaces;
using Metrobank.Model.RequestModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetroBankExm.Controllers
{
    [EnableCors("Government Exception")]
    [Route("api/[controller]")]
    [ApiController]
    public class MetroBankController : ControllerBase
    {
        private readonly IMetrobankServices _metrobankServices;
        public MetroBankController(IMetrobankServices metrobankRepository)
        {
            _metrobankServices = metrobankRepository;
        }
        /// <summary>    
        /// Returns all the List of Informations
        /// </summary>    
        /// <returns></returns> 
        [HttpGet]
        [Route("FetchInputList")]
        public async Task<IActionResult> FetchInputList()
        {
            var result = await _metrobankServices.FetchMainInformationAsync();

            if (result.ReturnStatus)
                return Ok(result.Entity);

            return BadRequest(result.ReturnMessage);
        }

        /// <summary>    
        /// Returns the pattern according to the input number
        /// </summary>    
        /// <returns></returns> 
        [HttpPost]
        [Route("FetchOutput")]
        public async Task<IActionResult> FetchInputList(InputRequestModel inputRequestModel)
        {
            var result = await _metrobankServices.FetchOutputResultAsync(inputRequestModel);

            if (result.ReturnStatus)
                return Ok(result.Entity);

            return BadRequest(result.ReturnMessage);
        }

        /// <summary>    
        /// Add input information
        /// </summary>    
        /// <returns></returns> 
        [HttpPost]
        [Route("AddInputList")]
        public async Task<IActionResult> AddInputList(ActionInputRequestModel inputRequestModel)
        {
            var result = await _metrobankServices.AddInputInformationAsync(inputRequestModel);

            if (result.ReturnStatus)
                return Ok(result.Entity);

            return BadRequest(result.ReturnMessage);
        }

        /// <summary>    
        /// Update input information
        /// </summary>    
        /// <returns></returns> 
        [HttpPost]
        [Route("UpdateInputList")]
        public async Task<IActionResult> UpdateInputList(ActionInputRequestModel inputRequestModel)
        {
            var result = await _metrobankServices.UpdateInputInformationAsync(inputRequestModel);

            if (result.ReturnStatus)
                return Ok(result.Entity);

            return BadRequest(result.ReturnMessage);
        }

        /// <summary>    
        /// Delete input information
        /// </summary>    
        /// <returns></returns> 
        [HttpPost]
        [Route("DeleteInputList")]
        [DisableCors]
        public async Task<IActionResult> DeleteInputList(InputRequestModel inputRequestModel)
        {
            var result = await _metrobankServices.DeleteInputInformationAsync(inputRequestModel);

            if (result.ReturnStatus)
                return Ok(result.Entity);

            return BadRequest(result.ReturnMessage);
        }
    }
}
