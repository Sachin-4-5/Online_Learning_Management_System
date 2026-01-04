## Online Learning Management System 

### 📘 Overview  
The Online Learning Management System (OLMS) is a web-based platform built using ASP.NET Web Forms (3-tier architecture) to manage online courses, students, trainers, quizzes, and enrollments.
It provides a complete learning ecosystem where Admins can manage courses & users, Trainers can create quizzes/materials, and Students can enroll in courses, take quizzes, and track progress.

This project was developed step-by-step to simulate a real-world enterprise application and demonstrate concepts of ASP.NET Web Forms, ADO.NET, stored procedures, authentication, and security.
<br /> <br />
IIS URL Example (my local machine server) - http://localhost:8880/

---
<br />


### 🎯 Features
✅ Role-based modular design (Admin, Trainer, Student). <br />
✅ Clear separation of concerns using 3-tier architecture. <br />
✅ Secure authentication with password hashing (SHA256 + Salt). <br />
✅ Scalable database design with stored procedures. <br />
✅ Modern responsive UI with Bootstrap. <br />
✅ Robust exception handling with user-friendly error pages. <br />
✅ Automated email notifications for workshop updates. <br />
✅ Secure file upload with validation and structured storage. <br />
✅ Centralized logging & activity tracking for security and auditing (e.g., log4net, NLog, or custom logger). <br />

---
<br />


### 💡 Future Enhancements
✅ Migrate to ASP.NET Core MVC / Blazor. <br />
✅ Add Web API layer for mobile/React/Angular integration. <br />
✅ Implement JWT authentication for secure API access. <br />
✅ Add certificate generation for completed courses. <br />
✅ Introduce payment gateway integration for paid courses. <br />
✅ Add real-time chat/communication between students and trainers. <br >

---
<br />


### 🎓 Key Learnings
✅ Fundamentals of ASP.NET Web Forms (Master Pages, User Controls, State Management). <br />
✅ 3-tier architecture and clean separation of UI, Business logic, and Data access layers. <br />
✅ ADO.NET (SqlConnection, SqlCommand, SqlDataAdapter, DataReader, Stored Procedures). <br />
✅ Database design (tables, keys, constraints, relationships, stored procs). <br />
✅ Secure authentication with password hashing. <br />
✅ Scalable database design with stored procedures. <br />
✅ Modern responsive UI with Bootstrap. <br />
✅ IIS configuration, application pool identities, and deployment best practices. <br />
✅ Troubleshooting common permission and authentication issues in a Windows Server/IIS environment. 

---
<br />


### ⚡Technology Used
✅ Frontend: ASP.NET Web Forms, Bootstrap, CSS <br />
✅ Backend: C#, ADO.NET <br />
✅ Database: SQL Server (Stored Procedures) <br />
✅ Architecture: 3-tier (UI, BLL, DAL, Model) <br />
✅ Security: Password hashing (SHA256), SMTP, role-based access <br />
✅ Visual Studio 2022, SSMS, IIS <br />

---
<br />


### 🎓 Project Plan:
```
step-1: Project & environment setup (Solution + Projects, references)
step-2: Database creation & seed data.
step-3: Adding model classes for each tables. (POCO classes)
step-4: DAL: DbHelper + (Interface & Repository) skeletons.
step-5: BLL: Services, Authentication, Business rules.
step-6: UI: MasterPage + Register/Login + course listing (responsive Bootstrap).
step-7: Admin pages: manage courses (CRUD), secure by role.
step-8: Advanced: file storage, quiz engine, AJAX partial updates, charts, reports, email notifications.
step-9: Deploy to IIS and secure (production checklist).

```
---
<br />


### 📁 Project Structure
```
OnlineLearningManagementSystem.sln
│
├── OLMS.UI  (ASP.NET Web Forms)
│   ├── Account\
│   │    ├── Login.aspx
│   │    ├── Register.aspx
│   │   
│   ├── Admin\
│   │    ├── AddQuiz.aspx
│   │    ├── Approval.aspx
│   │    ├── Course.aspx
│   │    ├── Enrollment.aspx
│   │    ├── Lesson.aspx
│   │    ├── Material.aspx
│   │    ├── Module.aspx
│   │    ├── Reports.aspx
│   │    ├── Student.aspx
│   │    ├── Trainer.aspx
│   │    ├── User.aspx
│   │    ├── Workshop.aspx
│   │
│   ├── Courses\
│   │    ├── CourseDetail.aspx
│   │    ├── CourseList.aspx
│   │
│   ├── CSS\
│   ├── Images\
│   ├── JS\
│   ├── Images\
│   ├──
│   ├── MasterPages\
│   │    ├── Site.Master
│   │
│   ├── Scripts\
│   ├── Student\
│   │    ├── TakeQuiz.aspx
│   │
│   ├── Trainer\
│   │    ├── AddTrainer.aspx
│   ├──
│   ├── Default.aspx
│   ├── Global.asax
│   ├── Web.config
│
├── OLMS.Models (Class Library) - Models / Entities
│   ├── Common\
│   ├── Entities\ ()POCO classes for all db tables
│
├── OLMS.DAL    (Class Library) - Data Access Layer
│   ├── Interfaces\
│   ├── Repositories\
|
├── OLMS.BLL   (Class Library) - Business Logic Layer
│   ├── Helpers\
│   ├── Services\

```

---
<br />


### 📷 UI Screenshots
![Landing Page](https://github.com/Sachin-4-5/Online_Learning_Management_System/blob/main/Output%20Images/Landing%20Page.png)
![Admin Page](https://github.com/Sachin-4-5/Online_Learning_Management_System/blob/main/Output%20Images/Admin%20Page.png)
![Trainer Page](https://github.com/Sachin-4-5/Online_Learning_Management_System/blob/main/Output%20Images/Trainer%20Page.png)
![Student Page](https://github.com/Sachin-4-5/Online_Learning_Management_System/blob/main/Output%20Images/Student%20Page.png)

---
<br />


### ▶️ How to run the project ?
1️⃣ Clone the Repository - <b>git clone https://github.com/Sachin-4-5/Online_Learning_Management_System.git</b> <br />
2️⃣ Execute the provided SQL script to create OLMSDB with necessary tables and seed data. <br>
3️⃣ Open OnlineLearningManagementSystem.sln in Visual Studio (recommended version: 2017 or later). <br />
4️⃣ Update the connection string in web.config with your SQL Server instance details and authentication mode. <br />
5️⃣ Set the UI project as the startup project. <br >
6️⃣ Build the entire solution to restore DLL references. <br />
7️⃣ Publish the OLMS.UI project to a local folder (e.g., C:\inetpub\wwwroot\OnlineLearningManagementSystem). <br />
8️⃣ Press F5 or click Start to run the application through VS built-in IIS Server. <b>or</b> <br />
9️⃣ IIS Setup: <br />
    🔹Create a new site in IIS pointing to the published folder. <br />
    🔹Assign an application pool with .NET CLR Version v4.0 and set identity with appropriate DB access. <br />
    🔹Set folder permissions for the app pool identity. <br />
🔟 Access the site via your configured URL and port.

---
<br />



### 🤝 Contribution
Pull requests are welcome! To contribute:

1️⃣ Fork the repo <br />
2️⃣ Create a feature branch (git checkout -b feature-xyz) <br />
3️⃣ Commit changes (git commit -m "Added feature xyz") <br />
4️⃣ Push to your branch (git push origin feature-xyz) <br />
5️⃣ Create a pull request 

---
<br />
<br />








