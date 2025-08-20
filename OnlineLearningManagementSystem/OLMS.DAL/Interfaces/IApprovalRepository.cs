using System;
using System.Data;
using OLMS.Models.Entities;

namespace OLMS.DAL.Interfaces
{
    public interface IApprovalRepository
    {
        DataTable GetAll();                      
        DataTable GetByStudentId(int studentId);  
        DataRow GetById(int approvalId);          
        int Add(Approval approval);               
        int Update(Approval approval);            
        int Delete(int approvalId);
    }
}