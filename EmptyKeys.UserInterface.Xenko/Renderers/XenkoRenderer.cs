using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyKeys.UserInterface.Media;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Xenko.Games;
using SiliconStudio.Xenko.Graphics;
using SiliconStudio.Xenko.Rendering;
using Texture2D = SiliconStudio.Xenko.Graphics.Texture;

namespace EmptyKeys.UserInterface.Renderers
{
    public class XenkoRenderer : Renderer
    {
        private static GraphicsDeviceManager manager;

        /// <summary>
        /// The graphics device
        /// </summary>
        /// <value>
        /// The graphics device.
        /// </value>
        public static GraphicsDevice GraphicsDevice
        {
            get
            {
                return manager.GraphicsDevice;
            }                    
        }

        private Matrix view = Matrix.LookAtRH(new Vector3(0.0f, 0.0f, 1.0f), Vector3.Zero, new Vector3(0, 1, 0));

        private SpriteBatch spriteBatch;
        private Vector2 vecPosition;
        private Vector2 vecScale;
        private Color vecColor;
        private Rectangle testRectangle;
        private Rectangle sourceRect;
        private Rectangle currentScissorRectangle;
        private Stack<Rectangle> clipRectanges;

        private bool isSpriteRenderInProgress;
        private bool isClipped;
        private Rectangle clipRectangle;
        private RasterizerState rasterizerScissorState;
        private RasterizerState rasterizeStateGeometry;

        public override bool IsFullScreen
        {
            get { return manager.IsFullScreen; }
        }

        public XenkoRenderer(GraphicsDeviceManager graphicsDeviceManager)
            : base()
        {
            manager = graphicsDeviceManager;
            spriteBatch = new SpriteBatch(manager.GraphicsDevice);
            clipRectanges = new Stack<Rectangle>();

            var rasterizerStateDescription = new RasterizerStateDescription(CullMode.None);
            rasterizerStateDescription.ScissorTestEnable = true; // enables the scissor test
            rasterizerScissorState = RasterizerState.New(GraphicsDevice, rasterizerStateDescription);

            var geometryStateDesc = new RasterizerStateDescription(CullMode.None);
            rasterizeStateGeometry = RasterizerState.New(GraphicsDevice, geometryStateDesc);
        }

        public override void Begin()
        {
            isClipped = false;
            isSpriteRenderInProgress = true;
            if (clipRectanges.Count == 0)
            {
                spriteBatch.Begin();
            }
            else
            {
                Rectangle previousClip = clipRectanges.Pop();
                BeginClipped(previousClip);
            }                       
        }

        public override void End()
        {
            isClipped = false;
            spriteBatch.End();
            isSpriteRenderInProgress = false;
        }

        public override void BeginClipped(Rect clipRect)
        {
            clipRectangle.X = (int)clipRect.X;
            clipRectangle.Y = (int)clipRect.Y;
            clipRectangle.Width = (int)clipRect.Width;
            clipRectangle.Height = (int)clipRect.Height;

            BeginClipped(clipRectangle);
        }

        private void BeginClipped(Rectangle clipRect)
        {
            isClipped = true;
            isSpriteRenderInProgress = true;

            if (clipRectanges.Count > 0)
            {
                Rectangle previousClip = clipRectanges.Pop();
                if (previousClip.Intersects(clipRect))
                {
                    clipRect = Rectangle.Intersect(previousClip, clipRect);
                }
                else
                {
                    clipRect = previousClip;
                }

                clipRectanges.Push(previousClip);
            }
            
            manager.GraphicsDevice.SetScissorRectangles(clipRect.Left, clipRect.Top, clipRect.Right, clipRect.Bottom);
            currentScissorRectangle = clipRect;
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, rasterizerScissorState, null, null, 0);
            clipRectanges.Push(clipRect);
        }

        public override void EndClipped()
        {
            isClipped = false;
            isSpriteRenderInProgress = false;
            spriteBatch.End();
            clipRectanges.Pop();            
        }

