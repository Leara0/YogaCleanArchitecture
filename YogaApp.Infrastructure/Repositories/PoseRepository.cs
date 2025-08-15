using System.Data;
using Dapper;
using YogaApp.Application.RespositoryInterfaces;
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

    public async Task<Pose> GetPoseByIdAsync(int id)
    {
        var dto = await _db.QuerySingleOrDefaultAsync<PoseDto>("SELECT * FROM poses WHERE pose_id = @id", new { id });
        return MapDtoToEntity(dto);
    }

    public async Task UpdateToDbPoseAsync(Pose pose)
    {
                var sql = @"UPDATE poses SET 
                english_name = @PoseName, 
                sanskrit_name_adapted = @SanskritName, 
                translation_name = @TranslationOfName, 
                pose_description = @PoseDescription,
                pose_benefits = @PoseBenefits, 
                difficulty_id = @DifficultyId,
                url_svg = @UrlSvg, 
                url_svg_alt = @ThumbnailUrlSvg 
                WHERE pose_id = @PoseId";
                
        await _db.ExecuteAsync(sql, new
        {
            PoseId = pose.PoseId,
            PoseName = pose.PoseName,
            SanskritName = pose.SanskritName, 
            TranslationOfName = pose.TranslationOfName, 
            PoseDescription = pose.PoseDescription, 
            PoseBenefits = pose.PoseBenefits, 
            DifficultyId = pose.DifficultyId,
            UrlSvg = pose.UrlSvg, 
            ThumbnailUrlSvg = pose.ThumbnailUrlSvg
        });
    }

    public async Task<int> CreatePoseAsync(Pose pose)
    {
        var sql = @"INSERT INTO poses (English_Name, Sanskrit_Name, Translation_Name, Pose_Description, 
                   Pose_Benefits, Difficulty_Id, Url_Svg, Url_Svg_Alt)
                   VALUES (@PoseName, @SanskritName, @TranslationOfName, @PoseDescription, @PoseBenefits, 
                           @DifficultyId, @UrlSvg, @ThumbnailUrlSvg);
                    SELECT LAST_INSERT_ID();";

        var id = await _db.QuerySingleAsync<int>(sql, pose);
        //return the id of the new entry
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

    public async Task<List<(int PoseId, string PoseName)>> GetPoseLinkByPoseIdAsync(List<int> poseIds)
    {
        return (await _db.QueryAsync<(int PoseId, string PoseName)>("SELECT Pose_id AS PoseId, English_Name AS PoseName FROM poses WHERE pose_id IN @PoseIds",
            new { PoseIds = poseIds })).ToList();
    }

    public Task DeletePoseByPoseIdAsync(int poseId)
    {
        throw new NotImplementedException();
    }

    private Pose MapDtoToEntity(PoseDto dto)
    {
        var pose = new Pose(dto.English_Name, dto.Difficulty_Id);
        pose.SetProperties(dto.Sanskrit_Name, dto.Translation_Name, dto.Pose_Description, dto.Pose_Benefits,
        dto.Url_Svg, dto.Url_Svg_Alt);
        pose.PoseId = dto.Pose_Id;
        return pose;
    }

}