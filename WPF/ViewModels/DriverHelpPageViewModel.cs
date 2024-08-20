using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels
{
    public class DriverHelpPageViewModel : INotifyPropertyChanged
    {
        public NavigationService NavigationService { get; set; }

        private readonly int id;


        public DriverHelpPageViewModel(int id, NavigationService navigationService)
        {
            this.id = id;
            NavigationService = navigationService;
        }

        private readonly Dictionary<string, (string Title, string Content, string ImagePath)> HelpContent = new()
    {
        { "MainPage", ("Main Page Help",
            "1. Today's Drives:\n" +
            "View a list of all your scheduled drives, including the start and end addresses, and the scheduled time.\n\n" +
            "2. Notifications Tab:\n" +
            "Stay updated with the latest notifications.\n\n" +
            "3. Dismiss Drive Button:\n" +
            "Easily dismiss drive.\n\n" +
            "4. Status Buttons:\n" +
            "Indicate your status with 'Late' and 'On Location' buttons.\n\n" +
            "5. Timer:\n" +
            "Track your driving time accurately with the built-in timer.\n\n" +
            "6. Action Buttons:\n" +
            "Use the 'Leave' and 'Start' buttons to manage your drive actions.\n\n" +
            "7. Profile Button:\n" +
            "Check your profile and vehicle iformation.\n",
            "/Resources/Images/1.png") },
        { "ProfilePage", ("Profile Page Help",
            "This page is designed to provide a comprehensive overview of the driver's profile and vehicle information.\n\n" +
            "Here are the key sections and functionalities:\n\n" +
            "1. Driver's Information:\n" +
            "Includes important details about the driver and personal information.\n\n" +
            "2. Driving Points:\n" +
            "Shows the total driving points, accumulated by the driver, used to become a SuperDriver (15 points).\n\n" +
            "3. Drive Statistics Button:\n" +
            "Provides access to detailed statistics about the driver's trips, including driving price, time spent driving, and number of drives.\n\n" +
            "4. Next Drive Timer:\n" +
            "A countdown timer showing the time remaining until the next scheduled drive.\n\n" +
            "5. Request Break:\n" +
            "A button for the driver to request a break.\n\n" +
            "6. Vehicle information:\n" +
            "Includes details about the registered vehicle.\n\n" +
            "7. Dismiss Vehicle:\n" +
            "A button that unregister a vehicle.\n\n",
            "/Resources/Images/2.png") },
        // Add more pages and their content here
    };

        public (string Title, string Content, string ImagePath) GetHelpContent(string pageKey)
        {
            return HelpContent.TryGetValue(pageKey, out var content) ? content : ("Help", "No help content available for this page.", null);
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
