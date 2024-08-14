using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string UserId { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public User User { get; set; }
    public ICollection<People> People { get; set; } = new List<People>();
    public ICollection<ServiceOrder> ServiceOrders { get; set; } = new List<ServiceOrder>();

    public CompanyDTO ToDTO()
    {
        return new CompanyDTO
        {
            Id = this.Id,
            Name = this.Name,
            Address = this.Address,
            Phone = this.Phone,
            UserId = this.UserId
        };
    }
}
