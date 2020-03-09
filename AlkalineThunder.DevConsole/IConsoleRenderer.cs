using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlkalineThunder.DevConsole
{
    public interface IConsoleRenderer
    {
        void FillRectangle(Rectangle rect, Color color);
        void FillRectangle(Rectangle rect, Texture2D texture, Color color);
        void DrawRectangle(Rectangle rect, Color color, int thickness);
        void DrawString(SpriteFont font, string text, Vector2 position, Color color);

        void Begin();
        void End();
    }
}
