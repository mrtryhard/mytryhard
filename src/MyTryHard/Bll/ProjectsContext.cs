using MyTryHard.Models;
using System.Collections.Generic;

namespace MyTryHard.Bll
{
    public class ProjectsContext : BaseContext
    {
        public ProjectsContext(string connStr) : base(connStr)
        {

        }

        public List<Project> GetProjectsList()
        {
            List<Project> lstProjects = new List<Project>();

            Project p = new Project();
            p.Title = "Fitness Tracker";
            p.Url = "../tracker";
            p.Repository = "https://www.github.com/mrtryhard/mytryhard";
            p.Description = "Petit traqueur d'exercice, intégré à même ce site web!";

            lstProjects.Add(p);

            return lstProjects;
        }
    }
}
