# CMIntern1

Source Code Repository for Intern 1

## 01 - Development Environment Setup
- I have completed the installation of all the required tools that are necessary for day to day tasks(visual studio insder 2026)

## 02 - First Console Application
- The ArithmaticOperation Console app contains methods with which we can do addition, substraction, multipication, division, modulus, increment and decrement on the given number (ArithmaticOperation).

## 03 - Variables & Data Types
- I have added a new solution in the repo with having a name TrainingTasks. In the solution i have added a project VariablesAndDataTypes.
- i have added some practice methods to Understand variables, data types, operators, and casting me clearly.
- I have created a new project for calculation of different data types(Calculator).
- I use method overloading to perform calculation on integer, float and double data type.

## 04 - Control Statements
- Build the number guesing game and practice methods i.e EvenOdd, PrimeNumber, Factorial, FindTable, LoginAuthentication (ControlStatements).

## 05 - Methods
- Build grade calculator with simple if else and for loop (Methods).

## 06 - Arrays & Collections
- Build Student Record with Collection and Dictionary.(ArrayCollection)
- Explore and practice Collections.
- Explore and practice Dictionary.

## 18-07-classes-objects
- Add a Save user method with which we can add and update the user at the same time (if entered id already existed then update that user otherwise created a new one).
- Add a method to get all the users form the list.
- Add a method to search a User from the list.
- Add a method to found and then delete that User from the list.

## 19-08-encapsulation-properties
- Implement encapsulation in the employee services class.
- Implement try catch in the curd methods of employee in employee service class.
- Add properties validations to enter the user name and email.

## 20-09-inheritance-polymorphism
- Parent class vehicle has initialization constructor and virtually defined Display vehcle details method.
- The virtually defined method is overridden in each category of vehicle that i have added in the system.
- Vehicle manager class has the generic methods like AddVehecle, DisplayVehicles, SearchVehicleById, and SearchVehicleByBrand.
- I have handled the operations in program class with switch statements by applying the try catch.

## 10 - Interfaces & Abstraction
- Stripe payment system
- create customer
- add product
- add price
- add payment method
- add payment intent (reviewed)
- confirm payment
- refund payment
- list transactions

## 11 - LINQ Basics
- Explore Linq methods
- Build Student details console using random generated student record.
- Practice where, orderby, select, firstordefault.

## 12 - Advanced LINQ
- I have defined region for all possible linq operations
- Build a employee record system using randomly seeded employee record.
- Implement each linq operation with an example.
- Detailed practice of filtering, grouping and aggregations and real World examples.

## 13 - Git Workflow
- Explore git commands, use git branching in the codebase.
- Explore chery pick, how to resolve conflicts, push, pull and fetch.

## 14 - Mini Project - Student Management System
- Build Student management system.
- BusinessLogic Layer [Added the StudentValidator, ResultCalculator and RegistrationManager].
- Interface layer [Added the interfaces: IStudentService, ICourseService, IDepartmentService and IResultService].
- Service Layer [Added the services in which i implement the Interface methods: StudentService, CourseService, DepartmentService and ResultService].
- Menu Option layer [Added the menu classes : StudentOptions, CourseOptions, DepartmentOptions and ResultOptions].
- Helper classes layer [Added the helper classes: ConsoleHelper, StudentDisplayHelper, CourseDisplayHelper, DepartmentDisplayHelper and ResultDisplayHelper].
- Models
- Strings [I have added the classes: Prompts, SuccessMessages and ErrorMessages].
	- The working flow of the SMS as follow.
		- The application provides four major modules:

		#1 - Student ManagementRegister Student
		- Update Student
		- Delete Student
		- Display All Students
		- Display Student By Id
		- Display Student By Registration Number
		- Display Students By Department
		- Display Active Students

		#2 - Course Management
		- Display All Courses
		- Get Course By Id
		- Get Course By Code
		- Search Course
		- Display Courses By Credit Hours
		- Display Courses Ordered By Name
		- Display Courses Ordered By Credit Hours

		#3 - Department Management
		- Display All Departments
		- Get Department By Id
		- Get Department By Code
		- Search Department
		- Display Departments Ordered By Name
		- Display Departments Ordered By Code
		- Display First Department
		- Display Last Department

		#4 - Result Management
		- Display All Results
		- Display Student Result
		- Display Passed Students
		- Display Failed Students
		- Display Results Ordered By Percentage
		- Display Top Student
		- Display Lowest Student
		- Display Class Average Percentage
		- Display Pass Percentage
		- Display Grade Distribution

		#5 - Exit
		- Exit

