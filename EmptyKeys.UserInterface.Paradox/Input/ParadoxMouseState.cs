using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SiliconStudio.Core.Mathematics;

namespace EmptyKeys.UserInterface.Input
{
    public class ParadoxMouseState : MouseStateBase
    {        
        private int scrollWheelValue;

        public override bool IsLeftButtonPressed
        {
            get { return ParadoxInputDevice.NativeInputManager.IsMouseButtonDown(SiliconStudio.Paradox.Input.MouseButton.Left); }
        }

        public override bool IsMiddleButtonPressed
        {
            get { return ParadoxInputDevice.NativeInputManager.IsMouseButtonDown(SiliconStudio.Paradox.Input.MouseButton.Middle); }
        }

        public override bool IsRightButtonPressed
        {
            get { return ParadoxInputDevice.NativeInputManager.IsMouseButtonDown(SiliconStudio.Paradox.Input.MouseButton.Right); }
        }

        public override float NormalizedX
        {
            get { return ParadoxInputDevice.NativeInputManager.MousePosition.X; }
        }

        public override float NormalizedY
        {
            get { return ParadoxInputDevice.NativeInputManager.MousePosition.Y; }
        }

        public override int ScrollWheelValue
        {
            get { return scrollWheelValue; }
        }

        public override bool IsVisible
        {
            get
            {
                return false;
            }
            set
            {
                
            }
        }

        public override void SetPosition(int x, int y)
        {
        }

        public override void Update()
        {            
            scrollWheelValue += (int)ParadoxInputDevice.NativeInputManager.MouseWheelDelta;
        }
    }
}
