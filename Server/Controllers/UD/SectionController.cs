using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OCTOBER.EF.Data;
using OCTOBER.EF.Models;
using OCTOBER.Server.Controllers.Base;
using OCTOBER.Shared.DTO;
using static System.Collections.Specialized.BitVector32;
using Section = OCTOBER.EF.Models.Section;

namespace OCTOBER.Server.Controllers.UD
{
    [Route("api/[controller]")]
    [ApiController]

    public class SectionController : BaseController, GenericRestController<SectionDTO>
    {
        public SectionController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
        : base(context, httpContextAccessor)
        {
        }

        [HttpDelete]
        [Route("Delete/{SchoolID}/{SectionID}")]

        public async Task<IActionResult> Delete(int SchoolID, int SectionID)
        {
            Debugger.Launch();
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Sections.Where(x => x.SchoolId == SchoolID).Where(x => x.SectionId == SectionID).FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.Sections.Remove(itm);
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

        public Task<IActionResult> Delete(int KeyVal)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            Debugger.Launch();
            try
            {
                await _context.Database.BeginTransactionAsync();

                var result = await _context.Sections.Select(sp => new SectionDTO
                {
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    SchoolId = sp.SchoolId,
                     SectionId=sp.SectionId,
                      Capacity=sp.Capacity,
                       CourseNo=sp.CourseNo,
                        InstructorId=sp.InstructorId,
                         Location=sp.Location,
                          SectionNo=sp.SectionNo,
                           StartDateTime=sp.StartDateTime
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
        [Route("Get/{SchoolID}/{SectionID}")]
        public async Task<IActionResult> Get(int SchoolID, int SectionID)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                SectionDTO? result = await _context.Sections
                    .Where(x => x.SchoolId == SchoolID)
                    .Where(x => x.SectionId == SectionID)
                    .Select(sp => new SectionDTO
                    {
                        CreatedBy = sp.CreatedBy,
                        CreatedDate = sp.CreatedDate,
                        ModifiedBy = sp.ModifiedBy,
                        ModifiedDate = sp.ModifiedDate,
                        SchoolId = sp.SchoolId,
                        SectionId = sp.SectionId,
                        Capacity = sp.Capacity,
                        CourseNo = sp.CourseNo,
                        InstructorId = sp.InstructorId,
                        Location = sp.Location,
                        SectionNo = sp.SectionNo,
                        StartDateTime = sp.StartDateTime
                    })
                .SingleAsync();
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

        [HttpPost]
        [Route("Post")]

        public async Task<IActionResult> Post([FromBody] SectionDTO _SectionDTO)
        {
            try 
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Sections
                    .Where(x => x.SchoolId == _SectionDTO.SchoolId)
                    .Where(x => x.SectionId == _SectionDTO.SectionId)
                    .FirstOrDefaultAsync();

                if (itm == null)
                {
                    Section s = new Section
                    {

                        SchoolId = _SectionDTO.SchoolId,
                        SectionId = _SectionDTO.SectionId,
                        Capacity = _SectionDTO.Capacity,
                        CourseNo = _SectionDTO.CourseNo,
                        InstructorId = _SectionDTO.InstructorId,
                        Location = _SectionDTO.Location,
                        SectionNo = _SectionDTO.SectionNo,
                        StartDateTime = _SectionDTO.StartDateTime
                    };
                    _context.Sections.Add(s);
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
        public async Task<IActionResult> Put([FromBody] SectionDTO _SectionDTO)
        {

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Sections
                    .Where(x => x.SchoolId == _SectionDTO.SchoolId)
                    .Where(x => x.SectionId == _SectionDTO.SectionId)
                    .FirstOrDefaultAsync();

                itm.SectionId = _SectionDTO.SectionId;

                _context.Sections.Update(itm);
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
}

