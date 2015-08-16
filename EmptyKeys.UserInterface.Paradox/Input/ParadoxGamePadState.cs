using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SiliconStudio.Paradox.Input;

namespace EmptyKeys.UserInterface.Input
{
    public class ParadoxGamePadState : GamePadStateBase
    {
        private GamePadState state;

        public override bool IsAButtonPressed
        {
            get { return state.Buttons.HasFlag(GamePadButton.A); }
        }

        public override bool IsBButtonPressed
        {
            get { return state.Buttons.HasFlag(GamePadButton.B); }
        }

        public override bool IsCButtonPressed
        {
            get { return state.Buttons.HasFlag(GamePadButton.Y); }
        }

        public override bool IsDButtonPressed
        {
            get { return state.Buttons.HasFlag(GamePadButton.X); }
        }

        public override PointF DPad
        {
            get
            {
                PointF pad = new PointF();
                pad.X = state.Buttons.HasFlag(GamePadButton.PadRight) ? 1 : pad.X;
                pad.Y = state.Buttons.HasFlag(GamePadButton.PadUp) ? 1 : pad.Y;
                pad.X = state.Buttons.HasFlag(GamePadButton.PadLeft) ? -1 : pad.X;
                pad.Y = state.Buttons.HasFlag(GamePadButton.PadDown) ? -1 : pad.Y;
                return pad;
            }
        }

        public override bool IsLeftShoulderButtonPressed
        {
            get { return state.Buttons.HasFlag(GamePadButton.LeftShoulder); }
        }

        public override bool IsLeftStickButtonPressed
        {
            get { return state.Buttons.HasFlag(GamePadButton.LeftThumb); }
        }

        public override PointF LeftThumbStick
        {
            get { return new PointF(state.LeftThumb.X, state.LeftThumb.Y); }
        }

        public override float LeftTrigger
        {
            get { return state.LeftTrigger; }
        }

        public override int PlayerNumber
        {
            get { return 0; }
        }

        public override bool IsRightShoulderButtonPressed
        {
            get { return state.Buttons.HasFlag(GamePadButton.RightShoulder); }
        }

        public override bool IsRightStickButtonPressed
        {
            get { return state.Buttons.HasFlag(GamePadButton.RightThumb); ; }
        }

        public override PointF RightThumbStick
        {
            get { return new PointF(state.RightThumb.X, state.RightThumb.Y); }
        }

        public override float RightTrigger
        {
            get { return state.RightTrigger; }
        }

        public override bool IsSelectButtonPressed
        {
            get { return state.Buttons.HasFlag(GamePadButton.Back); }
        }

        public override bool IsStartButtonPressed
        {
            get { return state.Buttons.HasFlag(GamePadButton.Start); }
        }

        public override void Update(int gamePadIndex)
        {
            if (ParadoxInputDevice.NativeInputManager.HasGamePad)
            {
                state = ParadoxInputDevice.NativeInputManager.GetGamePad(0);
            }
        }
    }
}
