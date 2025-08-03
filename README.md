# FreemarketFx ShoppingBasket

## Running the Application

The easiest way to run this application is to run it
in Visual Studio 2022. It can also be packaged and
deployed to any web server (including cloud services)
which supports running .Net 9 applications. Docker
support has not currently been added to the project.

## Setting up the Database

The application uses SQL Server as its data store.

The default configuration assumes that the application
is running locally, and that SQL Server Express is
installed locally, with the current user having dbo
permissions on the server. The connection string which
is used is:

`Server=.\SqlExpress;Database=FreemarketFxShoppingBasket;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true`

This setting can be found in the file
`appsettings.Development.json`. If it is preferred to
use a SQL Server instance other than `.\SqlExpress`,
this should be changed in that file before running the
application for the first time.

The application uses Entity Framework Core Code First
migrations. Although it's possible to update the database
from the Package Manager Console, this is not necessary -
the application will, upon startup, create the database
and run the migrations if necessary to ensure that the
database exists and is in the correct state.

## Demonstrating the Application

The file `FreemarketFx.ShoppingBasket/.http/app.http`
contains a series of HTTPS calls which, if called in order,
demonstrate all of the features on the "happy path" of the 
application. This file demonstrates the endpoints to be used,
and the body of each request.

The .http file assumes the application is running locally, e.g.
in debug mode in Visual Studio. If this is not the case, change
the `@root` variable on line 1 of the file.

In addition to the .http file, there are a number of unit
tests which test the business logic aspects of the application.

## API Overview

APIs exist for:

- Creating a Basket, and adding a Discount to the Basket
- Creating Basket Items - either single items, multiple of the
same item, or multiple different items. Discounts can be included
when adding items
- Adding Shipping costs to a Basket, either for the UK, or for
other countries
- Retrieving the contents of a Basket. This includes:
    - Basic details of the Basket
    - Details of all items in the Basket, including applying
      any Discounts (either Basket or Item discounts), and
      calculating the price before and after VAT
    - Details of Shipping costs, both to the UK and to other
      countries

## RESTfulness of the API

The principles of REST have been broadly followed in designing
the API. However, it should be noted that many PUT and POST
requests create or update resourcese which can't be accessed by
a corresponding GET request as would be normal in REST. Instead,
the resourcds are included in the response to the GET request to
retrieve the Basket details. This is largely because of time
constraints - it was felt that this implementation meets all the
requirements of the project, and allows it to be built within the
available time, as well as following the spirit of REST.

## Implementation Notes

### Third Party Libraries

The following third-party libraries are used. They are all free
and open-source:

- Main project:
    - FluentValidation
- Test project:
    - XUnit
    - Moq

### Nuget Configuration

Centralised Package Management has been used to prevent mis-matches
of Nuget package versions between different projects. See
`Directory.Packages.props` for details.

Package Source Mapping has been used. Although the benefits of this
won't be apparent whilst there is only a single Nuget source, the use
of Package Sourcee Mapping is considered best practice so that if, in
future, further Nuget sources are added, we can explicitly state which
package comes from which source, preventing a malicious (or accidental)
actor from publishing a package to a public source with the same name
as a package we are using from a private source.

### Future Enhancements

There are many features which would need to be considered in a
production system, but which are not mentioned in the specificion
for this project, and have not been implemented due to lack of time.
These include:

- Authentication
- Authorisation
- CORS
- API versioning
- CI/CD
- Database security
- Horizontal scalability
- Integration testing
    - To include the "happy path", and also invalid API calls
- Caching

#### Validation and Logging

Additionally, regarding input validation, some examples are shown of
the use of FluentValidation to validate incoming data, but the
validation is not complete.

There are places where validation rules exist in the database, but are
not duplicated in the C# code - this will result in invalid data
causing exceptions to be thrown, which will be returned to the client
as 500 return codes. If more time was available, validation such as
FluentValidation should be used to at least match all of the database
validation.

It may also be considered necessary to handle other exceptions on a
global scale, log them, and return a sanitised version of the exception
to the user - this can be done using custom middleware, and would
override the default behaviour (by default the exception details
are logged to the default place, e.g. Application Insights if the
application were deployed to Azure, or the console if the application
were to be run locally, as well as full exception details being returned
to the client).

In addition to giving further consideration to logging of exceptions, logging
in general might need more consideration. Examples of logging of adding
Items to a Basket have been included in the code, but further logging
would need to be added based on business requirements.