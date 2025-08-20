using OLMS.DAL.Interfaces;
using OLMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace OLMS.BLL.Services
{
    public class TrainerService
    {
        private readonly ITrainerRepository _repo;

        public TrainerService(ITrainerRepository repo)
        {
            _repo = repo;
        }

        public List<Trainer> GetAll()
        {
            DataTable dt = _repo.GetAll();
            List<Trainer> trainers = new List<Trainer>();

            foreach (DataRow row in dt.Rows)
            {
                trainers.Add(new Trainer
                {
                    TrainerID = Convert.ToInt32(row["TrainerID"]),
                    Name = row["Name"].ToString(),
                    Email = row["Email"].ToString(),
                    Phone = row["Phone"].ToString(),
                    Expertise = row["Expertise"].ToString(),
                    CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                });
            }

            return trainers;
        }

        public Trainer GetById(int trainerId)
        {
            DataTable dt = _repo.GetById(trainerId);
            if (dt.Rows.Count == 0) return null;

            DataRow row = dt.Rows[0];
            return new Trainer
            {
                TrainerID = Convert.ToInt32(row["TrainerID"]),
                Name = row["Name"].ToString(),
                Email = row["Email"].ToString(),
                Phone = row["Phone"].ToString(),
                Expertise = row["Expertise"].ToString(),
                CreatedDate = Convert.ToDateTime(row["CreatedDate"])
            };
        }

        public void Add(Trainer trainer)
        {
            int result = _repo.Add(trainer);
            if (result <= 0) throw new Exception("Failed to add trainer.");
        }

        public void Update(Trainer trainer)
        {
            int result = _repo.Update(trainer);
            if (result <= 0) throw new Exception("Failed to update trainer.");
        }

        public void Delete(int trainerId)
        {
            int result = _repo.Delete(trainerId);
            if (result <= 0) throw new Exception("Failed to delete trainer.");
        }
    }
}