## 15. Project setup
- Add Default ASP.Net Core MVC Project into the Repository.

## 18. Configuration
- Explore appsettings.JSON
- How can we set up configuration variables
- what is the criteria to setup a valiable in the configuaration
- How can we use them in the program.cs class.
- Add a controller method to log a configuration key by reegistering into program.cs file then accessing it by using that service into the controller

## 19. Middleware
- Explore the Middlewares in details
- Categories of middlewares
- Execution pattern of middlewares
- How they behave when a user request for a action.
- Build a logging middleware for practice.

## 20. Controllers
- Explore and build a functional Controllers.
- Apply crud operations in the controller, add middleware for exception handling.

## 21. Views
- Explore and practice Views.
- Render Data from the controller to view by:
	- ViewData
	- ViewBag
	- TempData

## 22. Razor syntax
- Explore razor syntax in detail, asp for -- usuage.
- Build developer crud to practice razor syntax in details.
- Build an example for the DI Life Cycle by generating a random guid id.

## 23. Layouts
- Explore Layout synax, naming convention.
- Learn about @RenderBody() and @RenderSection().
- Create a Practice layout, update the header, navigation bar and footer of the project with in that layout.
- Register the layout in the ViewStart file.

## 24. Partial Views
- Explore the partial views.
- Build reuseable partial view i.e. _AlertMessage.cshtml and _Details.cshtml.

## 25. Static Files
- Explore wwwroot folder structure, and its static (styling and js related) files.
- Add a texting css file, add a folder for the images.
- use images from the images folder into the testing.css file to render them in the views.

## 26. Mini assignment
- Create a new mvc project to implement the Users cruds using the repository based pattern without using database.
- Create the interface and the service folder to implement the crud logic.
- Store data with student seed class.
- Add Sidepanal to switch through different modules i.e.
	- Dashboard
	- Users
	- Create Users
	- Get Active Users
	- Get Inactive Users

## 48. Repository Pattern
- Explore repository pattern and its way of implementation.
- Add a new solution to the repo to implement the repository pattern.
- create the repository for the user using the DI, add the exception filter class, the ui for the user crud.

## 49. Unit of Work (intro)
- Implement the Unit of Work in the repository pattern project as i have...
- Seed the Career through the career seed data class.
- Added the service and repository for the Career.
- Added the crud feature for the Careers.
- Associate the careers to the users using the Unit of work i have associate the career to the different users and delete the career by validating the users.

## 50. Weekly Assignment
- Implemented the Employee CRUD feature with dedicated ViewModels and Razor Views.
- Implemented attribute-based routing for the Employee.
- Implemented centralized exception handling using custom middleware.
- Implemented an MVC Action Filter for logging controller action execution and performance.
- Reviewed and structured the project architecture using Repository Pattern, Unit of Work, Services, Dependency Injection, and In-Memory Data Seeding.


## 51. UI Improvements
- Added the Statics detail summary for the Employee based on the designations.
- Added a partial view to remove the duplicate code.
- Add an icon for the Create Button at the Employee Index page.

## 52. Bootstrap integration
- In the dotnet core mvc project, bootstrap is integrated into the project by default when we create a MVC Web App project.
- In order to use the bootstrap features into the presentation layer (views) we register it into the _Layout page as "<link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.min.css" />"
- If want to manually integrate the bootstrap into the project then.... 
   	 - right click onto the project and from the => add menu => select the client side libray,
	 - then from cdnjs select any bootstrap feature that you what to integrate into the project.

## 53. Refactoring
- Remove the duplicated code form the controller.
- Add region into the controller to define the private methods there. then overload those methods into the controller actions to make code little clean.
- Remove unused and irrelevant code from the Repositories and the service.

## 54. Code Cleanup
- Clean-Up the duplicated code from the views, add a partial view for that.
- Add a new view model for repeated properties into the create and edit vm.
- Add a method "GetEmployeeDetails" to get the details related to employee and the designation. that make the controller short.
- Update the Employee service method "GetEmployeeList".
- For each purpose I have added a private method then reuse that into the main service method to get the employee details against the designation.

## 55. Testing
- Added a new XUnit project into the solution "WeeklyAssignment.Tests"
- Added a test class for the EmployeeService class in the test project.
- Added test cases for the EmployeeService class into the EmployeeServiceTests class.
- Run these test to check whether they are failed or not.

