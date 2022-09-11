using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IFeedbackBL
    {
        public bool AddFeedback(FeedbackModel feedback, int UserId);
        public List<FeedbackModel> GetAllFeedback();
    }
}
