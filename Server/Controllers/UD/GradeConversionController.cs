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

    public class GradeConversionController : BaseController, GenericRestController<GradeConversionDTO>
    {
        public GradeConversionController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
        : base(context, httpContextAccessor)
        {
        }

        [HttpDelete]
        [Route("Delete/{SchoolId}/{LetterGrade}")]
        public async Task<IActionResult> Delete(int SchoolId, string LetterGrade)
        {

            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeConversions
                    .Where(x => x.SchoolId == SchoolId)
                    .Where(x => x.LetterGrade == LetterGrade).FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.GradeConversions.Remove(itm);
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
            try
            {
                await _context.Database.BeginTransactionAsync();

                var result = await _context
                    .GradeConversions
                    .Select(sp => new GradeConversionDTO
                    {
                        SchoolId = sp.SchoolId,
                        LetterGrade = sp.LetterGrade,
                        MaxGrade = sp.MaxGrade,
                        MinGrade = sp.MinGrade,
                        GradePoint = sp.GradePoint,
                        CreatedBy = sp.CreatedBy,
                        CreatedDate = sp.CreatedDate,
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

        [HttpGet]
        [Route("Get/{SchoolID}/{LetterGrade}")]

        public async Task<IActionResult> Get(int SchoolId, string LetterGrade)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                GradeConversionDTO? result = await _context
                    .GradeConversions
                    .Where(x => x.SchoolId == SchoolId)
                    .Where(x => x.LetterGrade == LetterGrade)
                    .Select(sp => new GradeConversionDTO
                     {
                        SchoolId=sp.SchoolId,
                        LetterGrade=sp.LetterGrade,
                        MaxGrade=sp.MaxGrade,
                        MinGrade=sp.MinGrade,
                        GradePoint=sp.GradePoint,
                         CreatedBy = sp.CreatedBy,
                         CreatedDate = sp.CreatedDate,
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

        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> Post([FromBody]
                                                GradeConversionDTO _gradeConversionDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeConversions.Where(x => x.SchoolId == _gradeConversionDTO.SchoolId).FirstOrDefaultAsync();

                if (itm == null)
                {
                    GradeConversion c = new GradeConversion
                    {
                        SchoolId = _gradeConversionDTO.SchoolId,
                        LetterGrade = _gradeConversionDTO.LetterGrade,
                        MaxGrade = _gradeConversionDTO.MaxGrade,
                        MinGrade = _gradeConversionDTO.MinGrade,
                        GradePoint = _gradeConversionDTO.GradePoint,
                    };
                    _context.GradeConversions.Add(c);
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
                                                GradeConversionDTO _gradeConversionDTO)
        {

            Debugger.Launch();

            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeConversions.Where(x => x.SchoolId == _gradeConversionDTO.SchoolId).FirstOrDefaultAsync();

                itm.SchoolId = _gradeConversionDTO.SchoolId;
                itm.LetterGrade = _gradeConversionDTO.LetterGrade;
                itm.MaxGrade = _gradeConversionDTO.MaxGrade;
                itm.MinGrade = _gradeConversionDTO.MinGrade;
                itm.GradePoint = _gradeConversionDTO.GradePoint;

                _context.GradeConversions.Update(itm);
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

