using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SiliconStudio.Paradox.Input;

namespace EmptyKeys.UserInterface.Input
{
    public class ParadoxKeyboardState : KeyboardStateBase
    {
        private Dictionary<int, int> translationTable = new Dictionary<int, int>();

        public ParadoxKeyboardState()
            : base()
        {
            translationTable.Add(8, 2);
            translationTable.Add(9, 3);
            translationTable.Add(13, 6);
            translationTable.Add(19, 7);
            translationTable.Add(20, 8);
            translationTable.Add(21, 9);
            translationTable.Add(25, 12);
            translationTable.Add(27, 13);
            translationTable.Add(28, 14);
            translationTable.Add(29, 15);
            translationTable.Add(144, 114);
            translationTable.Add(145, 115);
            translationTable.Add(226, 154);
            translationTable.Add(254, 171);            
        }

        public override bool IsKeyPressed(KeyCode keyCode)
        {
            Keys key = Keys.None;
            int code = (int)keyCode;
            if (code >= 32 && code <= 57)
            {
                key = (Keys)(code - 14);
            }
            else
                if (code >= 65 && code <= 93)
                {
                    key = (Keys)(code - 21);
                }
                else
                    if (code >= 95 && code <= 135)
                    {
                        key = (Keys)(code - 22);
                    }
                    else
                        if (code >= 160 && code <= 183)
                        {
                            key = (Keys)(code - 44);
                        }
                        else
                            if (code >= 186 && code <= 192)
                            {
                                key = (Keys)(code - 46);
                            }
                            else
                                if (code >= 219 && code <= 223)
                                {
                                    key = (Keys)(code - 70);
                                }
                                else
                                    if (code >= 246 && code <= 251)
                                    {
                                        key = (Keys)(code - 83);
                                    }
                                    else
                                    {
                                        int newCode = 0;
                                        if (translationTable.TryGetValue(code, out newCode))
                                        {
                                            key = (Keys)newCode;
                                        }
                                        else
                                        {
                                            return false;
                                        }
                                    }

            return ParadoxInputDevice.NativeInputManager.IsKeyDown(key);
        }

        public override void Update()
        {            
        }
    }
}