        public override void DrawText(FontBase font, string text, PointF position, Size renderSize, ColorW color, PointF scale, float depth)
        {
            if (isClipped)
            {
                testRectangle.X = (int)position.X;
                testRectangle.Y = (int)position.Y;
                testRectangle.Width = (int)renderSize.Width;
                testRectangle.Height = (int)renderSize.Height;

                if (!currentScissorRectangle.Intersects(testRectangle))
                {
                    return;
                }
            }
            
            vecPosition.X = position.X;
            vecPosition.Y = position.Y;
            vecScale.X = scale.X;
            vecScale.Y = scale.Y;
            vecColor.A = color.A;
            vecColor.R = color.R;
            vecColor.G = color.G;
            vecColor.B = color.B;
            SpriteFont native = font.GetNativeFont() as SpriteFont;
            spriteBatch.DrawString(native, text, vecPosition, vecColor);
        }

        public override void Draw(TextureBase texture, PointF position, Size renderSize, ColorW color, bool centerOrigin)
        {            
            testRectangle.X = (int)position.X;
            testRectangle.Y = (int)position.Y;
            testRectangle.Width = (int)renderSize.Width;
            testRectangle.Height = (int)renderSize.Height;
            if (isClipped && !currentScissorRectangle.Intersects(testRectangle))
            {
                return;
            }
            
            vecColor.A = color.A;
            vecColor.R = color.R;
            vecColor.G = color.G;
            vecColor.B = color.B;
            Texture2D native = texture.GetNativeTexture() as Texture2D;
            spriteBatch.Draw(native, testRectangle, vecColor);
        }

        public override void Draw(TextureBase texture, PointF position, Size renderSize, ColorW color, Rect source, bool centerOrigin)
        {
            testRectangle.X = (int)position.X;
            testRectangle.Y = (int)position.Y;
            testRectangle.Width = (int)renderSize.Width;
            testRectangle.Height = (int)renderSize.Height;
            if (isClipped && !currentScissorRectangle.Intersects(testRectangle))
            {
                return;
            }

            sourceRect.X = (int)source.X;
            sourceRect.Y = (int)source.Y;
            sourceRect.Width = (int)source.Width;
            sourceRect.Height = (int)source.Height;
            vecColor.A = color.A;
            vecColor.R = color.R;
            vecColor.G = color.G;
            vecColor.B = color.B;
            Texture2D native = texture.GetNativeTexture() as Texture2D;
            spriteBatch.Draw(native, testRectangle, sourceRect, vecColor, 0, Vector2.Zero);
        }

        public override Rect GetViewport()
        {
            Viewport viewport = GraphicsDevice.Viewport;
            return new Rect(viewport.X, viewport.Y, viewport.Width, viewport.Height);
        }

        public override TextureBase CreateTexture(object nativeTexture)
        {
            if (nativeTexture == null)
            {
                return null;
            }

            return new XenkoTexture(nativeTexture);
        }

        public override TextureBase CreateTexture(int width, int height, bool mipmap, bool dynamic)
        {
            if (width == 0 || height == 0)
            {
                return null;
            }

            Texture2D native = null;
            if (dynamic)
            {
                native = Texture2D.New2D(GraphicsDevice, width, height, PixelFormat.R8G8B8A8_UNorm, usage: GraphicsResourceUsage.Dynamic);
            }
            else
            {
                native = Texture2D.New2D(GraphicsDevice, width, height, PixelFormat.R8G8B8A8_UNorm);
            }

            XenkoTexture texture = new XenkoTexture(native);
            return texture;
        }

        public override GeometryBuffer CreateGeometryBuffer()
        {
            return new XenkoGeometryBuffer();
        }

