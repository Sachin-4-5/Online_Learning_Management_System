using OLMS.DAL.Interfaces;
using OLMS.Models.Entities;
using System;
using System.Data;
using System.Collections.Generic;

namespace OLMS.BLL.Services
{
    public class OptionService
    {
        private readonly IOptionRepository _repo;

        public OptionService(IOptionRepository repo)
        {
            _repo = repo;
        }

        public List<Option> GetByQuestionId(int questionId)
        {
            DataTable dt = _repo.GetByQuestion(questionId);
            List<Option> options = new List<Option>();
            foreach (DataRow row in dt.Rows)
            {
                options.Add(new Option
                {
                    OptionID = Convert.ToInt32(row["OptionID"]),
                    QuestionID = Convert.ToInt32(row["QuestionID"]),
                    OptionText = row["OptionText"].ToString(),
                    IsCorrect = Convert.ToBoolean(row["IsCorrect"])
                });
            }
            return options;
        }

        public void Add(Option option)
        {
            int result = _repo.Add(option);
            if (result <= 0) throw new Exception("Failed to add option.");
        }

        public void Update(Option option)
        {
            int result = _repo.Update(option);
            if (result <= 0) throw new Exception("Failed to update option.");
        }

        public void Delete(int optionId)
        {
            int result = _repo.Delete(optionId);
            if (result <= 0) throw new Exception("Failed to delete option.");
        }
    }
}
