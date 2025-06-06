﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Stride.Core.Mathematics;

namespace EmptyKeys.UserInterface.Input
{
    /// <summary>
    /// Implements Stride specific mouse state
    /// </summary>
    public class StrideMouseState : MouseStateBase
    {        
        private int scrollWheelValue;

        /// <summary>
        /// Gets a value indicating whether this instance is left button pressed.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is left button pressed; otherwise, <c>false</c>.
        /// </value>
        public override bool IsLeftButtonPressed
        {
            get { return StrideInputDevice.NativeInputManager.IsMouseButtonDown(Stride.Input.MouseButton.Left); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is middle button pressed.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is middle button pressed; otherwise, <c>false</c>.
        /// </value>
        public override bool IsMiddleButtonPressed
        {
            get { return StrideInputDevice.NativeInputManager.IsMouseButtonDown(Stride.Input.MouseButton.Middle); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is right button pressed.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is right button pressed; otherwise, <c>false</c>.
        /// </value>
        public override bool IsRightButtonPressed
        {
            get { return StrideInputDevice.NativeInputManager.IsMouseButtonDown(Stride.Input.MouseButton.Right); }
        }

        /// <summary>
        /// Gets the normalized x.
        /// </summary>
        /// <value>
        /// The normalized x.
        /// </value>
        public override float NormalizedX
        {
            get { return StrideInputDevice.NativeInputManager.MousePosition.X; }
        }

        /// <summary>
        /// Gets the normalized y.
        /// </summary>
        /// <value>
        /// The normalized y.
        /// </value>
        public override float NormalizedY
        {
            get { return StrideInputDevice.NativeInputManager.MousePosition.Y; }
        }

        /// <summary>
        /// Gets the scroll wheel value.
        /// </summary>
        /// <value>
        /// The scroll wheel value.
        /// </value>
        public override int ScrollWheelValue
        {
            get { return scrollWheelValue; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is visible.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is visible; otherwise, <c>false</c>.
        /// </value>
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

        /// <summary>
        /// Sets the position.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public override void SetPosition(int x, int y)
        {
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public override void Update()
        {            
            scrollWheelValue += (int)StrideInputDevice.NativeInputManager.MouseWheelDelta;
        }
    }
}
