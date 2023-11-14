
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
    public class EnrollmentController : BaseController, GenericRestController<EnrollmentDTO>
    {
        public EnrollmentController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
        : base(context, httpContextAccessor)
        {
        }

        [HttpDelete]
        [Route("Delete/{StudentID}/{SchoolID}/{SectionID}")]

        public async Task<IActionResult> Delete(int StudentID, int SchoolID, int SectionID)
        {
            Debugger.Launch();
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Enrollments
                    .Where(x => x.StudentId == StudentID)
                    .Where(x => x.SchoolId == SchoolID)
                    .Where(x => x.SectionId == SectionID)
                    .FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.Enrollments.Remove(itm);
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

                var result = await _context.Enrollments.Select(sp => new EnrollmentDTO
                {
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    SchoolId = sp.SchoolId,
                    SectionId = sp.SectionId,
                     StudentId=sp.StudentId,
                      EnrollDate=sp.EnrollDate,
                       FinalGrade=sp.FinalGrade
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
        [Route("Get/{StudentID}/{SchoolID}/{SectionID}")]
        public async Task<IActionResult> Get(int StudentID, int SchoolID, int SectionID)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                EnrollmentDTO? result = await _context.Enrollments
                    .Where(x => x.StudentId == StudentID)
                    .Where(x => x.SchoolId == SchoolID)
                    .Where(x => x.SectionId == SectionID)
                    .Select(sp => new EnrollmentDTO
                    {
                        CreatedBy = sp.CreatedBy,
                        CreatedDate = sp.CreatedDate,
                        ModifiedBy = sp.ModifiedBy,
                        ModifiedDate = sp.ModifiedDate,
                        SchoolId = sp.SchoolId,
                        SectionId = sp.SectionId,
                        StudentId = sp.StudentId,
                        EnrollDate = sp.EnrollDate,
                        FinalGrade = sp.FinalGrade
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

        public async Task<IActionResult> Post([FromBody] EnrollmentDTO _EnrollmentDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Enrollments
                    .Where(x => x.StudentId == _EnrollmentDTO.StudentId)
                    .Where(x => x.SchoolId == _EnrollmentDTO.SchoolId)
                    .Where(x => x.SectionId == _EnrollmentDTO.SectionId)
                    .FirstOrDefaultAsync();

                if (itm == null)
                {
                    Enrollment s = new Enrollment
                    {

                        SchoolId = _EnrollmentDTO.SchoolId,
                        SectionId = _EnrollmentDTO.SectionId,
                        StudentId = _EnrollmentDTO.StudentId,
                        EnrollDate = _EnrollmentDTO.EnrollDate,
                        FinalGrade = _EnrollmentDTO.FinalGrade
                    };
                    _context.Enrollments.Add(s);
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
        public async Task<IActionResult> Put([FromBody] EnrollmentDTO _EnrollmentDTO)
        {

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Enrollments
                    .Where(x => x.StudentId == _EnrollmentDTO.StudentId)
                    .Where(x => x.SchoolId == _EnrollmentDTO.SchoolId)
                    .Where(x => x.SectionId == _EnrollmentDTO.SectionId)
                    .FirstOrDefaultAsync();

                itm.StudentId = _EnrollmentDTO.StudentId;

                _context.Enrollments.Update(itm);
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

