This project is a Call Detail Record (CDR) management system designed to facilitate the tracking and analysis of call data within an organization.
It provides functionalities such as appending adata from csv file, calculating average call cost, determining the longest call duration, total calls count, total calls count and cost of calls in period
also provide information about longest calls and search calls by call ID

This solution consists of several projects:
*CallDetailRecorderAPI: This is a Web API project responsible for handling HTTP calls. It includes controllers to manage API endpoints for handling calls data.
Additionally, it may contain services like CSVServise to handle CSV files related to call details.

*Model Library: This project serves as a library containing models representing the data structure used within the application.
These models define the structure of entities such as call detail records, caller IDs, and any other relevant data entities.

*Data Access Library: This project is responsible for accessing and interacting with the underlying data storage, typically a database. 
It includes components such as the AppDbContext which acts as an interface between the application and the database. 
The repository pattern may also be implemented within this project to provide a layer of abstraction over the data access logic, facilitating easier data manipulation and retrieval operations.

Future Enhancements

Given more time, the following enhancements and considerations could be implemented:
- Designing a more sophisticated database model to separately store caller ID data, including information about the callers and their attributes.
- Implementing authentication and authorization mechanisms to secure the API endpoints and restrict access to authorized users.
- Adding pagination support for endpoints that return a large number of records to improve performance and user experience.
- Enhancing error handling and logging mechanisms to provide better feedback to users and assist in debugging.
- Implementing integration tests to ensure the seamless interaction between different components of the system.
