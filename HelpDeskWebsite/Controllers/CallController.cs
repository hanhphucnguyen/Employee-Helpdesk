/* CallController Class
 * Creator: Phuc Hanh Nguyen
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HelpdeskViewModels;

namespace ExercisesWebsite.Controllers
{
    public class CallController : ApiController
    {
        //Get
        [Route("api/calls")]
        public IHttpActionResult GetAll()
        {
            try
            {
                CallViewModel call = new CallViewModel();
                List<CallViewModel> allCalls = call.GetAll();
                return Ok(allCalls);
            }
            catch (Exception ex)
            {
                return BadRequest("Update failed - " + ex.Message);
            }
        }

        //Update
        [Route("api/calls")]
        public IHttpActionResult Put(CallViewModel callVm)
        {
            try
            {
                int retVal = callVm.Update();
                switch (retVal)
                {
                    case 1:
                        return Ok("Call " + callVm.Id + " updated!");
                    case -1:
                        return Ok("Call " + callVm.Id + " not updated!");
                    case -2:
                        return Ok("Data is stale for " + callVm.Id + ", Call not updated!");
                    default:
                        return Ok("Call " + callVm.Id + " not updated!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Update failed - " + ex.Message);
            }
        }

        //Add
        [Route("api/calls")]
        public IHttpActionResult Post(CallViewModel callVm)
        {
            try
            {
                callVm.Add();
                if (callVm.Id > 0)
                {
                    return Ok("Call " + callVm.Id + " added!");
                }
                else
                {
                    return Ok("Call " + callVm.Id + " not added!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Creation failed - Contact Tech Support");
            }
        }

        //Delete
        [Route("api/calls/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                CallViewModel callVm = new CallViewModel();
                callVm.Id = id;


                if (callVm.Delete() == 1)
                {
                    return Ok("Call deleted!");
                }
                else
                {
                    return Ok("Call not deleted!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Delete failed - Contact Tech Support");
            }
        }
    }
}

