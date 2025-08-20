USE [OLMSDB]
GO

-- ==================================
-- Author: Sachin Kumar
-- Created on: 2025-08-13
-- Purpose: Db Script for Online Learning Management System.
-- Stored Procedures scripts.
-- ==================================



--=============
-- 1. Roles
--=============

CREATE PROCEDURE sp_Role_Add
@RoleName NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Roles (RoleName) VALUES (@RoleName);
    SELECT CAST(SCOPE_IDENTITY() AS INT) AS NewId;
END
GO


CREATE PROCEDURE sp_Role_Update
@RoleID INT,
@RoleName NVARCHAR(50)
AS
BEGIN
    UPDATE Roles SET RoleName = @RoleName WHERE RoleID = @RoleID;
END
GO


CREATE PROCEDURE sp_Role_Delete
@RoleID INT
AS
BEGIN
    DELETE FROM Roles WHERE RoleID = @RoleID;
END
GO


CREATE PROCEDURE sp_Role_GetAll
AS
BEGIN
    SELECT RoleID, RoleName FROM Roles ORDER BY RoleName;
END
GO


--------------------------------------------------------------------------------------------



--=============
-- 2. Users
--=============

CREATE PROCEDURE sp_User_Add
@FullName NVARCHAR(100),
@Email NVARCHAR(100),
@PasswordHash NVARCHAR(255),
@RoleID INT,
@IsActive BIT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Users (FullName, Email, PasswordHash, RoleID, DateCreated, IsActive)
    VALUES (@FullName, @Email, @PasswordHash, @RoleID, GETDATE(), @IsActive);
    SELECT CAST(SCOPE_IDENTITY() AS INT) AS NewId;
END
GO


CREATE PROCEDURE sp_User_Update
@UserID INT,
@FullName NVARCHAR(100),
@Email NVARCHAR(100),
@PasswordHash NVARCHAR(255),
@RoleID INT,
@IsActive BIT
AS
BEGIN
    UPDATE Users
    SET FullName = @FullName,
        Email = @Email,
        PasswordHash = @PasswordHash,
        RoleID = @RoleID,
        IsActive = @IsActive
    WHERE UserID = @UserID;
END
GO


CREATE PROCEDURE sp_User_Delete
@UserID INT
AS
BEGIN
    DELETE FROM Users WHERE UserID = @UserID;
END
GO


CREATE PROCEDURE sp_User_GetAll
AS
BEGIN
    SELECT u.UserID, u.FullName, u.Email, u.RoleID, r.RoleName, u.DateCreated, u.IsActive
    FROM Users u
    LEFT JOIN Roles r ON u.RoleID = r.RoleID
    ORDER BY u.FullName;
END
GO


CREATE PROCEDURE sp_User_GetById
@UserID INT
AS
BEGIN
    SELECT u.UserID, u.FullName, u.Email, u.RoleID, r.RoleName, u.DateCreated, u.IsActive
    FROM Users u
    LEFT JOIN Roles r ON u.RoleID = r.RoleID
    WHERE u.UserID = @UserID;
END
GO


CREATE PROCEDURE sp_User_ValidateLogin
    @Email NVARCHAR(100),
    @PasswordHash NVARCHAR(255)
AS
BEGIN
    SELECT UserID, FullName, Email, RoleID, IsActive
    FROM Users
    WHERE Email = @Email AND PasswordHash = @PasswordHash AND IsActive = 1;
END
GO


CREATE PROCEDURE sp_User_GetByEmail
@Email NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1 
        UserID,
        FullName,
        Email,
        PasswordHash,
        RoleID,
        IsActive,
        DateCreated
    FROM Users
    WHERE Email = @Email;
END
GO

-------------------------------------------------------------------------------------------------



--=============
-- 3. Courses
--=============

CREATE PROCEDURE sp_Course_Add
@Title NVARCHAR(200),
@Description NVARCHAR(MAX),
@Category NVARCHAR(100),
@CreatedBy INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Courses (Title, Description, Category, CreatedBy, CreatedDate, IsActive)
    VALUES (@Title, @Description, @Category, @CreatedBy, GETDATE(), 1);
    SELECT CAST(SCOPE_IDENTITY() AS INT) AS NewId;
END
GO


CREATE PROCEDURE sp_Course_Update
@CourseID INT,
@Title NVARCHAR(200),
@Description NVARCHAR(MAX),
@Category NVARCHAR(100),
@IsActive BIT
AS
BEGIN
    UPDATE Courses
    SET Title = @Title,
        Description = @Description,
        Category = @Category,
        IsActive = @IsActive
    WHERE CourseID = @CourseID;
END
GO


