/* CallModel Class
 * Creator: Phuc Hanh Nguyen
 */
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HelpdeskDAL
{
    public class CallModel
    {
        IRepository<Call> repo;

        public CallModel()
        {
            repo = new HelpdeskRepository<Call>();
        }

        public Call GetById(int id)
        {
            List<Call> selectedCall = null;

            try
            {
                selectedCall = repo.GetByExpression(c => c.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return selectedCall.FirstOrDefault();
        }

        public List<Call> GetAll()
        {
            List<Call> allCalls = new List<Call>();

            try
            {
                allCalls = repo.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return allCalls;
        }

        public int Add(Call newCall)
        {
            try
            {
                repo.Add(newCall);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return newCall.Id;
        }

        public UpdateStatus Update(Call updatedCall)
        {
            UpdateStatus upStatus = UpdateStatus.Failed;
            try
            {

                upStatus = repo.Update(updatedCall);
            }
            catch (DbUpdateConcurrencyException dbx)
            {
                upStatus = UpdateStatus.Stale;
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + dbx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return upStatus;
        }

        public int Delete(int id)
        {
            int callDeleted = -1;

            try
            {
                callDeleted = repo.Delete(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return callDeleted;
        }
    }
}














