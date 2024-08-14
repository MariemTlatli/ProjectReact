using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
public class Article
{
    public int Id { get; set; }
    public string Categorie { get; set; }
    public int price { get; set; }
    public int quantite { get; set; }
    public string CreatedById { get; set; }
    public User CreatedBy { get; set; }
     public int ServiceOrderID { get; set; }
    // public ServiceOrder ServiceOrder { get; set; }

}