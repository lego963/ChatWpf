using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ChatWpf.Animation;

namespace ChatWpf.AttachedProperties
{
    public abstract class AnimateBaseProperty<TParent> : BaseAttachedProperty<TParent, bool> where TParent : BaseAttachedProperty<TParent, bool>, new()
    {
        protected Dictionary<DependencyObject, bool> mAlreadyLoaded = new Dictionary<DependencyObject, bool>();
        protected Dictionary<DependencyObject, bool> mFirstLoadValue = new Dictionary<DependencyObject, bool>();

        public override void OnValueUpdated(DependencyObject sender, object value)
        {
            if (!(sender is FrameworkElement element))
                return;

            if ((bool)sender.GetValue(ValueProperty) == (bool)value && mAlreadyLoaded.ContainsKey(sender))
                return;


            if (!mAlreadyLoaded.ContainsKey(sender))
            {
                mAlreadyLoaded[sender] = false;

                element.Visibility = Visibility.Hidden;

                RoutedEventHandler onLoaded = null;
                onLoaded = async (ss, ee) =>
                {
                    element.Loaded -= onLoaded;

                    await Task.Delay(5);

                    DoAnimation(element, mFirstLoadValue.ContainsKey(sender) ? mFirstLoadValue[sender] : (bool)value, true);

                    mAlreadyLoaded[sender] = true;

                };

                element.Loaded += onLoaded;
            }
            else if (mAlreadyLoaded[sender] == false)
                mFirstLoadValue[sender] = (bool)value;
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

    public class AnimateSlideInFromBottomOnLoadProperty : AnimateBaseProperty<AnimateSlideInFromBottomOnLoadProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            // Animate in
            await element.SlideAndFadeInAsync(AnimationSlideInDirection.Bottom, !value, !value ? 0 : 0.3f, keepMargin: false);
        }
    }

    public class FadeInImageOnLoadProperty : AnimateBaseProperty<FadeInImageOnLoadProperty>
    {
        public override void OnValueUpdated(DependencyObject sender, object value)
        {
            // Make sure we have an image
            if (!(sender is Image image))
                return;

            // If we want to animate in...
            if ((bool)value)
                // Listen for target change
                image.TargetUpdated += Image_TargetUpdatedAsync;
            // Otherwise
            else
                // Make sure we unhooked
                image.TargetUpdated -= Image_TargetUpdatedAsync;
        }

        private async void Image_TargetUpdatedAsync(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
            // Fade in image
            await (sender as Image).FadeInAsync(false);
        }
    }
}