CREATE PROCEDURE sp_Course_Delete
@CourseID INT
AS
BEGIN
    DELETE FROM Courses WHERE CourseID = @CourseID;
END
GO


CREATE PROCEDURE sp_Course_GetAll
AS
BEGIN
    SELECT c.CourseID, c.Title, c.Description, c.Category, c.CreatedBy, u.FullName AS CreatedByName, c.CreatedDate, c.IsActive
    FROM Courses c
    LEFT JOIN Users u ON c.CreatedBy = u.UserID
    ORDER BY c.CreatedDate DESC;
END
GO


CREATE PROCEDURE sp_Course_GetById
@CourseID INT
AS
BEGIN
    SELECT c.CourseID, c.Title, c.Description, c.Category, c.CreatedBy, u.FullName AS CreatedByName, c.CreatedDate, c.IsActive
    FROM Courses c
    LEFT JOIN Users u ON c.CreatedBy = u.UserID
    WHERE c.CourseID = @CourseID;
END
GO


CREATE PROCEDURE sp_Course_GetAllActive
AS
BEGIN
    SELECT * 
    FROM Courses
    WHERE IsActive = 1
    ORDER BY CreatedDate DESC
END
GO


CREATE PROCEDURE sp_Course_SoftDelete
@CourseID INT
AS
BEGIN
    UPDATE Courses
    SET IsActive = 0
    WHERE CourseID = @CourseID
END
GO

-------------------------------------------------------------------------------------------------




--=============
-- 4. Modules
--=============

CREATE PROCEDURE sp_Module_Add
@CourseID INT,
@Title NVARCHAR(200),
@Description NVARCHAR(MAX),
@SortOrder INT
AS
BEGIN
    INSERT INTO Modules (CourseID, Title, Description, SortOrder)
    VALUES (@CourseID, @Title, @Description, @SortOrder);
    SELECT CAST(SCOPE_IDENTITY() AS INT) AS NewId;
END
GO


CREATE PROCEDURE sp_Module_Update
@ModuleID INT,
@Title NVARCHAR(200),
@Description NVARCHAR(MAX),
@SortOrder INT
AS
BEGIN
    UPDATE Modules
    SET Title = @Title,
        Description = @Description,
        SortOrder = @SortOrder
    WHERE ModuleID = @ModuleID;
END
GO


CREATE PROCEDURE sp_Module_Delete
@ModuleID INT
AS
BEGIN
    DELETE FROM Modules WHERE ModuleID = @ModuleID;
END
GO


CREATE PROCEDURE sp_Module_GetByCourse
@CourseID INT
AS
BEGIN
    SELECT ModuleID, CourseID, Title, Description, SortOrder
    FROM Modules
    WHERE CourseID = @CourseID
    ORDER BY SortOrder;
END
GO


CREATE PROCEDURE sp_Module_GetById
@ModuleID INT
AS
BEGIN
    SELECT ModuleID, CourseID, Title, Description, SortOrder
    FROM Modules
    WHERE ModuleID = @ModuleID;
END
GO

-----------------------------------------------------------------------------------------------




--=============
-- 5. Lessons
--=============

CREATE PROCEDURE sp_Lesson_Add
@ModuleID INT,
@Title NVARCHAR(200),
@Content NVARCHAR(MAX),
@VideoUrl NVARCHAR(500),
@SortOrder INT
AS
BEGIN
    INSERT INTO Lessons (ModuleID, Title, Content, VideoUrl, SortOrder)
    VALUES (@ModuleID, @Title, @Content, @VideoUrl, @SortOrder);
    SELECT CAST(SCOPE_IDENTITY() AS INT) AS NewId;
END
GO


CREATE PROCEDURE sp_Lesson_Update
@LessonID INT,
@Title NVARCHAR(200),
@Content NVARCHAR(MAX),
@VideoUrl NVARCHAR(500),
@SortOrder INT
AS
BEGIN
    UPDATE Lessons
    SET Title = @Title, Content = @Content, VideoUrl = @VideoUrl, SortOrder = @SortOrder
    WHERE LessonID = @LessonID;
END
GO


CREATE PROCEDURE sp_Lesson_Delete
@LessonID INT
AS
BEGIN
    DELETE FROM Lessons WHERE LessonID = @LessonID;
END
GO


CREATE PROCEDURE sp_Lesson_GetByModule
@ModuleID INT
AS
BEGIN
    SELECT LessonID, ModuleID, Title, Content, VideoUrl, SortOrder
    FROM Lessons
    WHERE ModuleID = @ModuleID
    ORDER BY SortOrder;
END
GO


CREATE PROCEDURE sp_Lesson_GetById
    @LessonID INT
