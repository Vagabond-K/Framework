using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VagabondK
{
    //public interface INavigationManager
    //{
    //    void Navigate(string title, string viewName = null);

    //    void ClearBackStack();
    //    void ClearForwardStack();

    //    bool CanGoBack { get; }
    //    bool CanGoForward { get; }

    //    void GoBack();
    //    void GoForward();


    //    public object ViewModel { get { return GetValue(null); } private set { SetValue(value); } }

    //    public void Navigate<T>(string title) where T : class => Navigate(title, null as string, null as Action<T>);

    //    public void Navigate<T>(string title, string viewName) where T : class => Navigate(title, viewName, null as Action<T>);

    //    public void Navigate<T>(string title, out T viewModel) where T : class => Navigate(title, out viewModel, null, null as Action<T>);

    //    public void Navigate<T>(string title, out T viewModel, string viewName) where T : class => Navigate(title, out viewModel, viewName, null as Action<T>);

    //    public void Navigate(string title, string viewModelName, string viewName) => Navigate(title, viewModelName, viewName, null as Action<object>);

    //    public void Navigate(string title, object viewModel) => Navigate(title, viewModel, null as string);

    //    public void Navigate(string title, object viewModel, string viewName) => Navigate(title, viewModel, viewName, null as Action<object>);


    //    public void Navigate<T>(string title, Action<T> initializer) where T : class => Navigate(title, null as string, initializer);

    //    public void Navigate<T>(string title, string viewName, Action<T> initializer) where T : class
    //    {
    //        var viewModel = ShellViewModel.AppManager.Subscribe<T>();
    //        if (viewModel != null) initializer?.Invoke(viewModel);
    //        Navigate(title, viewModel, viewName);
    //    }

    //    public void Navigate<T>(string title, out T viewModel, string viewName, Action<T> initializer) where T : class
    //    {
    //        viewModel = ShellViewModel.AppManager.Subscribe<T>();
    //        if (viewModel != null) initializer?.Invoke(viewModel);
    //        Navigate(title, viewModel, viewName);
    //    }

    //    public void Navigate(string title, string viewModelName, string viewName, Action<object> initializer)
    //    {
    //        var viewModel = ShellViewModel.AppManager.Subscribe(viewModelName);
    //        if (viewModel != null) initializer?.Invoke(viewModel);
    //        Navigate(title, viewModel, viewName);
    //    }

    //    public void Navigate(string title, object viewModel, Action<object> initializer) => Navigate(title, viewModel, null as string, initializer);

    //    public void Navigate(string title, object viewModel, string viewName, Action<object> initializer)
    //    {
    //        if (viewModel == null) return;

    //        ViewModel = viewModel;
    //        if (viewModel != null) initializer?.Invoke(viewModel);
    //        Navigate(title, viewName);
    //    }

    //    public void Navigate(string title, string viewName, Action<object> initializer)
    //    {
    //        if (ViewModel != null) initializer?.Invoke(ViewModel);
    //        Navigate(title, viewName);
    //    }

    //}
}
