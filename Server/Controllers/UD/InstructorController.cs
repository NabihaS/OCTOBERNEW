using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OCTOBER.EF.Data;
using OCTOBER.EF.Models;
using OCTOBER.Server.Controllers.Base;
using OCTOBER.Shared.DTO;

namespace OCTOBER.Server.Controllers.UD
{
    [Route("api/[controller]")]
    [ApiController]

    public class InstructorController : BaseController, GenericRestController<InstructorDTO>
    {
        public InstructorController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
        : base(context, httpContextAccessor)
        {
        }

        [HttpDelete]
        [Route("Delete/{SchoolID}/{InstructorID}")]

        public async Task<IActionResult> Delete(int SchoolID, int InstructorID)
        {
            Debugger.Launch();
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Instructors
                    .Where(x => x.SchoolId == SchoolID)
                    .Where(x => x.InstructorId == InstructorID)
                    .FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.Instructors.Remove(itm);
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

                var result = await _context.Instructors.Select(sp => new InstructorDTO
                {
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    SchoolId = sp.SchoolId,
                    InstructorId=sp.InstructorId,
                    FirstName = sp.FirstName,
                    LastName = sp.LastName,
                    Phone = sp.Phone,
                    Salutation = sp.Salutation,
                    StreetAddress = sp.StreetAddress,
                    Zip = sp.Zip

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
        [Route("Get/{SchoolID}/{InstructorID}")]
        public async Task<IActionResult> Get(int SchoolID, int InstructorID)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                InstructorDTO? result = await _context.Instructors
                    .Where(x => x.SchoolId == SchoolID)
                    .Where(x => x.InstructorId == InstructorID)
                    .Select(sp => new InstructorDTO
                    {
                        CreatedBy = sp.CreatedBy,
                        CreatedDate = sp.CreatedDate,
                        ModifiedBy = sp.ModifiedBy,
                        ModifiedDate = sp.ModifiedDate,
                        SchoolId = sp.SchoolId,
                        InstructorId = sp.InstructorId,
                        FirstName = sp.FirstName,
                        LastName = sp.LastName,
                        Phone = sp.Phone,
                        Salutation = sp.Salutation,
                        StreetAddress = sp.StreetAddress,
                        Zip = sp.Zip
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

        public async Task<IActionResult> Post([FromBody] InstructorDTO _InstructorDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Instructors.Where(x => x.InstructorId == _InstructorDTO.InstructorId).FirstOrDefaultAsync();

                if (itm == null)
                {
                    Instructor s = new Instructor
                    {

                        SchoolId = _InstructorDTO.SchoolId,
                        InstructorId = _InstructorDTO.InstructorId
                    };
                    _context.Instructors.Add(s);
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
        public async Task<IActionResult> Put([FromBody] InstructorDTO _InstructorDTO)
        {

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Instructors.Where(x => x.InstructorId == _InstructorDTO.InstructorId).FirstOrDefaultAsync();

                itm.InstructorId = _InstructorDTO.InstructorId;

                _context.Instructors.Update(itm);
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

