﻿using System.Windows.Input;
using Lorenzo.WorkWatcher.Models;

namespace Lorenzo.WorkWatcher.ViewModels
{
    public interface IShellViewModel
    {
        ShellModel Model { get; }
        ICommand StartCommand { get; }
        ICommand StopCommand { get; }
        ICommand TickCommand { get; }
        ICommand TestCommand { get; }
        ICommand DetailCommand { get; }

        void LoadData();
        void ClosingWindow();
        void ViewShown();
    }
}