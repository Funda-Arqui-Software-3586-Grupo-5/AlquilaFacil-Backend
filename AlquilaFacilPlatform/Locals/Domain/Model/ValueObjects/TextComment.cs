namespace AlquilaFacilPlatform.Locals.Domain.Model.ValueObjects;

public record TextComment(string Text)
{
    public TextComment() : this(String.Empty)
    {
        
    }
}