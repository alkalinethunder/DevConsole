using Microsoft.Xna.Framework;
using System;

namespace AlkalineThunder.DevConsole
{
    public struct ConsoleText
    {
        public string Text;
        public Color Color;

        public ConsoleText(string text, Color color)
        {
            Text = text;
            Color = color;
        }

        public ConsoleText(string text) : this(text, Color.White)
        {

        }

        public static bool operator ==(ConsoleText a, ConsoleText b)
        {
            return a.Text == b.Text && a.Color == b.Color;
        }

        public static bool operator !=(ConsoleText a, ConsoleText b)
        {
            return !(a == b);
        }

        public static implicit operator ConsoleText(string text)
        {
            return new ConsoleText(text);
        }

        public override string ToString()
        {
            return $"ConsoleText (Text={Text}, Color={Color})";
        }

        public override bool Equals(object obj)
        {
            return obj is ConsoleText ct && ct == this;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Text, Color);
        }
    }
}
