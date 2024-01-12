using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.AppService;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1
{
  /// <summary>
  /// An empty page that can be used on its own or navigated to within a Frame.
  /// </summary>
  public sealed partial class MainPage : Page
  {
    public MainPage()
    {
      this.InitializeComponent();
    }

    private AppServiceConnection pricesService;

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
      if (this.pricesService == null)
      {
        this.pricesService = new AppServiceConnection();

        this.pricesService.AppServiceName = "com.mytruckdomain.foodprices";
        this.pricesService.PackageFamilyName = "057a0e7e-0420-4dac-a6b2-ac74b1d528dc_5fvvhcaa5mb7r";

        var status = await this.pricesService.OpenAsync();
        if (status != AppServiceConnectionStatus.Success)
        {
          this.pricesService = null;
          return;
        }
      }

      var message = new ValueSet();
      message.Add("item", "cheeseburger");
      AppServiceResponse response = await this.pricesService.SendMessageAsync(message);
      if (response.Status == AppServiceResponseStatus.Success)
      {
        object priceobject = null;
        if (response.Message.TryGetValue("price", out priceobject))
        {
          this.Result.Text = priceobject.ToString();
        }
      }
    }
  }
}
