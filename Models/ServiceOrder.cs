using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
public class ServiceOrder
{
    public int ServiceOrderId { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }


    /*public int CompanyId { get; set; }
    public Company Company { get; set; }
    
   
    public int PeopleId { get; set; }
    public People People { get; set; }
    
        public List<User> Users { get; set; } = new List<User>();

   
    public ICollection<Article> Articles { get; set; }*/
}