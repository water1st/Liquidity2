using Liquidity2.UI.Components.Interface;
using Liquidity2.UI.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Liquidity2.UI.Components.Chart
{
    public abstract class ChartBase<TChart> : FrameworkElement, IChart
        where TChart : class, IChart
    {
        public static DependencyProperty BackgroundBrushProperty = DependencyProperty.Register(nameof(BackgroundBrush), typeof(Brush), typeof(ChartBase<TChart>), new PropertyMetadata(Brushes.Transparent));
        public static readonly DependencyProperty MouseDragSensitivityProperty = DependencyProperty.Register(nameof(MouseDragSensitivity), typeof(int), typeof(ChartBase<TChart>), new PropertyMetadata(1));
        public static readonly DependencyProperty ChartProxyProperty = DependencyProperty.Register(nameof(Proxy), typeof(IChartProxy<TChart>), typeof(ChartBase<TChart>));
        private readonly VisualCollection _visualCollection;
        private readonly ConcurrentDictionary<string, IEnumerable<Visual>> _visualDictionary = new ConcurrentDictionary<string, IEnumerable<Visual>>();

        private Point _mousePosition;
        private int _mouseDragLeftTriggerCount;
        private int _mouseDragRightTriggerCount;

        public ChartBase()
        {
            _visualCollection = new VisualCollection(this);
            Renders = Container.Insecure.GetService<IRendererFactory>().Create().OrderBy(render => render.Stage);
            Loaded += OnLoaded;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            drawingContext.DrawRectangle(BackgroundBrush, null, new Rect(0, 0, ActualWidth, ActualHeight));
            base.OnRender(drawingContext);
        }

        protected IEnumerable<IRender> Renders { get; }

        public IChartProxy<TChart> Proxy
        {
            get => (IChartProxy<TChart>)GetValue(ChartProxyProperty);
            set => SetValue(ChartProxyProperty, value);
        }

        public int MouseDragSensitivity
        {
            get => (int)GetValue(MouseDragSensitivityProperty);
            set => SetValue(MouseDragSensitivityProperty, value);
        }

        public Brush BackgroundBrush
        {
            get => (Brush)GetValue(BackgroundBrushProperty);
            set => SetValue(BackgroundBrushProperty, value);
        }

        protected override int VisualChildrenCount => _visualCollection.Count;

        protected override Visual GetVisualChild(int index)
        {
            return _visualCollection[index];
        }

        protected void UpdateVisual(string key, Visual visual)
        {
            var visuals = new List<Visual>();
            if (visual != null)
                visuals.Add(visual);

            UpdateVisuals(key, visuals);
        }

        protected void UpdateVisuals(string key, IEnumerable<Visual> visuals)
        {
            if (_visualDictionary.TryRemove(key, out var oldVisuals))
            {
                foreach (var oldVisual in oldVisuals)
                    _visualCollection.Remove(oldVisual);
            }
            if (visuals != null && _visualDictionary.TryAdd(key, visuals))
            {
                foreach (var visual in visuals)
                    _visualCollection.Add(visual);
            }
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                ZoomIn();
            }
            else
            {
                ZoomOut();
            }
        }

        protected virtual void OnLoaded(object sender, RoutedEventArgs e) { }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (_mousePosition == new Point())
                {
                    _mousePosition = e.GetPosition(null);
                    return;
                }
                if (_mousePosition.X > e.GetPosition(null).X)
                {
                    _mouseDragLeftTriggerCount++;
                    if (_mouseDragLeftTriggerCount > MouseDragSensitivity)
                    {
                        ScrollLeft();
                        _mouseDragLeftTriggerCount = 0;
                    }
                    _mouseDragRightTriggerCount = 0;
                }
                else
                {
                    _mouseDragRightTriggerCount++;
                    if (_mouseDragRightTriggerCount > MouseDragSensitivity)
                    {
                        ScrollRight();
                        _mouseDragRightTriggerCount = 0;
                    }
                    _mouseDragLeftTriggerCount = 0;
                }
                _mousePosition = e.GetPosition(null);
            }
            else
            {
                VisualTreeHelper.HitTest(this, null, VisualHitCallback, new PointHitTestParameters(_mousePosition));
            }
            MouseMoving();
        }

        protected virtual void MouseMoving() { }

        protected virtual HitTestResultBehavior VisualHitCallback(HitTestResult result)
        {
            return HitTestResultBehavior.Stop;
        }

        public abstract void Draw();
        public abstract void ZoomIn();
        public abstract void ZoomOut();
        public abstract void ScrollLeft();
        public abstract void ScrollRight();
    }
}
