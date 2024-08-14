using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
public class People
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string CompanyId { get; set; }
    public Company Company { get; set; }

}