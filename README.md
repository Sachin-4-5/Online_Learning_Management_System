# Online Learning Management System 

## 📖 Overview  
An ASP.NET Web Forms application for managing online learning login, registrations, and users with role-based access. <br />
IIS URL Example (my local machine server) - 

---
<br />


## 📘 Project Overview
This system allows: <br />
🔹<br />
🔹<br />
🔹<br />
🔹<br />
🔹<br />

---
<br />


## 🎓 Key Learnings
🔹Practical application of 3-tier architecture concepts in ASP.NET Web Forms. <br />
🔹Managing state and security in Web Forms applications using sessions and role-based UI. <br />
🔹Working with ADO.NET for database connectivity and operations. <br />
🔹Using Bootstrap to create responsive, user-friendly interfaces. <br />
🔹Understanding IIS configuration, application pool identities, and deployment best practices. <br />
🔹Troubleshooting common permission and authentication issues in a Windows Server/IIS environment. <br />

---
<br />


## 🛠 Technology used
🔹ASP.NET Web Forms (.NET Framework 4.7.2) <br />
🔹C# <br />
🔹SQL Server (Database: WorkshopDB) <br />
🔹ADO.NET for data access <br />
🔹Bootstrap 4 for responsive UI <br />
🔹IIS for local hosting and deployment <br />

---
<br />


## 🚀 Features  
✅ <br />
✅ <br />
✅ <br />
✅ <br />
✅ <br />

---
<br />


## 🎓 Project structure
```
OnlineLearningManagementSystem.sln
│
├── OnlineLearningManagementSystem.UI  (ASP.NET Web Forms)
│   ├── Admin
│   │    ├── AdminMasterPage.Master
│   │    ├── Home.aspx
│   │    ├── Course.aspx
│   │    ├── Instructor.aspx
│   │    ├── Student.aspx
│   │    ├── Material.aspx
│   │    ├── Approval.aspx
│   ├── Student 
│   │    ├── StudentMasterPage.Master
│   │    ├── Home.aspx
│   │    ├── MyCourses.aspx
│   │    ├── Materials.aspx
│   │    ├── Exams.aspx
│   │    ├── Results.aspx
│   ├── Common
│   │    ├── Login.aspx
│   │    ├── Register.aspx
│   │    ├── ChangePassword.aspx
│   ├── CSS
│   ├── Images
│
├── OnlineLearningManagementSystem.BLL   (Class Library) - Business Logic Layer
│
├── OnlineLearningManagementSystem.Models (Class Library) - Models / Entities
│
├── OnlineLearningManagementSystem.DAL    (Class Library) - Data Access Layer

```

---
<br />



## 💡 Future Enhancements
🔹<br />
🔹<br />
🔹<br />
🔹<br />
🔹<br />

---
<br />



## ▶️ How to run the project ?
1️⃣ Clone the Repository - <b>git clone https://github.com/Sachin-4-5/online-learning-management-system.git</b> <br />
2️⃣ Execute the provided SQL script to create OLMSDB with necessary tables and seed data. <br>
3️⃣ Open OnlineLearningManagementSystem.sln in Visual Studio (recommended version: 2017 or later). <br />
4️⃣ Update the connection string in web.config with your SQL Server instance details and authentication mode. <br />
5️⃣ Set the UI project as the startup project. <br >
6️⃣ Build the entire solution to restore DLL references. <br />
7️⃣ Publish the OnlineLearningManagementSystem.UI project to a local folder (e.g., C:\inetpub\wwwroot\OnlineLearningManagementSystem). <br />
8️⃣ Press F5 or click Start to run the application through VS built-in IIS Server. <b>or</b> <br />
9️⃣ IIS Setup: <br />
    🔹Create a new site in IIS pointing to the published folder. <br />
    🔹Assign an application pool with .NET CLR Version v4.0 and set identity with appropriate DB access. <br />
    🔹Set folder permissions for the app pool identity. <br />
🔟 Access the site via your configured URL and port.

---
<br />



## 🤝 Contribution
Pull requests are welcome! To contribute:

1️⃣ Fork the repo <br />
2️⃣ Create a feature branch (git checkout -b feature-xyz) <br />
3️⃣ Commit changes (git commit -m "Added feature xyz") <br />
4️⃣ Push to your branch (git push origin feature-xyz) <br />
5️⃣ Create a pull request 

---
<br />
<br />













