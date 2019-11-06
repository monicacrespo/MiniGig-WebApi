# Mini Gig Web API RESTFul Service
MiniGigWebApi is a ASP.NET Web API 2 service created in Visual Studio 2017 Professional, .NET Framework 4.7.2, ASP.NET Web API 2 and Entity Framework 6

The motivation is to build a RESTful application to be consumed by MiniGig-ASP.NET-MVC5.


The solution contains five projects:
* MiniGigWebApi
* MiniGigWebApi.Data	
* MiniGigWebApi.Domain	
* MiniGigWebApi.Services
* MiniGigWebApi.SharedKernel.Data

The code illustrates the following topics:

* Implementation of a RESTful service using ASP.Net Web API 
* Encapsulation of Entity Framework 6 using GenericRepository for CRUD operations
* Dependency Injection in Web API using Autofac.WebApi2
* Use of Automapper to map the DTOs (Data Transfer Objects) to the model objects
* Allow for error handling in an HTTP way
* Testing API using POSTMAN


Here's the URI Design

| Resource  				    | GET (read)	  | POST (create)	| PUT (update)	| DELETE (delete) |
| ------------------------- | ------------- | ------------- | ------------ | --------------- |
| api/gigs 					    | Get List		  | Create Item	| Error			| Error			   |
| api/gigs/2  			       | Get Item		  | Error			| Update Item	| Delete Item	   |
| api/gigs?page=1&pageSize=4| Get 4 Items** | Error			| Error			| Error			   |

** The result will be sorted (OrderByDescending) by gig's date before its paged it.
   That's because page 2 of a list of items sorted by Id is much different than page 2 of a list of items sorted by Date.


| Resource  				    | GET (read)	| POST (insert)	  | PUT (update)	    | DELETE (delete)	    |
| ------------------------- | ----------- | ----------------- | ----------------- | --------------------- |
| api/gigs 					    | List		   | New Item			  | Status Code Only**| Status Code Only**	 |
| api/gigs/2  				    | Item			| Status Code Only**| Updated Item	    | Status Code Only (200)|
| api/gigs?page=1&pageSize=4| 1st Page*** | Status Code Only**| Status Code Only**| Status Code Only**	 |

** Error Status Code (405 Method Not Allowed)

*** For pagination, both page and pagesize in the query string need to be greater than zero.
http://.../api/gigs?page=0&pageSize=4 will return the same result as http://.../api/gigs, the list sorted (OrderBy) by Id.



How to test your GET Gig Request With Postman

![PostmanGetGig](https://github.com/monicacrespo/MiniGig_WebApi/blob/master/MiniGigWebApi/Images/PostmanGetGig.JPG)


GET Gigs Request 

![PostmanGetGigs](https://github.com/monicacrespo/MiniGig_WebApi/blob/master/MiniGigWebApi/Images/PostmanGetGigs.JPG)


GET the First Page with Four Gigs Request

![PostmanGetGig](https://github.com/monicacrespo/MiniGig_WebApi/blob/master/MiniGigWebApi/Images/PostmanGetPagedGigs.JPG)


POST Request to Create a Gig 

![PostmanGetPagedGigs](https://github.com/monicacrespo/MiniGig_WebApi/blob/master/MiniGigWebApi/Images/PostmanCreateGig.JPG)


DELETE Gig Request

![PostmanDeleteGig](https://github.com/monicacrespo/MiniGig_WebApi/blob/master/MiniGigWebApi/Images/PostmanDeleteGig.JPG)

We can see that the Gig with Id 18 has been deleted by trying to request that gig 

![PostmanGetGigNotFound](https://github.com/monicacrespo/MiniGig_WebApi/blob/master/MiniGigWebApi/Images/PostmanGetGigNotFound.JPG)



# Getting Started
To run the sample locally from Visual Studio:
* Build the solution
* Open the Package Manager Console (Tools > NuGet Package Manager > Package Manager Console)
* Select the MiniGigWebApi.Data as Default Project
* Enter the following command: Update-Database –Verbose
* Press F5 to debug

