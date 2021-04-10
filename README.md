# CredentialsValidation
A web application for validating and registering credentials.\
The application offers an interface to enter user credentials, which are validated against a set of predefined rules. If the validation fails, the user is presented with meaningful error messages to correct the input. Once validated, the credentials are stored in a persistance layer. If the same Email is used again to register, it is detected and informed to the user.

## Description

### Front-end
The front-end application is an ASP.NET Core Web application written in C#. It has been implemented with the MVC pattern.
Bootstrap has been used to make UI user-friendly, along with Jquery.\
The front-end provides a page for the user to enter credentials. For validation, the user input is sent to the back-end. Once the back-end process finishes, the user is informed about the results, along with the appropriate messages.


### Back-end
.NET Core WebAPI is used as the back-end written in C#. The WebAPI exposes a POST endpoint to which the credentials can be posted. The credentials are then checked against a set of predefined rules. If the credentials pass all the rules, they are stored, otherwise appropriate failure messages are generated. The result is then returned to the front-end in a response envelope.

### Data Access Layer
Repository pattern is implemented to access the underlying database. The DAL is essentially database agnostic, which is achieved through abstraction and dependency injection. Currently the DAL uses a SQL Server implementation of the repository to use SQL Server, which can be swapped easily with any other database with minimal code change.


## License
[MIT](https://choosealicense.com/licenses/mit/)
