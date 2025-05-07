namespace AlquilaFacilPlatform.Locals.Domain.Model.ValueObjects;

public record RatingComment(int Rating)
{
    public RatingComment() : this(0)
    {
        
    }
}