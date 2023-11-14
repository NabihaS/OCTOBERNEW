using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OCTOBER.EF.Data;
using OCTOBER.EF.Models;
using OCTOBER.Shared;
using Telerik.DataSource;
using Telerik.DataSource.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Linq.Dynamic.Core;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;
using AutoMapper;
using OCTOBER.Server.Controllers.Base;
using OCTOBER.Shared.DTO;

namespace OCTOBER.Server.Controllers.UD
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeTypeController : BaseController, GenericRestController<GradeTypeDTO>
    {
        public GradeTypeController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
        : base(context, httpContextAccessor)
        {
        }

        [HttpDelete]
        [Route("Delete/{SchoolID}")]

        public async Task<IActionResult> Delete(int SchoolID)
        {
            Debugger.Launch();
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeTypes.Where(x => x.SchoolId == SchoolID).WhereFirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.GradeTypes.Remove(itm);
                }
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }


        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var result = await _context.GradeTypes.Select(sp => new GradeTypeDTO
                {
                    SchoolId=sp.SchoolId,
                    GradeTypeCode= sp.GradeTypeCode,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    Description = sp.Description,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,

                })
                .ToListAsync();
                await _context.Database.RollbackTransactionAsync();
                return Ok(result);
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        [HttpGet]
        [Route("Get/{SchoolID}/{GradeTypeCode}")]
        public async Task<IActionResult> Get(int SchoolID, string GradeTypeCode)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                GradeTypeDTO? result = await _context
                    .GradeTypes
                    .Where(x => x.GradeTypeCode == GradeTypeCode)
                    .Where(x => x.SchoolId == SchoolID)
                     .Select(sp => new GradeTypeDTO
                     {
                         GradeTypeCode = sp.GradeTypeCode,
                         SchoolId=sp.SchoolId,
                         CreatedBy = sp.CreatedBy,
                         CreatedDate = sp.CreatedDate,
                         Description = sp.Description,
                         ModifiedBy = sp.ModifiedBy,
                         ModifiedDate = sp.ModifiedDate,
                     })
                .SingleOrDefaultAsync();

                await _context.Database.RollbackTransactionAsync();
                return Ok(result);
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        public Task<IActionResult> Get(int KeyVal)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Post([FromBody] GradeTypeDTO _T)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Put([FromBody] GradeTypeDTO _T)
        {
            throw new NotImplementedException();
        }
    }


    [HttpPost]
    [Route("Post")]
    public async Task<IActionResult> Post([FromBody]
                                                GradeTypeDTO _GradeTypeDTO)
    {
        try
        {
            await _context.Database.BeginTransactionAsync();

            var itm = await _context.GradeTypes.Where(x => x.SchoolId == _GradeTypeDTO.SchoolId).FirstOrDefaultAsync();

            if (itm == null)
            {
                GradeType c = new GradeType
                {
                    Description = _GradeTypeDTO.Description,
                    SchoolId = _GradeTypeDTO.SchoolId,
                    GradeTypeCode= _GradeTypeDTO.GradeTypeCode
                };
                _context.GradeTypes.Add(c);
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();
            }
            return Ok();
        }
        catch (Exception Dex)
        {
            await _context.Database.RollbackTransactionAsync();
            //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
            return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
        }
    }

    [HttpPut]
    [Route("Put")]
    public async Task<IActionResult> Put([FromBody]
                                                GradeTypeDTO _GradeTypeDTO)
    {

        Debugger.Launch();

        try
        {
            await _context.Database.BeginTransactionAsync();

            var itm = await _context.GradeTypes.Where(x => x.CourseNo == _GradeTypeDTO.SchoolId).FirstOrDefaultAsync();

            itm.Description = _GradeTypeDTO.Description;
            itm.SchoolId = _GradeTypeDTO.SchoolId;
            itm.GradeTypeCode = _GradeTypeDTO.GradeTypeCode;

            _context.GradeTypes.Update(itm);
            await _context.SaveChangesAsync();
            await _context.Database.CommitTransactionAsync();

            return Ok();
        }
        catch (Exception Dex)
        {
            await _context.Database.RollbackTransactionAsync();
            //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
            return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
        }
    }

}

