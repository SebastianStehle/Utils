// ==========================================================================
// CanvasVirtualWrapper.cs
// Universal App Utils
// ==========================================================================
// Copyright (c) Sebastian Stehle
// All rights reserved.
// ==========================================================================

using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GP.Utils.Mathematics;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace GP.Utils.UI.Controls
{
    [TemplatePart(Name = PartContentControl, Type = typeof(ContentControl))]
    public sealed class CanvasVirtualWrapper : Control, ICanvasControl
    {
        private const string PartContentControl = "PART_ContentControl";
        private const int NumRetriesToInvalidate = 3;
        private const int WaitingTimeAfterInvalidate = 100;
        private ContentControl contentControl;
        private CanvasVirtualControl canvasControl;
        private bool isSuccessfullyRendered;
        private float dpiScale = 1;

        /// <summary>
        /// Gets the canvas device.
        /// </summary>
        public CanvasDevice Device
        {
            get { return canvasControl?.Device; }
        }

        /// <summary>
        /// Gets or sets a scaling factor applied to this control's Dpi.
        /// </summary>
        public float DpiScale
        {
            get
            {
                return dpiScale;
            }
            set
            {
                dpiScale = value;

                if (canvasControl != null)
                {
                    canvasControl.DpiScale = value;
                }
            }
        }

        /// <summary>
        /// This is where the magic happens! Hook this event to issue your immediate mode 2D drawing calls.
        /// </summary>
        public event EventHandler<BoundedCanvasDrawEventArgs> Draw;

        /// <summary>
        /// This occurs before a drawing operation.
        /// </summary>
        public event EventHandler BeforeDraw;

        /// <summary>
        /// This occurs before a drawing operation.
        /// </summary>
        public event EventHandler AfterDraw;

        /// <summary>
        /// Occurs when the resources must be created.
        /// </summary>
        public event EventHandler CreateResources;

        /// <summary>
        /// Initializes a new instance of the <see cref="CanvasVirtualWrapper"/> class.
        /// </summary>
        public CanvasVirtualWrapper()
        {
            DefaultStyleKey = typeof(CanvasVirtualWrapper);

            Loaded += CanvasControlWrapper_Loaded;
        }

        private void CanvasControlWrapper_Loaded(object sender, RoutedEventArgs e)
        {
            Unloaded += CanvasControlWrapper_Unloaded;

            Application.Current.Resuming += Application_Resuming;
        }

        private void CanvasControlWrapper_Unloaded(object sender, RoutedEventArgs e)
        {
            Unloaded -= CanvasControlWrapper_Unloaded;

            Application.Current.Resuming -= Application_Resuming;
        }

        private void Application_Resuming(object sender, object e)
        {
            InvalidateAfterResuming().Forget();
        }

        private async Task InvalidateAfterResuming()
        {
            if (canvasControl == null)
            {
                return;
            }

            isSuccessfullyRendered = false;

            for (var i = 0; i < NumRetriesToInvalidate; i++)
            {
                canvasControl?.Invalidate();

                await Task.Delay(WaitingTimeAfterInvalidate);

                if (isSuccessfullyRendered)
                {
                    return;
                }
            }

            RecreateCanvas();
        }
        /// <summary>
        /// Binds the controls when the template is applied for the canvas wrapper.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            BindContentControl();
        }

        private void BindContentControl()
        {
            contentControl = GetTemplateChild(PartContentControl) as ContentControl;

            RecreateCanvas();
        }

        private void RecreateCanvas()
        {
            if (contentControl == null)
            {
                return;
            }

            if (canvasControl != null)
            {
                ReleaseCanvas();
            }

            contentControl.Content = canvasControl = new CanvasVirtualControl();

            InitializeCanvas();
        }

        private void InitializeCanvas()
        {
            canvasControl.DpiScale = dpiScale;

            canvasControl.CreateResources += CanvasVirtualControl_CreateResources;
            canvasControl.RegionsInvalidated += CanvasVirtualControl_RegionsInvalidated;
        }

        private void ReleaseCanvas()
        {
            canvasControl.CreateResources -= CanvasVirtualControl_CreateResources;
            canvasControl.RegionsInvalidated -= CanvasVirtualControl_RegionsInvalidated;

            canvasControl.RemoveFromVisualTree();
        }

        private void CanvasVirtualControl_RegionsInvalidated(CanvasVirtualControl sender, CanvasRegionsInvalidatedEventArgs args)
        {
            if (args.InvalidatedRegions.Length <= 0)
            {
                return;
            }

            OnBeforeDraw();

            foreach (var region in args.InvalidatedRegions)
            {
                using (var session = canvasControl.CreateDrawingSession(region))
                {
#if DRAW_RECTS
                    byte[] color = new byte[3];

                    random.NextBytes(color);

                    session.FillRectangle(region, Color.FromArgb(255, color[0], color[1], color[2]));
#endif
                    OnDraw(new BoundedCanvasDrawEventArgs(session, region.ToRect2()));
                }
            }

            OnAfterDraw();

            isSuccessfullyRendered = true;
        }

        /// <summary>
        /// Converts the dips to pixels.
        /// </summary>
        /// <param name="dips">The dips to convert.</param>
        /// <param name="dpiRounding">The rounding mode.</param>
        /// <returns>
        /// The resulting pixels.
        /// </returns>
        public int ConvertDipsToPixels(float dips, CanvasDpiRounding dpiRounding)
        {
            return canvasControl?.ConvertDipsToPixels(dips, dpiRounding) ?? -1;
        }

        /// <summary>
        /// Indicates that the contents of the CanvasControl need to be redrawn.
        ///  Calling <see cref="ICanvasControl.Invalidate"/> results in
        /// the <see cref="ICanvasControl.Draw"/> event being raised shortly afterward.
        /// </summary>
        public void Invalidate()
        {
            Dispatcher.RunIdleAsync(x => canvasControl?.Invalidate()).Forget();
        }

        /// <summary>
        /// Removes the control from the last FrameworkElement it was parented to.
        /// </summary>
        public void RemoveFromVisualTree()
        {
            if (canvasControl == null)
            {
                return;
            }

            ReleaseCanvas();

            contentControl.Content = null;
        }

        private void CanvasVirtualControl_CreateResources(CanvasVirtualControl sender, CanvasCreateResourcesEventArgs args)
        {
            OnCreateResources();
        }

        private void OnDraw(BoundedCanvasDrawEventArgs e)
        {
            Draw?.Invoke(this, e);
        }

        private void OnBeforeDraw()
        {
            BeforeDraw?.Invoke(this, EventArgs.Empty);
        }

        private void OnAfterDraw()
        {
            AfterDraw?.Invoke(this, EventArgs.Empty);
        }

        private void OnCreateResources()
        {
            CreateResources?.Invoke(this, EventArgs.Empty);
        }
    }
}
