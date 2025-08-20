using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using OLMS.DAL.Interfaces;
using OLMS.DAL.Repositories;
using OLMS.Models.Entities;

namespace OLMS.BLL.Services
{
    public class WorkshopService
    {
        private readonly IWorkshopRepository _repo;

        public WorkshopService()
        {
            string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;
            _repo = new WorkshopRepository(cs);
        }

        public List<Workshop> GetAll()
        {
            DataTable dt = _repo.GetAll();
            List<Workshop> workshops = new List<Workshop>();
            foreach (DataRow row in dt.Rows)
            {
                workshops.Add(new Workshop
                {
                    WorkshopID = Convert.ToInt32(row["WorkshopID"]),
                    Title = row["Title"].ToString(),
                    Description = row["Description"].ToString(),
                    TrainerID = Convert.ToInt32(row["TrainerID"]),
                    StartDate = Convert.ToDateTime(row["StartDate"]),
                    EndDate = Convert.ToDateTime(row["EndDate"]),
                    Location = row["Location"].ToString(),
                    IsActive = Convert.ToBoolean(row["IsActive"])
                });
            }
            return workshops;
        }

        public void Add(Workshop workshop)
        {
            if (_repo.Add(workshop) <= 0) throw new Exception("Failed to add workshop.");
        }

        public void Update(Workshop workshop)
        {
            if (_repo.Update(workshop) <= 0) throw new Exception("Failed to update workshop.");
        }

        public void Delete(int id)
        {
            if (_repo.Delete(id) <= 0) throw new Exception("Failed to delete workshop.");
        }
    }
}
