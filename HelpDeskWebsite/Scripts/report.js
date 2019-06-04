/* report js file
 * Creator: Phuc Hanh Nguyen
 */
$(function () {
    //generating employee report
    $('#pdfbutton1').click(async (e) => {
        try {
            $('#lblstatus1').removeClass('success error').text('generating employee report on server - please wait...');
            let response = await fetch(`api/employeereport`);
            if (!response.ok) // check for response.status
                throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
            let data = await response.json(); // this returns a promise, so we await it
            (data === 'employee report generated')
                ? window.open('/Pdfs/Employee.pdf')
                : $('#lblstatus1').addClass('error').text('problem generating employee report');
        } catch (error) {
            $('#lblstatus1').addClass('error').text(error.message);
        } 
    }); // button click 

    // handle generating call report
    $('#pdfbutton2').click(async (e) => {
        try {
            $('#lblstatus2').text('generating call report on server - please wait...');
            let response = await fetch(`api/callreport`);
            if (!response.ok) // check for response.status
                throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);
            let data = await response.json(); // this returns a promise, so we await it
            (data === 'call report generated')
                ? window.open('/Pdfs/Call.pdf')
                : $('#lblstatus2').addClass('error2').text('problem generating call report');
        } catch (error) {
            $('#lblstatus2').addClass('error2').text(error.message);
        } 
    }); // button click 
}); // jQuery