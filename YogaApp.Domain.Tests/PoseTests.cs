using YogaApp.Domain.Entities;

namespace YogaApp.Domain.Tests;

public class PoseTests
{
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t")]
    [InlineData("\n")]
    [InlineData(null)]
    public void SetName_ThrowsException_WhenNameIsInvalid(string invalidName)
    {
        //arrange - create a new pose object
        var pose = new Pose("Initial name", 1);

        //act & assert
        Assert.Throws<ArgumentException>(() => pose.SetName(invalidName));
    }

    [Theory]
    [InlineData("Downward Dog")]
    [InlineData("Upward Dog")]
    [InlineData("Warrior 1")]
    [InlineData("Warrior 2")]
    [InlineData("Child's Pose")]
    public void SetName_SetsName_WhenNameIsValid(string validName)
    {
        //arrange
        var pose = new Pose("Initial name", 1);

        //act
        pose.SetName(validName);

        //assert
        Assert.Equal(validName, pose.PoseName);
    }

    [Theory]
    [InlineData(null)]
    public void SetDifficulty_ThrowsExceptioon_WhenDifficultyIsInvalid(int invalidDifficulty)
    {
        var pose = new Pose("Initial name", 1);

        Assert.Throws<ArgumentException>(() => pose.SetDifficulty(invalidDifficulty));
    }

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(1)]
    public void SetDifficulty_SetsDifficulty_WhenDifficultyIsValid(int validDifficulty)
    {
        var pose = new Pose("Initial name", 1);
        pose.SetDifficulty(validDifficulty);
        Assert.Equal(validDifficulty, pose.DifficultyId);
    }

    [Theory]
    [InlineData("https://example.com/image.jpg")]
    [InlineData("https://example.com/image.png")]
    [InlineData("https://example.com/image.gif")]
    [InlineData("not-an-svg-file.txt")]
    [InlineData("pose.jpeg")]
    public void SetUrlSvg_ThrowsException_WhenUrlIsNotSvg(string invalidUrl)
    {
        var pose = new Pose("Initial name", 1);
        Assert.Throws<ArgumentException>(() => pose.SetUrlSvg(invalidUrl));
    }

    [Theory]
    [InlineData("https://example.com/pose.svg")]
    [InlineData("pose.svg")]
    [InlineData("/images/warrior.svg")]
    [InlineData("https://cdn.example.com/poses/downward-dog.svg")]
    public void SetUrlSvg_SetsUrl_WhenUrlIsValidSvg(string validUrl)
    {
        var pose = new Pose("Initial name", 1);
        pose.SetUrlSvg(validUrl);
        Assert.Equal(validUrl, pose.UrlSvg);
    }

    [Theory]
    [InlineData("Adho Mukha Svanasana", "Downward-Facing Dog", "Great for stretching", "Improves flexibility",
        "pose.svg", "thumb.svg")]
    [InlineData("Virabhadrasana I", "Warrior I", "Standing pose", "Builds strength", "Vira.svg", "thumb.svg")]
    [InlineData("Balasana", "Child's Pose", "Resting pose", "Calms the mind", "Balasana.svg", "Bala.svg.com")]
    public void SetProperties_AssignAllProperties_WhenValidInputProvided(string sanskrit, string translation,
        string description, string benefits, string urlSvg, string thumbnailUrlSvg)
    {
        var pose = new Pose("Initial name", 1);
        
        pose.SetProperties(sanskrit, translation, description, benefits, urlSvg, thumbnailUrlSvg);
        
        Assert.Equal(sanskrit, pose.SanskritName);
        Assert.Equal(translation, pose.TranslationOfName);
        Assert.Equal(description, pose.PoseDescription);
        Assert.Equal(benefits, pose.PoseBenefits);
        Assert.Equal(urlSvg, pose.UrlSvg);
        Assert.Equal(thumbnailUrlSvg, pose.ThumbnailUrlSvg);
    }
}