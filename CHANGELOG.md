# 📦 Changelog — WinPilot

All notable changes to this project will be documented here.

This project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

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

- Project fully structured: `Views`, `ViewModels`, `Helpers`, `Services`, `Native`
- Shared Win32API helper
- Clean `App.config` / `settings.settings` sync
- `README.md`, `LICENSE`, `CHANGELOG.md` added

---

## 🛣️ Roadmap (Next)

- [ ] Log history of suggestions with prompt and screenshot
- [ ] GPT prompt debugger / preview
- [ ] More languages