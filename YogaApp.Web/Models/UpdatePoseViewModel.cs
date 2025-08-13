using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using YogaApp.Application.UseCases.UpdatePose;

namespace YogaApp.Web.Models;

public class UpdatePoseViewModel
{
    [Required] 
    public string PoseName { get; set; }
    public string? SanskritName { get; set; }
    public string? TranslationOfName { get; set; }
    public string? PoseDescription { get; set; }
    public string? PoseBenefits { get; set; }
    public int? DifficultyId { get; set; }

    //you must either enter nothing or a file with .svg
    [RegularExpression(@"^$|.*\.svg$", ErrorMessage = "Please enter a valid SVG URL or leave blank")]
    [Display(Name = "SVG Image URL")]
    public string? UrlSvg { get; set; }

    [RegularExpression(@"^(|.*svg.*)$", ErrorMessage = "Please enter a valid SVG URL or leave blank")]
    [Display(Name = "SVG URL")]
    public string? UrlSvgAlt { get; set; }

    public List<int>? CategoryIds { get; set; } = new List<int>();

    public List<SelectListItem>? DifficultyOptions { get; set; }
    public List<SelectListItem>? CategoryOptions { get; set; }
}