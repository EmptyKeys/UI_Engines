using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyKeys.UserInterface.Media;
using Stride.Core.Serialization.Contents;
using Stride.Audio;
using Stride.Graphics;
using Stride.Rendering;

namespace EmptyKeys.UserInterface
{
    public class StrideAssetManager : AssetManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StrideAssetManager"/> class.
        /// </summary>
        public StrideAssetManager()
            : base()
        {
        }

        /// <summary>
        /// Loads the texture.
        /// </summary>
        /// <param name="contentManager">The content manager.</param>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        public override TextureBase LoadTexture(object contentManager, string file)
        {
            file = file.Replace("\\", "/");
            ContentManager database = contentManager as ContentManager;
            Texture native = database.Load<Texture>(file);
            return Engine.Instance.Renderer.CreateTexture(native);
        }

        /// <summary>
        /// Loads the font.
        /// </summary>
        /// <param name="contentManager">The content manager.</param>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        public override FontBase LoadFont(object contentManager, string file)
        {
            file = file.Replace("\\", "/");
            file = file.Replace(".", "-");
            ContentManager database = contentManager as ContentManager;
            SpriteFont native = database.Load<SpriteFont>(file);
            return Engine.Instance.Renderer.CreateFont(native);
        }

        /// <summary>
        /// Loads the sound.
        /// </summary>
        /// <param name="contentManager">The content manager.</param>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        public override Media.SoundBase LoadSound(object contentManager, string file)
        {
            file = file.Replace("\\", "/");
            ContentManager database = contentManager as ContentManager;
            Sound native = database.Load<Sound>(file);
            return Engine.Instance.AudioDevice.CreateSound(native);
        }

        /// <summary>
        /// Loads the effect.
        /// </summary>
        /// <param name="contentManager">The EffectSystem instance</param>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        public override EffectBase LoadEffect(object contentManager, string file)
        {
            file = file.Replace("\\", "/");
            EffectSystem effectSystem = contentManager as EffectSystem;            
            Effect effect = effectSystem.LoadEffect(file).WaitForResult();
            return Engine.Instance.Renderer.CreateEffect(effect);
        }
    }
}
