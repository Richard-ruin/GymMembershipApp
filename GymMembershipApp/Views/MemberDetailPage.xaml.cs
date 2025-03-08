using GymMembershipApp.Models;
using GymMembershipApp.Services;
using System.ComponentModel;
using System.Windows.Input;

namespace GymMembershipApp.Views
{
    public partial class MemberDetailPage : ContentPage, INotifyPropertyChanged
    {
        private readonly DatabaseService _databaseService;
        private Member _member;
        private bool _isEditable;
        private bool _isViewMode;
        private string _pageTitle;

        public event PropertyChangedEventHandler PropertyChanged;

        public Member Member
        {
            get => _member;
            set
            {
                _member = value;
                OnPropertyChanged(nameof(Member));
            }
        }

        public bool IsEditable
        {
            get => _isEditable;
            set
            {
                _isEditable = value;
                OnPropertyChanged(nameof(IsEditable));
                OnPropertyChanged(nameof(IsViewMode));
            }
        }

        public bool IsViewMode => !_isEditable;

        public string PageTitle
        {
            get => _pageTitle;
            set
            {
                _pageTitle = value;
                OnPropertyChanged(nameof(PageTitle));
            }
        }

        public List<string> MembershipTypes { get; } = new List<string>
        {
            "Basic",
            "Standard",
            "Premium",
            "Gold",
            "Platinum"
        };

        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand EditCommand { get; private set; }

        // Constructor for Add
        public MemberDetailPage()
        {
            InitializeComponent();
            _databaseService = new DatabaseService();

            // Initialize with a new member
            Member = new Member
            {
                DateOfBirth = DateTime.Now.AddYears(-25),
                JoinDate = DateTime.Now,
                ExpiryDate = DateTime.Now.AddYears(1),
                ActiveStatus = true,
                MembershipType = "Basic"
            };

            IsEditable = true;
            PageTitle = "Add New Member";
            InitializeCommands();
            BindingContext = this;
        }

        // Constructor for Edit/View
        public MemberDetailPage(Member member, bool viewOnly = false)
        {
            InitializeComponent();
            _databaseService = new DatabaseService();

            // Clone the member to avoid direct modifications
            Member = new Member
            {
                MemberID = member.MemberID,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Email = member.Email,
                PhoneNumber = member.PhoneNumber,
                Address = member.Address,
                DateOfBirth = member.DateOfBirth,
                MembershipType = member.MembershipType,
                JoinDate = member.JoinDate,
                ExpiryDate = member.ExpiryDate,
                ActiveStatus = member.ActiveStatus
            };

            IsEditable = !viewOnly;
            PageTitle = viewOnly ? "Member Details" : "Edit Member";
            InitializeCommands();
            BindingContext = this;
        }

        private void InitializeCommands()
        {
            SaveCommand = new Command(ExecuteSave);
            CancelCommand = new Command(ExecuteCancel);
            EditCommand = new Command(ExecuteEdit);
        }

        private async void ExecuteSave()
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(Member.FirstName) ||
                string.IsNullOrWhiteSpace(Member.LastName) ||
                string.IsNullOrWhiteSpace(Member.Email) ||
                string.IsNullOrWhiteSpace(Member.MembershipType))
            {
                await DisplayAlert("Validation Error",
                    "Please fill in all required fields (First Name, Last Name, Email, Membership Type)",
                    "OK");
                return;
            }

            bool success;

            if (Member.MemberID == 0)
            {
                // Add new member
                success = _databaseService.AddMember(Member);
            }
            else
            {
                // Update existing member
                success = _databaseService.UpdateMember(Member);
            }

            if (success)
            {
                await DisplayAlert("Success",
                    Member.MemberID == 0 ? "Member added successfully" : "Member updated successfully",
                    "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error",
                    Member.MemberID == 0 ? "Failed to add member" : "Failed to update member",
                    "OK");
            }
        }

        private async void ExecuteCancel()
        {
            await Navigation.PopAsync();
        }

        private void ExecuteEdit()
        {
            IsEditable = true;
            PageTitle = "Edit Member";
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}