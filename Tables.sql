-- ==================================
-- Author: Sachin Kumar
-- Created on: 2025-08-13
-- Purpose: Db Script for Online Learning Management System.
-- Database & Table scripts.
-- ==================================


/*
select * from Roles
select * from Users
select * from Courses
select * from Modules 
select * from Lessons
select * from Enrollments
select * from Quizzes
select * from Questions
select * from Options
select * from QuizAttempts
select * from Files
select * from Notifications
select * from Trainers
select * from Workshops
select * from Materials
select * from Students
select * from Approvals
select * from StudentQuizzes
select * from StudentAnswers
*/


-- =========================
-- ******* database ********
-- =========================

-- Create Database
CREATE DATABASE OLMSDB;
GO
USE OLMSDB;
GO



-- =========================
-- ******** Tables *********
-- =========================


-- 1. Roles
CREATE TABLE Roles (
    RoleID INT IDENTITY(1,1) PRIMARY KEY,
    RoleName NVARCHAR(50) NOT NULL UNIQUE
);

-- 2. Users
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    RoleID INT NOT NULL FOREIGN KEY REFERENCES Roles(RoleID),
    DateCreated DATETIME DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1
);

-- 3. Courses
CREATE TABLE Courses (
    CourseID INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    Category NVARCHAR(100),
    CreatedBy INT FOREIGN KEY REFERENCES Users(UserID),
    CreatedDate DATETIME DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1,
	IsDeleted BIT NOT NULL DEFAULT 0
);

-- 4. Modules
CREATE TABLE Modules (
    ModuleID INT IDENTITY(1,1) PRIMARY KEY,
    CourseID INT FOREIGN KEY REFERENCES Courses(CourseID),
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX),
    SortOrder INT
);

-- 5. Lessons
CREATE TABLE Lessons (
    LessonID INT IDENTITY(1,1) PRIMARY KEY,
    ModuleID INT FOREIGN KEY REFERENCES Modules(ModuleID),
    Title NVARCHAR(200) NOT NULL,
    Content NVARCHAR(MAX),
    VideoUrl NVARCHAR(500),
    SortOrder INT
);

-- 6. Enrollments
CREATE TABLE Enrollments (
    EnrollmentID INT IDENTITY(1,1) PRIMARY KEY,
    CourseID INT FOREIGN KEY REFERENCES Courses(CourseID),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    EnrolledDate DATETIME DEFAULT GETDATE(),
    Status NVARCHAR(50) DEFAULT 'Active'
);

-- 7. Quizzes
CREATE TABLE Quizzes (
    QuizID INT IDENTITY(1,1) PRIMARY KEY,
    CourseID INT FOREIGN KEY REFERENCES Courses(CourseID),
    Title NVARCHAR(200),
    TotalMarks INT
);

-- 8. Questions
CREATE TABLE Questions (
    QuestionID INT IDENTITY(1,1) PRIMARY KEY,
    QuizID INT FOREIGN KEY REFERENCES Quizzes(QuizID),
    QuestionText NVARCHAR(MAX),
    Marks INT
);

-- 9. Options
CREATE TABLE Options (
    OptionID INT IDENTITY(1,1) PRIMARY KEY,
    QuestionID INT FOREIGN KEY REFERENCES Questions(QuestionID),
    OptionText NVARCHAR(MAX),
    IsCorrect BIT
);

-- 10. Quiz Attempts
CREATE TABLE QuizAttempts (
    AttemptID INT IDENTITY(1,1) PRIMARY KEY,
    QuizID INT FOREIGN KEY REFERENCES Quizzes(QuizID),
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    Score INT,
    AttemptDate DATETIME DEFAULT GETDATE()
);

-- 11. Files / Resources
CREATE TABLE Files (
    FileID INT IDENTITY(1,1) PRIMARY KEY,
    LessonID INT FOREIGN KEY REFERENCES Lessons(LessonID),
    FileName NVARCHAR(255),
    FilePath NVARCHAR(500),
    UploadedDate DATETIME DEFAULT GETDATE()
);

-- 12. Notifications
CREATE TABLE Notifications (
    NotificationID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT FOREIGN KEY REFERENCES Users(UserID),
    Message NVARCHAR(MAX),
    IsRead BIT DEFAULT 0,
    CreatedDate DATETIME DEFAULT GETDATE()
);

-- 13. Trainers
CREATE TABLE Trainers (
    TrainerID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20),
    Expertise NVARCHAR(200),
    CreatedDate DATETIME DEFAULT GETDATE(),
	IsActive BIT NOT NULL DEFAULT 1
);

