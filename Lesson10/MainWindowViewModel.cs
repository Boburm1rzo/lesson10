using MvvmHelpers;
using MvvmHelpers.Commands;
using System.Windows;

namespace Lesson10
{
    public class MainWindowViewModel : BaseViewModel
    {
        private int _counter;
        public int Counter 
        {
            get => _counter;
            set => SetProperty(ref _counter, value);
        }

        public Command ButtonClickedCommand { get; }

        Channel channel = new Channel
        {
            Id = 1,
            Name = "Free Code Camp",
        };

        public MainWindowViewModel()
        {
            ButtonClickedCommand = new Command(OnButtonClicked);

            Load();
        }

        private void Load()
        {
            User user = new User
            {
                Id = 1,
                Name = "User 1"
            };
            User user1 = new User
            {
                Id = 2,
                Name = "User 2"
            };
            User user2 = new User
            {
                Id = 3,
                Name = "User 3"
            };

            user.SubscribeToChannel(channel);
            user1.SubscribeToChannel(channel);
            user2.SubscribeToChannel(channel);
        }

        public decimal CalculateTotalSalary(List<Employee> employees)
        {
            decimal total = 0m;
            decimal tax = 0.05m;

            if (employees is null)
            {
                return 0;
            }

            foreach(var employee in employees)
            {
                if (employee.Salary <= 0)
                {
                    continue;
                }

                total += employee.Salary - (employee.Salary * tax);
            }

            return total;
        }

        private void OnButtonClicked()
        {
            Counter += 1;

            channel.AddVideo(new Video()
            {
                Id = Counter,
                Name = $"New Video: {Counter}"
            });
        }
    }

    public class Employee
    {
        public int Id { get; set; }
        public decimal Salary { get; set; }
    }

    public class Channel
    {
        public event EventHandler<VideoEventArgs> NewVideoUploaded;
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Video> Videos { get; private set; }
        public List<User> Subscribers { get; private set; }

        public Channel()
        {
            Videos = new();
            Subscribers = new();
        }

        public void AddVideo(Video video)
        {
            var args = new VideoEventArgs(video);
            NewVideoUploaded.Invoke(this, args);

            //foreach (var subscriber in Subscribers)
            //{
            //    var args = new VideoEventArgs(video);
            //    subscriber.SubscribeToVideoUpdates(this, args);
            //}

            Videos.Add(video);
        }
    }

    public class VideoEventArgs : EventArgs
    {
        public Video Video { get; }

        public VideoEventArgs(Video video)
        {
            Video = video;
        }
    }

    public class Video
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Channel> SubscribedChannels { get; set; }

        public User()
        {
            SubscribedChannels = new List<Channel>();
        }

        public void SubscribeToChannel(Channel channel)
        {
            //channel.NewVideoUploaded += SubscribeToVideoUpdates;
            channel.Subscribers.Add(this);
        }

        public void SubscribeToVideoUpdates(object sender, VideoEventArgs e)
        {
            // send notification
            MessageBox.Show($"{Name}: {e.Video.Name}");
        }
    }
}
