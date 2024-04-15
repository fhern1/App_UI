using Microsoft.Maui.Controls;
using System;
using CommunityToolkit.Mvvm.Messaging;

namespace App_UI
{
    public partial class AddDevicePage : ContentPage
    {
        public AddDevicePage()
        {
            InitializeComponent();
        }

        private async void OnAddDeviceButtonClicked(object sender, EventArgs e)
        {
            var deviceName = deviceNameEntry.Text; // Assuming you have an Entry with x:Name="deviceNameEntry"
            if (string.IsNullOrWhiteSpace(deviceName) || deviceTypePicker.SelectedItem == null || deviceCompanyPicker.SelectedItem == null)
            {
                // Optionally, alert the user that they need to fill all fields.
                await DisplayAlert("Error", "Please fill in all fields.", "OK");
                return;
            }

            // Use null-conditional operator ?. and ?? to provide a fallback value to avoid CS8604.
            var deviceTypeString = deviceTypePicker.SelectedItem?.ToString() ?? string.Empty;
            var deviceCompanyString = deviceCompanyPicker.SelectedItem?.ToString() ?? string.Empty;

            // The above ensures deviceTypeString and deviceCompanyString are never null here,
            // but you might want to check if they're empty and handle accordingly.

            if (string.IsNullOrEmpty(deviceTypeString) || string.IsNullOrEmpty(deviceCompanyString))
            {
                await DisplayAlert("Error", "Invalid selection. Please select both a device type and company.", "OK");
                return;
            }

            // Now it's safe to parse the enums
            var deviceType = (DeviceType)Enum.Parse(typeof(DeviceType), deviceTypeString);
            var deviceCompany = (DeviceCompany)Enum.Parse(typeof(DeviceCompany), deviceCompanyString);
            var isActive = isActiveSwitch.IsToggled;

            var newDevice = new Device
            {
                DeviceName = deviceName,
                Type = deviceType,
                Company = deviceCompany,
                IsActive = isActive
            };
            DeviceManager.Devices.Add(newDevice); // Add the newly created device to the global list

            // Send a message that a new device is added
            WeakReferenceMessenger.Default.Send(new DeviceAddedMessage(newDevice));

            // Navigate back to the previous page
            await Navigation.PopAsync();
        }
    }
}