AS
BEGIN
    SELECT LessonID, ModuleID, Title, Content, VideoUrl, SortOrder
    FROM Lessons
    WHERE LessonID = @LessonID;
END
GO

------------------------------------------------------------------------------------------



--================
-- 6. Enrollments
--================

CREATE PROCEDURE sp_Enrollment_Add
@CourseID INT,
@UserID INT,
@Status NVARCHAR(50)
AS
BEGIN
    INSERT INTO Enrollments (CourseID, UserID, EnrolledDate, Status)
    VALUES (@CourseID, @UserID, GETDATE(), @Status);
    SELECT CAST(SCOPE_IDENTITY() AS INT) AS NewId;
END
GO


CREATE PROCEDURE sp_Enrollment_UpdateStatus
@EnrollmentID INT,
@Status NVARCHAR(50)
AS
BEGIN
    UPDATE Enrollments
    SET Status = @Status
    WHERE EnrollmentID = @EnrollmentID;
END
GO


CREATE PROCEDURE sp_Enrollment_Delete
@EnrollmentID INT
AS
BEGIN
    DELETE FROM Enrollments WHERE EnrollmentID = @EnrollmentID;
END
GO


CREATE PROCEDURE sp_Enrollment_GetByUser
@UserID INT
AS
BEGIN
    SELECT e.EnrollmentID, e.CourseID, c.Title AS CourseTitle, e.UserID, e.EnrolledDate, e.Status
    FROM Enrollments e
    LEFT JOIN Courses c ON e.CourseID = c.CourseID
    WHERE e.UserID = @UserID
    ORDER BY e.EnrolledDate DESC;
END
GO


CREATE PROCEDURE sp_Enrollment_GetByCourse
@CourseID INT
AS
BEGIN
    SELECT e.EnrollmentID, e.CourseID, e.UserID, u.FullName AS UserName, e.EnrolledDate, e.Status
    FROM Enrollments e
    LEFT JOIN Users u ON e.UserID = u.UserID
    WHERE e.CourseID = @CourseID
    ORDER BY e.EnrolledDate DESC;
END
GO

-------------------------------------------------------------------------------------------------------



--================
-- 7. Quizzes
--================

CREATE PROCEDURE sp_Quiz_Add
@CourseID INT,
@Title NVARCHAR(200),
@TotalMarks INT
AS
BEGIN
    INSERT INTO Quizzes (CourseID, Title, TotalMarks)
    VALUES (@CourseID, @Title, @TotalMarks);
    SELECT CAST(SCOPE_IDENTITY() AS INT) AS NewId;
END
GO


CREATE PROCEDURE sp_Quiz_Update
@QuizID INT,
@Title NVARCHAR(200),
@TotalMarks INT
AS
BEGIN
    UPDATE Quizzes
    SET Title = @Title, TotalMarks = @TotalMarks
    WHERE QuizID = @QuizID;
END
GO


CREATE PROCEDURE sp_Quiz_Delete
@QuizID INT
AS
BEGIN
    DELETE FROM Quizzes WHERE QuizID = @QuizID;
END
GO


CREATE PROCEDURE sp_Quiz_GetByCourse
@CourseID INT
AS
BEGIN
    SELECT QuizID, CourseID, Title, TotalMarks FROM Quizzes WHERE CourseID = @CourseID;
END
GO


CREATE PROCEDURE sp_Quiz_GetById
@QuizID INT
AS
BEGIN
    SELECT QuizID, CourseID, Title, TotalMarks FROM Quizzes WHERE QuizID = @QuizID;
END
GO


CREATE PROCEDURE sp_Quiz_GetAll
AS
BEGIN
    SELECT *
    FROM Quizzes
END
GO

---------------------------------------------------------------------------------------------



--================
-- 8. Questions
--================

CREATE PROCEDURE sp_Question_Add
@QuizID INT,
@QuestionText NVARCHAR(MAX),
@Marks INT
AS
BEGIN
    INSERT INTO Questions (QuizID, QuestionText, Marks)
    VALUES (@QuizID, @QuestionText, @Marks);
    SELECT CAST(SCOPE_IDENTITY() AS INT) AS NewId;
END
GO


CREATE PROCEDURE sp_Question_Update
@QuestionID INT,
@QuestionText NVARCHAR(MAX),
@Marks INT
AS
BEGIN
    UPDATE Questions SET QuestionText = @QuestionText, Marks = @Marks WHERE QuestionID = @QuestionID;
END
GO


CREATE PROCEDURE sp_Question_Delete
@QuestionID INT
AS
BEGIN
    DELETE FROM Questions WHERE QuestionID = @QuestionID;
