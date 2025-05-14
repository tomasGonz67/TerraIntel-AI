using OpenAI.Chat;

namespace TerraIntel_lAI.Services
{
    public class AIService : IAIService
    {
        // grab your API key however you prefer
        readonly string _apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

        public async Task<string> GetFactAsync(double latitude, double longitude)
        {

            // build your prompt
            var prompt =
                $"Give me an interesting historical fact about the location at coordinates {latitude}, {longitude}. " +
                "Start it off with 'You are currently located at <Name of area>:' then the fact.";

            var client = new ChatClient(model: "gpt-4.1", apiKey: _apiKey);
            ChatCompletion completion = await client.CompleteChatAsync(prompt);

            // return the trimmed text of the first response
            return completion.Content[0].Text.Trim();
        }
    }
}
