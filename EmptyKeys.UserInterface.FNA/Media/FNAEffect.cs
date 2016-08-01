using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace EmptyKeys.UserInterface.Media
{
    /// <summary>
    /// Implements FNA specific effect
    /// </summary>
    /// <seealso cref="EmptyKeys.UserInterface.Media.EffectBase" />
    public class FNAEffect : EffectBase
    {
        private Effect effect;

        /// <summary>
        /// Initializes a new instance of the <see cref="FNAEffect"/> class.
        /// </summary>
        /// <param name="nativeEffect">The native effect.</param>
        public FNAEffect(object nativeEffect) : base(nativeEffect)
        {
            effect = nativeEffect as Effect;            
        }

        /// <summary>
        /// Gets the native effect.
        /// </summary>
        /// <returns></returns>
        public override object GetNativeEffect()
        {
            return effect;
        }

        /// <summary>
        /// Updates the effect parameters.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public override void UpdateEffectParameters(params object[] parameters)
        {                     
        }
    }
}
