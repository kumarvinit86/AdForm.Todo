# AdForm.Todo
Assignment for Adform Client to create Todo Service.
 > This app is desgined using `Hexagonal Architecture` and `Reference Architecture`.

 > Must have VS 2019 and .net core 3.1
## Tech Stack:
* .Net core 3.1
* Xunit
* Entityframework
* Swashbuckle
* Jwt Token
* SimpleInjector
* Newtonsoft.Json
* AutoMapper
* AutoFixture
* Moq
* MockQueryable.Moq
* FluentAssertions
* SeriLog
* Sonarlint `dynamic code analyzer`

## How to configure the Application
* Clone the application 
* Restore the packages of solution
* Project inclued Database migration, to create database please execute below command in Package Manager Console
    ### `Update-Database`

* Build the solution 
* Start application by F5
### under diagram directory we can see the two level C4 modeling walkthrough of application architect.
Diagrams are created in `.drawio` format. To view/open the diagram please follow the below steps.
* Add `Draw.io Integration` extension to VS Code. 
![image](https://user-images.githubusercontent.com/15344216/138424800-3b294fc9-7e9e-48b6-9c2e-655365fda44d.png)

* Open the diagrams in VS code. 
Note: These diagrams can be drilled to next level 3 and 4. But for now we only have 2 levels

## Note: Use Sonarlint as code analysis tool. code ruleset file is already updated into the project, so just install sonarlint extension.


# Graphql query Examples : 

> Token Example
`{
  "Authorization":"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiMiIsImp0aSI6IjU1MWQ5NGFkLTkxM2QtNDg0OC05Y2NmLTU1M2M0NzU1MGQzNCIsImV4cCI6MTYzNDY1NzY2NCwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzMjMiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo0NDMyMyJ9.uuCUwWDx8hgJYvPTQnUlO9K7dvDdNrlBeWclQxLpWtc"
}`

### To Get Lable
{
 labels{
  id,
  name
}
}

### To Get Lable by id
{
 labelsbyId(id:3){
  id,
  name
}
}


### To Get Item
{
 item(pagingDataRequest: {pageNumber:1, pageSize:5}){
 item{
  name
}
}
}

### To Get Item by Id
{
 itembyId(id:2){
  name,
  labelName,
  id
}
}

### To Get List
{
 list(pagingDataRequest: {pageNumber:1, pageSize:5}){
 item{
  name
}
}
}

### To Get List by Id
{
 listbyId(id:2){
name,
  labelName,
  id
}
}

###  Mutation create lable
mutation{
  create(label:{id:0, name: "jai ho"})
}

###  Mutation delete lable by id
mutation{
  deleteLabelbyId(id:4)
}

###  Add todo Item
mutation{
  addToDoItem(item:{id:0,name:"New Item", labelName:"new lable",userId:0})
}

###  Update Lable to Item
mutation{
  updateLabeltoItem(itemId:3, labelId:7)
}

###  Delete Item by id
mutation{
  deleteToDoItembyId(id:2)
}

###  Add todo List
mutation{
  addToDoList(itemList:{id:0,name:"New Item list3", labelName:"new lable list",userId:0})
}

###  Update Lable to List
mutation{
  updateLabeltoItem(itemId:2, labelId:5)
}

###  Delete List by id
mutation{
  deleteToDoItembyId(id:2)
}
