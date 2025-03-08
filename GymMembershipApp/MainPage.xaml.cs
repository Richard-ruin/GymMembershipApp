using GymMembershipApp.Models;
using GymMembershipApp.Services;
using GymMembershipApp.Views;
using System.Collections.ObjectModel;

namespace GymMembershipApp
{
    public partial class MainPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        private ObservableCollection<Member> _members;

        public MainPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();
            _members = new ObservableCollection<Member>();
            MembersCollectionView.ItemsSource = _members;

            // Load data when page appears
            this.Appearing += (sender, e) => LoadMembers();
        }

        private void LoadMembers()
        {
            try
            {
                var members = _databaseService.GetAllMembers();
                _members.Clear();

                foreach (var member in members)
                {
                    _members.Add(member);
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", $"Failed to load members: {ex.Message}", "OK");
            }
        }

        private async void OnAddMemberClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MemberDetailPage());
        }

        private async void OnEditMemberClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is int memberId)
            {
                var member = _databaseService.GetMemberById(memberId);
                if (member != null)
                {
                    await Navigation.PushAsync(new MemberDetailPage(member));
                }
            }
        }

        private async void OnDeleteMemberClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is int memberId)
            {
                bool confirm = await DisplayAlert("Confirm Delete",
                    "Are you sure you want to delete this member?", "Yes", "No");

                if (confirm)
                {
                    bool success = _databaseService.DeleteMember(memberId);
                    if (success)
                    {
                        LoadMembers();
                        await DisplayAlert("Success", "Member deleted successfully", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Error", "Failed to delete member", "OK");
                    }
                }
            }
        }

        private void OnRefreshClicked(object sender, EventArgs e)
        {
            LoadMembers();
        }

        private async void OnMemberSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Member selectedMember)
            {
                // Clear selection
                MembersCollectionView.SelectedItem = null;

                // Show member details
                await Navigation.PushAsync(new MemberDetailPage(selectedMember, true));
            }
        }
    }
}