# 🛠️ Custom Controls Usage Guide

## RichEditorControl

### Basic Usage

```xml
<controls:RichEditorControl 
    Text="{Binding NoteContent, Mode=TwoWay}"/>
```

### Features

- **Markdown Toolbar**: Bold, Italic, Underline, Headings (H1-H3)
- **List Support**: Bullet and numbered lists
- **Link Insertion**: Standard links and wiki links `[[]]`
- **Preview Toggle**: Live Markdown preview
- **Word Count**: Real-time word and character count
- **Spell Check**: Built-in spell checking

### Code Example

```csharp
// In ViewModel
[ObservableProperty]
private string noteContent = "# My Note\n\nStart writing...";

// In XAML
<controls:RichEditorControl Text="{x:Bind ViewModel.NoteContent, Mode=TwoWay}"/>
```

### Customization

```xml
<controls:RichEditorControl 
    Text="{Binding Content}"
    FontFamily="Cascadia Code"
    FontSize="14"/>
```

---

## NoteCard

### Basic Usage

```xml
<controls:NoteCard 
    Title="Meeting Notes"
    ContentPreviewText="Discussed project timeline and deliverables..."
    DateString="2h ago"/>
```

### With All Features

```xml
<controls:NoteCard 
    Title="My Encrypted Note"
    ContentPreviewText="This is a preview of the note content..."
    IsPinned="True"
    IsEncrypted="True"
    DateString="Yesterday"
    Tags="{Binding NoteTags}"/>
```

### In ListView

```xml
<ListView ItemsSource="{Binding Notes}">
    <ListView.ItemTemplate>
        <DataTemplate x:DataType="models:Note">
            <controls:NoteCard
                Title="{x:Bind Title}"
                ContentPreviewText="{x:Bind Content}"
                IsPinned="{x:Bind IsPinned}"
                IsEncrypted="{x:Bind IsEncrypted}"
                DateString="{x:Bind Updated, Converter={StaticResource DateTimeToStringConverter}}"/>
        </DataTemplate>
    </ListView.ItemTemplate>
</ListView>
```

### Tags Format

```csharp
// In ViewModel
public List<string> NoteTags => new() { "work", "important", "review" };
```

---

## AnimatedCard

### Basic Usage

```xml
<controls:AnimatedCard>
    <StackPanel Spacing="12">
        <TextBlock Text="Card Title" Style="{StaticResource SubtitleTextBlockStyle}"/>
        <TextBlock Text="Card content goes here..."/>
    </StackPanel>
</controls:AnimatedCard>
```

### With Data Binding

```xml
<controls:AnimatedCard CardContent="{Binding MyContent}">
    <!-- Content automatically bound -->
</controls:AnimatedCard>
```

### Hover Effects

- **Hover**: Scale 1.02, elevation lift
- **Press**: Scale 0.98, slight dip
- **Exit**: Smooth return to normal

---

## LoadingIndicator

### Basic Usage

```xml
<controls:LoadingIndicator/>
```

### Custom Message

```xml
<controls:LoadingIndicator Message="Loading your notes..."/>
```

### Conditional Display

```xml
<Grid>
    <!-- Content -->
    <controls:LoadingIndicator 
        Message="Syncing..."
        Visibility="{Binding IsLoading, Converter={StaticResource BoolToVisibilityConverter}}"/>
</Grid>
```

### In ViewModel

```csharp
[ObservableProperty]
private bool isLoading = false;

public async Task LoadDataAsync()
{
    IsLoading = true;
    try
    {
        await _service.LoadAsync();
    }
    finally
    {
        IsLoading = false;
    }
}
```

---

## EmptyState

### Basic Usage

```xml
<controls:EmptyState 
    Icon="&#xE74C;"
    Title="No notes found"
    Description="Start by creating your first note"/>
```

### With Action Button

```xml
<controls:EmptyState 
    Icon="&#xE8F1;"
    Title="Your notebook is empty"
    Description="Create notes, organize with tags, and link your ideas"
    ActionButtonText="Create First Note"
    ActionButtonClicked="CreateNote_Click"/>
```

### Dynamic Display

```xml
<Grid>
    <!-- Show list if has notes, otherwise show empty state -->
    <ListView ItemsSource="{Binding Notes}"
              Visibility="{Binding Notes.Count, Converter={StaticResource CountToVisibilityConverter}}"/>
    
    <controls:EmptyState 
        Icon="&#xE8F1;"
        Title="No notes yet"
        Description="Get started by creating your first note"
        ActionButtonText="Create Note"
        ActionButtonClicked="CreateNote_Click"
        Visibility="{Binding Notes.Count, Converter={StaticResource CountToVisibilityConverter}, ConverterParameter='Inverted'}"/>
</Grid>
```

