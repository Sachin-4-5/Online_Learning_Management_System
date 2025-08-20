using System;
using System.Collections.Generic;
using System.Data;
using OLMS.DAL.Interfaces;
using OLMS.Models.Entities;
using OLMS.DAL.Repositories;
using System.Configuration;

namespace OLMS.BLL.Services
{
    public class ApprovalService
    {
        private readonly IApprovalRepository _repo;

        public ApprovalService()
        {
            string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            _repo = new ApprovalRepository(cs);
        }

        public List<Approval> GetAll()
        {
            DataTable dt = _repo.GetAll();
            List<Approval> approvals = new List<Approval>();
            foreach (DataRow row in dt.Rows)
            {
                approvals.Add(MapRow(row));
            }
            return approvals;
        }

        public Approval GetById(int approvalId)
        {
            DataRow row = _repo.GetById(approvalId);
            if (row != null)
            {
                return MapRow(row);
            }
            return null;
        }

        public List<Approval> GetByStudentId(int studentId)
        {
            DataTable dt = _repo.GetByStudentId(studentId);
            List<Approval> approvals = new List<Approval>();
            foreach (DataRow row in dt.Rows)
            {
                approvals.Add(MapRow(row));
            }
            return approvals;
        }

        public void Add(Approval approval)
        {
            if (_repo.Add(approval) <= 0)
                throw new Exception("Failed to add approval.");
        }

        public void Update(Approval approval)
        {
            if (_repo.Update(approval) <= 0)
                throw new Exception("Failed to update approval.");
        }

        public void Delete(int approvalId)
        {
            if (_repo.Delete(approvalId) <= 0)
                throw new Exception("Failed to delete approval.");
        }

        private Approval MapRow(DataRow row)
        {
            return new Approval
            {
                ApprovalID = Convert.ToInt32(row["ApprovalID"]),
                StudentID = Convert.ToInt32(row["StudentID"]),
                WorkshopID = Convert.ToInt32(row["WorkshopID"]),
                MaterialID = row["MaterialID"] != DBNull.Value ? (int?)Convert.ToInt32(row["MaterialID"]) : null,
                Status = row["Status"].ToString(),
                ApprovedBy = row["ApprovedBy"] != DBNull.Value ? row["ApprovedBy"].ToString() : null,
                ApprovalDate = row["ApprovalDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["ApprovalDate"]) : null,
                Comments = row["Comments"] != DBNull.Value ? row["Comments"].ToString() : null,
                CreatedOn = Convert.ToDateTime(row["CreatedOn"])
            };
        }
    }
}
