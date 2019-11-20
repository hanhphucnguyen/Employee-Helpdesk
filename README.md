# Employee-Helpdesk

C# & ASP.NET project
A logical 5 tier employee editor for a helpdesk.

Functionality will consist of providing FULL CRUD capabilities of employee/call information using the techniques demonstrated in the exercises application. Employee Add/Update should allow adding/updating of Image data. Include a Reports page which lists the Employees and Calls report. The top Navigation bar should only show the Home page, Employees page, Calls page, and Reports page.
The layers of the solution are to be architected as follows:
1. View Layer (Helpdesk Website)
• Javascript Scripts
• HTML Pages
2. Web Layer (Helpdesk Website)
• Controller classes (API methods)
• Call Controller
• Problem Controller
• Employee Controller
• Department Controller
• Call Report
• Employee Report
3. ViewModels Layer (HelpdeskViewModels.dll)
• Employee View Model
• Department View Model
• Call View Model
• Problem View Model
4. Data Access Layer (HelpdeskDAL.dll)
• Model Classes
• Database Context
• Repository
• Entity classes
5. Data Layer (HelpdeskDB Database)
• Employees T able
• Departments T able
• Calls T able
• Problem T able
