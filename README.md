# ✨ WinPilot — Your Context-Aware AI Copilot for Windows

![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet)
![WPF](https://img.shields.io/badge/WPF-MVVM-darkgreen)
![AI](https://img.shields.io/badge/AI-Multi--Provider-blue)
![License](https://img.shields.io/badge/License-MIT-brightgreen)
![MadeBy](https://img.shields.io/badge/Made%20with%20❤️%20by-BlinkSun-blue)

---

## 🧠 What is WinPilot?

> _Ever wish AI could just pop in and finish your thoughts... before you even ask?_  
> No more alt-tab. No more copy-paste.  
> **WinPilot** is your universal AI assistant that works in **any Windows app**, triggered by a simple **keyboard shortcut**.

It reads your current context — active app, window title, selected text, focused content, even a screenshot — and gives you exactly what you were about to type.

Like **Copilot**, but **everywhere**.

---

## 💡 Features

- ✅ Global **hotkey trigger** (configurable)  
- ✅ Automatically reads your screen & text context  
- ✅ Takes a screenshot of the active window  
- ✅ Sends everything to **your preferred AI provider** (OpenAI or Anthropic)  
- ✅ Supports **multimodal models** (image input + text)  
- ✅ Beautiful animated suggestion popup  
- ✅ 1-click “Accept” → auto-pastes into your app  
- ✅ Full WPF + MVVM + .NET 8  
- ✅ **ResX localized** (English + Français + Español + Deutsch)  
- ✅ ❤️ Designed with love and keyboard nerd energy

---

## 🤖 Supported AI Providers

| Provider   | Models Included                    | Image Support |
|------------|-------------------------------------|----------------|
| **OpenAI** | GPT-4o, GPT-4 Turbo, GPT-4.5        | ✅              |
| **Anthropic** | Claude 3.5 / 3.7 (Sonnet, Opus, Haiku) | ✅              |

You can switch providers and models from the Settings window.

---

## 🖼️ Screenshot

> _“Cooking something...”_

![WinPilot Preview](Assets/winpilot-preview.png)

---

## 🛠 How it works

1. You press `Ctrl + Alt + W` (or your own shortcut)  
2. WinPilot captures:
   - App name
   - Window title
   - Focused control
   - Selected text (via Ctrl+C)
   - Screenshot of the active window  
3. It builds a prompt like:



```
The user is currently in SQL Server.
The selected text is: "SELECT * FROM Client".
The window title is "SSMS - Query.sql".
The user wrote in the editor: "-- I want to add a new column for the phone number."
```

4. The AI model (OpenAI or Claude) replies:

```sql
ALTER TABLE Client ADD Phone VARCHAR(50);
```

5. You accept it, and boom 💥 it gets pasted into your app. Zero effort.

---

## ⚙️ Settings

You can configure:

- Your **API key**
- The **AI model** (with image support only)
- Auto-send on hotkey
- Your preferred shortcut keys

---

## 🔐 Privacy

🧠 All context is kept locally except when explicitly sent to the AI provider.  
You can choose to disable auto-send.

---

## 🔧 Built With

- WPF (.NET 8)
- MVVM (almost zero code-behind)
- Custom Hotkey manager with Win32
- ResX resources (EN/FR/ES/DE)
- Love and stubbornness

---

## 📥 Installation

> _For now: clone + build in Visual Studio (Release mode)_  
> .EXE version & installer coming soon™

---

## 📄 License

[LICENSE](LICENSE) MIT — Free to use, remix, improve, and star ⭐  
Just don't call it "Copilot" or Microsoft might... pilot your repo into oblivion 😅

---

## 👨‍💻 About the author

**Damien Villeneuve (aka BlinkSun)**  
Dad. Dev. Dreamer.  
[blinksun.ca](https://www.blinksun.ca)

---

<p align="center">
  <strong>☕ Enjoying WinPilot?</strong><br/>
  If this tool saved you a few brain cells or ALT+TABs...
</p>

<p align="center">
  <a href="https://www.buymeacoffee.com/blinksun">
    <img src="https://img.shields.io/badge/☕-Buy me a coffee-FFDD00?style=for-the-badge&logo=buymeacoffee&logoColor=black" alt="Buy Me A Coffee"/>
  </a>
</p>

<p align="center">
  <em>Made with ❤️ by BlinkSun — fuelled by espresso and AI tokens 😄</em>
</p>