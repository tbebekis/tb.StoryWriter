# tbStoryWriter

A lightweight Windows app that helps novelists plan, write, and organize long-form fiction.  

It combines a component tree (characters, locations, etc.), a chapter manager with reordering, a scene manager with reordering, and a focused tabbed editor for each chapter (Body, Scenes, Synopsis, Concept, Outcome).

tbStoryWriter creates a new Sqlite database for each project/book.

Chapter body, Scene, Synopsis, Concept, Outcome and Component body are saved in Rich Text Format (RTF). Actually the text editor of this application is a Rich Text editor.

> Status: Feature-complete for the current phase. More features can be added later.

## Why tbStoryWriter?

Long projects need structure **and** speed. tbStoryWriter gives you a left-hand sidebar to map your world, a chapter list you can rearrange in seconds, and a central editor that keeps each chapter’s materials (text, scenes, notes) together—no hunting through files.

## Components and Tags

**Components** are your world-building building blocks: user-defined entries (e.g., persons, locations, artifacts) organized in the left sidebar. 

A component may be assigned one or more tags. Tags are also uer defined.

Some example tags could be *Character, Location, People, Device, Event, Artifact, etc*.

You can freely define as many tags as you like and assign them to components.

A component like 

- **Navek** (under *Character* tag) or 
- **Darky** (under *Location and Planet* tags) 

is always one double-click away, so you can maintain consistent names and details without hunting through files.

## Chapter Workspace (Tabbed)

Opening a chapter opens the following tabs:

- **Body** — The main chapter text
- **Scenes** — Short decriptions to help in writing a chapter. Add, edit, remove, and re-order scene cards within the chapter
- **Synopsis** — A compact summary for quick orientation
- **Concept** — The “idea core” of the chapter
- **Outcome** — What changes (plot/character/world) after this chapter

## Links and Quick Navigation

Think of tbStoryWriter as a lightweight wiki: press **Ctrl + Left-Click** on a selected word (or hit **Ctrl + L**) and the app searches **across all components** as well as **chapter and scene titles**. 

If it finds a match, it opens that item instantly in a new editor tab, so you can jump from a mention in the text to the canonical entry (or chapter/scene) without losing your flow.


## App Settings
- **Auto-save** toggle
- **Load last project on startup**
- **Font family** and **font size** for the editor
- Settings UI: `AppSettingDialog` bound to:



```csharp
public class AppSettings
{
    public bool LoadLastProjectOnStartup { get; set; } = true;
    public string LastProject { get; set; } = "Story";
    public bool AutoSave { get; set; } = true;
    public int AutoSaveSecondsInterval { get; set; } = 30;
    public string FontFamily { get; set; } = "Times New Roman";
    public int FontSize { get; set; } = 13; 
}
````

 
## Screenshots

Tags

![Tags.png](./Images/Tags.png)

Components

![Components.png](./Images/Components.png)

Chapters

![Chapters.png](./Images/Chapters.png)

Scenes

![Scenes.png](./Images/Scenes.png)
 
## Requirements

* Windows 10/11
* .NET 9.0 SDK (or open with Visual Studio 2022 17.8+)

## Clone, Build, Run

```bash
git clone https://github.com/tbebekis/tb.StoryWriter.git
cd tb.StoryWriter
dotnet build
dotnet run --project StoryWriter.App/StoryWriter.App.csproj
```
 
## Export

Currently the application can export a project/book to the following.

- JSON, saves chapters and other
- TXT (markdown)
- RTF
- DOCX
- ODT

## Import

The application can import a previously exported JSON file and create a new project/book.

## Usage Notes

* **Projects**: tb.StoryWriter uses a Sqlite database file (default extension: `.db3`). Each project/book has its own database.
* **Chapters**: Add, edit, remove and reorder chapters. Double-click a chapter to open its editor tabs. Reorder, edit its body text, synopsis, concept, and outcome texts, add scenes, etc.
* **Scenes**: Maintain a per-chapter scene list. Add, edit, remove and reorder scenes.
* **Fonts**: Choose editor font family & size from *Settings → Editor*.
* **Auto-save**: Optional; can be toggled in settings.
* **Load last project**: Re-open your last project on startup if enabled.

## Roadmap Ideas

* Graph view of relationships (characters ↔ locations ↔ chapters)



