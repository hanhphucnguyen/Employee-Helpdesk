/* PDFController
 * Creator: Phuc Hanh Nguyen
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HelpDeskWebsite.Reports;

namespace HelpDeskWebsite.Controllers
{
    public class PDFController : ApiController
    {
        [Route("api/employeereport")]
        public IHttpActionResult GetEmployeeReport()
        {
            try
            {
                EmployeeReport report = new EmployeeReport();
                report.getEmpReport();
                return Ok("employee report generated");
            }
            catch(Exception ex)
            {
                return BadRequest("Call Report Generation failed - " + ex.Message);
            }
        }

        [Route("api/callreport")]
        public IHttpActionResult GetCallReport()
        {
            try
            {
                CallReport report = new CallReport();
                report.getCallReport();
                return Ok("call report generated");
            }
            catch (Exception ex)
            {
                return BadRequest("Call Report Generation failed - " + ex.Message);
            }
        }
    }
}
