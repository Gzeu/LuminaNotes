using OllamaSharp;
using System;
using System.Threading.Tasks;

namespace LuminaNotes.Core.Services;

/// <summary>
/// Handles AI integration via Ollama for note assistance
/// </summary>
public class AiService
{
    private OllamaApiClient? _ollamaClient;
    private string _currentModel = "llama3.1:8b";
    private readonly Uri _ollamaUri = new("http://localhost:11434");

    public AiService()
    {
        InitializeClient();
    }

    private void InitializeClient()
    {
        try
        {
            _ollamaClient = new OllamaApiClient(_ollamaUri, _currentModel);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to initialize Ollama client: {ex.Message}");
        }
    }

    /// <summary>
    /// Changes the active AI model
    /// </summary>
    public void SetModel(string modelName)
    {
        _currentModel = modelName;
        InitializeClient();
    }

    /// <summary>
    /// Checks if Ollama service is available
    /// </summary>
    public async Task<bool> IsAiAvailableAsync()
    {
        if (_ollamaClient == null) return false;

        try
        {
            await _ollamaClient.ListLocalModelsAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Summarizes note content concisely
    /// </summary>
    public async Task<string> SummarizeAsync(string content, string? customPrompt = null)
    {
        if (_ollamaClient == null)
            return "AI service not available. Please ensure Ollama is running.";

        try
        {
            var prompt = customPrompt ?? "Summarize the following note concisely, keeping key points:";
            var fullPrompt = $"{prompt}\n\n{content}";
            
            var response = await _ollamaClient.GenerateAsync(fullPrompt);
            return response.Response ?? "No response generated.";
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}. Check if Ollama is running on {_ollamaUri}.";
        }
    }

    /// <summary>
    /// Rewrites text in specified style (formal, casual, professional, etc.)
    /// </summary>
    public async Task<string> RewriteAsync(string content, string style = "formal")
    {
        if (_ollamaClient == null)
            return "AI service not available.";

        try
        {
            var prompt = $"Rewrite the following text in a {style} style:\n\n{content}";
            var response = await _ollamaClient.GenerateAsync(prompt);
            return response.Response ?? "No response generated.";
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }

    /// <summary>
    /// Generates ideas based on a topic
    /// </summary>
    public async Task<string> GenerateIdeasAsync(string topic)
    {
        if (_ollamaClient == null)
            return "AI service not available.";

        try
        {
            var prompt = $"Generate 5 creative and actionable ideas about: {topic}";
            var response = await _ollamaClient.GenerateAsync(prompt);
            return response.Response ?? "No ideas generated.";
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }

    /// <summary>
    /// Answers questions based on provided notes context
    /// </summary>
    public async Task<string> QueryNotesAsync(string question, string notesContext)
    {
        if (_ollamaClient == null)
            return "AI service not available.";

        try
        {
            var prompt = $"Based on these notes:\n{notesContext}\n\nAnswer this question: {question}";
            var response = await _ollamaClient.GenerateAsync(prompt);
            return response.Response ?? "No answer generated.";
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }
}
