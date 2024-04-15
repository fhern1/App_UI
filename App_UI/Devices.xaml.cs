using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using Microsoft.Maui.Dispatching;

namespace App_UI
{
    public partial class Devices : ContentPage
    {
        public ObservableCollection<Device> DevicesList { get; set; }

        public Devices()
        {
            InitializeComponent();
            DevicesList = new ObservableCollection<Device>();
            devicesCollectionView.ItemsSource = DevicesList;
            this.BindingContext = this;

            // Subscribe to the AddDevice message
            // WeakReferenceMessenger.Default.Register<DeviceAddedMessage>(this, (r, m) =>
            //{
            //   Device.BeginInvokeOnMainThread(() =>
            // {
            // Add the new device to the list
            //      DevicesList.Add(m.Value);
            //  });
            //});
            WeakReferenceMessenger.Default.Register<DeviceAddedMessage>(this, (r, m) =>
            {
                MainThread.BeginInvokeOnMainThread(() => {
                    DevicesList.Add(m.Value);
                });
            });
        }

        private async void OnAddDeviceClicked(object sender, EventArgs e)
        {
            // Navigate to the AddDevicePage
            await Navigation.PushAsync(new AddDevicePage());
        }

        private void OnDeleteDeviceClicked(object sender, EventArgs e)
        {
            var selectedItem = devicesCollectionView.SelectedItem as Device;
            if (selectedItem != null)
            {
                DevicesList.Remove(selectedItem);
                devicesCollectionView.SelectedItem = null;
            }
            else
            {
                DisplayAlert("Delete Device", "No device selected.", "OK");
            }
        }

        protected override void OnDisappearing()
        {
            var selectedItem = devicesCollectionView.SelectedItem as Device;
            if (selectedItem != null)
            {
                // Remove the selected device from the global list
                DeviceManager.Devices.Remove(selectedItem);

                // Refresh the CollectionView to reflect the change
                RefreshDevicesList();

                // Optionally, clear the current selection
                devicesCollectionView.SelectedItem = null;
            }
            else
            {
                DisplayAlert("Delete Device", "No device selected.", "OK");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshDevicesList();
        }

        private void RefreshDevicesList()
        {
            devicesCollectionView.ItemsSource = null; // Clear existing items to force refresh
            devicesCollectionView.ItemsSource = DeviceManager.Devices; // Reassign the list
        }


    }

    public class Device
    {
        public string DeviceName { get; set; } = string.Empty;
        public DeviceType Type { get; set; }
        public DeviceCompany Company { get; set; }
        public bool IsActive { get; set; }
    }

    public enum DeviceType
    {
        Sensor,
        DoorLock,
        Light

    }

    public enum DeviceCompany
    {
        Philips,
        PercepiCam,
        Kuikset

    }
    public class DeviceAddedMessage : ValueChangedMessage<Device>
    {
        public DeviceAddedMessage(Device device) : base(device)
        {
        }
    }
}