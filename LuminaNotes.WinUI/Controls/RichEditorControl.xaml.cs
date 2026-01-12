using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Linq;

namespace LuminaNotes.WinUI.Controls;

public sealed partial class RichEditorControl : UserControl
{
    public RichEditorControl()
    {
        this.InitializeComponent();
        MarkdownEditor.TextChanged += MarkdownEditor_TextChanged;
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(RichEditorControl),
            new PropertyMetadata(string.Empty, OnTextChanged));

    private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is RichEditorControl editor && e.NewValue is string text)
        {
            if (editor.MarkdownEditor.Text != text)
            {
                editor.MarkdownEditor.Text = text;
            }
        }
    }

    private void MarkdownEditor_TextChanged(object sender, TextChangedEventArgs e)
    {
        Text = MarkdownEditor.Text;
        UpdateWordCount();
    }

    private void UpdateWordCount()
    {
        var text = MarkdownEditor.Text;
        var words = text.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
        var chars = text.Length;

        WordCountText.Text = $"{words} words";
        CharCountText.Text = $"{chars} characters";
    }
}
