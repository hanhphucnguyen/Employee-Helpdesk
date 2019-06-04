/* ProblemModel Class
 * Creator: Phuc Hanh Nguyen
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HelpdeskDAL
{
    public class ProblemModel
    {
        IRepository<Problem> repo;
        public ProblemModel()
        {
            repo = new HelpdeskRepository<Problem>();
        }
        public Problem GetByDescription(string desc)
        {
            List<Problem> selectedProblem = null;

            try
            {
                selectedProblem = repo.GetByExpression(prb => prb.Description == desc);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return selectedProblem.FirstOrDefault();
        }

        public List<Problem> GetAll()
        {
            List<Problem> allProblems = new List<Problem>();

            try
            {
                allProblems = repo.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return allProblems;
        }

        public Problem GetById(int id)
        {
            List<Problem> selectedProblem = null;

            try
            {
                selectedProblem = repo.GetByExpression(prb => prb.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return selectedProblem.FirstOrDefault();
        }
    }
}
