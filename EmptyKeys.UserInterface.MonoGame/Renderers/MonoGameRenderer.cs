﻿using System.Collections.Generic;
using EmptyKeys.UserInterface.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EmptyKeys.UserInterface.Renderers
{
    /// <summary>
    /// Implements Mono Game renderer
    /// </summary>
    public class MonoGameRenderer : Renderer
    {
        /// <summary>
        /// The graphics device
        /// </summary>
        /// <value>
        /// The graphics device.
        /// </value>
        public static GraphicsDevice GraphicsDevice
        {
            get;
            private set;
        }

        private RasterizerState clippingRasterizeState = new RasterizerState { ScissorTestEnable = true, CullMode = CullMode.None };
        private RasterizerState previousState;
        private SpriteBatch spriteBatch;

        private bool isClipped;
        private Rectangle testRectangle;
        private Stack<Rectangle> clipRectanges;
        private Vector2 vecPosition;        
        private Vector2 vecScale;
        private Color vecColor;
        private Rectangle sourceRect;
        private Rectangle clipRectangle;

        private BasicEffect effect;
        private RasterizerState rasterizeStateGeometry = new RasterizerState { ScissorTestEnable = false, CullMode = CullMode.None, FillMode = FillMode.Solid };
        private bool isSpriteRenderInProgress;

        /// <summary>
        /// Gets a value indicating whether is full screen.
        /// </summary>
        /// <value>
        /// <c>true</c> if is full screen; otherwise, <c>false</c>.
        /// </value>
        public override bool IsFullScreen
        {
            get { return GraphicsDevice.PresentationParameters.IsFullScreen; }
        }

        /// <summary>
        /// Gets or sets the projection.
        /// </summary>
        /// <value>
        /// The projection.
        /// </value>
        /// <exception cref="System.NotImplementedException">
        /// </exception>
        public Matrix Projection
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MonoGameRenderer" /> class.
        /// </summary>
        /// <param name="graphicsDevice">The graphics device.</param>
        /// <param name="nativeScreenWidth">Width of the native screen.</param>
        /// <param name="nativeScreenHeight">Height of the native screen.</param>
        public MonoGameRenderer(GraphicsDevice graphicsDevice, int nativeScreenWidth, int nativeScreenHeight)
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            GraphicsDevice = graphicsDevice;

            if (graphicsDevice.PresentationParameters.IsFullScreen)
            {
                NativeScreenWidth = nativeScreenWidth;
                NativeScreenHeight = nativeScreenHeight;
            }
            else
            {
                NativeScreenWidth = graphicsDevice.PresentationParameters.BackBufferWidth;
                NativeScreenHeight = graphicsDevice.PresentationParameters.BackBufferHeight;
            }

            clipRectanges = new Stack<Rectangle>();
        }

        /// <summary>
        /// Begins the rendering
        /// </summary>
        public override void Begin()
        {
            isClipped = false;
            isSpriteRenderInProgress = true;
            if (previousState != null)
            {
                spriteBatch.GraphicsDevice.RasterizerState = previousState;
                previousState = null;
            }

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

        /// <summary>
        /// Draws the text.
        /// </summary>
        /// <param name="font">The font.</param>
        /// <param name="text">The text.</param>
        /// <param name="position">The position.</param>
        /// <param name="renderSize">Size of the render.</param>
        /// <param name="color">The color.</param>
        /// <param name="scale">The scale.</param>
        /// <param name="depth">The depth.</param>
        public override void DrawText(FontBase font, string text, PointF position, Size renderSize, ColorW color, PointF scale, float depth)
        {           
            if (isClipped)
            {
                testRectangle.X = (int)position.X;
                testRectangle.Y = (int)position.Y;
                testRectangle.Width = (int)renderSize.Width;
                testRectangle.Height = (int)renderSize.Height;

                if (!spriteBatch.GraphicsDevice.ScissorRectangle.Intersects(testRectangle))
                {
                    return;
                }
            }

            vecPosition.X = position.X;
            vecPosition.Y = position.Y;
            vecScale.X = scale.X;
            vecScale.Y = scale.Y;
            vecColor.PackedValue = color.PackedValue;
            SpriteFont native = font.GetNativeFont() as SpriteFont;
            spriteBatch.DrawString(native, text, vecPosition, vecColor);
        }        

        /// <summary>
        /// Draws the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="position">The position.</param>
        /// <param name="renderSize">Size of the render.</param>
        /// <param name="color">The color.</param>
        /// <param name="centerOrigin">if set to <c>true</c> [center origin].</param>
        public override void Draw(TextureBase texture, PointF position, Size renderSize, ColorW color, bool centerOrigin)
        {            
            testRectangle.X = (int)position.X;
            testRectangle.Y = (int)position.Y;
            testRectangle.Width = (int)renderSize.Width;
            testRectangle.Height = (int)renderSize.Height;
            if (isClipped && !spriteBatch.GraphicsDevice.ScissorRectangle.Intersects(testRectangle))
            {
                return;
            }

            vecColor.PackedValue = color.PackedValue;
            Texture2D native = texture.GetNativeTexture() as Texture2D;
            spriteBatch.Draw(native, testRectangle, vecColor);
        }

        /// <summary>
        /// Draws the specified texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="position">The position.</param>
        /// <param name="renderSize">Size of the render.</param>
        /// <param name="color">The color.</param>
        /// <param name="source">The source.</param>
        /// <param name="centerOrigin">if set to <c>true</c> [center origin].</param>
        public override void Draw(TextureBase texture, PointF position, Size renderSize, ColorW color, Rect source, bool centerOrigin)
        {            
            testRectangle.X = (int)position.X;
            testRectangle.Y = (int)position.Y;
            testRectangle.Width = (int)renderSize.Width;
            testRectangle.Height = (int)renderSize.Height;
            if (isClipped && !spriteBatch.GraphicsDevice.ScissorRectangle.Intersects(testRectangle))
            {
                return;
            }

            sourceRect.X = (int)source.X;
            sourceRect.Y = (int)source.Y;
            sourceRect.Width = (int)source.Width;
            sourceRect.Height = (int)source.Height;
            vecColor.PackedValue = color.PackedValue;
            Texture2D native = texture.GetNativeTexture() as Texture2D;
            spriteBatch.Draw(native, testRectangle, sourceRect, vecColor, 0, Vector2.Zero, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Ends rendering
        /// </summary>
        public override void End()
        {
            isClipped = false;
            isSpriteRenderInProgress = false;
            spriteBatch.End();
        }

        /// <summary>
        /// Begins the clipped.
        /// </summary>
        /// <param name="clipRect">The clip rect.</param>
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

            spriteBatch.GraphicsDevice.ScissorRectangle = clipRect;
            previousState = spriteBatch.GraphicsDevice.RasterizerState;
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, clippingRasterizeState);
            clipRectanges.Push(clipRect);
        }

        /// <summary>
        /// Ends the clipped drawing
        /// </summary>
        public override void EndClipped()
        {
            isClipped = false;
            isSpriteRenderInProgress = false;
            spriteBatch.End();
            clipRectanges.Pop();
        }

        /// <summary>
        /// Gets the viewport.
        /// </summary>
        /// <returns></returns>
        public override Rect GetViewport()
        {
            Viewport viewport = GraphicsDevice.Viewport;
            return new Rect(viewport.X, viewport.Y, viewport.Width, viewport.Height);
        }

        /// <summary>
        /// Creates the texture.
        /// </summary>
        /// <param name="nativeTexture">The native texture.</param>
        /// <returns></returns>
        public override TextureBase CreateTexture(object nativeTexture)
        {
            if (nativeTexture == null)
            {
                return null;
            }

            return new MonoGameTexture(nativeTexture);
        }

        /// <summary>
        /// Creates the texture.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="mipmap">if set to <c>true</c> [mipmap].</param>
        /// <param name="dynamic">if set to <c>true</c> [dynamic].</param>
        /// <returns></returns>
        public override TextureBase CreateTexture(int width, int height, bool mipmap, bool dynamic)
        {
            if (width == 0 || height == 0)
            {
                return null;
            }

            Texture2D native = new Texture2D(GraphicsDevice, width, height, false, SurfaceFormat.Color);
            MonoGameTexture texture = new MonoGameTexture(native);
            return texture;
        }

        /// <summary>
        /// Creates the font.
        /// </summary>
        /// <param name="nativeFont">The native font.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override FontBase CreateFont(object nativeFont)
        {
            return new MonoGameFont(nativeFont);
        }

        /// <summary>
        /// Resets the size of the native. Sets NativeScreenWidth and NativeScreenHeight based on active back buffer
        /// </summary>
        public override void ResetNativeSize()
        {
            NativeScreenWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
            NativeScreenHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;
        }

        public override GeometryBuffer CreateGeometryBuffer()
        {
            return new MonoGameGeometryBuffer();
        }

        public override void DrawGeometryColor(GeometryBuffer buffer, PointF position, ColorW color, float opacity, float depth)
        {
            if (effect == null)
            {
                effect = new BasicEffect(GraphicsDevice);                
            }

            effect.Alpha = color.A / (float)byte.MaxValue * opacity;
            //color = color * effect.Alpha;
            effect.DiffuseColor = new Vector3(color.R / (float)byte.MaxValue, color.G / (float)byte.MaxValue, color.B / (float)byte.MaxValue);
            effect.TextureEnabled = false;
            effect.VertexColorEnabled = true;

            DrawGeometry(buffer, position, depth);
        }

        private void DrawGeometry(GeometryBuffer buffer, PointF position, float depth)
        {
            if (isSpriteRenderInProgress)
            {
                spriteBatch.End();
            }

            MonoGameGeometryBuffer monoGameBuffer = buffer as MonoGameGeometryBuffer;
            GraphicsDevice device = GraphicsDevice;

            RasterizerState rasState = device.RasterizerState;
            BlendState blendState = device.BlendState;
            DepthStencilState stencilState = device.DepthStencilState;

            device.BlendState = BlendState.AlphaBlend;
            device.DepthStencilState = DepthStencilState.DepthRead;
            
            if (isClipped)
            {
                device.RasterizerState = clippingRasterizeState;
            }
            else
            {
                device.RasterizerState = rasterizeStateGeometry;
            }

            effect.World = Matrix.CreateTranslation(position.X, position.Y, depth);
            effect.View = Matrix.CreateLookAt(new Vector3(0.0f, 0.0f, 1.0f), Vector3.Zero, Vector3.Up);
            effect.Projection = Matrix.CreateOrthographicOffCenter(0, (float)device.Viewport.Width, (float)device.Viewport.Height, 0, 1.0f, 1000.0f);
            
            device.SetVertexBuffer(monoGameBuffer.VertexBuffer);
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                switch (buffer.PrimitiveType)
                {
                    case GeometryPrimitiveType.TriangleList:
                        device.DrawPrimitives(PrimitiveType.TriangleList, 0, monoGameBuffer.PrimitiveCount);
                        break;
                    case GeometryPrimitiveType.TriangleStrip:
                        device.DrawPrimitives(PrimitiveType.TriangleStrip, 0, monoGameBuffer.PrimitiveCount);
                        break;
                    case GeometryPrimitiveType.LineList:
                        device.DrawPrimitives(PrimitiveType.LineList, 0, monoGameBuffer.PrimitiveCount);
                        break;
                    case GeometryPrimitiveType.LineStrip:
                        device.DrawPrimitives(PrimitiveType.LineStrip, 0, monoGameBuffer.PrimitiveCount);
                        break;
                    default:
                        break;
                }                
            }

            device.DepthStencilState = stencilState;
            device.BlendState = blendState;
            device.RasterizerState = rasState;

            if (isSpriteRenderInProgress)
            {
                if (isClipped)
                {
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, clippingRasterizeState);
                }
                else
                {
                    spriteBatch.Begin();
                }
            }
        }

        public override void DrawGeometryTexture(GeometryBuffer buffer, PointF position, TextureBase texture, float opacity, float depth)
        {
            if (effect == null)
            {
                effect = new BasicEffect(GraphicsDevice);                
            }

            effect.Alpha = opacity;
            effect.DiffuseColor = new Vector3(1, 1, 1);
            effect.Texture = texture.GetNativeTexture() as Texture2D;
            effect.VertexColorEnabled = false;
            effect.TextureEnabled = true;

            DrawGeometry(buffer, position, depth);
        }
    }
}