This Repo Contains the code for the server.  It contains three micro-services:  Test, Report and User that implement the backend logic of the appication.
The code is deployed in the Azure cloud in App Services, but I configured it to be deployed in docker as well, hence the docker configuration files.
The Microservices are exposed using Rest and a technology called GraphQL.  Below are the links to the microservice endpoints:

http://flexiontestapi.azurewebsites.net/graphql/
http://flexionreportapi.azurewebsites.net/graphql/
http://flexionuserapi.azurewebsites.net/graphql/

The code is built using a Domain Driven Design and Onion approach


The microservice endpoint also have a gateway microservice implemented using node.js, express server and apollo client.  Check the other repo 
to see this implementation.  All calls go through the node.js gateway and are then routed to the microservices under this repo.

If I had more time I would have implemented the micro-services using a messaging bus instead of rest.
