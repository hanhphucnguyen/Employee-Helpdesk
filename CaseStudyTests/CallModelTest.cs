/* CallModelTest
 * Creator: Phuc Hanh Nguyen
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpdeskDAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CaseStudyTests
{
    [TestClass]
    public class CallModelTest
    {
        [TestMethod]

        public void ComprehensiveModelTestsShouldReturnTrue()
        {
            CallModel cmodel = new CallModel();
            EmployeeModel emodel = new EmployeeModel();
            ProblemModel pmodel = new ProblemModel();
            Call call = new Call();
            call.DateOpened = DateTime.Now;
            call.DateClosed = null;
            call.OpenStatus = true;
            call.EmployeeId = emodel.GetByLastname("Nguyen").Id;
            call.TechId = emodel.GetByLastname("Joe").Id;
            call.ProblemId = pmodel.GetByDescription("Hard Drive Failure").Id;
            call.Notes = "Evan's drive is shot, Burner to fix it";
            int newCallId = cmodel.Add(call);
            Console.WriteLine("New Call Generated - Id = " + newCallId);
            call = cmodel.GetById(newCallId);
            byte[] oldtimer = call.Timer;
            Console.WriteLine("New Call Retrieved");
            call.Notes += "\n Ordered new RAM!";

            if (cmodel.Update(call) == UpdateStatus.Ok)
            {
                Console.WriteLine("Call was updated " + call.Notes);
            }
            else
            {
                Console.WriteLine("Call was not updated !");
            }

            call.Timer = oldtimer;
            if (cmodel.Update(call) == UpdateStatus.Stale)
            {
                Console.WriteLine("Call was not updated due to stale data");
            }
            cmodel = new CallModel();
            call = cmodel.GetById(newCallId);

            if (cmodel.Delete(newCallId) == 1)
            {
                Console.WriteLine("Call was deleted");
            }
            else
            {
                Console.WriteLine("Call was not deleted");
            }
            Assert.IsNull(cmodel.GetById(newCallId));
        }
    }
}
