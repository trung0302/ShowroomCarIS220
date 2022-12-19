using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ShowroomCarIS220.Data;
using ShowroomCarIS220.DTForm;
using ShowroomCarIS220.Models;
using System.Numerics;
using System.Text.RegularExpressions;
using ShowroomCarIS220.Response;

namespace ShowroomCarIS220.Controllers
{
    [Route("forms")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly DataContext _db;
        public FormController(DataContext db)
        {
            _db = db;
        }

        //Get Form
        [HttpGet]
        public async Task<ActionResult<FormResponse<List<Form>>>> getForm([FromQuery] string? dateForm, [FromQuery] int? pageIndex, [FromQuery] int? pageSize)
        {

            int pageResults = (pageSize != null) ? (int)pageSize : 10;
            int skip = (pageIndex != null) ? ((int)pageIndex * pageResults) : 0;
            //var pageCounts = Math.Ceiling(_db.Car.Count() / pageResults);
            string datesForm;
            if (dateForm != null)
            {
                DateTime datetimeForm = DateTime.ParseExact(dateForm, "dd-MM-yyyy", null);
                datesForm = String.Format("{0:yyyy-MM-dd}", datetimeForm);
            }
            else
            {
                datesForm = dateForm;
            }

            var formResponse = new FormResponse<List<Form>>();

            try
            {
                if (datesForm != null)
                {
                    var forms = (from form in _db.Form
                                 where (form.createdAt.ToString().Contains(datesForm))
                                 select new Form
                                 {
                                     id = form.id,
                                     name = form.name,
                                     mobile = form.mobile,
                                     email = form.email,
                                     message = form.message,
                                     createdAt = form.createdAt,
                                     updatedAt = form.updatedAt,
                                 })
                                .Skip(skip)
                                .Take((int)pageResults);
                    formResponse.Forms = forms.ToList();
                    formResponse.totalForms = _db.Form.ToList().Count();
                    formResponse.totalForms = formResponse.Forms.Count();
                }

                else if (pageIndex != null)
                {
                    var forms = await _db.Form
                        .Skip(skip)
                        .Take(pageResults)
                        .ToListAsync();
                    formResponse.Forms = forms;
                    formResponse.totalForms = _db.Form.ToList().Count();
                    formResponse.totalForms = forms.Count();
                }
                else
                {
                    var forms = await _db.Form
                        .Skip(skip)
                        .Take(pageResults)
                        .ToListAsync();
                    formResponse.Forms = forms;
                    formResponse.totalForms = _db.Form.ToList().Count();
                    formResponse.totalForms = _db.Car.ToList().Count();
                }
                formResponse.totalForms = _db.Form.ToList().Count();
                formResponse.totalForms = formResponse.Forms.Count();
                return StatusCode(StatusCodes.Status200OK, formResponse);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }

        //Get Form By ID
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<FormResponse<Form>>> getFormById([FromRoute] Guid id)
        {
            var formResponse = new FormResponse<Form>();
            try
            {
                var form = await _db.Form.FindAsync(id);
                if (form != null)
                {
                    formResponse.Forms = form;
                    formResponse.totalForms = _db.Form.ToList().Count();
                    formResponse.totalForms = 1;

                    return StatusCode(StatusCodes.Status200OK, form);
                }
                else
                    return StatusCode(StatusCodes.Status400BadRequest, "Không tồn tại ID!");
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }

        //Remove Form By ID
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<FormResponse<List<Form>>>> getFormByNameOrCode([FromRoute] Guid id)
        {
            var formResponse = new FormResponse<List<Form>>();
            try
            {
                var form = await _db.Form.FindAsync(id);
                if (form != null)
                {
                    _db.Form.Remove(form);
                    await _db.SaveChangesAsync();
                    formResponse.Forms = _db.Form.ToList();
                    formResponse.totalForms = _db.Form.ToList().Count();
                    formResponse.totalForms = formResponse.Forms.Count();

                    return StatusCode(StatusCodes.Status200OK, form);
                }
                else
                    return StatusCode(StatusCodes.Status400BadRequest, "Không tồn tại ID!");
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }

        //Add Form
        [HttpPost]
        public async Task<ActionResult<FormResponse<List<Form>>>> addForm(AddFormDTFrom formDTForm)
        {
            var formResponse = new FormResponse<List<Form>>();
            try
            {
                var form = new Form()
                {
                    id = Guid.NewGuid(),
                    name = formDTForm.name,
                    mobile = formDTForm.mobile,
                    email = formDTForm.email,
                    message = formDTForm.message,
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now,
                };

                await _db.Form.AddAsync(form);
                await _db.SaveChangesAsync();
                formResponse.Forms = _db.Form.ToList();
                formResponse.totalForms = formResponse.Forms.Count();
                formResponse.totalForms = formResponse.Forms.Count();
                return StatusCode(StatusCodes.Status200OK, formResponse.Forms);
            }
            catch (Exception err)
            {
                return StatusCode(StatusCodes.Status400BadRequest, err);
            }
        }

    }
}
