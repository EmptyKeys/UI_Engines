using EmptyKeys.UserInterface.Input;
using EmptyKeys.UserInterface.Media;
using EmptyKeys.UserInterface.Renderers;
using Stride.Games;
using Stride.Rendering;

namespace EmptyKeys.UserInterface
{
    /// <summary>
    /// Implements Stride specific engine
    /// </summary>
    public class StrideEngine : Engine
    {
        private Renderer renderer;
        private AudioDevice audioDevice = new StrideAudioDevice();
        private AssetManager assetManager = new StrideAssetManager();
        private InputDeviceBase inputDevice = new StrideInputDevice();

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
        /// Initializes a new instance of the <see cref="StrideEngine"/> class.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="nativeScreenWidth">Width of the native screen.</param>
        /// <param name="nativeScreenHeight">Height of the native screen.</param>
        public StrideEngine(GraphicsDeviceManager manager, EffectSystem effectSystem)
            : base()
        {
            renderer = new StrideRenderer(manager, effectSystem);
        }

        /// <inheritdoc/>
        public override void Update()
        {
        }
    }
}
