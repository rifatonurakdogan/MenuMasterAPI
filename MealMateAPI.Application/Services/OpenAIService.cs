using MenuMasterAPI.Application;
using MenuMasterAPI.Application.DTOs;
using System.Net.Http.Json;
using System.Text.Json;

public class OpenAIService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public OpenAIService(string apiKey)
    {
        _httpClient = new HttpClient();
        _apiKey = apiKey;
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
    }

    public async Task<string> SendRequestAsync(UserSendContract prompt, string model = OpenAIConstants.TextModel)
    {
        var masterRequest = new
        {
            role = "system",
            content = OpenAIConstants.TextPrompt
        };
        var userRequest = new
        {
            role = "user",
            content = JsonSerializer.Serialize(prompt)
        };
        var messages = new[]
        {
            masterRequest,userRequest
        };
        var requestBody = new
        {
            model,
            messages
        };
        var response = await _httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestBody);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<OpenAIResponse>(responseBody, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return result.Choices[0].Message.Content;
    }
    public async Task<string> SendImageRequestAsync(string imagePath, string model = OpenAIConstants.ImageModel)
    {
        var imageBytes = await File.ReadAllBytesAsync(imagePath);
        var base64Image = Convert.ToBase64String(imageBytes);
        var requestBody = new
        {
            model = model,
            messages = new object[]
               {
                    new
                    {
                        role = "user",
                        content = new object[]
                        {
                            new
                            {
                                type = "text",
                                text = OpenAIConstants.ImagePrompt
                            },
                            new
                            {
                                type = "image_url",
                                image_url = new
                                {
                                    url = $"data:image/jpeg;base64,{base64Image}"
                                }
                            }
                        }
                    }
               }
        };
        var response = await _httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestBody);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<OpenAIResponse>(responseBody, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return result.Choices[0].Message.Content;
    }

    private class OpenAIResponse
    {
        public Choice[] Choices { get; set; }
    }

    private class Choice
    {
        public Message Message { get; set; }
    }
    private class Message
    {
        public string Content { get; set; }
    }
}
