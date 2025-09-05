
using Microsoft.Extensions.Configuration;
using YogaApp.Application.Interfaces;
using OpenAI.Chat;

namespace YogaApp.Infrastructure.Services;

public class AIYogaService : IAIYogaService
{
    //chat client is like a 'phone' to talk to the ai - you give it messages and it gives you back ai's response
    private readonly ChatClient _chatClient;
    
    public AIYogaService(IConfiguration configuration)
    {
        var apiKey = configuration["OpenAI:ApiKey"];
        //create a new ChatClient for each model you want to use
        _chatClient = new ChatClient("gpt-5-nano", apiKey);
    }
    
    public async Task<List<string>> GetPoseSuggestionsAsync(string userGoal)
    {
        //you give the ai a conversation (a list of messages)
        var messages = new List<ChatMessage>
        {
            new SystemChatMessage("You are a yoga instructor. Given a user's goal, suggest 9-10 yoga pose " +
                                  "names that would help achieve that goal. Return only pose names, separated by " +
                                  "commas. Use standard pose names like 'Warrior I', 'Downward Dog', etc. Do not repeat" +
                                  "any poses. Put the responses in a nice order for a yoga flow."),
            new UserChatMessage(userGoal)
        };
        
        //it uses a built in method to handle all the HTTP requests, JSON formatting, authentication, etc
        var response = await _chatClient.CompleteChatAsync(messages);

        var poseNames = response.Value.Content[0].Text
            .Split(',')
            .Select(name => name.Trim())
            .Select(name => name.Replace(" Pose", "").Replace("Pose", "").Trim())
            .Where(name => !string.IsNullOrEmpty(name))
            .ToList();

        return poseNames;
    }
}