using System;
using System.Collections.Generic;
using System.Data;
using OLMS.DAL.Interfaces;
using OLMS.Models.Entities;
using OLMS.DAL.Repositories;
using System.Configuration;

namespace OLMS.BLL.Services
{
    public class MaterialService
    {
        private readonly IMaterialRepository _repo;

        public MaterialService()
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            _repo = new MaterialRepository(cs);
        }

        public List<Material> GetAll()
        {
            DataTable dt = _repo.GetAll();
            List<Material> materials = new List<Material>();
            foreach (DataRow row in dt.Rows)
            {
                materials.Add(MapRow(row));
            }
            return materials;
        }

        public Material GetById(int materialId)
        {
            DataRow row = _repo.GetById(materialId);
            if (row != null)
            {
                return MapRow(row);
            }
            return null;
        }

        public List<Material> GetByWorkshopId(int workshopId)
        {
            DataTable dt = _repo.GetByWorkshopId(workshopId);
            List<Material> materials = new List<Material>();
            foreach (DataRow row in dt.Rows)
            {
                materials.Add(MapRow(row));
            }
            return materials;
        }

        public void Add(Material material)
        {
            if (_repo.Add(material) <= 0)
                throw new Exception("Failed to add material.");
        }

        public void Update(Material material)
        {
            if (_repo.Update(material) <= 0)
                throw new Exception("Failed to update material.");
        }

        public void Delete(int materialId)
        {
            if (_repo.Delete(materialId) <= 0)
                throw new Exception("Failed to delete material.");
        }

        private Material MapRow(DataRow row)
        {
            return new Material
            {
                MaterialID = Convert.ToInt32(row["MaterialID"]),
                WorkshopID = Convert.ToInt32(row["WorkshopID"]),
                Title = row["Title"].ToString(),
                FilePath = row["FilePath"].ToString(),
                Description = row["Description"].ToString(),
                UploadDate = Convert.ToDateTime(row["UploadDate"]),
                IsActive = Convert.ToBoolean(row["IsActive"]),
                WorkshopTitle = row["WorkshopTitle"].ToString()
            };
        }
    }
}
