using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlkalineThunder.DevConsole
{
    public sealed class ConsoleInputManager : GameComponent
    {
        public bool IsConsoleOpen { get; private set; } = false;
        public Keys ConsoleToggleKey { get; set; } = Keys.OemTilde;
        public Keys FullHeightToggle { get; set; } = Keys.F11;

        public string InputText { get; private set; } = string.Empty;
        public int CursorPos { get; private set; } = 0;

        public DevConsole DevConsole => Game.Components.First(x => x is DevConsole) as DevConsole;

        public ConsoleInputManager(Game game) : base(game)
        {

        }

        public override void Initialize()
        {
            Game.Window.KeyUp += HandleKeyUp;
            Game.Window.KeyDown += HAndleKeyDown;
            Game.Window.TextInput += HandleTextInput;
            base.Initialize();
        }

        private void HandleTextInput(object sender, TextInputEventArgs e)
        {
            if (e.Key == Keys.OemTilde) return;

            if(IsConsoleOpen)
            {
                switch(e.Character)
                {
                    case '\u007f': // DEL
                        if(CursorPos < InputText.Length)
                        {
                            InputText = InputText.Remove(CursorPos, 1);
                        }
                        break;
                    case '\b':
                        if(CursorPos > 0)
                        {
                            CursorPos--;
                            InputText = InputText.Remove(CursorPos, 1);
                        }
                        break;
                    case '\r':
                    case '\n':
                        if(InputText.Length > 0)
                        {
                            CursorPos = 0;
                            DevConsole.Print(" >>> " + InputText + Environment.NewLine, DevConsole.InputColor);
                            InputText = "";

                        }
                        break;
                    default:
                        if(DevConsole.ConsoleFont.Characters.Contains(e.Character))
                        {
                            InputText = InputText.Insert(CursorPos, e.Character.ToString());
                            CursorPos++;
                        }
                        break;
                }
            }
        }

        private void HAndleKeyDown(object sender, InputKeyEventArgs e)
        {
            if(e.Key == Keys.Left)
            {
                if (CursorPos > 0) CursorPos--;
            }
            if(e.Key == Keys.Home)
            {
                CursorPos = 0;
            }
            if(e.Key == Keys.End)
            {
                CursorPos = InputText.Length;
            }
            if(e.Key == Keys.Right)
            {
                if (CursorPos < InputText.Length) CursorPos++;
            }
        }

        private void HandleKeyUp(object sender, InputKeyEventArgs e)
        {
            if(e.Key == ConsoleToggleKey)
            {
                IsConsoleOpen = !IsConsoleOpen;
            }

            if(IsConsoleOpen && e.Key == FullHeightToggle)
            {
                DevConsole.UseFullHeight = !DevConsole.UseFullHeight;
            }
        }
    }
}
