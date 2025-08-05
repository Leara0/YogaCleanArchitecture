using System.Data;
using Dapper;
using YogaApp.Application.Interfaces;
using YogaApp.Domain.Entities;
using YogaApp.Infrastructure.DTO;

namespace YogaApp.Infrastructure.Repositories;

public class PoseRepository : IPoseRepository
{
    private readonly IDbConnection _db;

    public PoseRepository(IDbConnection db)
    {
        _db = db;
    }


    public async Task<List<Pose>> GetAllPosesAsync()
    {
        var dtos = await _db.QueryAsync<PoseDto>("SELECT * FROM poses");
        //map dto to pose entity here
        return dtos.Select(MapDtoToEntity).ToList();
    }

    public Task<Pose> GetPoseByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Pose> UpdatePoseAsync(Pose pose)
    {
        throw new NotImplementedException();
    }

    public async Task<int> CreatePoseAsync(Pose pose)
    {
        var sql = @"INSERT INTO poses (English_Name, Sanskrit_Name, Translation_Name, Pose_Description, 
                   Pose_Benefits, Difficulty_Id, Url_Svg, Url_Svg_Alt)
                   VALUES (@PoseName, @SanskritName, @TranslationName, @PoseDescription, @PoseBenefits, 
                           @DifficultyId, @UrlSvg, @ThumbnailUrlSvg);
                    SELECT LAST_INSERT_ID();";

        var id = await _db.QuerySingleAsync<int>(sql, new
        {
            PoseName = pose.PoseName,
            SanskritName = pose.SanskritName,
            TranslationName = pose.TranslationOfName,
            PoseDescription = pose.PoseDescription,
            PoseBenefits = pose.PoseBenefits,
            DifficultyId = pose.DifficultyId,
            UrlSvg = pose.UrlSvg,
            ThumbnailUrlSvg = pose.ThumbnailUrlSvg
        });
        //set the id of the entity
        return id;
    }

    public async Task SavePoseCategoryAsync(int poseId, List<int> categoryIds)
    {
        foreach (var categoryId in categoryIds)
        {
            await _db.ExecuteAsync("INSERT INTO pose_mapping (Pose_id, Category_id) VALUES (@PoseId, @CategoryId)",
                new { PoseId = poseId, CategoryId = categoryId });
        }
    }

    public Task DeletePoseByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    private Pose MapDtoToEntity(PoseDto dto)
    {
        var pose = new Pose(dto.English_Name);
        pose.SetProperties(dto.Sanskrit_Name, dto.Translation_Name, dto.Pose_Description, dto.Pose_Benefits,
        dto.Difficulty_Id, dto.Url_Svg, dto.Url_Svg_Alt);
        pose.PoseId = dto.Pose_Id;
        return pose;
    }

}