## 63. IdentityUser & IdentityRole
- Added a new Web Api Project
- Inherit Identity tables for Application user and role.
- Connect the sql database
- Run first migration to add the identity tables into the database.
- Added the services methods to get, create users and role.
- Add a method to assign a role to a user.
- Added the api controller for the users and roles to handle data added different dto's for users table and roles table.

## 64. Claims-Managers
- Added the functionality to add the claims for the users.
- Added the Sign In Api for User Authentication.

## 65. Claims-Based Authentication
- Added the functionality to Create the JWT Token against the user login.
- Added a section for jwt credentials into the app settings file, register the jwt token into the program.cs file.
- Added the Token Service. 
- Updated the existing SignUp/Login functionality.
- Implemented support for Access Token and Refresh Token rotation.
- Added functionality to consume the generated token for role creation/assignment.
- Updated the authentication flow to properly handle token generation and rotation.

## 66. External Providers & OIDC
- Added the Google Authentication.
- Register through the  google cloud, add a project there and create client credentials there to use them to add a user using the google signup UI.
- Added a endpoint into the authentication controller "GoogleLogin" to handle the google login authentication.

## 67. Multi-Factor Authentication
- (Implemented Email Service) Added an SMTP-based email service for sending emails to users.
- (Implemented Email-Based MFA Service) Added an MFA service to generate, send, and verify OTPs using ASP.NET Core Identity.
- (Added MFA Login Flow) When a user with MFA enabled logs in, the system generates a pendingMfaToken and sends an OTP to the user's registered email address.
- (Added MFA Verification) The user must provide the pendingMfaToken and OTP to complete authentication. After successful verification, the system generates and returns the access token, refresh token and other user details.

## 68. WebAPI Authentication
- Added an endpoint to get the user by id.
- Added the autherization policy
- Validate each endpoint based on the functionality
- Use the [AllowAnonymous] for authentication related endpoints.

## 69. Role-based policies
- Added a new policy (ManagerOnly) into the program.cs file.
- Implement the policy in the ROles controller to authorize that only user with the role manager can only get all the available roles.
- Make the create role endpoint as AllowAnonymous, now anyone can add new roles.

## 71. UserManager customization
- Customized UserService with UserManager-based user-management methods.
- Added APIs to update a user’s username and delete a user.
- Added role assignment, role replacement, and role removal functionality.
- Added validation for users, roles, duplicate assignments, and existing role membership.
- Added required DTOs and verified the project builds successfully without warnings or errors.

## 72. SignInManager customization
- Customized SignInManager with a CustomSignInManager implementation featuring custom sign-in validation, lockout checks, and IP-based audit logging.
- Added support to sign in using either username or email address.
- Added handling for NotAllowed sign-in status returning an appropriate 403 Forbidden response.
- Added APIs and service methods to retrieve and update user lockout enablement (LockoutEnabled).
- Added global ExceptionHandlingMiddleware for centralized unhandled exception logging and standard Problem Details responses.
- Registered the custom sign-in manager and middleware in Program.cs, and verified the project builds successfully without warnings or errors.

## 73. RoleManager customization
- Customized RoleManager with a CustomRoleManager implementation featuring custom role validation, duplicate role checks, and role deletion restrictions.
- Added three end points into the roles controller to update, delete and get the assigned roles.

## 75. Advanced concurrency
- Add migration to create a new table 'Products'
- Concurrency Implementation using the Product entity (implement 3optimistic concurrency in the Update Product method).

## 76. Optimistic vs. Pessimistic Locking
- Implemented Optimistic Concurrency Control in the Product entity to handle concurrent updates.
- Added a new endpoint for purchase counter in which implement the transaction to validate the user action.

## 77. Query optimization
- Implemented query optimization techniques in the Product entity to improve performance.
- Implemented query projection using dedicated Product response DTOs.
- Optimized read queries with AsNoTracking() and selective column retrieval.
- Implemented pagination for product listing with bounded page size.
- Reviewed query performance considerations including offset pagination.

## 78. Execution plans
- Update the GetProducts method using the ToQueryString method to get the SQL query from the EF LINQ logic.
- Execute that query in SSMS, include the actual execution plan, and then display the estimated execution plan to compare the actual and estimated plans.
- Analyzed query plan analysis using SQL Server Management Studio (SSMS) to identify potential performance.

