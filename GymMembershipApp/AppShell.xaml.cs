using GymMembershipApp.Views;

namespace GymMembershipApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register routes for navigation
            Routing.RegisterRoute(nameof(MemberDetailPage), typeof(MemberDetailPage));
        }
    }
}