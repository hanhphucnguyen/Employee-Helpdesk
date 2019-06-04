/* DepartmentViewModel Class
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
    public class DepartmentViewModel
    {
        private DepartmentModel _deptmodel;

        public string Timer { get; set; }

        public string Name { get; set; }

        public int Id { get; set; }

        public DepartmentViewModel()
        {
            _deptmodel = new DepartmentModel();
        }

        public List<DepartmentViewModel> GetAll()
        {
            List<DepartmentViewModel> allDeptVm = new List<DepartmentViewModel>();
            try
            {
                List<Department> allDepts = _deptmodel.GetAll();
                foreach (Department dpt in allDepts)
                {
                    DepartmentViewModel deptVm = new DepartmentViewModel();
                    deptVm.Id = dpt.Id;
                    deptVm.Name = dpt.DepartmentName;
                    deptVm.Timer = Convert.ToBase64String(dpt.Timer);
                    allDeptVm.Add(deptVm);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name +
                   " " + ex.Message);
                throw ex;
            }
            return allDeptVm;
        }
    }
}
