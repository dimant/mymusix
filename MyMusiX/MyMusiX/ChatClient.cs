namespace MyMusiX
{
    using Newtonsoft.Json;
    using System.Net.Http.Headers;
    using System.Text;
    public class ChatCompletionResponse
    {
        public string Id { get; set; }
        public string Object { get; set; }
        public long Created { get; set; }
        public string Model { get; set; }
        public Usage Usage { get; set; }
        public List<Choice> Choices { get; set; }
    }

    public class Usage
    {
        public int PromptTokens { get; set; }
        public int CompletionTokens { get; set; }
        public int TotalTokens { get; set; }
    }

    public class Choice
    {
        public Message Message { get; set; }
        public string FinishReason { get; set; }
        public int Index { get; set; }
    }

    public class Message
    {
        public string Role { get; set; }
        public string Content { get; set; }
    }

    public class ChatClient
    {
        private string? key = string.Empty;
        private readonly string endpoint = "https://api.openai.com/v1/chat/completions";

        public async Task<string?> AskQuestionAsync(string question)
        {
            if (string.IsNullOrEmpty(question))
            {
                throw new ArgumentNullException(nameof(question));
            }

            using (var client = new HttpClient())
            {
                ReadApiKey();
                client.DefaultRequestHeaders.Authorization = 
                    new AuthenticationHeaderValue("Bearer", this.key);

                var payload = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                        new { role = "user", content = question }
                    },
                    temperature = 0.7
                };

                string payloadJson = JsonConvert.SerializeObject(payload);

                using (var response = await client.PostAsync(endpoint, new StringContent(payloadJson, Encoding.UTF8, "application/json")))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();

                        ChatCompletionResponse? completion = 
                            JsonConvert.DeserializeObject<ChatCompletionResponse>(json);

                        return completion?.Choices[0].Message.Content;
                    }
                    else
                    {
                        throw new Exception($"Request failed with: {response.StatusCode}");
                    }
                }
            }
        }

        private void ReadApiKey()
        {
            using (var reader = new StreamReader("api.key"))
            {
                this.key = reader.ReadLine()?.Trim();
            }

            if (string.IsNullOrEmpty(this.key))
            {
                throw new InvalidDataException("Couldn't read api key");
            }
        }
    }
}
