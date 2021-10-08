# CodingExercise
The coding exercise for one of the job applications.

1. AutoFac is used as the IoC container, Dapper as the data access technology.
2. AutoFac has to be wired up manually for this project as ASP MVC 5 doesn't ship with a default IoC container.
3. There are two ways to export list of users into excel and pdf namely by using export functionalities provided by jQuery Datatables and by using my own generic export methods that can take different models.
4. Basic OWIN cookie authentication and claim-based authorization is implemented.
5. OWIN external login provider for Google is implemented.

Future enhancements:
1. Configure NLog to log errors to a database.

