using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.WPF.View.OwnerViews;
using BookingApp.WPF.View.Guest;
using BookingApp.WPF.View.Guide;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BookingApp.WPF.View
{
    /// <summary>
    /// Interaction logic for SignInForm.xaml
    /// </summary>
    public partial class SignInForm : Window
    {

        private readonly UserRepository repository;

        private string username;
        public static int curretnUserId;
        public string Username
        {
            get => username;
            set
            {
                if (value != username)
                {
                    username = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SignInForm()
        {
            InitializeComponent();
            DataContext = this;
            repository = new UserRepository();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            User user = repository.GetByUsername(Username);

            if (user != null)
            {
                curretnUserId = user.Id;    //prebaceno tu da ne bi pucalo ako se pogresi userName Ivan

                if (user.Password == txtPassword.Password)
                {
                    switch (user.Vocation)
                    {
                        case 1:
                            // OWNER MAIN VIEW
                            var owner = new MainView(user);
                            owner.Show();
                            Close();
                            break;
                        case 2:
                            GuestMainWindow guest = new GuestMainWindow(user);
                            guest.Show();
                            Close();
                            break;
                        case 3:
                            //Guide HomePage (ili kako vec)
                            GuideMainWindow guideMainWindow = new GuideMainWindow();
                            guideMainWindow.Show();
                            Close();
                            break;
                        case 4:
                            DriverMainWindow driverMainWindow = new DriverMainWindow(user.Id);
                            driverMainWindow.Show();
                            Close();
                            break;
                        case 5:
                            TouristHomePage touristHomePage = new TouristHomePage();
                            touristHomePage.Show();
                            Close();
                            break;
                    }
                } 
                else
                {
                    MessageBox.Show("Wrong password!");
                }
            }
            else
            {
                MessageBox.Show("Wrong username!");
            }
        }
    }
}
