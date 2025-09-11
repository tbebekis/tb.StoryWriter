namespace StoryWriter
{
    static public partial class App
    {
        static public void CloseAllUi()
        {
            SideBarPagerHandler.CloseAll();
            ContentPagerHandler.CloseAll();
        }
        static public void ShowSideBarPages()
        {
            SideBarPagerHandler.ShowPage(typeof(UC_TagList), nameof(UC_TagList), null);
            SideBarPagerHandler.ShowPage(typeof(UC_ComponentTypeList), nameof(UC_ComponentTypeList), null);
            SideBarPagerHandler.ShowPage(typeof(UC_ComponentList), nameof(UC_ComponentList), null);                     
            SideBarPagerHandler.ShowPage(typeof(UC_NoteList), nameof(UC_NoteList), null);
            SideBarPagerHandler.ShowPage(typeof(UC_Search), nameof(UC_Search), null);
            SideBarPagerHandler.ShowPage(typeof(UC_QuickViewList), nameof(UC_QuickViewList), null);
            SideBarPagerHandler.ShowPage(typeof(UC_Temp), nameof(UC_Temp), null);

            var Page = SideBarPagerHandler.ShowPage(typeof(UC_ChapterList), nameof(UC_ChapterList), null);
            TabControl Pager = Page.Parent as TabControl;
            Pager.SelectTab(Pager.TabPages.Count - 1);     
        }

        /// <summary>
        /// Shows the settings dialog
        /// </summary>
        static public void ShowSettingsDialog()
        {
            string Message = @"This will close all opened UI. 
Any unsaved changes will be lost. 
Do you want to continue?
";
            if (!App.QuestionBox(Message))
                return;

            App.CloseStory();
            Application.DoEvents();

            if (AppSettingDialog.ShowModal())
            {
                AutoSaveService.Enabled = false;

                App.Settings.Load();

                AutoSaveService.AutoSaveSecondsInterval = Settings.AutoSaveSecondsInterval;
                AutoSaveService.Enabled = Settings.AutoSave;
            }

            App.LoadLastStory();
        }
        /// <summary>
        /// Opens an existing story and makes it the current story
        /// </summary>
        static public void SelectStoryToOpen()
        {
            List<string> List = App.GetStoryNameList();

            if (App.CloseStory != null)
                List.Remove(App.CurrentStory.Name);

            if (List.Count == 0)
            {
                string Message = "No projects found. Please create a new story.";
                App.ErrorBox(Message);
                LogBox.AppendLine(Message);
                return;
            }

            if (SelectStoryDialog.ShowModal(List, out string StoryName))
            {
                App.LoadStory(StoryName, CloseCurrentStory: true);
            }

        }

        // ● tag to components relation
        /// <summary>
        /// Edits the tags of the given component
        /// </summary>
        static public void AddTagsToComponent(Component Component)
        {
            List<string> ItemSelectedIdList = CurrentStory.TagToComponentList
                .Where(x => x.ComponentId == Component.Id)
                .Select(x => x.TagId)
                .ToList();

            List<Tag> TagList = new(CurrentStory.TagList);
            TagList.Sort((x, y) => x.Name.CompareTo(y.Name));

            List<BaseEntity> ItemList = TagList.Cast<BaseEntity>().ToList();

            string Title = $"Select Tags for Component: {Component.Name}";

            if (SelectItemsForItemDialog.ShowModal(Title, ItemList, Component, ItemSelectedIdList))
            {
                TagList = ItemList.Cast<Tag>().ToList();
                CurrentStory.AdjustComponentTags(Component, TagList);
                TagToComponetsChanged?.Invoke(null, EventArgs.Empty);
            }
        }
        static public void AddComponentsToTag(Tag Tag)
        {
            List<string> ItemSelectedIdList = CurrentStory.TagToComponentList
                .Where(x => x.TagId == Tag.Id)
                .Select(x => x.ComponentId)
                .ToList();

            List<Component> ComponentList = new(CurrentStory.ComponentList);
            ComponentList.Sort((x, y) => x.Name.CompareTo(y.Name));

            List<BaseEntity> ItemList = ComponentList.Cast<BaseEntity>().ToList();

            string Title = $"Select Componets for Tag: {Tag.Name}";

            if (SelectItemsForItemDialog.ShowModal(Title, ItemList, Tag, ItemSelectedIdList))
            {
                ComponentList = ItemList.Cast<Component>().ToList();
                CurrentStory.AdjustTagComponents(Tag, ComponentList);
                TagToComponetsChanged?.Invoke(null, EventArgs.Empty);
            }
        }
        static public void AddScenesToComponent(Component Component)
        {
            List<string> ItemSelectedIdList = CurrentStory.ComponentToSceneList
                .Where(x => x.ComponentId == Component.Id)
                .Select(x => x.SceneId)
                .ToList();

            List<Scene> SceneList = new(CurrentStory.SceneList);
            SceneList.Sort((x, y) => x.Name.CompareTo(y.Name));

            List<BaseEntity> ItemList = SceneList.Cast<BaseEntity>().ToList();

            string Title = $"Select Scenes for Component: {Component.Name}";

            if (SelectItemsForItemDialog.ShowModal(Title, ItemList, Component, ItemSelectedIdList))
            {
                SceneList = ItemList.Cast<Scene>().ToList();
                CurrentStory.AdjustComponentScenes(Component, SceneList);
                TagToComponetsChanged?.Invoke(null, EventArgs.Empty);
            }
        }
        static public void AddComponentsToScene(Scene Scene)
        {
            List<string> ItemSelectedIdList = CurrentStory.ComponentToSceneList
                .Where(x => x.SceneId == Scene.Id)
                .Select(x => x.ComponentId)
                .ToList();

            List<Component> ComponentList = new(CurrentStory.ComponentList);
            ComponentList.Sort((x, y) => x.Name.CompareTo(y.Name));

            List<BaseEntity> ItemList = ComponentList.Cast<BaseEntity>().ToList();

            string Title = $"Select Components for Scene: {Scene.Name}";

            if (SelectItemsForItemDialog.ShowModal(Title, ItemList, Scene, ItemSelectedIdList))
            {
                ComponentList = ItemList.Cast<Component>().ToList();
                CurrentStory.AdjustSceneComponents(Scene, ComponentList);
                TagToComponetsChanged?.Invoke(null, EventArgs.Empty);
            }
        }
    }
}
