﻿using Core.Screens.GamePlay;
using Engine;
using Engine.Audio;
using Engine.Content;
using Engine.Graphics;
using Engine.Screens;
using Microsoft.Extensions.DependencyInjection;
using System.Numerics;

using XTime = Microsoft.Xna.Framework.GameTime;

namespace Core.Screens
{
    public sealed class MainMenuScreen : MenuScreen
    {
        private IPainter _painter;
        private IMusicPlayer _musicPlayer;
        private Font _h1;
        private Font _h2;
        private Font _h4;
        private string _version;
        private Vector2 _versionPosition;

        /// <summary>
        /// Constructor fills in the menu contents.
        /// </summary>
        public MainMenuScreen()
            : base("Main Menu")
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            var content = ScreenManager.Container.GetService<IContentProvider>();
            var fontService = ScreenManager.Container.GetService<IFontService>();
            _painter = ScreenManager.Container.GetService<IPainter>();
            _musicPlayer = ScreenManager.Container.GetService<IMusicPlayer>();
            _h1 = content.Load<Font>("Fonts/kenney-future.h1.font");
            _h2 = content.Load<Font>("Fonts/kenney-future.h2.font");
            _h4 = content.Load<Font>("Fonts/kenney-future.h4.font");
            _version = Version.Current;

            var viewport = ScreenManager.Container.GetService<IViewport>();
            var size = fontService.MeasureText(_version, _h4);
            _versionPosition = new Vector2(viewport.Width - size.Width, viewport.Height - size.Height);

            // Create our menu entries.
            var playGameMenuEntry = new MenuEntry("Play Game", _h2, fontService);
            var settingsMenuEntry = new MenuEntry("Settings", _h2, fontService);
            var creditsMenuEntry = new MenuEntry("Credits", _h2, fontService);
            var exitMenuEntry = new MenuEntry("Exit", _h2, fontService);

            // Hook up menu event handlers.
            playGameMenuEntry.Selected += PlayGameMenuEntrySelected;
            settingsMenuEntry.Selected += SettingsMenuEntrySelected;
            creditsMenuEntry.Selected += CreditsMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(playGameMenuEntry);
            MenuEntries.Add(settingsMenuEntry);
            MenuEntries.Add(creditsMenuEntry);
            MenuEntries.Add(exitMenuEntry);


            var music = content.Load<Music>("Music/menu.song");
            _musicPlayer.Play(music);
        }

        public override void Draw(XTime gameTime)
        {
            base.Draw(gameTime);

            _painter.DrawString(_h4, _version, _versionPosition, Colors.White);
        }

        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>
        void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, e.PlayerIndex, new StarScreen(), new GamePlayScreen());
        }

        /// <summary>
        /// Event handler for when the Options menu entry is selected.
        /// </summary>
        void SettingsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new SettingsScreen(), e.PlayerIndex);
        }

        /// <summary>
        /// Event handler for when the Credits menu entry is selected.
        /// </summary>
        void CreditsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new CreditScreen(), e.PlayerIndex);
        }

        /// <summary>
        /// When the user cancels the main menu, ask if they want to exit the sample.
        /// </summary>
        protected override void OnCancel(Microsoft.Xna.Framework.PlayerIndex playerIndex)
        {
            const string message = "Exit game?\nA button, Space, Enter = ok\nB button, Esc = cancel";

            MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message);

            confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
        }

        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to exit" message box.
        /// </summary>
        void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }
    }
}
