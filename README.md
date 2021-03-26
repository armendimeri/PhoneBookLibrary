# PhoneBookLibrary - Validata
Phonebook Library with API as an interface, XML documentation for API, Nunit tests

Assumptions Made
 - Documentation via Swagger and XML
 - Save to file (non-text and not as embedded database) assumption was made to save as binary file
 - No validation requirements were specified, therefore validation was added as required to all fields, and custom validation for number type (Work, Cellphone or Home).
 - Iterating over list in alphabetical order implemented in main fuction via sort parameter
 - IDs for entries were not specified, but as multiple entries for the same name are allowed, they are handled by the library itself via Guid.
 - API requests were not specified as to what they will return or accept as parameters, the assumptions made and how these look specified via xml and swagger.