        public override void DrawGeometryColor(GeometryBuffer buffer, PointF position, ColorW color, float opacity, float depth)
        {
            XenkoGeometryBuffer xenkoBuffer = buffer as XenkoGeometryBuffer;

            Color4 nativeColor = new Color4(color.PackedValue) * opacity;
            xenkoBuffer.Parameters.Set(SpriteEffectKeys.Color, nativeColor);
            xenkoBuffer.Parameters.Set(TexturingKeys.Texture0, GraphicsDevice.GetSharedWhiteTexture());
            DrawGeometry(buffer, position, depth);
        }

        public override void DrawGeometryTexture(GeometryBuffer buffer, PointF position, TextureBase texture, float opacity, float depth)
        {
            XenkoGeometryBuffer paradoxBuffer = buffer as XenkoGeometryBuffer;
            Texture2D nativeTexture = texture.GetNativeTexture() as Texture2D;
            paradoxBuffer.Parameters.Set(SpriteEffectKeys.Color, Color.White * opacity);
            paradoxBuffer.Parameters.Set(TexturingKeys.Texture0, nativeTexture);
            DrawGeometry(buffer, position, depth);
        }

        private void DrawGeometry(GeometryBuffer buffer, PointF position, float depth)
        {
            if (isSpriteRenderInProgress)
            {
                spriteBatch.End();
            }

            XenkoGeometryBuffer paradoxBuffer = buffer as XenkoGeometryBuffer;
            if (isClipped)
            {
                GraphicsDevice.SetRasterizerState(rasterizerScissorState);
            }
            else
            {
                GraphicsDevice.SetRasterizerState(rasterizeStateGeometry);
            }

            Matrix world = Matrix.Translation(position.X, position.Y, depth);
            Matrix projection = Matrix.OrthoOffCenterRH(0, (float)GraphicsDevice.Viewport.Width, (float)GraphicsDevice.Viewport.Height, 0, 1.0f, 1000.0f);

            Matrix worldView;
            Matrix.MultiplyTo(ref world, ref view, out worldView);
            Matrix result;
            Matrix.MultiplyTo(ref worldView, ref projection, out result);
            paradoxBuffer.Parameters.Set(SpriteBaseKeys.MatrixTransform, result);

            GraphicsDevice.SetVertexArrayObject(paradoxBuffer.VertexArray);            
            paradoxBuffer.Effect.Apply(GraphicsDevice, paradoxBuffer.ParameterCollectionGroup, false);

            switch (buffer.PrimitiveType)
            {
                case GeometryPrimitiveType.TriangleList:
                    GraphicsDevice.Draw(PrimitiveType.TriangleList, paradoxBuffer.PrimitiveCount);
                    break;
                case GeometryPrimitiveType.TriangleStrip:
                    GraphicsDevice.Draw(PrimitiveType.TriangleStrip, paradoxBuffer.PrimitiveCount);
                    break;
                case GeometryPrimitiveType.LineList:
                    GraphicsDevice.Draw(PrimitiveType.LineList, paradoxBuffer.PrimitiveCount);
                    break;
                case GeometryPrimitiveType.LineStrip:
                    GraphicsDevice.Draw(PrimitiveType.LineStrip, paradoxBuffer.PrimitiveCount);
                    break;
                default:
                    break;
            }

            GraphicsDevice.SetVertexArrayObject(null);

            if (isSpriteRenderInProgress)
            {
                if (isClipped)
                {
                    spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, rasterizerScissorState, null, null, 0);
                }
                else
                {
                    spriteBatch.Begin();
                }
            }
        }

        public override FontBase CreateFont(object nativeFont)
        {
            return new XenkoFont(nativeFont);
        }

        public override void ResetNativeSize()
        {            
        }

        public override bool IsClipped(PointF position, Size renderSize)
        {
            if (isClipped)
            {
                testRectangle.X = (int)position.X;
                testRectangle.Y = (int)position.Y;
                testRectangle.Width = (int)renderSize.Width;
                testRectangle.Height = (int)renderSize.Height;

                if (!currentScissorRectangle.Intersects(testRectangle))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
