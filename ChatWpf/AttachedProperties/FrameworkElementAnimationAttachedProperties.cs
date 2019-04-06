using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ChatWpf.Animation;

namespace ChatWpf.AttachedProperties
{
    public abstract class AnimateBaseProperty<Parent> : BaseAttachedProperty<Parent, bool>
        where Parent : BaseAttachedProperty<Parent, bool>, new()
    {
        protected Dictionary<WeakReference, bool> mAlreadyLoaded = new Dictionary<WeakReference, bool>();

        protected Dictionary<WeakReference, bool> mFirstLoadValue = new Dictionary<WeakReference, bool>();

        public override void OnValueUpdated(DependencyObject sender, object value)
        {
            if (!(sender is FrameworkElement element))
                return;

            var alreadyLoadedReference = mAlreadyLoaded.FirstOrDefault(f => f.Key.Target == sender);

            // Try and get the first load reference
            var firstLoadReference = mFirstLoadValue.FirstOrDefault(f => f.Key.Target == sender);

            if ((bool)sender.GetValue(ValueProperty) == (bool)value && alreadyLoadedReference.Key != null)
                return;

            if (alreadyLoadedReference.Key == null)
            {
                var weakReference = new WeakReference(sender);

                mAlreadyLoaded[weakReference] = false;

                element.Visibility = Visibility.Hidden;

                RoutedEventHandler onLoaded = null;
                onLoaded = async (ss, ee) =>
                {
                    element.Loaded -= onLoaded;

                    await Task.Delay(5);

                    DoAnimation(element, firstLoadReference.Key != null ? firstLoadReference.Value : (bool)value, true);

                    mAlreadyLoaded[weakReference] = true;
                };

                element.Loaded += onLoaded;
            }
            else if (alreadyLoadedReference.Value == false)
                mFirstLoadValue[new WeakReference(sender)] = (bool)value;
            else
                DoAnimation(element, (bool)value, false);
        }

        protected virtual void DoAnimation(FrameworkElement element, bool value, bool firstLoad) { }
    }

    public class AnimateSlideInFromLeftProperty : AnimateBaseProperty<AnimateSlideInFromLeftProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            if (value)
                await element.SlideAndFadeInAsync(AnimationSlideInDirection.Left, firstLoad, firstLoad ? 0 : 0.3f, keepMargin: false);
            else
                await element.SlideAndFadeOutAsync(AnimationSlideInDirection.Left, firstLoad ? 0 : 0.3f, keepMargin: false);
        }
    }

    public class AnimateSlideInFromBottomProperty : AnimateBaseProperty<AnimateSlideInFromBottomProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            if (value)
                await element.SlideAndFadeInAsync(AnimationSlideInDirection.Bottom, firstLoad, firstLoad ? 0 : 0.3f, keepMargin: false);
            else
                await element.SlideAndFadeOutAsync(AnimationSlideInDirection.Bottom, firstLoad ? 0 : 0.3f, keepMargin: false);
        }
    }

    public class AnimateSlideInFromBottomOnLoadProperty : AnimateBaseProperty<AnimateSlideInFromBottomOnLoadProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            await element.SlideAndFadeInAsync(AnimationSlideInDirection.Bottom, !value, !value ? 0 : 0.3f, keepMargin: false);
        }
    }

    public class AnimateSlideInFromBottomMarginProperty : AnimateBaseProperty<AnimateSlideInFromBottomMarginProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            if (value)
                await element.SlideAndFadeInAsync(AnimationSlideInDirection.Bottom, firstLoad, firstLoad ? 0 : 0.3f, keepMargin: true);
            else
                await element.SlideAndFadeOutAsync(AnimationSlideInDirection.Bottom, firstLoad ? 0 : 0.3f, keepMargin: true);
        }
    }

    public class AnimateFadeInProperty : AnimateBaseProperty<AnimateFadeInProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            if (value)
                await element.FadeInAsync(firstLoad, firstLoad ? 0 : 0.3f);
            else
                await element.FadeOutAsync(firstLoad ? 0 : 0.3f);
        }
    }

    public class AnimateMarqueeProperty : AnimateBaseProperty<AnimateMarqueeProperty>
    {
        protected override void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            element.MarqueeAsync(firstLoad ? 0 : 3f);
        }
    }

    public class FadeInImageOnLoadProperty : AnimateBaseProperty<FadeInImageOnLoadProperty>
    {
        public override void OnValueUpdated(DependencyObject sender, object value)
        {
            if (!(sender is Image image))
                return;
            if ((bool)value)
                image.TargetUpdated += Image_TargetUpdatedAsync;
            else
                image.TargetUpdated -= Image_TargetUpdatedAsync;
        }

        private async void Image_TargetUpdatedAsync(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            await (sender as Image).FadeInAsync(false);
        }
    }

    public class AnimateSlideInFromRightProperty : AnimateBaseProperty<AnimateSlideInFromRightProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            if (value)
                await element.SlideAndFadeInAsync(AnimationSlideInDirection.Right, firstLoad, firstLoad ? 0 : 0.3f, keepMargin: false);
            else
                await element.SlideAndFadeOutAsync(AnimationSlideInDirection.Right, firstLoad ? 0 : 0.3f, keepMargin: false);
        }
    }

    public class AnimateSlideInFromRightMarginProperty : AnimateBaseProperty<AnimateSlideInFromRightMarginProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            if (value)
                await element.SlideAndFadeInAsync(AnimationSlideInDirection.Right, firstLoad, firstLoad ? 0 : 0.3f, keepMargin: true);
            else
                await element.SlideAndFadeOutAsync(AnimationSlideInDirection.Right, firstLoad ? 0 : 0.3f, keepMargin: true);
        }
    }
}