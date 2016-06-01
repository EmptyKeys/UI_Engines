using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiliconStudio.Xenko.Audio;

namespace EmptyKeys.UserInterface.Media
{
    public class XenkoSound : SoundBase
    {
        private SoundEffect sound;       

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public override SoundState State
        {
            get { return sound == null ? SoundState.Stopped : (SoundState)(int) sound.PlayState; }
        }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        /// <value>
        /// The volume.
        /// </value>
        public override float Volume
        {
            get
            {
                if (sound != null)
                {
                    return sound.Volume;
                }

                return 0;
            }

            set
            {
                if (sound != null)
                {
                    sound.Volume = value;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SunBurnSound"/> class.
        /// </summary>
        /// <param name="nativeSound">The native sound.</param>
        public XenkoSound(object nativeSound)
            : base(nativeSound)
        {
            sound = nativeSound as SoundEffect;            
        }

        /// <summary>
        /// Plays this instance.
        /// </summary>
        public override void Play()
        {
            if (sound == null)
            {
                return;
            }
            
            sound.Play();
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public override void Stop()
        {
            if (sound == null)
            {
                return;
            }

            sound.Stop();
        }

        /// <summary>
        /// Pauses this instance.
        /// </summary>
        public override void Pause()
        {
            if (sound == null)
            {
                return;
            }

            sound.Pause();
        }
    }
}
