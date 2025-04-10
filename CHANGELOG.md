# 📦 Changelog — WinPilot

All notable changes to this project will be documented here.

This project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

---

## [1.1.0] - 2025-04-11

🧠 Multi-AI support is here! WinPilot is no longer GPT-exclusive.

### ✨ Features

- Switched from "OpenAI-only" to **generic AI architecture**
- Added support for **Claude 3 models** from **Anthropic** (Sonnet, Opus, Haiku, etc.)
- Unified model selector (`ChatModel`) with provider info (`OpenAI`, `Anthropic`)
- New `AnthropicClient` with native support for **image input**
- Abstracted all AI calls via a common interface `IAIProviderService`
- Updated settings and bindings to reflect this shift:
  - `OpenAIKey` → `APIKey`
  - Display updated throughout UI (`OpenAI` → `AI`)
- Auto-filter for models that **support image input only**
- Dynamic handling of provider-specific API logic (headers, endpoints, response parsing)
- Internal refactor: renamed `GPTModel` → `ChatModel`, moved to `ChatModels.cs`

### 🛠 Internal

- Future-ready architecture for supporting additional providers (Google, Mistral, etc.)
- Even cleaner ViewModel/Service separation
- No breaking changes for users (preserves previous config)

---

## [1.0.0] - 2025-04-09

🎉 First official release! It's alive!

### ✨ Features

- Global keyboard shortcut (configurable) to trigger the AI popup
- Automatically captures:
  - Process name
  - Window title
  - Focused control
  - Selected text (via clipboard)
  - Screenshot of the active window
- Builds an enriched prompt with context
- Sends data to OpenAI GPT API (with image support)
- Displays suggestion with fade-in animation
- Paste result automatically in the original app
- Custom loading animation (✨ Copilot style)
- Complete WPF MVVM architecture (almost no code-behind for logic)
- Clean Win32 API helpers (clipboard, input, hotkey, screenshot, etc.)
- Settings window:
  - Choose your model
  - Set your OpenAI API key
  - Enable/disable auto-send
  - Customize your hotkey
- Full RESX localization (English + Français + Español + Deutsch)
- Dark theme UI + scrollbar + glyphs
- Sexy icon with ✨

### 🔒 Privacy

- Local-only context collection (except on explicit GPT request)
- No background tracking
- No telemetry
- No OpenAI key included

### 🧹 Internal / Dev

- Project fully structured: `Views`, `ViewModels`, `Helpers`, `Services`, `Native`, etc.
- Shared Win32API helper
- Clean `App.config` / `settings.settings` sync
- `README.md`, `LICENSE`, `CHANGELOG.md` added

---

## 🛣️ Roadmap (Next)

- [ ] Log history of suggestions with prompt and screenshot
- [ ] GPT prompt debugger / preview
- [ ] More languages