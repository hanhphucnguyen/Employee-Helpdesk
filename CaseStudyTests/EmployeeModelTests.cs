/* EmployeeModelTest
 * Creator: Phuc Hanh Nguyen
 */
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HelpdeskDAL;
using System.Collections.Generic;

namespace CaseStudyTests
{
    [TestClass]
    public class EmployeeModelTests
    {

        [TestMethod]
        public void EmployeeModelGetAllShouldReturnList()
        {
            EmployeeModel model = new EmployeeModel();
            List<Employee> allEmployees = model.GetAll();
            Assert.IsTrue(allEmployees.Count > 0);
        }

        [TestMethod]
        public void EmployeeModelAddShouldReturnNewId()
        {
            EmployeeModel model = new EmployeeModel();
            Employee newEmployee = new Employee();
            newEmployee.Title = "Mr";
            newEmployee.FirstName = "Phuc";
            newEmployee.LastName = "Nguyen";
            newEmployee.Email = "leonguyen@gmail.com";
            newEmployee.PhoneNo = "(555)555-0001";
            newEmployee.DepartmentId = 200;
            int newId = model.Add(newEmployee);
            Assert.IsTrue(newId > 0);
        }

        [TestMethod]
        public void EmployeeModelGetByLastNameShouldReturnEmployee()
        {
            EmployeeModel model = new EmployeeModel();
            Employee someEmployee = model.GetByLastname("Nguyen");
            Employee anotherEmployee = model.GetById(someEmployee.Id);
            Assert.IsNotNull(anotherEmployee);
        }

        [TestMethod]
        public void EmployeeModelUpdateShouldReturnOkStatus()
        {
            EmployeeModel model = new EmployeeModel();
            Employee updateEmployee = model.GetByLastname("Nguyen");
            updateEmployee.Email = (updateEmployee.Email.IndexOf(".ca") > 0) ? "leonguyen@abc.com" : "leonguyen@abc.ca";
            UpdateStatus EmployeeUpdated = model.Update(updateEmployee);
            Assert.IsTrue(EmployeeUpdated == UpdateStatus.Ok);
        }
        
        [TestMethod]
        public void EmployeeModelUpdateTwiceShouldReturnStaleStatus()
        {
            EmployeeModel model1 = new EmployeeModel();
            EmployeeModel model2 = new EmployeeModel();
            Employee updateEmployee1 = model1.GetByLastname("Nguyen"); // Should already exist
            Employee updateEmployee2 = model2.GetByLastname("Nguyen"); // Should already exist
            updateEmployee1.Email = (updateEmployee1.Email.IndexOf(".ca") > 0) ? "leonguyen@abc.com" : "leonguyen@abc.ca";
            if (model1.Update(updateEmployee1) == UpdateStatus.Ok)
            {
                updateEmployee2.Email = (updateEmployee2.Email.IndexOf(".ca") > 0) ? "leonguyen@abc.com" : "leonguyen@abc.ca";
                Assert.IsTrue(model2.Update(updateEmployee2) == UpdateStatus.Stale);
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void EmployeeModelDeleteShouldReturnOne()
        {
            EmployeeModel model = new EmployeeModel();
            Employee deleteEmployee = model.GetByLastname("Nguyen");
            int EmployeeDeleted = model.Delete(deleteEmployee.Id);
            Assert.IsTrue(EmployeeDeleted == 1);
        }

        [TestMethod]
        public void LoadPicsShouldReturnTrue()
        {
            DALUtil util = new DALUtil();
            Assert.IsTrue(util.AddEmployeePicsToDb());
        }
    }
}
