using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlkalineThunder.DevConsole
{
    public sealed class DevConsole : DrawableGameComponent
    {
        private List<ConsoleText> _lines = new List<ConsoleText>();

        public IConsoleRenderer Renderer { get; private set; }
        public ConsoleInputManager InputManager { get; set; }

        public Color BorderColor { get; set; } = Color.CornflowerBlue;
        public Color BackgroundColor { get; set; } = new Color(22, 22, 22);
        public float BackgroundOpacity { get; set; } = 0.6f;
        public int BorderThickness { get; set; } = 2;
        public int InnerPadding { get; set; } = 3;
        public int OuterPadding { get; set; } = 12;
        public bool UseFullHeight { get; set; } = false;
        public int InputBorderThickness { get; set; } = 1;
        public Color InputBorderColor { get; set; } = Color.CornflowerBlue;
        public Color InputColor { get; set; } = Color.White;


        public string InputPrompt { get; set; } = "> ";

        public SpriteFont ConsoleFont { get; set; }

        public Color DefaultColor { get; set; } = Color.White;

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(
                    OuterPadding,
                    OuterPadding,
                    Game.GraphicsDevice.PresentationParameters.BackBufferWidth - (OuterPadding * 2),
                    ((UseFullHeight ? Game.GraphicsDevice.PresentationParameters.BackBufferHeight : Game.GraphicsDevice.PresentationParameters.BackBufferHeight / 2) - (OuterPadding * 2))
                    );
            }
        }

        public DevConsole(Game game, IConsoleRenderer renderer) : base(game)
        {
            Renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            InputManager = new ConsoleInputManager(Game);
            Game.Components.Add(InputManager);
            Game.Components.Add(this);
        }

        public void Print(string text)
        {
            Print(text, DefaultColor);
        }

        public void Print(string text, Color color)
        {
            _lines.Add(new ConsoleText(text, color));
        }

        public void Clear()
        {
            _lines.Clear();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if(InputManager.IsConsoleOpen && ConsoleFont != null)
            {
                Renderer.Begin();

                Renderer.FillRectangle(Bounds, BackgroundColor * BackgroundOpacity);
                Renderer.DrawRectangle(Bounds, BorderColor, BorderThickness);

                var inputLine = InputPrompt + InputManager.InputText;
                var inputPos = InputManager.CursorPos + InputPrompt.Length;

                var inputTextHeight = ConsoleFont.MeasureString(inputLine).Y;
                var inputHeight = inputTextHeight + (InnerPadding * 2);

                Renderer.FillRectangle(
                    new Rectangle(
                        Bounds.Left + BorderThickness, 
                        Bounds.Bottom - BorderThickness - (int)inputHeight - InputBorderThickness,
                        Bounds.Width - (BorderThickness * 2), InputBorderThickness), 
                    InputBorderColor);

                var cursorPos = ConsoleFont.MeasureString(inputLine.Substring(0, inputPos)).X + Bounds.Left + BorderThickness + InnerPadding;

                Renderer.DrawString(ConsoleFont, inputLine, new Vector2(Bounds.Left + BorderThickness + InnerPadding, Bounds.Bottom - BorderThickness - InnerPadding - inputTextHeight), InputColor);
                Renderer.DrawString(ConsoleFont, "_", new Vector2(cursorPos, Bounds.Bottom - BorderThickness - InnerPadding - inputTextHeight), InputColor);

                float linePos = Bounds.Bottom - BorderThickness - inputHeight - InputBorderThickness - InnerPadding;

                for(int i = _lines.Count - 1; i >= 0; i--)
                {
                    var line = _lines[i];

                    var lineText = line.Text;
                    var lineColor = line.Color;

                    var lineMeasure = ConsoleFont.MeasureString(lineText);

                    linePos -= lineMeasure.Y;

                    Renderer.DrawString(ConsoleFont, lineText, new Vector2(Bounds.Left + BorderThickness + InnerPadding, linePos), lineColor);

                    if (linePos <= Bounds.Top + BorderThickness + InnerPadding) break;
                }

                Renderer.End();
            }
        }
    }
}
