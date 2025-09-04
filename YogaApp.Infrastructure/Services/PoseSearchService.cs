using System.Data;
using Dapper;
using YogaApp.Application.Interfaces;
using YogaApp.Domain.Entities;
using YogaApp.Infrastructure.DTO;

namespace YogaApp.Infrastructure.Services;

public class PoseSearchService: IPoseSearchService
{
    private readonly IDbConnection _db;

    public PoseSearchService(IDbConnection db)
    {
        _db = db;
    }
    public async Task<List<Pose>> SearchByNameAsync(string name)
    {
        //use wildcards in the parameter so it will find search term anywhere in the name
        var poseDto = (await _db.QueryAsync<PoseDto>("SELECT * FROM poses WHERE English_Name LIKE @name",
            new { name = $"%{name}%" })).ToList();
        return poseDto.Select(MapDtoToEntity).ToList();
    }

    public async Task<List<Pose>> SearchByBenefitsAsync(string benefits)
    {
        //use wildcards in the parameter so it will find search term anywhere in the benefits
        var poseDto = (await _db.QueryAsync<PoseDto>
            ("SELECT * FROM poses WHERE Pose_Benefits LIKE @benefits", 
                new { benefits = $"%{benefits}%" })).ToList();
        return poseDto.Select(MapDtoToEntity).ToList();
    }

    public async Task<List<Pose>> SearchByDescriptionAsync(string description)
    {
         //use wildcards in the parameter so it will find search term anywhere in the description
         var poseDto = (await _db.QueryAsync<PoseDto>
             ("SELECT * FROM poses WHERE Pose_Description LIKE @description", 
                 new { description = $"%{description}%"})).ToList();
             return poseDto.Select(MapDtoToEntity).ToList();
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

    public async Task<Pose?> SearchForSingleNameAsync(string name)
    {
        var exactMatchDto = await _db.QueryFirstOrDefaultAsync<PoseDto>("SELECT * FROM poses WHERE English_Name = @name",
            new { name });
        if (exactMatchDto != null)
            return MapDtoToEntity(exactMatchDto);
        
        var partialMatchDto = await _db.QueryFirstOrDefaultAsync<PoseDto>
        ("SELECT * FROM poses WHERE English_Name LIKE @searchPattern OR @name LIKE CONCAT('%', English_Name, '%')",
            new { name, searchPattern = $"%{name}%" });
        if(partialMatchDto != null)
            return MapDtoToEntity(partialMatchDto);
        
        return null;
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