using KanbanBoardMandatoryV2.Models;
using System.Collections.Generic;

namespace KanbanBoardMandatoryV2.Services
{
    public interface IUserstory
    {

        List<Userstory> GetAllUserStorys();
        Userstory GetUserStoryById(int id);

        public void AddStory(Userstory story);
        void Forward(Userstory story);
        void Backward(Userstory story);
      
    }
}
