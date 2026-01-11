using OllamaSharp;
using System;
using System.Threading.Tasks;

namespace LuminaNotes.Core.Services;

/// <summary>
/// Handles AI operations using Ollama local LLM
/// </summary>
public class AiService
{
    private readonly OllamaApiClient _ollamaClient;
    private string _currentModel = "llama3.1:8b";

    public AiService(string ollamaEndpoint = "http://localhost:11434", string defaultModel = "llama3.1:8b")
    {
        _ollamaClient = new OllamaApiClient(new Uri(ollamaEndpoint), defaultModel);
        _currentModel = defaultModel;
    }

    /// <summary>
    /// Check if Ollama server is running and accessible
    /// </summary>
    public async Task<bool> IsAiAvailableAsync()
    {
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
    /// Summarize note content
    /// </summary>
    public async Task<string> SummarizeAsync(string content, string customPrompt = "")
    {
        try
        {
            var prompt = string.IsNullOrEmpty(customPrompt)
                ? $"Summarize the following note concisely in 2-3 sentences:\n\n{content}"
                : $"{customPrompt}\n\n{content}";

            var response = await _ollamaClient.GenerateAsync(prompt);
            return response.Response ?? "Unable to generate summary.";
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}. Ensure Ollama is running.";
        }
    }

    /// <summary>
    /// Rewrite text in different style
    /// </summary>
    public async Task<string> RewriteAsync(string content, string style = "formal")
    {
        try
        {
            var prompt = $"Rewrite the following text in a {style} style, maintaining the core message:\n\n{content}";
            var response = await _ollamaClient.GenerateAsync(prompt);
            return response.Response ?? "Unable to rewrite text.";
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }

    /// <summary>
    /// Generate ideas on a topic
    /// </summary>
    public async Task<string> GenerateIdeasAsync(string topic, int count = 5)
    {
        try
        {
            var prompt = $"Generate {count} creative and actionable ideas related to: {topic}";
            var response = await _ollamaClient.GenerateAsync(prompt);
            return response.Response ?? "Unable to generate ideas.";
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }

    /// <summary>
    /// Answer question based on notes context
    /// </summary>
    public async Task<string> QueryNotesAsync(string question, string notesContext)
    {
        try
        {
            var prompt = $"Based on the following notes:\n\n{notesContext}\n\nAnswer this question: {question}";
            var response = await _ollamaClient.GenerateAsync(prompt);
            return response.Response ?? "Unable to answer question.";
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }

    /// <summary>
    /// Change the active model
    /// </summary>
    public void SetModel(string modelName)
    {
        _currentModel = modelName;
        _ollamaClient.SelectedModel = modelName;
    }

    /// <summary>
    /// Get list of available models
    /// </summary>
    public async Task<string[]> GetAvailableModelsAsync()
    {
        try
        {
            var models = await _ollamaClient.ListLocalModelsAsync();
            return models.Select(m => m.Name).ToArray();
        }
        catch
        {
            return Array.Empty<string>();
        }
    }
}
