﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Telegram.Api.TL;
using Template10.Services.SerializationService;
using Unigram.Controls;
using Unigram.Converters;
using Unigram.Core.Dependency;
using Unigram.Core.Notifications;
using Unigram.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Unigram.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel => DataContext as MainViewModel;

        private object _lastSelected;
        private object _lastSelectedContact;

        public MainPage()
        {
            InitializeComponent();

            NavigationCacheMode = NavigationCacheMode.Required;

            DataContext = UnigramContainer.Instance.ResolverType<MainViewModel>();

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            OnStateChanged(null, null);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Frame.BackStack.Clear();

            if (MasterDetail.NavigationService == null)
            {
                MasterDetail.Initialize("Main");
            }

            ViewModel.NavigationService = MasterDetail.NavigationService;

            if (e.Parameter is string)
            {
                var parameter = SerializationService.Json.Deserialize((string)e.Parameter) as string;
                if (parameter != null)
                {
                    var data = Toast.SplitArguments((string)parameter);
                    if (data.ContainsKey("from_id"))
                    {
                        var user = ViewModel.CacheService.GetUser(int.Parse(data["from_id"]));
                        if (user != null)
                        {
                            ClearNavigation();
                            ViewModel.NavigationService.Navigate(typeof(DialogPage), new TLPeerUser { UserId = user.Id });
                        }
                    }
                    else if (data.ContainsKey("chat_id"))
                    {
                        var chat = ViewModel.CacheService.GetChat(int.Parse(data["chat_id"]));
                        if (chat != null)
                        {
                            ClearNavigation();
                            ViewModel.NavigationService.Navigate(typeof(DialogPage), new TLPeerChat { ChatId = chat.Id });
                        }
                    }
                    else if (data.ContainsKey("channel_id"))
                    {
                        var chat = ViewModel.CacheService.GetChat(int.Parse(data["channel_id"]));
                        if (chat != null)
                        {
                            ClearNavigation();
                            ViewModel.NavigationService.Navigate(typeof(DialogPage), new TLPeerChannel { ChannelId = chat.Id });
                        }
                    }
                }
            }
        }

        private void ClearNavigation()
        {
            while (ViewModel.NavigationService.Frame.BackStackDepth > 1)
            {
                ViewModel.NavigationService.Frame.BackStack.RemoveAt(1);
            }

            if (ViewModel.NavigationService.CanGoBack)
            {
                ViewModel.NavigationService.GoBack();
                ViewModel.NavigationService.Frame.ForwardStack.Clear();
            }
        }

        private void OnStateChanged(object sender, EventArgs e)
        {
            if (lvMasterChats.SelectionMode == ListViewSelectionMode.Multiple)
            {
                ChangeListState();
            }

            if (MasterDetail.CurrentState == MasterDetailState.Narrow)
            {
                lvMasterChats.IsItemClickEnabled = true;
                lvMasterChats.SelectionMode = ListViewSelectionMode.None;
                lvMasterChats.SelectedItem = null;
                Separator.BorderThickness = new Thickness(0);
            }
            else
            {
                lvMasterChats.IsItemClickEnabled = false;
                lvMasterChats.SelectionMode = ListViewSelectionMode.Single;
                lvMasterChats.SelectedItem = _lastSelected;
                Separator.BorderThickness = new Thickness(0, 0, 1, 0);
            }
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (lvMasterChats.SelectionMode != ListViewSelectionMode.Multiple)
            {
                _lastSelected = e.ClickedItem;

                var dialog = e.ClickedItem as TLDialog;
                ViewModel.NavigationService.Navigate(typeof(DialogPage), dialog.Peer);
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvMasterChats.SelectedItem != null && _lastSelected != lvMasterChats.SelectedItem && lvMasterChats.SelectionMode != ListViewSelectionMode.Multiple)
            {
                _lastSelected = lvMasterChats.SelectedItem;

                var dialog = lvMasterChats.SelectedItem as TLDialog;
                ViewModel.NavigationService.Navigate(typeof(DialogPage), dialog.Peer);
            }
        }

        private void cbtnMasterSelect_Click(object sender, RoutedEventArgs e)
        {
            lvMasterChats.SelectionMode = ListViewSelectionMode.Multiple;
            cbtnMasterDeleteSelected.Visibility = Visibility.Visible;
            cbtnMasterMuteSelected.Visibility = Visibility.Visible;
            cbtnCancelSelection.Visibility = Visibility.Visible;
            cbtnMasterSelect.Visibility = Visibility.Collapsed;
            cbtnMasterNewChat.Visibility = Visibility.Collapsed;

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            SystemNavigationManager.GetForCurrentView().BackRequested += Select_BackRequested;
        }

        private void cbtnMasterAbout_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.NavigationService.Navigate(typeof(AboutPage));
        }

        private void cbtnMasterSearch_Click(object sender, RoutedEventArgs e)
        {
            //PLEASE REMOVE THE BELOW LINE ONCE THE CHATPAGE HAS BEEN IMPLEMENTED
            ViewModel.NavigationService.Navigate(typeof(DialogSharedMediaPage));
        }

        private void Select_BackRequested(object sender, BackRequestedEventArgs e)
        {
            // Mark event as handled so we don't get bounced out of the app.
            e.Handled = true;
            ChangeListState();
        }

        private void ChangeListState()
        {
            cbtnMasterDeleteSelected.Visibility = Visibility.Collapsed;
            cbtnMasterMuteSelected.Visibility = Visibility.Collapsed;
            cbtnCancelSelection.Visibility = Visibility.Collapsed;
            cbtnMasterSelect.Visibility = Visibility.Visible;
            cbtnMasterNewChat.Visibility = Visibility.Visible;
            lvMasterChats.SelectionMode = ListViewSelectionMode.Single;
            SystemNavigationManager.GetForCurrentView().BackRequested -= Select_BackRequested;

            if (!ViewModel.NavigationService.CanGoBack)
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            }
        }

        private void cbtnCancelSelection_Click(object sender, RoutedEventArgs e)
        {
            ChangeListState();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearch.Text != "")
            {
                if (lvMasterChats.ItemsSource != ViewModel.SearchDialogs)
                {
                    lvMasterChats.ItemsSource = ViewModel.SearchDialogs;
                }
                ViewModel.GetSearchDialogs(txtSearch.Text);
            }
            else
            {
                lvMasterChats.ItemsSource = ViewModel.Dialogs;
            }
        }

        private async void PivotItem_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.Contacts.getTLContacts();
            await ViewModel.Contacts.GetSelfAsync();
        }

        private void UsersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UsersListView.SelectedItem != null && _lastSelectedContact != UsersListView.SelectedItem && UsersListView.SelectionMode != ListViewSelectionMode.Multiple)
            {
                var user = UsersListView.SelectedItem as UsersPanelListItem;
                ViewModel.NavigationService.Navigate(typeof(DialogPage), new TLPeerUser { UserId = user._parent.Id });
            }
        }

        private void Self_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.NavigationService.Navigate(typeof(DialogPage), new TLPeerUser { UserId = ViewModel.Contacts.Self?._parent.Id ?? 0 });
        }
    }
}
