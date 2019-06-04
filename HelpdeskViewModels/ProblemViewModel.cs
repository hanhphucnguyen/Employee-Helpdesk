/* ProblemViewModel Class
 * Creator: Phuc Hanh Nguyen
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HelpdeskDAL;

namespace HelpdeskViewModels
{
    public class ProblemViewModel
    {
        private ProblemModel _model;
        public int Id { get; set; }
        public string Timer { get; set; }
        public string Description { get; set; }
        
        public ProblemViewModel()
        {
            _model = new ProblemModel();
        }
        
        public List<ProblemViewModel> GetAll()
        {
            List<ProblemViewModel> allVms = new List<ProblemViewModel>();
            try
            {
                List<Problem> allProblems = _model.GetAll();

                foreach (Problem prob in allProblems)
                {
                    ProblemViewModel probVm = new ProblemViewModel();
                    probVm.Id = prob.Id;
                    probVm.Timer = Convert.ToBase64String(prob.Timer);
                    probVm.Description = prob.Description;
                    allVms.Add(probVm);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name +
                   " " + ex.Message);
                throw ex;
            }

            return allVms;
        }
        
        public void GetByDescription()
        {
            try
            {
                Problem prob = _model.GetByDescription(Description);
                Id = prob.Id;
                Description = prob.Description;
                Timer = Convert.ToBase64String(prob.Timer);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name +
                    " " + ex.Message);
                throw ex;
            }
        }
        
        public void GetById()
        {
            try
            {
                Problem prob = _model.GetById(Id);
                Id = prob.Id;
                Description = prob.Description;
                Timer = Convert.ToBase64String(prob.Timer);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name +
                    " " + ex.Message);
                throw ex;
            }
        }
    }
}
