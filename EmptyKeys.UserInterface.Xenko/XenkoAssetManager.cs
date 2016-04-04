using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyKeys.UserInterface.Media;
using SiliconStudio.Core.Serialization.Assets;
using SiliconStudio.Xenko.Audio;
using SiliconStudio.Xenko.Graphics;

namespace EmptyKeys.UserInterface
{
    public class XenkoAssetManager : AssetManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XenkoAssetManager"/> class.
        /// </summary>
        public XenkoAssetManager()
            : base()
        {
        }

        public override TextureBase LoadTexture(object contentManager, string file)
        {
            file = file.Replace("\\", "/");
            ContentManager database = contentManager as ContentManager;
            Texture native = database.Load<Texture>(file);
            return Engine.Instance.Renderer.CreateTexture(native);
        }

        public override FontBase LoadFont(object contentManager, string file)
        {
            file = file.Replace("\\", "/");
            file = file.Replace(".", "-");
            ContentManager database = contentManager as ContentManager;
            SpriteFont native = database.Load<SpriteFont>(file);
            return Engine.Instance.Renderer.CreateFont(native);
        }

        public override Media.SoundBase LoadSound(object contentManager, string file)
        {
            file = file.Replace("\\", "/");
            ContentManager database = contentManager as ContentManager;
            SoundEffect native = database.Load<SoundEffect>(file);
            return Engine.Instance.AudioDevice.CreateSound(native);
        }
    }
}
