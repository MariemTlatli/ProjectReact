public static class CompanyMapper
{
    public static CompanyDTO ToDTO(this Company company)
    {
        return new CompanyDTO
        {
            Id = company.Id,
            Name = company.Name,
            Address = company.Address,
            Phone = company.Phone,
            UserId = company.UserId
        };
    }

    public static Company ToEntity(this CompanyDTO companyDTO)
    {
        return new Company
        {
            Id = companyDTO.Id,
            Name = companyDTO.Name,
            Address = companyDTO.Address,
            Phone = companyDTO.Phone,
            UserId = companyDTO.UserId
        };
    }
}
