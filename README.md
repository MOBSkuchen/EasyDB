# EasyDB

## About
### EasyDB is a simple experimental DataBase made in C#.
### You can use it as a Library in your project,
### or via the Console using the ConsoleDriver.cs file.
---------
## How to use it?
### When you compile the project using .NET / .NET CORE,
### you should see a "> " appearing in the console.
## Commands
### To create a new DataBase, type "create <name>", (like this : "create myDataBase")
### To create a new Document, type "new <database> <document>" (like this : "new myDataBase user1")
### To create a new Key in a Document, type "inject <database> <document> <key> <value>" (like this : "inject myDataBase user1 age 18")
### To list all Documents in a DataBase, type "list <database>" (like this : "list myDataBase")
### To list all Keys in a Document, type "doc <database> <document>" (like this : "doc myDataBase user1")
### To delete a Document from a DataBase, type "del <database> <document>" (like this : "del myDataBase user1")
