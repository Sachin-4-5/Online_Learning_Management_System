using OLMS.DAL.Interfaces;
using OLMS.Models.Entities;
using System;
using System.Data;
using System.Collections.Generic;
using OLMS.DAL.Repositories;

namespace OLMS.BLL.Services
{
    public class ModuleService
    {
        private readonly IModuleRepository _repo;

        public ModuleService()
        {
            _repo = new ModuleRepository();
        }

        public List<Module> GetByCourseId(int courseId)
        {
            DataTable dt = _repo.GetById(courseId);
            List<Module> modules = new List<Module>();
            foreach (DataRow row in dt.Rows)
            {
                modules.Add(new Module
                {
                    ModuleID = Convert.ToInt32(row["ModuleID"]),
                    CourseID = Convert.ToInt32(row["CourseID"]),
                    Title = row["Title"].ToString(),
                    Description = row["Description"].ToString(),
                    SortOrder = Convert.ToInt32(row["SortOrder"])
                });
            }
            return modules;
        }


        public List<Module> GetAll()
        {
            DataTable dt = _repo.GetAll();
            List<Module> modules = new List<Module>();
            foreach (DataRow row in dt.Rows)
            {
                modules.Add(new Module
                {
                    ModuleID = Convert.ToInt32(row["ModuleID"]),
                    CourseID = Convert.ToInt32(row["CourseID"]),
                    Title = row["Title"].ToString(),
                    Description = row["Description"].ToString(),
                    SortOrder = Convert.ToInt32(row["SortOrder"])
                });
            }
            return modules;
        }


        public void Add(Module module)
        {
            int result = _repo.Add(module);
            if (result <= 0) throw new Exception("Failed to add module.");
        }

        public void Update(Module module)
        {
            int result = _repo.Update(module);
            if (result <= 0) throw new Exception("Failed to update module.");
        }

        public void Delete(int moduleId)
        {
            int result = _repo.Delete(moduleId);
            if (result <= 0) throw new Exception("Failed to delete module.");
        }
    }
}