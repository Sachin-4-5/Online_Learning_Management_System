-- Script for db access (read/write) in IIS for windows

USE [master];
CREATE LOGIN [IIS APPPOOL\OnlineLearningManagementSystem] FROM WINDOWS;
GO

-- Access for INSET, UPDATE, DELETE, SELECT
USE OLMSDB;
CREATE USER [IIS APPPOOL\OnlineLearningManagementSystem] FOR LOGIN [IIS APPPOOL\OnlineLearningManagementSystem];
ALTER ROLE db_datareader ADD MEMBER [IIS APPPOOL\OnlineLearningManagementSystem];
ALTER ROLE db_datawriter ADD MEMBER [IIS APPPOOL\OnlineLearningManagementSystem];
GO

-- Access for Stored Procedures
USE OLMSDB;
GRANT EXECUTE TO [IIS APPPOOL\OnlineLearningManagementSystem];