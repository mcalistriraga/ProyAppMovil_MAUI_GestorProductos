using System;

#if ANDROID
using Android.Views.InputMethods;
using Microsoft.Maui.Platform;
#elif IOS
using UIKit;
#endif

namespace MauiAppGestorMovil.Helpers
{
    public static class CloseTecladoHelper
    {
#pragma warning disable CA1422
        public static void Ocultar()
        {
#if ANDROID
            var activity = Platform.CurrentActivity;
            if (activity == null)
                return;

            if (activity.GetSystemService(Android.Content.Context.InputMethodService) is InputMethodManager inputMethodManager &&
                activity.CurrentFocus is { } currentFocus)
            {
                inputMethodManager.HideSoftInputFromWindow(currentFocus.WindowToken, HideSoftInputFlags.None);
                currentFocus.ClearFocus();
            }

#elif IOS
            var window = UIApplication.SharedApplication.KeyWindow;
            var view = window?.RootViewController?.View;
            view?.EndEditing(true);
#endif
        }
#pragma warning restore CA1422
    }
}
