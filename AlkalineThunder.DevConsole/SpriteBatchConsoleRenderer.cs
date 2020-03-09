using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlkalineThunder.DevConsole
{
    public sealed class SpriteBatchConsoleRenderer : IConsoleRenderer, IDisposable
    {
        private SpriteBatch _batch = null;
        private Texture2D _white = null;

        public SpriteBatchConsoleRenderer(SpriteBatch batch)
        {
            _batch = batch;
            _white = new Texture2D(_batch.GraphicsDevice, 1, 1);
            _white.SetData<uint>(new[] { 0xFFFFFFFF });
        }

        public void Begin()
        {
            _batch.Begin();
        }

        public void Dispose()
        {
            _batch = null;
            _white.Dispose();
            _white = null;
        }

        public void DrawRectangle(Rectangle rect, Color color, int thickness)
        {
            if (thickness < 1) return;

            FillRectangle(new Rectangle(rect.X, rect.Y, thickness, rect.Height), color);
            FillRectangle(new Rectangle(rect.X + thickness, rect.Y, rect.Width - (thickness * 2), thickness), color);
            FillRectangle(new Rectangle(rect.Right - thickness, rect.Y, thickness, rect.Height), color);
            FillRectangle(new Rectangle(rect.X + thickness, rect.Bottom - thickness, rect.Width - (thickness * 2), thickness), color);
        }

        public void DrawString(SpriteFont font, string text, Vector2 position, Color color)
        {
            _batch.DrawString(font, text, position, color);
        }

        public void End()
        {
            _batch.End();
        }

        public void FillRectangle(Rectangle rect, Color color)
        {
            FillRectangle(rect, _white, color);
        }

        public void FillRectangle(Rectangle rect, Texture2D texture, Color color)
        {
            _batch.Draw(texture ?? _white, rect, color);
        }
    }
}
