/* EmployeeViewModel Class
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
    public class EmployeeViewModel
    {
        private EmployeeModel _model;
        public string Title { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Phoneno { get; set; }
        public string Timer { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int Id { get; set; }
        public bool IsTech { get; set; }
        public string StaffPicture64 { get; set; }
        
        public EmployeeViewModel()
        {
            _model = new EmployeeModel();
        }

        public List<EmployeeViewModel> GetAll()
        {
            List<EmployeeViewModel> allVms = new List<EmployeeViewModel>();
            try
            {
                List<Employee> allEmployees = _model.GetAll();

                foreach (Employee emp in allEmployees)
                {
                    EmployeeViewModel empVm = new EmployeeViewModel();
                    empVm.Title = emp.Title;
                    empVm.Firstname = emp.FirstName;
                    empVm.Lastname = emp.LastName;
                    empVm.Phoneno = emp.PhoneNo;
                    empVm.Email = emp.Email;
                    empVm.Id = emp.Id;

                    if (emp.IsTech != null)
                    {
                        empVm.IsTech = emp.IsTech.Value;
                    }

                    empVm.DepartmentId = emp.DepartmentId;
                    empVm.DepartmentName = emp.Department.DepartmentName;

                    if (emp.StaffPicture != null)
                    {
                        empVm.StaffPicture64 = Convert.ToBase64String(emp.StaffPicture);
                    }

                    empVm.Timer = Convert.ToBase64String(emp.Timer);
                    allVms.Add(empVm);
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

        public void GetByLastname()
        {
            try
            {
                Employee employee = _model.GetByLastname(Lastname);

                if(employee == null)
                {
                    Lastname = "not found";
                    return;
                }

                Title = employee.Title;
                Firstname = employee.FirstName;
                Lastname = employee.LastName;
                Phoneno = employee.PhoneNo;
                Email = employee.Email;
                Id = employee.Id;
                DepartmentId = employee.DepartmentId;

                if (employee.StaffPicture != null)
                {
                    StaffPicture64 = Convert.ToBase64String(employee.StaffPicture);
                }

                Timer = Convert.ToBase64String(employee.Timer);
            }
            catch (Exception ex)
            {
                Lastname = "not found";
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name +
                    " " + ex.Message);
                throw ex;
            }
        }

        public void GetById()
        {
            try
            {
                Employee employee = _model.GetById(Id);
                Title = employee.Title;
                Firstname = employee.FirstName;
                Lastname = employee.LastName;
                Phoneno = employee.PhoneNo;
                Email = employee.Email;
                Id = employee.Id;
                DepartmentId = employee.DepartmentId;

                if (employee.StaffPicture != null)
                {
                    StaffPicture64 = Convert.ToBase64String(employee.StaffPicture);
                }

                Timer = Convert.ToBase64String(employee.Timer);
            }
            catch (NullReferenceException nex)
            {
                Lastname = "not found";
            }
            catch (Exception ex)
            {
                Lastname = "not found";
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
                Employee employee = new Employee();
                employee.Title = Title;
                employee.FirstName = Firstname;
                employee.LastName = Lastname;
                employee.PhoneNo = Phoneno;
                employee.Email = Email;
                employee.DepartmentId = DepartmentId;
                Id = _model.Add(employee);
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
                Employee employee = new Employee();
                employee.Title = Title;
                employee.FirstName = Firstname;
                employee.LastName = Lastname;
                employee.PhoneNo = Phoneno;
                employee.Email = Email;
                employee.Id = Id;
                employee.DepartmentId = DepartmentId;

                if (StaffPicture64 != null)
                {
                    employee.StaffPicture = Convert.FromBase64String(StaffPicture64);
                }

                employee.Timer = Convert.FromBase64String(Timer);
                upStatus = _model.Update(employee);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name +
                    " " + ex.Message);
                throw ex;
            }
            return Convert.ToInt16(upStatus);
        }
        
        public int Delete()
        {
            int deleted = -1;

            try
            {
                deleted = _model.Delete(Id);
            }
            catch (Exception ex)
            {

                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name +
                    " " + ex.Message);
                throw ex;
            }
            return deleted;
        }
    }
}
