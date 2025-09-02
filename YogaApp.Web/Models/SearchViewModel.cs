using YogaApp.Domain.Entities;
using YogaApp.Web.Models.HelperViews;

namespace YogaApp.Web.Models;

public class SearchViewModel
{
    public List<PoseLinkViewModel> NameLinks { get; set; }
    public List<PoseLinkViewModel> DescriptionLinks { get; set; }
    public List<PoseLinkViewModel> BenePoseLinks { get; set; }
    public List<CategoryLinkViewModel> CategoryLinks { get; set; }
    public string SearchTerm { get; set; }
}