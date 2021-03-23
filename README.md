# Securing APIs
## Installation requirements
*  Docker (https://hub.docker.com/editions/community/docker-ce-desktop-windows)
* .NET5 SDK (https://dotnet.microsoft.com/download/dotnet/thank-you/sdk-5.0.201-windows-x64-installer)

## Information
The repository contains the below applications folders:
 - **JobClient**: NET5 Console Application
 - **ResourceServer**: NET5 API (http://localhost:5000)
 - **IdentityServer**: Identity Server 4 (http://localhost:5001)
 - **Client**: Angular 11 (http://localhost:4200) 

IdentityServer and Client are docker containerized applications, execute `.\run.ps1` in powershell console to run them.

### Identity server users:
#### Bob Smith:
- **Username:** bob 
- **Password:** bob
- **Role:** User
- **Email:** BobSmith@email.com
  
#### Alice Smith:
- **Username:** alice 
- **Password:** alice
- **Role:** Administrator
- **Email:** AliceSmith@email.com

## Let's code
### [Step 0 - Init](https://github.com/christian147/securing-apis/tree/step-0)
### [Step 1 - Jwt bearer middleware and authorization policies configuration](https://github.com/christian147/securing-apis/tree/step-1)
The goal of this step is being able to validate correctly the information of a jwt (Json Web Tokens) issued by a identity server and limit the access of the users depending its role and permissions (scope-based). 
We will also make sure than other client only has access to a specific endpoint and not all.
### [Step 2 - Access to claims and tokens from API](https://github.com/christian147/securing-apis/tree/step-2)
The goal of this step is being able to get the user claims and token from anywhere of the application once the user is authenticated.
### [Step 3 - Introspection middleware configuration](https://github.com/christian147/securing-apis/tree/step-3)
Once we have seen how to validate the information of the self-contained token, now we go to see how to validate a referential token. In this case, there are not information contained in the token, identity server will have to validate for us. The goal of this step is being able to change the validation of the token and configure the cache to reduce the number of request than the resource server does to validate the token.
### [Step 4 - Mocking authentication and authorization in integration tests](https://github.com/christian147/securing-apis/tree/step-4)
The goal of this step is being able to avoid the authorization and mock the authentication data when we have to implement integration tests