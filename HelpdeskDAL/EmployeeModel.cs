/* EmployeeModel Class
 * Creator: Phuc Hanh Nguyen
 */
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;


namespace HelpdeskDAL
{
    public class EmployeeModel
    {
        IRepository<Employee> repo;

        public EmployeeModel()
        {
            repo = new HelpdeskRepository<Employee>();
        }
        public Employee GetByEmail(string email)
        {
            List<Employee> selectedEmployee = null;

            try
            {
                selectedEmployee = repo.GetByExpression(emp => emp.Email == email);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return selectedEmployee.FirstOrDefault();
        }

        public Employee GetById(int id)
        {
            List<Employee> selectedEmployee = null;
            try
            {
                selectedEmployee = repo.GetByExpression(emp => emp.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return selectedEmployee.FirstOrDefault();
        }

        public List<Employee> GetAll()
        {
            List<Employee> allEmployees = new List<Employee>();
            try
            {
                allEmployees = repo.GetAll();   
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }

            return allEmployees;
        }

        public int Add(Employee newEmployee)
        {
            try
            {
                repo.Add(newEmployee);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return newEmployee.Id;
        }

        public Employee GetByLastname(string lastname)
        {
            List<Employee> selectedEmployee = null;

            try
            {
                selectedEmployee = repo.GetByExpression(emp => emp.LastName == lastname);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return selectedEmployee.FirstOrDefault();
        }
        
        public UpdateStatus Update(Employee updatedEmployee)
        {
            UpdateStatus upStatus = UpdateStatus.Failed;

            try
            {
                upStatus = repo.Update(updatedEmployee);
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
            int employeesDeleted = -1;
            try
            {
                employeesDeleted = repo.Delete(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Problem in " + GetType().Name + " " + MethodBase.GetCurrentMethod().Name + " " + ex.Message);
                throw ex;
            }
            return employeesDeleted;
        }
    }
}
