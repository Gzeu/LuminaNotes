# 🌟 LuminaNotes

**AI-Powered Personal Knowledge Management for Windows**

A modern, privacy-focused note-taking application built with WinUI 3.0, .NET 9, and local AI integration via Ollama. Features Fluent Design, bidirectional linking, graph visualization, intelligent assistance, and **fully customizable themes with design tokens**—all running locally on your machine.

![Windows](https://img.shields.io/badge/Windows-11%2F12-0078D6?logo=windows)
![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet)
![License](https://img.shields.io/badge/license-MIT-green)

## ✨ Key Features

### 📝 Note Management
- **Daily Notes** - Automatic daily note creation with calendar integration
- **Markdown Support** - Rich text editing with Markdown syntax
- **Bi-directional Links** - Connect notes with `[[wiki-style]]` links
- **Tagging System** - Organize with hierarchical tags
- **Full-Text Search** - Fast SQLite-based search

### 🤖 AI Integration (Ollama)
- **Summarization** - Condense long notes instantly
- **Text Rewriting** - Adjust tone and style
- **Idea Generation** - Brainstorm based on context
- **Q&A on Notes** - Ask questions about your knowledge base
- **100% Local** - No cloud, complete privacy

### 🎨 Modern Windows UI
- **Fluent Design 3.0** - Mica/Acrylic backgrounds
- **Dark/Light Themes** - Automatic or manual
- **Design Tokens System** - 8 built-in themes + full customization
- **Theme Customizer** - Visual editor with live preview
- **Responsive Layout** - Sidebar navigation + content + AI panel
- **Quick Capture** - Always-on-top mini window (coming soon)

### 📊 Knowledge Graph
- **Visual Graph View** - See connections between notes
- **Interactive Navigation** - Click nodes to jump to notes
- **Community Toolkit** - Leveraging WinUI controls

## 🎨 Design System

### Built-in Theme Presets

| Theme | Description | Colors |
|-------|-------------|--------|
| **Default** | Modern vibrant | Indigo → Purple → Pink |
| **Ocean** | Calm professional | Sky Blue → Cyan |
| **Forest** | Natural fresh | Green → Teal → Lime |
| **Sunset** | Warm energetic | Amber → Orange → Red |
| **Midnight** | Dark mysterious | Purple → Fuchsia |
| **Monochrome** | Minimalist neutral | Gray scale |
| **Compact** | Dense UI | Reduced spacing |
| **Comfortable** | Spacious relaxed | Generous spacing |

### Customization

- **Color Pickers**: Customize accent colors
- **Typography Sliders**: Adjust font sizes
- **Density Presets**: Compact/Default/Comfortable
- **Export/Import**: Share themes as JSON
- **Live Preview**: See changes in real-time

📖 [Complete Design System Documentation](docs/DESIGN_SYSTEM.md)

🎨 [Design Tokens Guide](docs/DESIGN_TOKENS.md)

## 🛠️ Tech Stack (January 2026)

| Component | Technology | Version |
|-----------|------------|----------|
| **Frontend** | WinUI 3 (Windows App SDK) | 1.8.2 |
| **Backend** | .NET | 9.0 LTS |
| **MVVM** | CommunityToolkit.Mvvm | 8.3.0 |
| **Database** | SQLite-net-pcl | 1.9.172 |
| **AI** | OllamaSharp | 0.6.0 |
| **Markdown** | Markdig | 0.35.0 |
| **UI Controls** | CommunityToolkit.WinUI.UI.Controls | 8.1.0 |

## 🚀 Getting Started

### Prerequisites
- **Windows 11 22H2+** or **Windows 12 Preview**
- **Visual Studio 2025** (v17.12+) with:
  - .NET 9.0 SDK
  - Windows App SDK workload
- **Ollama** (for AI features):
  ```bash
  # Install Ollama from https://ollama.ai
  ollama pull llama3.1:8b
  ```

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/Gzeu/LuminaNotes.git
   cd LuminaNotes
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Build and run**
   ```bash
   dotnet build
   dotnet run --project LuminaNotes.WinUI
   ```

   Or open `LuminaNotes.sln` in Visual Studio and press F5.

### Configuration

- **AI Settings**: Configure Ollama model in Settings page (default: `llama3.1:8b`)
- **Database**: Auto-created at `%LOCALAPPDATA%\LuminaNotes\LuminaNotes.db`
- **Theme**: Customize in Settings → Theme Customizer
- **Design Tokens**: Stored at `%LOCALAPPDATA%\LuminaNotes\design-tokens.json`

## 📁 Project Structure

```
LuminaNotes/
├── LuminaNotes.Core/          # Business logic & services
│   ├── Models/                # Data models (Note, Tag, Link, DesignTokens)
│   ├── Services/              # AI, Database, Note, Graph, Theme services
│   └── Utilities/             # Encryption, Markdown helpers
├── LuminaNotes.WinUI/         # UI layer
│   ├── Pages/                 # DailyNotes, AllNotes, Graph, Search, Settings, ThemeCustomizer
│   ├── ViewModels/            # MVVM view models
│   ├── Controls/              # Custom controls (RichEditor, NoteCard, AnimatedCard, etc.)
│   ├── Styles/                # Design system (Theme, Button, Card, Text, Animation styles)
│   ├── Converters/            # Value converters
│   ├── Helpers/               # UI helpers, animations
│   ├── Services/              # ThemeApplier
│   └── Assets/                # Icons, images
├── docs/                      # Documentation
│   ├── DESIGN_SYSTEM.md       # Complete design guide
│   ├── DESIGN_TOKENS.md       # Tokens system documentation
│   └── CONTROLS_USAGE.md      # Custom controls guide
└── README.md
```

## 🗺️ Roadmap

### Phase 1: MVP (Weeks 1-2) ✅
- [x] Project setup with WinUI 3 + .NET 9
- [x] Design system with tokens
- [x] Theme customizer with 8 presets
- [x] Custom UI controls
- [ ] Basic note CRUD
- [ ] Daily notes functionality
- [ ] Ollama integration
- [ ] Markdown editor

### Phase 2: Core Features (Weeks 3-4)
- [ ] Bidirectional linking with `[[]]` syntax
- [ ] Tag management
- [ ] Full-text search
- [ ] Graph view visualization
- [ ] Import/export (Markdown, JSON)

### Phase 3: Polish (Weeks 5-8)
- [ ] Encryption (AES-256)
- [ ] Quick capture window (global hotkey)
- [ ] Advanced AI prompts (custom templates)
- [ ] Plugin system (extensibility)
- [ ] Microsoft Store release

## 🤝 Contributing

Contributions welcome! Please:
1. Fork the repo
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit changes (`git commit -m 'Add amazing feature'`)
4. Push to branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

📖 [Contributing Guidelines](CONTRIBUTING.md)

## 📄 License

MIT License - see [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- [Ollama](https://ollama.ai) - Local LLM runtime
- [Windows Community Toolkit](https://github.com/CommunityToolkit/WindowsCommunityToolkit) - UI controls
- [Obsidian](https://obsidian.md) & [Notion](https://notion.so) - Inspiration for features

## 📧 Contact

**George Pricop** - [@Gzeu](https://github.com/Gzeu)

Project Link: [https://github.com/Gzeu/LuminaNotes](https://github.com/Gzeu/LuminaNotes)

---

*Built with ❤️ in București, Romania* 🇷🇴
