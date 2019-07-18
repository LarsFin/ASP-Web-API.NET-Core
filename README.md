# ASP-Web-API.NET-Core

Below, user stories defined.

Api for Person, managing peron data.  
C - create  
R - read  
U - update  
D - delete  

Security should be looked into as well.

**Chain of responsibilities**  
Controller has endpoint `/api/people`, a request made to this endpoint is 
consumed and processed. The Controller calls the relevant method to the HTTP 
request, in turn it calls the PeronService for heavy logic involved in the 
relevant `person` entities. It makes a call to the PeopleRepository to carry 
out a relevant data system method (query, delete, etc...). The results of these 
actions are passed back up to the Controller to feed back to the user or 
integrated system.

## CREATE

```
As an admin,
So, I can create users,
I should be able to POST data via /api/people/id
```

|Entity of Responsibility|Complete?|
|:----------------------:|:-------:|
|Controller Logic|✓|
|Server Logic||

## READ

```
As an admin,
So, I can read all the data on the system,
I should be able to GET all Person data via /api/people
```

|Entity of Responsibility|Complete?|
|:----------------------:|:-------:|
|Controller Logic|✓|
|Server Logic|✓|

```
As an admin,
So, I can read data on a specific user,
I should be able to GET Person data via /api/people/id
```

|Entity of Responsibility|Complete?|
|:----------------------:|:-------:|
|Controller Logic|✓|
|Server Logic||

## UPDATE

```
As an admin,
So, I can make changes to data,
I should be able to PUT data via /api/people/id
```

|Entity of Responsibility|Complete?|
|:----------------------:|:-------:|
|Controller Logic|✓|
|Server Logic|✓|

## DELETE

```
As an admin,
So, I can remove users,
I should be able to DELETE data via /api/people/id
```

|Entity of Responsibility|Complete?|
|:----------------------:|:-------:|
|Controller Logic|✓|
|Server Logic||