using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyKeys.UserInterface.Input;
using EmptyKeys.UserInterface.Media;
using EmptyKeys.UserInterface.Renderers;
using Microsoft.Xna.Framework.Graphics;

namespace EmptyKeys.UserInterface
{
    /// <summary>
    /// Implements FNA specific engine
    /// </summary>
    public class FNAEngine : Engine
    {
        private Renderer renderer;
        private AudioDevice audioDevice = new FNAAudioDevice();
        private AssetManager assetManager = new FNAAssetManager();
        private InputDeviceBase inputDevice = new FNAInputDevice();

        /// <summary>
        /// Gets the renderer.
        /// </summary>
        /// <value>
        /// The renderer.
        /// </value>
        public override Renderer Renderer
        {
            get { return renderer; }
        }

        /// <summary>
        /// Gets the audio device.
        /// </summary>
        /// <value>
        /// The audio device.
        /// </value>
        public override AudioDevice AudioDevice
        {
            get { return audioDevice; }
        }

        /// <summary>
        /// Gets the asset manager.
        /// </summary>
        /// <value>
        /// The asset manager.
        /// </value>
        public override AssetManager AssetManager
        {
            get { return assetManager; }
        }

        /// <summary>
        /// Gets the input device.
        /// </summary>
        /// <value>
        /// The input device.
        /// </value>
        public override InputDeviceBase InputDevice
        {
            get { return inputDevice; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FNAEngine"/> class.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="nativeScreenWidth">Width of the native screen.</param>
        /// <param name="nativeScreenHeight">Height of the native screen.</param>
        public FNAEngine(GraphicsDevice graphicsDevice, int nativeScreenWidth, int nativeScreenHeight)
            : base()
        {            
            renderer = new FNARenderer(graphicsDevice, nativeScreenWidth, nativeScreenHeight);
        }        
    }
}
