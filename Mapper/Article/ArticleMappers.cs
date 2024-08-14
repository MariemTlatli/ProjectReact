public static class ArticleMapper
{
    // Map Article entity to ArticleDTO
    public static ArticleDTO ToDTO(this Article article)
    {
        return new ArticleDTO
        {
            Id = article.Id,
            Categorie = article.Categorie,
            Price = article.price,
            Quantite = article.quantite,
            CreatedById = article.CreatedById
        };
    }

    // Map ArticleDTO to Article entity
    public static Article ToEntity(this ArticleDTO articleDTO)
    {
        return new Article
        {
            Id = articleDTO.Id,
            Categorie = articleDTO.Categorie,
            price = articleDTO.Price,
            quantite = articleDTO.Quantite,
            CreatedById = articleDTO.CreatedById
        };
    }
}
