Database setup:
1. Create a new database named 'RazorERPTechAssessment'
2. Run the following script to create the table and seed initial user data

USE RazorERPTechAssessment

CREATE TABLE [UserTest] (
    Id INT NOT NULL IDENTITY,
    Name NVARCHAR(100) NOT NULL,
    Role INT NOT NULL,
    Company INT NOT NULL,
    Password NVARCHAR(100) NOT NULL
);

INSERT INTO [UserTest] (Name, Role, Company, Password) VALUES ('John Doe', 0, 1, 'Password1234')
INSERT INTO [UserTest] (Name, Role, Company, Password) VALUES ('Jane Doe', 1, 1, 'Password1234')
INSERT INTO [UserTest] (Name, Role, Company, Password) VALUES ('James Doe', 1, 1, 'Password1234')