END
GO


CREATE PROCEDURE sp_Question_GetByQuiz
@QuizID INT
AS
BEGIN
    SELECT QuestionID, QuizID, QuestionText, Marks FROM Questions WHERE QuizID = @QuizID;
END
GO


CREATE PROCEDURE sp_Question_GetById
@QuestionID INT
AS
BEGIN
    SELECT QuestionID, QuizID, QuestionText, Marks FROM Questions WHERE QuestionID = @QuestionID;
END
GO

--------------------------------------------------------------------------------------------------



--================
-- 9. Options
--================

CREATE PROCEDURE sp_Option_Add
@QuestionID INT,
@OptionText NVARCHAR(MAX),
@IsCorrect BIT
AS
BEGIN
    INSERT INTO Options (QuestionID, OptionText, IsCorrect)
    VALUES (@QuestionID, @OptionText, @IsCorrect);
    SELECT CAST(SCOPE_IDENTITY() AS INT) AS NewId;
END
GO


CREATE PROCEDURE sp_Option_Update
@OptionID INT,
@OptionText NVARCHAR(MAX),
@IsCorrect BIT
AS
BEGIN
    UPDATE Options SET OptionText = @OptionText, IsCorrect = @IsCorrect WHERE OptionID = @OptionID;
END
GO


CREATE PROCEDURE sp_Option_Delete
@OptionID INT
AS
BEGIN
    DELETE FROM Options WHERE OptionID = @OptionID;
END
GO


CREATE PROCEDURE sp_Option_GetByQuestion
@QuestionID INT
AS
BEGIN
    SELECT OptionID, QuestionID, OptionText, IsCorrect FROM Options WHERE QuestionID = @QuestionID;
END
GO

----------------------------------------------------------------------------------------------------------



--==================
-- 10. QuizAttempts
--==================

CREATE PROCEDURE sp_QuizAttempt_Add
@QuizID INT,
@UserID INT,
@Score INT
AS
BEGIN
    INSERT INTO QuizAttempts (QuizID, UserID, Score, AttemptDate)
    VALUES (@QuizID, @UserID, @Score, GETDATE());
    SELECT CAST(SCOPE_IDENTITY() AS INT) AS NewId;
END
GO


CREATE PROCEDURE sp_QuizAttempt_GetByUser
@UserID INT
AS
BEGIN
    SELECT AttemptID, QuizID, UserID, Score, AttemptDate FROM QuizAttempts WHERE UserID = @UserID ORDER BY AttemptDate DESC;
END
GO


CREATE PROCEDURE sp_QuizAttempt_GetByQuiz
@QuizID INT
AS
BEGIN
    SELECT AttemptID, QuizID, UserID, Score, AttemptDate FROM QuizAttempts WHERE QuizID = @QuizID ORDER BY AttemptDate DESC;
END
GO

----------------------------------------------------------------------------------------------------------------



--=======================
-- 11. Files (Resources)
--=======================

CREATE PROCEDURE sp_File_Add
@LessonID INT,
@FileName NVARCHAR(255),
@FilePath NVARCHAR(500)
AS
BEGIN
    INSERT INTO Files (LessonID, FileName, FilePath, UploadedDate)
    VALUES (@LessonID, @FileName, @FilePath, GETDATE());
    SELECT CAST(SCOPE_IDENTITY() AS INT) AS NewId;
END
GO


CREATE PROCEDURE sp_File_Delete
@FileID INT
AS
BEGIN
    DELETE FROM Files WHERE FileID = @FileID;
END
GO


CREATE PROCEDURE sp_File_GetByLesson
@LessonID INT
AS
BEGIN
    SELECT FileID, LessonID, FileName, FilePath, UploadedDate FROM Files WHERE LessonID = @LessonID;
END
GO

--------------------------------------------------------------------------------------------------------



--=======================
-- 12. Notifications
--=======================

CREATE PROCEDURE sp_Notification_Add
@UserID INT,
@Message NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO Notifications (UserID, Message, IsRead, CreatedDate)
    VALUES (@UserID, @Message, 0, GETDATE());
END
GO


CREATE PROCEDURE sp_Notification_MarkAsRead
@NotificationID INT
AS
BEGIN
    UPDATE Notifications SET IsRead = 1 WHERE NotificationID = @NotificationID;
END
GO


CREATE PROCEDURE sp_Notification_GetByUser
@UserID INT
AS
BEGIN
    SELECT NotificationID, UserID, Message, IsRead, CreatedDate FROM Notifications WHERE UserID = @UserID ORDER BY CreatedDate DESC;
END
GO

--------------------------------------------------------------------------------------------------------