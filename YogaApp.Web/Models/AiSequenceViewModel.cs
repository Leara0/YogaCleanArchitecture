using YogaApp.Web.Models.HelperViews;

namespace YogaApp.Web.Models;

public class AiSequenceViewModel
{
    public List<PoseLinkViewModel> SuggestedPoses { get; set; } = new List<PoseLinkViewModel>();
    public string SearchTerm { get; set; }
    public bool HasResults { get; set; }
    public string ErrorMessage { get; set; }
}