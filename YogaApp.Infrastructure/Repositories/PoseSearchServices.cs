using System.Data;
using YogaApp.Application.Interfaces;
using Dapper;
using YogaApp.Domain.Entities;
using YogaApp.Infrastructure.DTO;

namespace YogaApp.Infrastructure.Repositories;

public class PoseSearchServices: IPoseSearchServices
{
    private readonly IDbConnection _db;

    public PoseSearchServices(IDbConnection db)
    {
        _db = db;
    }
    public Task<List<Pose>> SearchByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

    public Task<List<Pose>> SearchByBenefitsAsync(string benefits)
    {
        throw new NotImplementedException();
    }

    public Task<List<Pose>> SearchByDescriptionAsync(string description)
    {
        throw new NotImplementedException();
    }

    public async Task<List<int>> GetPoseIdsByCategoryIdAsync(int catId)
    {
        return (await _db.QueryAsync<int>("SELECT pose_id FROM pose_mapping WHERE Category_id = @Id", 
            new {Id = catId})).ToList();
    }

    public async Task<List<int>> GetPoseIdsByDifficultyIdAsync(int difficultyId)
    {
        return (await _db.QueryAsync<int>("SELECT pose_id FROM poses WHERE Difficulty_Id = @Id",
            new {Id = difficultyId})).ToList();
    }

    public async Task<List<Pose>> GetPosesByPoseIdsAsync(List<int> poseIds)
    {
        if (!poseIds.Any()) return new List<Pose>();
    
        var dto = await _db.QueryAsync<PoseDto>("SELECT * FROM poses WHERE pose_id IN @poseIds",
            new {poseIds});
        return dto.Select(MapDtoToEntity).ToList();
    }
    
    private Pose MapDtoToEntity(PoseDto dto)
    {
        var pose = new Pose(dto.English_Name, dto.Difficulty_Id);
        pose.SetProperties(dto.Sanskrit_Name, dto.Translation_Name, dto.Pose_Description, dto.Pose_Benefits,
            dto.Url_Svg, dto.Url_Svg_Alt);
        pose.ThumbnailLocalPath = dto.Thumbnail;//set separately because create pose won't have this
        pose.PoseId = dto.Pose_Id;
        return pose;
    }

}