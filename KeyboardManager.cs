using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML;
using SFML.Window;

namespace Swing
{
    class KeyboardManager
    {
        private static readonly bool[] keys = new bool[(int)Keyboard.Key.KeyCount];

        public static void OnKeyDown(object sender, KeyEventArgs e)
        {
            keys[(int)e.Code] = true;
        }

        public static void OnKeyUp(object sender, KeyEventArgs e)
        {
            keys[(int)e.Code] = false;
        }

        public static bool GetKey(Keyboard.Key key)
        {
            return keys[(int)key];
        }
    }
}
