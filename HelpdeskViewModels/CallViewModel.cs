/* CallViewModel Class
 * Creator: Phuc Hanh Nguyen
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpdeskDAL;
using System.Reflection;

namespace HelpdeskViewModels
{
    public class CallViewModel
    {
        private CallModel _model;

        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public int ProblemId { get; set; }
        public int TechId { get; set; }
        public DateTime DateOpened { get; set; }
        public DateTime? DateClosed { get; set; }
        public bool OpenStatus { get; set; }
        public string Notes { get; set; }
        public string Timer { get; set; }
        public string Problem { get; set; }
        public string Employee { get; set; }
        public string Tech { get; set; }
        
        public CallViewModel()
        {
            _model = new CallModel();
        }
        
        public List<CallViewModel> GetAll()
        {
            List<CallViewModel> allVms = new List<CallViewModel>();

            try
            {
                List<Call> allCalls = _model.GetAll();

                foreach (Call call in allCalls)
                {
                    CallViewModel callVm = new CallViewModel();
                    callVm.Id = call.Id;
                    callVm.EmployeeId = call.EmployeeId;
                    callVm.ProblemId = call.ProblemId;
                    callVm.DateOpened = call.DateOpened;
                    callVm.DateClosed = call.DateClosed;
                    callVm.OpenStatus = call.OpenStatus;
                    callVm.TechId = call.TechId;
                    callVm.Notes = call.Notes;
                    callVm.Timer = Convert.ToBase64String(call.Timer);
                    callVm.Problem = call.Problem.Description;
                    callVm.Employee = call.Employee.FirstName + " " + call.Employee.LastName;
                    callVm.Tech = call.Employee1.FirstName + " " + call.Employee1.LastName;
                    allVms.Add(callVm);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return allVms;
        }
        
        public void GetById()
        {
            try
            {
                Call call = _model.GetById(Id);
                Id = call.Id;
                EmployeeId = call.EmployeeId;
                ProblemId = call.ProblemId;
                TechId = call.TechId;
                DateOpened = call.DateOpened;
                DateClosed = call.DateClosed;
                OpenStatus = call.OpenStatus;
                Notes = call.Notes;
                Timer = Convert.ToBase64String(call.Timer);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name +
                    " " + ex.Message);
                throw ex;
            }
        }
        
        public void Add()
        {
            Id = -1;
            try
            {
                Call call = new Call();
                call.EmployeeId = EmployeeId;
                call.ProblemId = ProblemId;
                call.TechId = TechId;
                call.DateOpened = DateOpened;
                call.DateClosed = DateClosed;
                call.OpenStatus = OpenStatus;
                call.Notes = Notes;
                Id = _model.Add(call);
            }
            catch (Exception ex)
            {

                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name +
                    " " + ex.Message);
                throw ex;
            }
        }

        public int Update()
        {
            UpdateStatus upStatus = UpdateStatus.Failed;

            try
            {
                Call updateCall = new Call();
                updateCall.EmployeeId = EmployeeId;
                updateCall.ProblemId = ProblemId;
                updateCall.TechId = TechId;
                updateCall.DateOpened = DateOpened;
                updateCall.DateClosed = DateClosed;
                updateCall.OpenStatus = OpenStatus;
                updateCall.Notes = Notes;
                updateCall.Id = Id;
                updateCall.Timer = Convert.FromBase64String(Timer);
                upStatus = _model.Update(updateCall);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " "
                    + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return Convert.ToInt16(upStatus);
        }
        
        public int Delete()
        {
            int callDeleted = -1;

            try
            {
                callDeleted = (int)_model.Delete(Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " +
                    MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return callDeleted;
        }
    }
}










