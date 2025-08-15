using Microsoft.AspNetCore.Mvc.Rendering;
using YogaApp.Application.DTO;
using YogaApp.Application.UseCases.GetAllPoses;
using YogaApp.Application.UseCases.UpdatePose;
using YogaApp.Web.Models;

namespace YogaApp.Web.Extensions.Pose;

public static class PoseMappingExtensions
{
    private static AllPosesViewModel ToAllPosesViewModel(this GetAllPosesResponseDto dto)
    {
        return new AllPosesViewModel
        {
            PoseId = dto.PoseId,
            PoseName = dto.PoseName,
            SanskritName = dto.SanskritName,
            PoseDescription = dto.PoseDescription,
            Difficulty_Id = dto.DifficultyId,
            ThumbnailUrlSvg = dto.ThumbnailUrlSvg,
        };
    }
    public static PosesByDifficultyViewModel ToViewAllModel(this PosesByDifficultyDto dto)
    {
        return new PosesByDifficultyViewModel
        {
            EasyPoses = dto.EasyPoses.Select(p => p.ToAllPosesViewModel()).ToList(),
            MediumPoses = dto.MediumPoses.Select(p => p.ToAllPosesViewModel()).ToList(),
            HardPoses = dto.HardPoses.Select(p => p.ToAllPosesViewModel()).ToList(),
        };
    }
    public static PoseDetailViewModel ToViewDetailModel(this GetPoseByIdResponseDto dto)
    {
        return new PoseDetailViewModel
        {
            PoseId = dto.PoseId,
            PoseName = dto.PoseName,
            SanskritName = dto.SanskritName,
            TranslationOfName = dto.TranslationOfName,
            PoseDescription = dto.PoseDescription,
            PoseBenefits = dto.PoseBenefits,
            ImageUrl = dto.ImageUrl,
            DifficultyLink = dto.DifficultyLink,
            CategoryLink = dto.CategoryLink,
        }; 
    }
    public static UpdatePoseViewModel ToViewUpdateModel(this UpdatePoseResponseDto dto)
    {
        var pose = new UpdatePoseViewModel
        {
            PoseId = dto.PoseId,
            PoseName = dto.PoseName,
            SanskritName = dto.SanskritName,
            TranslationOfName = dto.TranslationOfName,
            PoseDescription = dto.PoseDescription,
            PoseBenefits = dto.PoseBenefits,
            DifficultyId = dto.DifficultyId,
            CategoryIds = dto.CategoryIds,
            UrlSvg = dto.UrlSvg,
            UrlSvgAlt = dto.ThumbnailUrlSvg,
        };
        // map difficulties and categories SelectListItem
        pose.DifficultyOptions = dto.DifficultyOptions.Select(d => new SelectListItem
        {
            Value = d.Value,
            Text = d.Text,
            Selected = d.Value == dto.DifficultyId.ToString()
        }).ToList();
        
        pose.CategoryOptions = dto.CategoryOptions.Select(c => new SelectListItem
        {
            Value = c.Value,
            Text = c.Text,
            Selected = pose.CategoryIds.Contains(int.Parse(c.Value))
        }).ToList();
        return pose;
    }

    public static UpdatePoseRequestToDbDto ToUpdatePoseDto(this UpdatePoseViewModel pose)
    {
        return new UpdatePoseRequestToDbDto()
        {
            PoseId = pose.PoseId,
            PoseName = pose.PoseName,
            SanskritName = pose.SanskritName,
            TranslationOfName = pose.TranslationOfName,
            PoseDescription = pose.PoseDescription,
            PoseBenefits = pose.PoseBenefits,
            DifficultyId = pose.DifficultyId,
            CategoryIds = pose.CategoryIds,
            UrlSvg = pose.UrlSvg,
            ThumbnailUrlSvg = pose.UrlSvgAlt
        };
    }

    public static CreatePoseRequestDto ToCreatePoseRequestDto(this CreatePoseViewModel pose)
    {
        return new CreatePoseRequestDto()
        {
            PoseName = pose.PoseName,
            SanskritName = pose.SanskritName,
            TranslationOfName = pose.TranslationOfName,
            PoseDescription = pose.PoseDescription,
            PoseBenefits = pose.PoseBenefits,
            DifficultyId = pose.DifficultyId,
            UrlSvg = pose.UrlSvg,
            ThumbnailUrlSvg = pose.UrlSvgAlt,
            CategoryIds = pose.CategoryIds
        };
    }
}