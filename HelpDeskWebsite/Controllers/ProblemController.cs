/* Problem Controller
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
    public class ProblemController : ApiController
    {
        [Route("api/problems")]
        public IHttpActionResult GetAll()
        {
            try
            {
                ProblemViewModel probs = new ProblemViewModel();
                List<ProblemViewModel> allProbs = probs.GetAll();
                return Ok(allProbs);
            }
            catch (Exception ex)
            {
                return BadRequest("Update failed - " + ex.Message);
            }
        }
    }
}
