# SpiralWorks Exam
### This solution is created using Microsoft Visual Studio 2017 and MS SQL Server 2017
### Project Solution is in Branch: Vanessa
#### Appsettings.json 
- Change the Data Source to your SQL Server. Database name is the Initial Catalog which is the SpiralWorks.

"ConnectionStrings": {
    "APIDBContext": <--"Data Source=(LocalDB)\\LocalHost;Initial Catalog=SpiralWorks;Persist Security Info=False; MultipleActiveResultSets=true"--> this part should be change according to your database configuration.
  },
  
#### Table and Column
- Table Name: Account
- Column Name: ID int (primary key), AccountID int, Username nvarchar(100), InitialBalance decimal(18,2) 

## Use the SpiralWorks_ExamDB file in repository
