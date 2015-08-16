using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiliconStudio.Paradox.Input;

namespace EmptyKeys.UserInterface.Input
{
    /// <summary>
    /// Implements Paradox specific input device
    /// </summary>
    public class ParadoxInputDevice : InputDeviceBase
    {
        public static SiliconStudio.Paradox.Input.InputManager NativeInputManager
        {
            get;
            set;
        }

        private MouseStateBase mouseState = new ParadoxMouseState();
        private GamePadStateBase gamePadState = new ParadoxGamePadState();
        private KeyboardStateBase keyboardState = new ParadoxKeyboardState();
        private TouchStateBase touchState = new ParadoxTouchState();

        /// <summary>
        /// Gets the state of the mouse.
        /// </summary>
        /// <value>
        /// The state of the mouse.
        /// </value>
        public override MouseStateBase MouseState
        {
            get { return mouseState; }
        }

        /// <summary>
        /// Gets the state of the game pad.
        /// </summary>
        /// <value>
        /// The state of the game pad.
        /// </value>
        public override GamePadStateBase GamePadState
        {
            get { return gamePadState; }
        }

        /// <summary>
        /// Gets the state of the keyboard.
        /// </summary>
        /// <value>
        /// The state of the keyboard.
        /// </value>
        public override KeyboardStateBase KeyboardState
        {
            get { return keyboardState; }
        }

        /// <summary>
        /// Gets or sets the state of the touch.
        /// </summary>
        /// <value>
        /// The state of the touch.
        /// </value>
        public override TouchStateBase TouchState
        {
            get { return touchState; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParadoxInputDevice"/> class.
        /// </summary>
        public ParadoxInputDevice()
            : base()
        {
            NativeInputManager.ActivatedGestures.Add(new GestureConfigDrag());
            NativeInputManager.ActivatedGestures.Add(new GestureConfigFlick());
            NativeInputManager.ActivatedGestures.Add(new GestureConfigTap());
            NativeInputManager.ActivatedGestures.Add(new GestureConfigComposite());
        }        
    }
}
