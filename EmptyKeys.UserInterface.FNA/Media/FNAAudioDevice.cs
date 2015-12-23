using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyKeys.UserInterface.Media
{
    /// <summary>
    /// Implements FNA specific audio device
    /// </summary>
    public class FNAAudioDevice : AudioDevice
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FNAAudioDevice"/> class.
        /// </summary>
        public FNAAudioDevice()
            : base()
        {
        }

        /// <summary>
        /// Creates the sound.
        /// </summary>
        /// <param name="nativeSound">The native sound.</param>
        /// <returns></returns>
        public override SoundBase CreateSound(object nativeSound)
        {
            return new FNASound(nativeSound);
        }
    }
}