## 79. Multi-tenant filtering
- Added the tenants endpoint (Add the interface, then implement that interface in the tenant service using the dto's).
- Added the tenant feature to the user (Added the endpoint to assign a user to a new tenant, using a DTO; updated the login logic to validate the tenant key as well).
- Associate a product with a user based on the tenant key (updated the application DB context class to add the global tenant filter on products; updated the token service to add the tenantId claim).


## Collaborate with team

- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/1)(on child branch 02---First-Console-Application -16-07-2026)
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/3)(on child branch 03---Variables-&-Data-Types-#01 -17-07-2026)
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/4)(on child branch 04---Control-Statements -20-07-2026)
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/5)(on child branch 05---Methods -#21-07-2026) 
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/6)(on child branch 06---Arrays-&-Collections -22-07-2026) 
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/7)(on child branch 18-07-classes-objects -23-07-2026) 
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/8)(on child branch 19-08-encapsulation-properties -24-07-2026) 
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/9)(on child branch 20-09-inheritance-polymorphism -27-07-2026) 
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/10)(on child branch 21-10-interfaces-abstraction -29-07-2026) 
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/11)(on child branch 22-11-linq-basics -30-07-2026) 
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/12)(on child branch 23-12-advanced-linq -31-07-2026) 
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/13)(on child branch Update Readme -03-08-2026) 
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/15)(on child branch 25-14-mini-project-student-management-system -04-08-2026)
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/16)(on child branch 26-15-project-setup -06-08-2026) 
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/20)(on child branch 29-18-Configuration -11-08-2026) 
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/17)(on child branch 30-19-middleware -07-08-2026)
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/18)(on child branch 31-20-controllers -10-08-2026)
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/19)(on child branch 32-21-views -11-08-2026)
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/21)(on child branch 33-22-Razor-syntax -12-08-2026)
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/22)(on child branch 34-23-Layouts -13-08-2026)
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/23)(on child branch 35-24-Partial-Views -17-08-2026)
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/24)(on child branch 36-25-Static-files -18-08-2026)
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/25)(on child branch 37-26-Mini-Assignment -19-08-2026)
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/26)(on child branch 59-48-repository-pattern -24-08-2026)
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/27)(on child branch 60-49-Unit-of-Work -31-08-2026)
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/30)(on child branch 61-50-Weekly-Assignment -01-09-2026)
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/31)(on child branch 62-51-UI-Improvements -02-09-2026)
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/32)(on child branch 64-53-Refactoring -04-09-2026)
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/33)(on child branch 65-54-Code-cleanup -07-09-2026)
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/34)(on child branch 66-55-Testing -08-09-2026)
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/35)(on child branch 63-IdentityUser-IdentityRole -09-09-2026)
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/36)(on child branch 64-Claims-Managers -10-09-2026)
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/37)(on child branch 66-External-Providers-&-OIDC -15-09-2026)
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/38)(on child branch 67-Multi-Factor-Authentication -17-09-2026)
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/39)(on child branch 68-WebAPI-Authentication -18-09-2026)
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/40)(on child branch 69-Role-based-policies -21-09-2026)
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/41)(on child branch 71-UserManager-customization -21-09-2026)
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/42)(on child branch 72-SignInManager-customization -22-09-2026)
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/43)(on child branch 73-RoleManager-customization -23-09-2026)
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/44)(on child branch 75-76-Advanced-concurrency -25-09-2026)
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/45)(on child branch 77-Query-optimization -28-09-2026)
- [Create a new merge request](https://gitlab.vteamslabs.com/dotnet/trainings/cmintern1/-/merge_requests/46)(on child branch 78-Execution-plans -30-09-2026)


## Project Structure
```text
cmintern1/	

├── 📁 ArithmaticOperation/

├── 📁 TrainingTasks/
│   ├── 📁 ControlStatements/          # Decision & Loop Statements
│   ├── 📁 VariablesAndDataTypes/      # C# Fundamentals
│   ├── 📁 CrudCalculator/             # CRUD Console Application
│   ├── 📁 UtilityLibrary/             # Shared Helper Library
│   ├── 📁 ArrayCollection/            # Arrays & Collections
│   ├── 📁 Methods/                    # Methods & Parameters
│   ├── 📁 ClassesObjects/             # OOP Fundamentals
│   ├── 📁 EncapsulationProperties/    # Encapsulation & Properties
│   ├── 📁 StripePaymentSystem#10/     # Stripe Payment Gateway Integration
│   └── 📁 VehicleManagement#09/       # Vehicle Management System
│   ├── 📁 LinqOperations/             # LINQ Basics
│   ├── 📁 AdvanceLinqOperations/      # Advanced LINQ Practice
│   ├── 📁 StudentManagementSystem/    # Complete Console CRUD Project
│
├── 📄 TrainingTasks.slnx              # Solution File

├── 📁 MvcFeatures/
│   ├── Configurations/       # Class for communication with appsettings entity
│   ├── Controllers/          # MVC Controllers
│   ├── Interfaces/           # Dependency Handler interfaces
│   ├── Middlewares/          # Custom Middleware Components
│   ├── Models/               # Domain Models
│   ├── Views/                # Razor Views
│   ├── Services/             # Dependency Implementation classes
│   ├── wwwroot/              # Static Files (CSS, JS, Images) Add the images folder in it.
│   ├── Program.cs            # Application Entry Point
│   ├── appsettings.json      # Configuration
│   └── MvcFeatures.csproj
│
├── 📁RepositoryPattern/
│	├── 📁 Connected Services/       # Connected external services
│	├── 📁 Dependencies/             # Project dependencies and NuGet packages
│	├── 📁 Properties/               # Project configuration and launch settings
│	├── 📁 wwwroot/                  # Static Files (CSS, JS, Images)
│	├── 📁 Controllers/              # MVC Controllers
│	├── 📁 Data/                     # Data Seeders
│	├── 📁 Filters/                  # Custom MVC Filters
│	├── 📁 Middlewares/              # Custom Middleware Components
│	├── 📁 Models/                   # Domain Models
│	├── 📁 Repositories/             # Repository Pattern Implementation
│	├── 📁 Services/                 # Service Implementations
│	├── 📁 ViewModels/               # View-Specific Models
│	├── 📁 Views/                    # Razor Views
│	├── 📄 appsettings.json          # Application Configuration
│	└── 📄 Program.cs                # Application Entry Point
├── 📄 RepositoryPattern.slnx              # Solution File
│
├── 📁 WeeklyAssignment/
│	├── 📁Connected Services/
│	├── 📁 Dependencies/
│	├── 📁 Properties/
│	├── 📁 wwwroot/
│	├── 📁 Controllers/
│	│		├── EmployeeController.cs
│	│		└── ErrorController.cs
│	├──📁 Data/
│	│		├── InMemoryDataStore.cs
│	│		├── EmployeeSeedData.cs
│	│		└── DesignationSeedData.cs
│	├── 📁 Filters/
│	│		└── ActionExecutionLoggingFilter.cs
│	├── 📁 Middlewares/
│	│		└── ExceptionHandlingMiddleware.cs
│	├── 📁 Models/
│	│		├── Employee.cs
│	│		└── Designation.cs
│	├── 📁 Repositories/		
│	│		├──  📁	Interfaces/
│	│		│		├── IEmployeeRepository.cs
│	│		│		├── IDesignationRepository.cs
│	│		│		└── IUnitOfWork.cs
│	│		│
│	│		└──  📁 Implementations/
│	│				├── EmployeeRepository.cs
│	│				├── DesignationRepository.cs
│	│				└── UnitOfWork.cs
│	├── 📁 Services/
│	│		│
│	│		├──  📁 Interfaces/
│	│		│		└── IEmployeeService.cs
│	│		│
│	│		└──  📁 Implementations/
│	│				└── EmployeeService.cs
│	├── 📁 ViewModels/		
│	│		└──  📁 Employees/
│	│				├── EmployeeListVM.cs
│	│				├── EmployeeCreateVM.cs
│	│				├── EmployeeEditVM.cs
│	│				└── EmployeeDetailsVM.cs
│	│				└── DesignationSummaryVM.cs
│	│				└── EmployeeInputVM.cs
│	│				└── EmployeeListItemVM.cs
│	├── 📁 Views/		
│	│		├──  📁 Employee/
│	│		│   ├── _EmployeeFormFields.cshtml
│	│		│   ├── _EmployeeInformation.cshtml
│	│		│   ├── Index.cshtml
│	│		│   ├── Details.cshtml
│	│		│   ├── Create.cshtml
│	│		│   ├── Edit.cshtml
│	│		│   └── Delete.cshtml
│	│		├──  📁 Error/
│	│		│   └── Index.cshtml
│	│		├──  📁 Shared/
│	│		├── _ViewImports.cshtml
│	│		└── _ViewStart.cshtml
│	├── 📄 appsettings.json
│	├── 📄 appsettings.Development.json
│	├── 📄 Program.cs
├── 📁 WeeklyAssignment.Tests/
│	│		├──  📁 Dependencies/
│	│		│		├── 📁 Projects/
│	│		│		│	├── 📄 WeeklyAssignment
│	│		├──  📁 Services/
│	│		│			├──  EmployeeServiceTests.cs
│	├── 📄 WeeklyAssignment.slnx

├── 📄 README.md
└── 📄 .gitignore
```
