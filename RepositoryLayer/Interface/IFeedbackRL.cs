using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IFeedbackRL
    {
        public bool AddFeedback(FeedbackModel feedback, int UserId);
        public List<FeedbackModel> GetAllFeedback();
    }
}
