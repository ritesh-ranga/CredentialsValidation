# CredentialsValidation
A web application for validating and registering credentials.

## Description

### Front-end
The front-end application is an ASP.NET Core Web application written in C#. It has been implemented with the MVC pattern.
Bootstrap has been used to make UI user-friendly, along with Jquery.\
The front-end provides a page for the user to enter credentials. After validation, the user is informed if they are valid or not, along with the appropriate messages.


### Back-end
.NET Core WebAPI is used as the back-end written in C#. The WebAPI exposes a POST endpoint to which the credentials can be posted. Then the credentials are checked against a set of rules and the result is returned in a response envelope.


## License
[MIT](https://choosealicense.com/licenses/mit/)