-- 14. Wrokshops
CREATE TABLE Workshops (
    WorkshopID INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    TrainerID INT NOT NULL,
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
    Location NVARCHAR(200) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (TrainerID) REFERENCES Trainers(TrainerID)
);

-- 15. Materials
CREATE TABLE Materials (
    MaterialID INT IDENTITY(1,1) PRIMARY KEY,
    WorkshopID INT NOT NULL,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    FilePath NVARCHAR(500) NULL,
    UploadDate DATETIME NOT NULL DEFAULT GETDATE(),
    IsActive BIT NOT NULL DEFAULT 1,
    FOREIGN KEY (WorkshopID) REFERENCES Workshops(WorkshopID)
);
ALTER TABLE Materials
DROP CONSTRAINT FK__Materials__Works__71D1E811;

ALTER TABLE Materials
ADD CONSTRAINT FK_Materials_Workshops
FOREIGN KEY (WorkshopID) REFERENCES Workshops(WorkshopID)
ON DELETE CASCADE;


-- 16. Students
CREATE TABLE Students (
    StudentID INT IDENTITY(1,1) PRIMARY KEY,
    WorkshopID INT NOT NULL,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(150) NOT NULL,
    Phone NVARCHAR(15),
    RegistrationDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Students_Workshop FOREIGN KEY (WorkshopID) REFERENCES Workshops(WorkshopID)
);

-- 17. Approvals
CREATE TABLE Approvals
(
    ApprovalID INT IDENTITY(1,1) PRIMARY KEY,
    StudentID INT NOT NULL,
    WorkshopID INT NOT NULL,           
    MaterialID INT NULL,               
    Status NVARCHAR(50) NOT NULL,      
    ApprovedBy NVARCHAR(100) NULL,     
    ApprovalDate DATETIME NULL,        
    Comments NVARCHAR(500) NULL,      
    CreatedOn DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Approvals_Student FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
    CONSTRAINT FK_Approvals_Workshop FOREIGN KEY (WorkshopID) REFERENCES Workshops(WorkshopID)
);

-- 18. StudentQuizzes
CREATE TABLE StudentQuizzes (
    StudentQuizID INT IDENTITY(1,1) PRIMARY KEY,
    StudentID INT NOT NULL, -- FK to Students table
    QuizID INT NOT NULL,    -- FK to Quizzes table
    Score INT NULL,
    AttemptDate DATETIME NOT NULL DEFAULT GETDATE()
);

-- 19. StudentAnswers
CREATE TABLE StudentAnswers (
    StudentAnswerID INT IDENTITY(1,1) PRIMARY KEY,
    StudentQuizID INT NOT NULL, -- FK to StudentQuizzes
    QuestionID INT NOT NULL,    -- FK to Questions
    AnswerText NVARCHAR(MAX),
    IsCorrect BIT
);





-- =========================
-- ****** SEED DATA ********
-- =========================

-- Roles
INSERT INTO Roles (RoleName) VALUES ('Admin'), ('Trainer'), ('Student');

-- Admin User
INSERT INTO Users (FullName, Email, PasswordHash, RoleID)
VALUES ('System Admin', 'admin@olms.com', 'HASHED_PASSWORD', 1);

-- Sample Courses
INSERT INTO Courses (Title, Description, Category, CreatedBy)
VALUES ('ASP.NET Web Forms Basics', 'Learn the fundamentals of ASP.NET Web Forms.', 'Programming', 1),
       ('Advanced SQL Server', 'Master SQL queries, performance tuning, and stored procedures.', 'Database', 1);

-- Sample Modules
INSERT INTO Modules (CourseID, Title, Description, SortOrder)
VALUES (1, 'Introduction', 'Getting started with ASP.NET Web Forms', 1),
       (1, 'Controls & Events', 'Working with server controls', 2);

-- Sample Lessons
INSERT INTO Lessons (ModuleID, Title, Content, SortOrder)
VALUES (1, 'What is ASP.NET Web Forms?', 'Content here...', 1),
       (2, 'TextBox and Button Controls', 'Content here...', 1);

-- Sample Trainer
INSERT INTO Trainers (Name, Email, Phone, IsActive)
VALUES 
('John Smith', 'john.smith@example.com', '9876543210', 1),
('Mary Johnson', 'mary.johnson@example.com', '9123456780', 1),
('David Lee', 'david.lee@example.com', '9988776655', 0);


-- Sample Quizzes
INSERT INTO Quizzes (CourseID, Title, TotalMarks)
VALUES (1, 'Basics of C#', 20)

/* --------------End------------------------*/