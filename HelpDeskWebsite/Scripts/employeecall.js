/* employeecall js file
 * Creator: Phuc Hanh Nguyen
 */
$(function () {
    $("#CallModalForm").validate({
        rules: {
            ddlProblems: { required: true },
            ddlEmployees: { required: true },
            ddlTechnicians: { required: true },
            noteBox: { maxlength: 250, required: true }
        },
        errorElement: "div",
        messages: {
            ddlProblems: {
                required: "Select a Problem"
            },
            ddlEmployees: {
                required: "Select a Employee"
            },
            ddlTechnicians: {
                required: "Select a Technician"
            },
            noteBox: {
                required: "Note required.", maxlength: "required max 250 chars."
            }
        }
    });

    const getAll = async (msg) => {
        try {
            $('#callList').html('<h3>Finding Call Information, please wait...</h3>');
            let response = await fetch(`api/calls/`);
            if (!response.ok) // or check for response.status
                throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
            let data = await response.json(); // this returns a promise, so we await it
            buildCallList(data, true);
            (msg === '') ? // are we appending to an existing message
                $('#status').text('Call Loaded') : $('#status').text(`${msg}`);
        } catch (error) {
            $('#status').text(error.message);
        }
    }; // getAll

    const filterData = () => {
        allData = JSON.parse(localStorage.getItem('allcalls'));
        let filteredData = allData.filter((call) => ~call.Employee.indexOf($('#srch').val()));
        buildCallList(filteredData, false);
    };

    $('#srch').keyup(filterData);

    let loadEmpDDL = async () => {
        let response = await fetch(`api/employees`);
        if (!response.ok)
            throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
        let emps = await response.json();
        html = '';
        $('#ddlEmployees').empty();

        emps.map(employee =>
            html += `<option value = "${employee.Id}">${employee.Firstname} ${employee.Lastname}</option>`
        )
        $('#ddlEmployees').append(html);
        $('#ddlEmployees').val(-1);
    }//loadEmpDDL

    let loadTechDDL = async () => {
        let response = await fetch(`api/employees`);
        if (!response.ok)
            throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
        let emps = await response.json();
        html = '';
        $('#ddlTechnicians').empty();
        emps.map(employee => {
            if (employee.IsTech)
                html += `<option value = "${employee.Id}">${employee.Firstname} ${employee.Lastname}</option>`
        });
        $('#ddlTechnicians').append(html);
        $('#ddlTechnicians').val(-1);
    }//loadTechDDL

    let loadProblemDDL = async () => {
        let response = await fetch(`api/problems`);
        if (!response.ok)
            throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
        let probs = await response.json();
        html = '';
        $('#ddlProblems').empty();

        probs.map(problem =>
            html += `<option value = "${problem.Id}">${problem.Description}</option>`
        )
        $('#ddlProblems').append(html);
        $('#ddlProblems').val(-1);
    }//loadProblemDDL

    // setup for Add
    const setupForAdd = () => {
        try {

            $('#actionbutton').val('add');
            $('#modaltitle').html('<h4>Add/Change Call Information</h4>');

            $('#actionbutton').prop('disabled', false);
            $('#ddlEmployees').val(-1);
            $('#ddlTechnicians').val(-1);
            $('#ddlProblems').val(-1);
            $('#dateOpened').val(formatDate());
            $('#dateOpenedLbl').text(formatDate());
            $('#dateClosed').val('');
            $('#dateClosedLbl').text('');
            $('#noteBox').text('');
            $('#modalstatus').text('');

            let validator = $('#CallModalForm').validate();
            validator.resetForm();

            $('#checkBoxClose').prop('checked', false);
            $('#checkBoxClose').prop('disabled', true);
            $('#deletebutton').fadeOut();
            $('#noteBox').prop('readonly', false);
            $('#ddlEmployees').prop('disabled', false);
            $('#ddlTechnicians').prop('disabled', false);
            $('#ddlProblems').prop('disabled', false);
            $('#checkBoxClose').prop('disabled', false);
            $('#actionButton').prop('disabled', false);
            $('#theModal').modal('toggle');

        }
        catch (error) {
            $('#modalstatus').text(error.message);
        }  // try catch
    } // setupForAdd

    const add = async () => {
        try {
            call = new Object();
            call.DateOpened = $('#dateOpened').val();
            call.DateClosed = $('#dateClosed').val();
            call.Notes = $('#noteBox').val();
            call.EmployeeId = $('#ddlEmployees').val();
            call.TechId = $('#ddlTechnicians').val();
            call.ProblemId = $('#ddlProblems').val();
            call.OpenStatus = !$('#checkBoxClose').is(':checked');
            // send to server asynchronously using POST
            let response = await fetch('api/calls', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json; charset = utf-8'
                },
                body: JSON.stringify(call)
            });
            if (response.ok) //check for response.status
            {
                let data = await response.json();
                $('#modalstatus').text(data);
                getAll(data);
            }
            else {
                $('#modalstatus').text(`${response.status}, Text - ${response.statusText}`);
            } // else
        } catch (error) {
            $('#modalstatus').text(error.message);
        } // try catch
    } // add

    // setup for Update
    const setupForUpdate = (Id) => {
        $('#actionbutton').val('update');
        $('#actionbutton').fadeOut();
        $('#deletebutton').fadeIn();
        $('#modaltitle').html('<h4>Update Calls</h4>');
        let data = JSON.parse(localStorage.getItem('allcalls'));
        data.map(call => {
            if (call.Id == parseInt(Id)) {
                $('#dateOpened').val(formatDate(call.DateOpened));
                $('#noteBox').val(call.Notes);
                $('#ddlEmployees').val(call.EmployeeId);
                $('#ddlTechnicians').val(call.TechId);
                $('#ddlProblems').val(call.ProblemId);
                $('#checkBoxClose').prop('checked', !call.OpenStatus);
                $('#dateOpenedLbl').text(formatDate(call.DateOpened));
                $('#modalstatus').text('');
                localStorage.setItem('selectedcall', JSON.stringify(call));
                if (!call.OpenStatus) {
                    $('#noteBox').prop('disabled', true);
                    $('#ddlEmployees').prop('disabled', true);
                    $('#ddlTechnicians').prop('disabled', true);
                    $('#ddlProblems').prop('disabled', true);
                    $('#checkBoxClose').prop('disabled', true);
                    $('#actionbutton').fadeOut();
                    $('#dateClosed').val(formatDate(call.DateClosed));
                    $('#dateClosedLbl').text(formatDate(call.DateClosed));
                }
                else
                {
                    $('#noteBox').prop('readonly', false);
                    $('#ddlEmployees').prop('disabled', false);
                    $('#ddlTechnicians').prop('disabled', false);
                    $('#ddlProblems').prop('disabled', false);
                    $('#checkBoxClose').prop('disabled', false);
                    $('#actionbutton').fadeIn();
                }
                $('#theModal').modal('toggle');

            } 
        }); 
    } 

    const update = async () => {
        try {
            let call = JSON.parse(localStorage.getItem('selectedcall'));
            call.DateOpened = $('#dateOpened').val();
            call.DateClosed = $('#dateClosed').val();
            call.Notes = $('#noteBox').val();
            call.EmployeeId = $('#ddlEmployees').val();
            call.TechId = $('#ddlTechnicians').val();
            call.ProblemId = $('#ddlProblems').val();
            call.OpenStatus = !$('#checkBoxClose').is(':checked');
            let response = await fetch('api/calls', {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json; charset = utf-8'
                },
                body: JSON.stringify(call)
            });

            if (response.ok) //check for response.status
            {
                let data = await response.json();
                $('#modalstatus').text(data);
                getAll(data);
            }
            else {
                $('#modalstatus').text(`${response.status}, Text - ${response.statusText}`);
            } 
        } catch (error) {
            $('#modalstatus').text(error.message);
        } // try catch
    } // update

    //set up delete
    let _delete = async () => {

        try {
            let call = JSON.parse(localStorage.getItem('selectedcall'))
            let response = await fetch(`api/calls/${call.Id}`, {
                method: 'DELETE',
                headers: { 'Content-type': 'application/json; charset=utf-8' }
            });

            if (response.ok) //check for response.status
            {
                let data = await response.json();
                $('#status').text(data);
                getAll('Call Deleted');
                //getAll(data);
            } else {
                $('#status').text(`${response.status}, Text - ${response.statusText}`);
            } // else

            $('#theModal').modal('toggle');
        } catch (error) {
            $('#status').text(error.message);
        }
    }

    // build a Call list
    const buildCallList = (data, allcalls) => {
        $('#callList').empty();
        // Add the header columns
        div = $(`<div class = "list-group-item text-white bg-secondary row d-flex" id="status">Call Info</div>
            <div class ="list-group-item row d-flex text-center" id="heading">
            <div class ="col-4 h4 text-left">Date</div>
            <div class ="col-4 h4 text-left">For</div>
            <div class ="col-4 h4 text-left">Problem</div>
            </div>`);
        div.appendTo($('#callList'));
        allcalls ? localStorage.setItem('allcalls', JSON.stringify(data)) : null;
        btn = $(`<button class = "list-group-item row d-flex" id = "0"><div class = "col-12 text-left">...Click to Add Call</div></button>`);
        btn.appendTo($('#callList'));

        data.map(call => {
            btn = $(`<button class ="list-group-item row d-flex" id="${call.Id}">`);
            btn.html(`<div class ="col-4 text-left" id ="callDate${call.Id}">${formatDate(call.DateOpened)}</div>
                     <div class ="col-4 text-left" id ="callFor${call.Id}">${call.Employee}</div >
                     <div class ="col-4 text-left" id ="callProblem${call.Id}">${call.Problem}</div>`
            );
            btn.appendTo($('#callList'));
        }); // map

    } // build a Call List

    // The modal's action button
    $("#actionbutton").click(async (e) => {
        $('#modalstatus').removeClass(); // remove any existing class on div
        if ($('#CallModalForm').valid()) {
            $("#actionbutton").val() === "update" ? update() : add();
        }
        else {
            $('#modalstatus').attr('class', 'badge badge-danger');
            $('#modalstatus').text('fix errors');
            e.preventDefault;
        }

    }); // actionbutton click

    //Handle click event when clicking on header or row
    $('#callList').click((e) => {
        if (!e) e = window.event;
        let Id = e.target.parentNode.id;
        if (Id === 'callList' || Id === '') {
            Id = e.target.id;
        } // clicked on row somewhere else

        if (Id !== 'status' && Id !== 'heading') {
            let data = JSON.parse(localStorage.getItem('allcalls'));
            Id === '0' ? setupForAdd() : setupForUpdate(Id, data);
        }
        else {
            return false; 
        }
    });

    $('[data-toggle=confirmation]').confirmation({ rootSelector: '[data-toggle=confirmation]' });
    $('#deletebutton').click(() => _delete()); 

    //format date
    const formatDate = (date) => {
        let d;
        date === (undefined) ? d = new Date() : d = new Date(Date.parse(date));
        let _day = d.getDate();
        let _month = d.getMonth() + 1;
        let _year = d.getFullYear();
        let _hour = d.getHours();
        let _min = d.getMinutes();
        if (_min < 10) { _min = "0" + _min; }
        if (_year > 2020) return "";
        return _year + "-" + _month + "-" + _day + " " + _hour + ":" + _min;
    };

    //handle click event when clicking on check box
    $('#checkBoxClose').click(() => {
        if ($('#checkBoxClose').is(':checked')) {
            $('#dateClosed').val(formatDate());
            $('#dateClosedLbl').text(formatDate());
        }
        else {
            $('#dateClosed').val('');
            $('#dateClosedLbl').text('');
        }
    });

    loadEmpDDL();
    loadTechDDL();
    loadProblemDDL(); 

    getAll(''); // first grab the data from the server
}); // jQuery ready method
