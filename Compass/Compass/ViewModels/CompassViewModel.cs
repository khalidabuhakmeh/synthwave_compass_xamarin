using System.ComponentModel;
using System.Runtime.CompilerServices;
using Compass.Annotations;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Compass.ViewModels
{
    public sealed class CompassViewModel : INotifyPropertyChanged
    {
        private double _magneticNorth;
        public Command StartCommand { get; private set; }
        public Command StopCommand { get; private set; }

        public CompassViewModel()
        {
            StartCommand = new Command(Start);
            StopCommand = new Command(Stop);
        }

        private void Stop()
        {
            try
            {
                Xamarin.Essentials.Compass.ReadingChanged -= CompassOnReadingChanged;
                Xamarin.Essentials.Compass.Stop();
            }
            catch (FeatureNotSupportedException e)
            {
                // feature not supported
            }
        }

        private void Start()
        {
            try
            {
                Xamarin.Essentials.Compass.Start(SensorSpeed.UI, applyLowPassFilter: true);
                Xamarin.Essentials.Compass.ReadingChanged += CompassOnReadingChanged;
            }
            catch (FeatureNotSupportedException e)
            {
                // feature not supported
            }
        }

        private void CompassOnReadingChanged(object sender, CompassChangedEventArgs e)
        {
            MagneticNorth = e.Reading.HeadingMagneticNorth;
        }

        public double MagneticNorth
        {
            get => _magneticNorth;
            set
            {
                if (value.Equals(_magneticNorth)) return;
                _magneticNorth = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}