/* DepartmentModel Class
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
    public class DepartmentModel
    {
        IRepository<Department> repo;
        public DepartmentModel()
        {
            repo = new HelpdeskRepository<Department>();
        }
        public List<Department> GetAll()
        {
            List<Department> allDepts = new List<Department>();
            try
            {
                allDepts = repo.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return allDepts;
        }
    }
}
