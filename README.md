# DotNet Auth Web Api
Authentication and Authorization using Web API

## API Configuration
Configure the following app settings

    <!-- Microsoft Authentication -->
    <add key="Microsoft.Auth.ClientId" value="" />
    <add key="Microsoft.Auth.ClientSecret" value="" />
    <add key="Microsoft.Auth.Enabled" value="false" />

    <!-- Twitter Authentication -->
    <add key="Twitter.Auth.ConsumerKey" value="" />
    <add key="Twitter.Auth.ConsumerSecret" value="" />
    <add key="Twitter.Auth.Enabled" value="false" />

    <!-- Facebook Authentication -->
    <add key="Facebook.Auth.AppId" value="" />
    <add key="Facebook.Auth.AppSecret" value="" />
    <add key="Facebook.Auth.Enabled" value="false" />

    <!-- Google Authentication -->
    <add key="Google.Auth.ClientId" value="" />
    <add key="Google.Auth.ClientSecret" value="" />
    <add key="Google.Auth.Enabled" value="true" />

## API Usage

##### Register External Flow
- **GET** /api/Account/ExternalLogins?returnUrl=%2F&generateState=true
- **GET** /api/Account/ExternalLogin?... (generated link by previous request)
- **POST** /api/Account/RegisterExternal (email + token provided by previous request)
- **Successfully registered!**

##### Login External Flow
- **GET** /api/Account/ExternalLogins?returnUrl=%2F&generateState=true
- **GET** /api/Account/ExternalLogin?... (generated link by previous request)
- **Successfully logged in!**

##### Register Internal Flow
- **POST** /api/Account/Register (email, password, confirmpassword)
- **Successfully registered!**

##### Login Internal Flow
- **POST** /Token (username, password, grant_type = password)
- **Successfully logged in!**

##### Add External Login Flow
- Login internally
- **GET** /api/Account/ExternalLogins?returnUrl=%2F&generateState=true
- **GET** /api/Account/ExternalLogin?... (generated link by previous request)
- **POST** /api/Account/AddExternalLogin (Authorization header, ExternalAccessToken provided by previous request)
- **Successfully login added!**

##### Add Internal Login Flow
- Login externally
- **POST** /api/Account/SetPassword (Authorization header, NewPassword, ConfirmPassword)
- **Successfully login added!**

## Postman integration
- AuthApiCollection.json.postman_collection
- Local.postman_environment