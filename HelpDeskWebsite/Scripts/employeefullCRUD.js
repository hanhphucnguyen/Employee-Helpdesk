/* employeefullCRUD js file
 * Creator: Phuc Hanh Nguyen
 */
$(function () { 
    let getAll = async (msg) => {
        try {
            $('#status').text('Finding Employee Information...');
            let response = await fetch(`api/employees/`);
            if (!response.ok) // or check for response.status
                throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
            let data = await response.json(); // this returns a promise, so we await it 
            buildEmployeeList(data, true);
            (msg === '') ? 
                $('#status').text('Employees Loaded') : $('#status').text(`${msg} - Employees Loaded`);

            // set all departments to local storage
            response = await fetch(`api/departments/`);
            if (!response.ok) // or check for response.status
                throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
            let depts = await response.json(); // this returns a promise, so we await it 
            localStorage.setItem('alldepartments', JSON.stringify(depts));

        } catch (error) {
            $('#status').text(error.message);
        }
    } // getAll

    //filter the stored JSON based on srch contents
    const filterData = () => {
        allData = JSON.parse(localStorage.getItem('allemployees'));
        //tilde below same as stu.Lastname.indexOf($('#srch').val > -1)
        let filteredData = allData.filter((emp) => ~emp.Lastname.indexOf($('#srch').val()));
        buildEmployeeList(filteredData, false);
    }//filter Data

    const loadDepartmentDDL = (empdept) => {
        html = '';
        $('#ddlDepts').empty();
        let alldepartments = JSON.parse(localStorage.getItem('alldepartments'));
        alldepartments.map(div => html += `<option value="${div.Id}">${div.Name}</option>`);
        $('#ddlDepts').append(html);
        $('#ddlDepts').val(empdept);
    } // load DepartmentDDL

    const setupForUpdate = (Id, data) => {
        $('#actionbutton').val('update');
        $('#modaltitle').html('<h4>update employee</h4>');
        clearModalFields();
        data.map(employee => {
            if (employee.Id === parseInt(Id)) {
                $('#TextBoxTitle').val(employee.Title);
                $('#TextBoxFirstname').val(employee.Firstname);
                $('#TextBoxLastname').val(employee.Lastname);
                $('#TextBoxPhone').val(employee.Phoneno);
                $('#TextBoxEmail').val(employee.Email);
                localStorage.setItem('Id', employee.Id);
                loadDepartmentDDL(employee.DepartmentId);
                localStorage.setItem('Timer', employee.Timer);
                localStorage.setItem('StaffPicture', employee.StaffPicture64);
                $('#ImageHolder').html(`<img heigh="120" width="110" src="data:image/png;base64, ${employee.StaffPicture64}"/>`)
                $('#modalstatus').text('update data');
                $('#theModal').modal('toggle');
            } 
        }); 
    } // set up for Update

    const setupForAdd = () => {
        $('#actionbutton').val('add');
        $('#modaltitle').html('<h4>Add Employee </h4>');
        $('#theModal').modal('toggle');
        $('#modalstatus').text('add new employee');
        loadDepartmentDDL(-1);
        clearModalFields();
    }// setupForAdd

    let clearModalFields = () => {
        $('#TextBoxTitle').val('');
        $('#TextBoxFirstname').val('');
        $('#TextBoxLastname').val('');
        $('#TextBoxPhone').val('');
        $('#TextBoxEmail').val('');
        localStorage.removeItem('Id');
        localStorage.removeItem('DepartmentId');
        localStorage.removeItem('Timer');
    } // Clear ModalFields

    let buildEmployeeList = (data, allemployees) => {
        $('#employeeList').empty();
        div = $(`<div class="list-group-item text-white bg-secondary row d-flex" id="status">Employee Info</div> 
            <div class= "list-group-item row d-flex text-center" id="heading">
            <div class="col-4 h4">Title</div> 
            <div class="col-4 h4">First</div> 
            <div class="col-4 h4">Last</div>
           </div>`);

        div.appendTo($('#employeeList'))
        allemployees ? localStorage.setItem('allemployees', JSON.stringify(data)) : null;
        btn = $(`<button class="list-group-item row d-flex" id="0"><div class="col-12 text-left">... click to add employee</div></button>`);
        btn.appendTo($('#employeeList'));
        data.map(emp => {
            btn = $(`<button class="list-group-item row d-flex" id="${emp.Id}">`);
            btn.html(`<div class="col-4" id="employeetitle${emp.Id}">${emp.Title}</div>
                    <div class="col-4" id="employeefname${emp.Id}">${emp.Firstname}</div>
                    <div class="col-4" id="employeelastname${emp.Id}">${emp.Lastname}</div>`
            );
            btn.appendTo($('#employeeList'));
        });
    } // buildEmployeeList

    const update = async () => {
        try {
            emp = new Object();
            emp.Title = $('#TextBoxTitle').val();
            emp.Firstname = $('#TextBoxFirstname').val();
            emp.Lastname = $('#TextBoxLastname').val();
            emp.Phoneno = $('#TextBoxPhone').val();
            emp.Email = $('#TextBoxEmail').val();
            emp.Id = localStorage.getItem('Id');
            emp.DepartmentId = $('#ddlDepts').val();
            emp.Timer = localStorage.getItem('Timer');
            localStorage.getItem('StaffPicture')
                ? emp.StaffPicture64 = localStorage.getItem('StaffPicture') : null
            // send the updated back to the server asynchronously using PUT
            let response = await fetch('api/employees', {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                },
                body: JSON.stringify(emp)
            });
            if (response.ok) //check for response.status
            {
                let data = await response.json();
                getAll(data);

            } else {
                $('#status').text(`${response.status}, Text - ${response.statusText}`);
            } 
            $('#theModal').modal('toggle');
        } catch (error) {
            $('#status').text(error.message);
        } //try catch
    }//update

    const add = async () => {
        try {
            emp = new Object();
            emp.Title = $('#TextBoxTitle').val();
            emp.Firstname = $('#TextBoxFirstname').val();
            emp.Lastname = $('#TextBoxLastname').val();
            emp.Phoneno = $('#TextBoxPhone').val();
            emp.Email = $('#TextBoxEmail').val();
            //emp.DepartmentId = 100; // hard code it for now, we'll add a dropdown later
            emp.DepartmentId = $('#ddlDepts').val();
            emp.Id = -1;
            // send the student info to the server asynchronously using POST
            let response = await fetch('api/employees', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                },
                body: JSON.stringify(emp)
            });
            if (response.ok) //check for response.status
            {
                let data = await response.json();
                getAll(data);

            } else {
                $('#status').text(`${response.status}, Text - ${response.statusText}`);
            } 
            $('#theModal').modal('toggle');
        } catch (error) {
            $('#status').text(error.message);
        }
    }// add

    let _delete = async () => {
        try {

            let response = await fetch(`api/employees/${localStorage.getItem('Id')}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                }
            });
            if (response.ok) // or check for response.status
            {
                let data = await response.json();
                getAll(data);

            } else {
                $('#status').text(`${response.status}, Text - ${response.statusText}`);
            } // else
            $('#theModal').modal('toggle');
        } catch (error) {
            $('#status').text(error.message);
        } // try catch

    }

    $("#EmployeeModalForm").validate({
        rules: {
            TextBoxTitle: { maxlength: 4, required: true, validTitle: true },
            TextBoxFirstname: { maxlength: 25, required: true },
            TextBoxLastname: { maxlength: 25, required: true },
            TextBoxEmail: { maxlength: 40, required: true, email: true },
            TextBoxPhone: { maxlength: 15, required: true }
        },
        errorElement: "div",
        messages: {
            TextBoxTitle: {
                required: "required 1-4 chars.", maxlength: "required 1-4 chars.", validTitle: "Mr. Ms. Mrs. or Dr."
            },
            TextBoxFirstname: {
                required: "required 1-25 chars.", maxlength: "required 1-25 chars."
            },
            TextBoxLastname: {
                required: "required 1-25 chars.", maxlength: "required 1-25 chars."
            },
            TextBoxPhone: {
                required: "required 1-15 chars.", maxlength: "required 1-15 chars."
            },
            TextBoxEmail: {
                required: "required 1-40 chars.", maxlength: "required 1-40 chars.", email: "Invalid Email Format!"
            }
        }
    });

    $.validator.addMethod("validTitle", function (value, element) { // custom rule
        return this.optional(element) || (value == "Mr." || value == "Ms." || value == "Mrs." || value == "Dr.");
    }, "");

    $("#actionbutton").click(() => {
        $("#actionbutton").val() === "update" ? update() : add();
    }); //actionbutton click

    $('[data-toggle=confirmation]').confirmation({ rootSelector: '[data-toggle=confirmation]' });
    $('#deletebutton').click(() => _delete()); 

    $('#employeeList').click((e) => {
        if (!e) e = window.event;
        let Id = e.target.parentNode.id;
        if (Id === 'employeeList' || Id === '') {
            Id = e.target.id;
        } // clicked on row somewhere else
        if (Id !== 'status' && Id !== 'heading') {
            let data = JSON.parse(localStorage.getItem('allemployees'));
            Id === '0' ? setupForAdd() : setupForUpdate(Id, data);

        } else {
            return false; 
        }
    }); // EmployeeListClick

    $('#srch').keyup(filterData); 

    $('input:file').change(() => {
        const reader = new FileReader();
        const file = $('#fileUpload')[0].files[0];

        file ? reader.readAsBinaryString(file) : null;

        reader.onload = (readerEvt) => {
            const binaryString = reader.result;
            const encodedString = btoa(binaryString);
            localStorage.setItem('StaffPicture', encodedString);
            $('#ImageHolder').html(`<img heigh="120" width="110" src="data:image/png;base64, ${encodedString}"/>`)
        }
    });

    getAll(''); 
});
