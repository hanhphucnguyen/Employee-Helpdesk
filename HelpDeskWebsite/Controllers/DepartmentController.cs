/* DepartmentController
 * Creator: Phuc Hanh Nguyen
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HelpdeskViewModels;

namespace HelpDeskWebsite.Controllers
{
    public class DepartmentController : ApiController
    {

        [Route("api/departments")]
        public IHttpActionResult GetAll()
        {
            try
            {
                DepartmentViewModel deptVm = new DepartmentViewModel();
                List<DepartmentViewModel> allDepartments = deptVm.GetAll();
                return Ok(allDepartments);
            }
            catch (Exception ex)
            {
                return BadRequest("Retrieve failed - " + ex.Message);
            }
        }
    }
}
