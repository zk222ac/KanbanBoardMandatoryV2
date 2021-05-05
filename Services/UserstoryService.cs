using KanbanBoardMandatoryV2.Models;
using System.Collections.Generic;

namespace KanbanBoardMandatoryV2.Services
{
    public class UserstoryService : IUserstory
    {


        private static List<Userstory> _userstory;

        public UserstoryService()
        {
            _userstory = new List<Userstory>();
        }


        
        // Add New UserStory 
        public void AddStory(Userstory story)
        {
            _userstory.Add(story);
        }

        // Get All Userstorys 
        public List<Userstory> GetAllUserStorys()
        {
            return _userstory;
        }


        // Get Userstory from ID
        public Userstory GetUserStoryById(int id)
        {
            return _userstory.Find(b => b.Id == id);
        }


        // Move Userstory Backwards 
        public void Backward(Userstory story)
        {
            switch (story.UserStatus)
            {
                
                case UserStatus.Done:
                    story.UserStatus = UserStatus.Doing;
                    break;

                case UserStatus.Doing:
                    story.UserStatus = UserStatus.ToDo;
                    break;
           
                
                default:
                    break;
            }
        }


        // Move Userstory Forward 
        public void Forward(Userstory story)
        {
            switch (story.UserStatus)
            {

                case UserStatus.ToDo:
                    story.UserStatus = UserStatus.Doing;
                    break;

                case UserStatus.Doing:
                    story.UserStatus = UserStatus.Done;
                    break;


                default:
                    break;
            }
        }


    }
}