### Common Icons

- Notes: `&#xE8F1;`
- Search: `&#xE721;`
- Tag: `&#xE8EC;`
- Graph: `&#xE915;`
- Error: `&#xE783;`
- Info: `&#xE946;`

---

## UIHelper Animations

### Fade In Element

```csharp
using LuminaNotes.WinUI.Helpers;

// When page loads
Loaded += (s, e) => UIHelper.FadeIn(myCard);
```

### Fade Out Before Hide

```csharp
await UIHelper.FadeOutAsync(myElement);
myElement.Visibility = Visibility.Collapsed;
```

### Slide In from Bottom

```csharp
// Animate new items appearing
foreach (var card in newCards)
{
    myPanel.Children.Add(card);
    UIHelper.SlideInFromBottom(card, distance: 30);
    await Task.Delay(50); // Stagger animation
}
```

### Success Feedback

```csharp
private async Task SaveNoteAsync()
{
    await _noteService.SaveAsync(note);
    await UIHelper.ShowSuccessAsync(saveButton);
    ShowNotification("Note saved successfully!");
}
```

---

## Complete Page Example

```xml
<Page x:Class="LuminaNotes.WinUI.Pages.NotesPage">
    <Grid RowSpacing="16" Padding="24">
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="*"/>
        </Grid.RowDefinitions>

        <!-- Header -->
        <TextBlock Grid.Row="0" 
                   Text="My Notes" 
                   Style="{StaticResource PageTitleStyle}"/>

        <!-- Content Area -->
        <Grid Grid.Row="1">
            <!-- Loading State -->
            <controls:LoadingIndicator 
                Message="Loading notes..."
                Visibility="{x:Bind ViewModel.IsLoading, Mode=OneWay, Converter={StaticResource BoolToVisibilityConverter}}"/>

            <!-- Notes List -->
            <ListView ItemsSource="{x:Bind ViewModel.Notes, Mode=OneWay}"
                      SelectionMode="Single"
                      Visibility="{x:Bind ViewModel.HasNotes, Mode=OneWay, Converter={StaticResource BoolToVisibilityConverter}}">
                <ListView.ItemTemplate>
                    <DataTemplate x:DataType="models:Note">
                        <controls:NoteCard
                            Title="{x:Bind Title}"
                            ContentPreviewText="{x:Bind Content}"
                            IsPinned="{x:Bind IsPinned}"
                            DateString="{x:Bind Updated, Converter={StaticResource DateTimeToStringConverter}}"/>
                    </DataTemplate>
                </ListView.ItemTemplate>
            </ListView>

            <!-- Empty State -->
            <controls:EmptyState 
                Icon="&#xE8F1;"
                Title="No notes found"
                Description="Create your first note to get started"
                ActionButtonText="Create Note"
                ActionButtonClicked="CreateNote_Click"
                Visibility="{x:Bind ViewModel.IsEmpty, Mode=OneWay, Converter={StaticResource BoolToVisibilityConverter}}"/>
        </Grid>
    </Grid>
</Page>
```

```csharp
public sealed partial class NotesPage : Page
{
    public NotesViewModel ViewModel { get; }

    public NotesPage()
    {
        InitializeComponent();
        ViewModel = Ioc.Default.GetRequiredService<NotesViewModel>();
        Loaded += OnLoaded;
    }

    private async void OnLoaded(object sender, RoutedEventArgs e)
    {
        await ViewModel.LoadNotesAsync();
        UIHelper.FadeIn(this);
    }

    private async void CreateNote_Click(object sender, RoutedEventArgs e)
    {
        await ViewModel.CreateNoteAsync();
        await UIHelper.ShowSuccessAsync((FrameworkElement)sender);
    }
}
```

---

## Tips & Tricks

### Performance

- Use virtualization for long lists (`ListView` with `ItemsStackPanel`)
- Debounce text input in `RichEditorControl`
- Lazy load images in cards

### Accessibility

- Set `AutomationProperties.Name` on custom controls
- Ensure keyboard navigation works
- Test with high contrast themes

### Theming

- All controls adapt to Light/Dark theme automatically
- Use `ThemeResource` brushes, not hardcoded colors
- Test both themes during development

---

*Last updated: January 2026*
