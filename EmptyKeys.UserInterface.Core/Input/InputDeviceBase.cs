
using System;

namespace EmptyKeys.UserInterface.Input
{
    /// <summary>
    /// Implements abstract input device
    /// </summary>
    public abstract class InputDeviceBase
    {
        /// <summary>
        /// Gets the state of the mouse.
        /// </summary>
        /// <value>
        /// The state of the mouse.
        /// </value>
        public abstract MouseStateBase MouseState { get; }

        /// <summary>
        /// Gets the state of the game pad.
        /// </summary>
        /// <value>
        /// The state of the game pad.
        /// </value>
        public abstract GamePadStateBase GamePadState { get; }

        /// <summary>
        /// Gets the state of the keyboard.
        /// </summary>
        /// <value>
        /// The state of the keyboard.
        /// </value>
        public abstract KeyboardStateBase KeyboardState { get; }

        /// <summary>
        /// Gets or sets the state of the touch.
        /// </summary>
        /// <value>
        /// The state of the touch.
        /// </value>
        public abstract TouchStateBase TouchState { get; }        

        /// <summary>
        /// Initializes a new instance of the <see cref="InputDeviceBase"/> class.
        /// </summary>
        public InputDeviceBase()
        {
        }

        /// <summary>
        /// Shows Virtual Keyboard - this is for gamepad textbox virtual keyboard (A button)
        /// </summary>
        /// <param name="onSuccess">action on success</param>
        /// <param name="onCancel">action on cancel</param>
        /// <param name="defaultText">starting text</param>
        /// <param name="title">title of the VK UI</param>
        /// <param name="maxLength">maximum length</param>
        public abstract void ShowVirtualKeyboard(Action<string> onSuccess, Action onCancel = null, string defaultText = null, string title = null, int maxLength = 0);

        /// <summary>
        /// Update method
        /// </summary>
        public abstract void Update();
    }
}
