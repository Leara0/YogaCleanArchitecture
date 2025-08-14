namespace YogaApp.Domain.Entities;

public class Pose
{
    public int PoseId { get; set; }
    public string PoseName { get; set; }
    public string? SanskritName { get; set; }
    public string? TranslationOfName { get; set; }
    public string? PoseDescription { get; set; }
    public string? PoseBenefits { get; set; }
    public int DifficultyId { get; set; }
    public Difficulty Difficulty { get; set; }
    public List<int>? CategoryIds { get; set; }
    public string? UrlSvg { get; set; }
    public string? ThumbnailUrlSvg { get; set; }

    public Pose(string Name, int DifficultyId)
    {
       SetName(Name);
       SetDifficulty(DifficultyId);
    }

    public void SetName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Name cannot be empty!");
        PoseName = newName;
    }

    public void SetDifficulty(int newDifficulty)
    {
        if(newDifficulty == null || newDifficulty == 0)
            throw new ArgumentException("Difficulty cannot be empty!");
        DifficultyId = newDifficulty;
    }

    public void SetUrlSvg(string url)
    {
        if(!url.Contains(".svg"))
            throw new ArgumentException("Url must be a valid svg image!");
        UrlSvg = url;
    }

    public void SetThumbnailUrlSvg(string url)
    {
        if (!url.Contains(".svg"))
        {
            throw new ArgumentException("Thumbnail Url must be a valid svg image!");
        }
        ThumbnailUrlSvg = url;
    }
    
    public void SetProperties(
        string? sanskritName,
        string? translationOfName,
        string? description,
        string? benefits,
        string? urlSvg,
        string? thumbnailUrlSvg) 
    {
        SanskritName = sanskritName;
        TranslationOfName = translationOfName;
        PoseDescription = description;
        PoseBenefits = benefits;
        if (!string.IsNullOrEmpty(urlSvg))
            SetUrlSvg(urlSvg);
    
        if (!string.IsNullOrEmpty(thumbnailUrlSvg))
            SetThumbnailUrlSvg(thumbnailUrlSvg);
    }
    
}