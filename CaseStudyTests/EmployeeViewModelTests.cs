/* EmployeeViewModelTest
 * Creator: Phuc Hanh Nguyen
 */
using System;
using System.Collections.Generic;
using HelpdeskDAL;
using HelpdeskViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CaseStudyTests
{
    [TestClass]
    public class EmployeeViewModelTests
    {
        [TestMethod]
        public void EmployeeViewModelAddShouldReturnId()
        {
            EmployeeViewModel vm = new EmployeeViewModel();
            vm.Title = "Mr";
            vm.Firstname = "Phuc";
            vm.Lastname = "Nguyen";
            vm.Email = "leonguyen@gmail.com";
            vm.Phoneno = "(555)555-5555";
            vm.DepartmentId = 400;
            vm.Add();
            Assert.IsTrue(vm.Id > 0);
        }

        [TestMethod]
        public void EmployeeViewModelGetByNameShouldPopulatePropertyFirstName()
        {
            EmployeeViewModel vm = new EmployeeViewModel();
            vm.Lastname = "Nguyen"; 
            vm.GetByLastname();
            Assert.IsNotNull(vm.Firstname);
        }

        [TestMethod]
        public void EmployeeViewModelGetAllShouldReturnAtLeastOneVM()
        {
            EmployeeViewModel vm = new EmployeeViewModel();
            List<EmployeeViewModel> allEmployeesVms = vm.GetAll();
            Assert.IsTrue(allEmployeesVms.Count > 0);
        }

        [TestMethod]
        public void EmployeeViewModelGetByIdShouldPopulatePropertyFirstname()
        {
            EmployeeViewModel vm = new EmployeeViewModel();
            vm.Lastname = "Nguyen";
            vm.GetByLastname();
            vm.GetById();
            Assert.IsNotNull(vm.Firstname);
        }

        [TestMethod]
        public void EmployeeViewModelUpdateShouldReturnOkStatus()
        {
            EmployeeViewModel vm = new EmployeeViewModel();
            vm.Lastname = "Nguyen";
            vm.GetByLastname();
            vm.Email = (vm.Email.IndexOf(".com") > 0) ? "leonguyen@gmail.org" : "leonguyen@gmail.com";
            UpdateStatus EmployeeUpdated = (UpdateStatus) vm.Update();
            Assert.IsTrue(EmployeeUpdated == UpdateStatus.Ok);
        }

        [TestMethod]
        public void EmployeeViewModelDeleteShouldReturnOne()
        {
            EmployeeViewModel vm = new EmployeeViewModel();
            vm.Lastname = "Nguyen";
            vm.GetByLastname();
            int EmployeeViewModel = vm.Delete();
            Assert.IsTrue(EmployeeViewModel == 1);
        }
    }
}
