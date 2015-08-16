using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyKeys.UserInterface.Renderers;
using SiliconStudio.Core;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Paradox.Graphics;
using SiliconStudio.Paradox.Graphics.Internals;
using SiliconStudio.Paradox.Rendering;
using VertexBuffer = SiliconStudio.Paradox.Graphics.Buffer;

namespace EmptyKeys.UserInterface.Media
{
    public class ParadoxGeometryBuffer : GeometryBuffer
    {
        private readonly Effect effect;
        
        private VertexBuffer vertexBuffer;
        private ParameterCollection parameters;
        private EffectParameterCollectionGroup parameterCollectionGroup;

        /// <summary>
        /// Gets the effect.
        /// </summary>
        /// <value>
        /// The effect.
        /// </value>
        public Effect Effect
        {
            get
            {
                return effect;
            }
        }

        /// <summary>
        /// Gets or sets the vertex array.
        /// </summary>
        /// <value>
        /// The vertex array.
        /// </value>
        public VertexArrayObject VertexArray { get; set; }

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <value>
        /// The parameters.
        /// </value>
        public ParameterCollection Parameters
        {
            get
            {
                return parameters;
            }
        }

        /// <summary>
        /// Gets the parameter collection group.
        /// </summary>
        /// <value>
        /// The parameter collection group.
        /// </value>
        public EffectParameterCollectionGroup ParameterCollectionGroup
        {
            get
            {
                return parameterCollectionGroup;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ParadoxGeometryBuffer"/> class.
        /// </summary>
        public ParadoxGeometryBuffer()
            : base()
        {
            effect = new Effect(ParadoxRenderer.GraphicsDevice, SpriteEffect.Bytecode, null);
            parameters = new ParameterCollection();            
            parameterCollectionGroup = new EffectParameterCollectionGroup(ParadoxRenderer.GraphicsDevice, effect, new[] { parameters });
        }

        /// <summary>
        /// Fills the color type buffer (VertexPositionColor)
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="primitiveType">Type of the primitive.</param>
        public override void FillColor(List<PointF> points, GeometryPrimitiveType primitiveType)
        {
            SetPrimitiveCount(primitiveType, points.Count);

            VertexPositionNormalTexture[] vertex = new VertexPositionNormalTexture[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                vertex[i] = new VertexPositionNormalTexture(new Vector3(points[i].X, points[i].Y, 0), new Vector3(0, 0, 1), Vector2.Zero);
            }            

            vertexBuffer = VertexBuffer.Vertex.New(ParadoxRenderer.GraphicsDevice, vertex);
            vertexBuffer.Reload = (graphicsResource) => ((VertexBuffer)graphicsResource).Recreate(vertex);
            VertexArray = VertexArrayObject.New(ParadoxRenderer.GraphicsDevice,
                new VertexBufferBinding(vertexBuffer, VertexPositionNormalTexture.Layout, vertex.Length, VertexPositionNormalTexture.Size));
        }

        private void SetPrimitiveCount(GeometryPrimitiveType primitiveType, int pointCount)
        {
            PrimitiveType = primitiveType;
            switch (primitiveType)
            {
                case GeometryPrimitiveType.TriangleList:
                    PrimitiveCount = pointCount / 3;
                    break;
                case GeometryPrimitiveType.TriangleStrip:
                    PrimitiveCount = pointCount;
                    break;
                case GeometryPrimitiveType.LineList:
                    PrimitiveCount = pointCount / 2;
                    break;
                case GeometryPrimitiveType.LineStrip:
                    PrimitiveCount = pointCount;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Fills the texture type buffer (VertexPositionTexture)
        /// </summary>
        /// <param name="points">The points.</param>
        /// <param name="destinationSize">Size of the destination.</param>
        /// <param name="sourceRect">The source rect.</param>
        /// <param name="primitiveType">Type of the primitive.</param>
        public override void FillTexture(List<PointF> points, Size destinationSize, Rect sourceRect, GeometryPrimitiveType primitiveType)
        {
            SetPrimitiveCount(primitiveType, points.Count);

            VertexPositionNormalTexture[] vertex = new VertexPositionNormalTexture[points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                Vector2 uv = new Vector2(sourceRect.X + (points[i].X / destinationSize.Width) * sourceRect.Width,
                                         sourceRect.Y + (points[i].Y / destinationSize.Height) * sourceRect.Height);
                vertex[i] = new VertexPositionNormalTexture(new Vector3(points[i].X, points[i].Y, 0), new Vector3(0,0,1), uv);
            }

            vertexBuffer = VertexBuffer.Vertex.New(ParadoxRenderer.GraphicsDevice, vertex);
            vertexBuffer.Reload = (graphicsResource) => ((VertexBuffer)graphicsResource).Recreate(vertex);
            VertexArray = VertexArrayObject.New(ParadoxRenderer.GraphicsDevice, effect.InputSignature,
                new VertexBufferBinding(vertexBuffer, VertexPositionNormalTexture.Layout, vertex.Length, VertexPositionNormalTexture.Size));
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public override void Dispose()
        {
            if (VertexArray != null && !VertexArray.IsDisposed)
            {
                VertexArray.Dispose();
            }

            if (vertexBuffer != null && !vertexBuffer.IsDisposed)
            {
                vertexBuffer.Dispose();
            }

            if (effect != null && !effect.IsDisposed)
            {
                effect.Dispose();
            }
        }
    }
}
