using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Microsoft.Phone.Net.NetworkInformation;
using System.Diagnostics;
using Microsoft.Phone.Tasks;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;

namespace cidr_calculator
{
    public partial class MainPage : PhoneApplicationPage
    {
        string CellularMobileOperator { get; set; }
        bool IsCellularDataEnabled { get; set; }
        bool IsCellularDataRoamingEnabled { get; set; }
        bool IsNetworkAvailable { get; set; }
        bool IsWifiEnabled { get; set; }
        IsolatedStorageSettings isolatedStorage;
        bool IsSelectionChangedEnabled = false;

        #region List Picker Data
        List<ClasslessInterDomainRouting> CIDR_Notation = new List<ClasslessInterDomainRouting>() {
            new ClasslessInterDomainRouting("/1",  "128.0.0.0" ),
            new ClasslessInterDomainRouting("/2","192.0.0.0" ),
            new ClasslessInterDomainRouting("/3", "224.0.0.0") ,
            new ClasslessInterDomainRouting("/4","240.0.0.0" ),
            new ClasslessInterDomainRouting("/5","248.0.0.0" ),
            new ClasslessInterDomainRouting("/6","252.0.0.0" ),
            new ClasslessInterDomainRouting("/7","254.0.0.0" ),
            new ClasslessInterDomainRouting("/8","255.0.0.0" ),
            new ClasslessInterDomainRouting("/9","255.128.0.0" ),
            new ClasslessInterDomainRouting("/10","255.192.0.0" ),
            new ClasslessInterDomainRouting("/11","255.224.0.0" ),
            new ClasslessInterDomainRouting("/12","255.240.0.0" ),
            new ClasslessInterDomainRouting("/13","255.248.0.0"),
            new ClasslessInterDomainRouting("/14","255.252.0.0" ),
            new ClasslessInterDomainRouting("/15","255.254.0.0"),
            new ClasslessInterDomainRouting("/16","255.255.0.0"),
            new ClasslessInterDomainRouting("/17","255.255.128.0"),
            new ClasslessInterDomainRouting("/18","255.255.192.0" ),
            new ClasslessInterDomainRouting("/19","255.255.224.0"),
            new ClasslessInterDomainRouting("/20","255.255.240.0" ),
            new ClasslessInterDomainRouting("/21","255.255.248.0"),
            new ClasslessInterDomainRouting("/22","255.255.252.0"),
            new ClasslessInterDomainRouting("/23","255.255.254.0" ),
            new ClasslessInterDomainRouting("/24","255.255.255.0" ),
            new ClasslessInterDomainRouting("/25","255.255.255.128" ),
            new ClasslessInterDomainRouting("/26","255.255.255.192" ),
            new ClasslessInterDomainRouting("/27","255.255.255.224" ),
            new ClasslessInterDomainRouting("/28","255.255.255.240" ),
            new ClasslessInterDomainRouting("/29","255.255.255.248" ),
            new ClasslessInterDomainRouting("/30","255.255.255.252" ),
            new ClasslessInterDomainRouting("/31","255.255.255.254" ),
            new ClasslessInterDomainRouting("/32","255.255.255.255" )
            //            new ClasslessInterDomainRouting("/21","255.255.248.0",2048,2046),
            //new ClasslessInterDomainRouting("/22","255.255.252.0",1024,1022),
            //new ClasslessInterDomainRouting("/23","255.255.254.0",512,510 ),
            //new ClasslessInterDomainRouting("/24","255.255.255.0",256,254 ),
            //new ClasslessInterDomainRouting("/25","255.255.255.128",128,126 ),
            //new ClasslessInterDomainRouting("/26","255.255.255.192",64,62 ),
            //new ClasslessInterDomainRouting("/27","255.255.255.224",32,30 ),
            //new ClasslessInterDomainRouting("/28","255.255.255.240",16,14 ),
            //new ClasslessInterDomainRouting("/29","255.255.255.248",8,6 ),
            //new ClasslessInterDomainRouting("/30","255.255.255.252",4,2 ),
            //new ClasslessInterDomainRouting("/31","255.255.255.254",2,2 ),
            //new ClasslessInterDomainRouting("/32","255.255.255.255",1,1 )
        };

        List<ClasslessInterDomainRouting> CIDR_Decimal = new List<ClasslessInterDomainRouting>() {
            new ClasslessInterDomainRouting("/1",  "128.0.0.0" ),
            new ClasslessInterDomainRouting("/2","192.0.0.0" ),
            new ClasslessInterDomainRouting("/3", "224.0.0.0") ,
            new ClasslessInterDomainRouting("/4","240.0.0.0" ),
            new ClasslessInterDomainRouting("/5","248.0.0.0" ),
            new ClasslessInterDomainRouting("/6","252.0.0.0" ),
            new ClasslessInterDomainRouting("/7","254.0.0.0" ),
            new ClasslessInterDomainRouting("/8","255.0.0.0" ),
            new ClasslessInterDomainRouting("/9","255.128.0.0" ),
            new ClasslessInterDomainRouting("/10","255.192.0.0" ),
            new ClasslessInterDomainRouting("/11","255.224.0.0" ),
            new ClasslessInterDomainRouting("/12","255.240.0.0" ),
            new ClasslessInterDomainRouting("/13","255.248.0.0"),
            new ClasslessInterDomainRouting("/14","255.252.0.0" ),
            new ClasslessInterDomainRouting("/15","255.254.0.0"),
            new ClasslessInterDomainRouting("/16","255.255.0.0"),
            new ClasslessInterDomainRouting("/17","255.255.128.0"),
            new ClasslessInterDomainRouting("/18","255.255.192.0" ),
            new ClasslessInterDomainRouting("/19","255.255.224.0"),
            new ClasslessInterDomainRouting("/20","255.255.240.0" ),
            new ClasslessInterDomainRouting("/21","255.255.248.0"),
            new ClasslessInterDomainRouting("/22","255.255.252.0"),
            new ClasslessInterDomainRouting("/23","255.255.254.0" ),
            new ClasslessInterDomainRouting("/24","255.255.255.0" ),
            new ClasslessInterDomainRouting("/25","255.255.255.128" ),
            new ClasslessInterDomainRouting("/26","255.255.255.192" ),
            new ClasslessInterDomainRouting("/27","255.255.255.224" ),
            new ClasslessInterDomainRouting("/28","255.255.255.240" ),
            new ClasslessInterDomainRouting("/29","255.255.255.248" ),
            new ClasslessInterDomainRouting("/30","255.255.255.252" ),
            new ClasslessInterDomainRouting("/31","255.255.255.254" ),
            new ClasslessInterDomainRouting("/32","255.255.255.255" )
        };


        List<ClasslessInterDomainRouting> CIDR_Binary = new List<ClasslessInterDomainRouting>() {
            new ClasslessInterDomainRouting("/1",  "128.0.0.0" ),
            new ClasslessInterDomainRouting("/2","192.0.0.0" ),
            new ClasslessInterDomainRouting("/3", "224.0.0.0") ,
            new ClasslessInterDomainRouting("/4","240.0.0.0" ),
            new ClasslessInterDomainRouting("/5","248.0.0.0" ),
            new ClasslessInterDomainRouting("/6","252.0.0.0" ),
            new ClasslessInterDomainRouting("/7","254.0.0.0" ),
            new ClasslessInterDomainRouting("/8","255.0.0.0" ),
            new ClasslessInterDomainRouting("/9","255.128.0.0" ),
            new ClasslessInterDomainRouting("/10","255.192.0.0" ),
            new ClasslessInterDomainRouting("/11","255.224.0.0" ),
            new ClasslessInterDomainRouting("/12","255.240.0.0" ),
            new ClasslessInterDomainRouting("/13","255.248.0.0"),
            new ClasslessInterDomainRouting("/14","255.252.0.0" ),
            new ClasslessInterDomainRouting("/15","255.254.0.0"),
            new ClasslessInterDomainRouting("/16","255.255.0.0"),
            new ClasslessInterDomainRouting("/17","255.255.128.0"),
            new ClasslessInterDomainRouting("/18","255.255.192.0" ),
            new ClasslessInterDomainRouting("/19","255.255.224.0"),
            new ClasslessInterDomainRouting("/20","255.255.240.0" ),
            new ClasslessInterDomainRouting("/21","255.255.248.0"),
            new ClasslessInterDomainRouting("/22","255.255.252.0"),
            new ClasslessInterDomainRouting("/23","255.255.254.0" ),
            new ClasslessInterDomainRouting("/24","255.255.255.0" ),
            new ClasslessInterDomainRouting("/25","255.255.255.128" ),
            new ClasslessInterDomainRouting("/26","255.255.255.192" ),
            new ClasslessInterDomainRouting("/27","255.255.255.224" ),
            new ClasslessInterDomainRouting("/28","255.255.255.240" ),
            new ClasslessInterDomainRouting("/29","255.255.255.248" ),
            new ClasslessInterDomainRouting("/30","255.255.255.252" ),
            new ClasslessInterDomainRouting("/31","255.255.255.254" ),
            new ClasslessInterDomainRouting("/32","255.255.255.255" )
        };

        List<ClasslessInterDomainRouting> CIDR_MaximumSubnets = new List<ClasslessInterDomainRouting>() {
            new ClasslessInterDomainRouting("/1",  "128.0.0.0" ),
            new ClasslessInterDomainRouting("/2","192.0.0.0" ),
            new ClasslessInterDomainRouting("/3", "224.0.0.0") ,
            new ClasslessInterDomainRouting("/4","240.0.0.0" ),
            new ClasslessInterDomainRouting("/5","248.0.0.0" ),
            new ClasslessInterDomainRouting("/6","252.0.0.0" ),
            new ClasslessInterDomainRouting("/7","254.0.0.0" ),
            new ClasslessInterDomainRouting("/8","255.0.0.0" ),
            new ClasslessInterDomainRouting("/9","255.128.0.0" ),
            new ClasslessInterDomainRouting("/10","255.192.0.0" ),
            new ClasslessInterDomainRouting("/11","255.224.0.0" ),
            new ClasslessInterDomainRouting("/12","255.240.0.0" ),
            new ClasslessInterDomainRouting("/13","255.248.0.0"),
            new ClasslessInterDomainRouting("/14","255.252.0.0" ),
            new ClasslessInterDomainRouting("/15","255.254.0.0"),
            new ClasslessInterDomainRouting("/16","255.255.0.0"),
            new ClasslessInterDomainRouting("/17","255.255.128.0"),
            new ClasslessInterDomainRouting("/18","255.255.192.0" ),
            new ClasslessInterDomainRouting("/19","255.255.224.0"),
            new ClasslessInterDomainRouting("/20","255.255.240.0" ),
            new ClasslessInterDomainRouting("/21","255.255.248.0"),
            new ClasslessInterDomainRouting("/22","255.255.252.0"),
            new ClasslessInterDomainRouting("/23","255.255.254.0" ),
            new ClasslessInterDomainRouting("/24","255.255.255.0" ),
            new ClasslessInterDomainRouting("/25","255.255.255.128" ),
            new ClasslessInterDomainRouting("/26","255.255.255.192" ),
            new ClasslessInterDomainRouting("/27","255.255.255.224" ),
            new ClasslessInterDomainRouting("/28","255.255.255.240" ),
            new ClasslessInterDomainRouting("/29","255.255.255.248" ),
            new ClasslessInterDomainRouting("/30","255.255.255.252" ),
            new ClasslessInterDomainRouting("/31","255.255.255.254" ),
            new ClasslessInterDomainRouting("/32","255.255.255.255" )
        };

        List<ClasslessInterDomainRouting> CIDR_MaximumAddresses = new List<ClasslessInterDomainRouting>() {
            new ClasslessInterDomainRouting("/1",  "128.0.0.0" ),
            new ClasslessInterDomainRouting("/2","192.0.0.0" ),
            new ClasslessInterDomainRouting("/3", "224.0.0.0") ,
            new ClasslessInterDomainRouting("/4","240.0.0.0" ),
            new ClasslessInterDomainRouting("/5","248.0.0.0" ),
            new ClasslessInterDomainRouting("/6","252.0.0.0" ),
            new ClasslessInterDomainRouting("/7","254.0.0.0" ),
            new ClasslessInterDomainRouting("/8","255.0.0.0" ),
            new ClasslessInterDomainRouting("/9","255.128.0.0" ),
            new ClasslessInterDomainRouting("/10","255.192.0.0" ),
            new ClasslessInterDomainRouting("/11","255.224.0.0" ),
            new ClasslessInterDomainRouting("/12","255.240.0.0" ),
            new ClasslessInterDomainRouting("/13","255.248.0.0"),
            new ClasslessInterDomainRouting("/14","255.252.0.0" ),
            new ClasslessInterDomainRouting("/15","255.254.0.0"),
            new ClasslessInterDomainRouting("/16","255.255.0.0"),
            new ClasslessInterDomainRouting("/17","255.255.128.0"),
            new ClasslessInterDomainRouting("/18","255.255.192.0" ),
            new ClasslessInterDomainRouting("/19","255.255.224.0"),
            new ClasslessInterDomainRouting("/20","255.255.240.0" ),
            new ClasslessInterDomainRouting("/21","255.255.248.0"),
            new ClasslessInterDomainRouting("/22","255.255.252.0"),
            new ClasslessInterDomainRouting("/23","255.255.254.0" ),
            new ClasslessInterDomainRouting("/24","255.255.255.0" ),
            new ClasslessInterDomainRouting("/25","255.255.255.128" ),
            new ClasslessInterDomainRouting("/26","255.255.255.192" ),
            new ClasslessInterDomainRouting("/27","255.255.255.224" ),
            new ClasslessInterDomainRouting("/28","255.255.255.240" ),
            new ClasslessInterDomainRouting("/29","255.255.255.248" ),
            new ClasslessInterDomainRouting("/30","255.255.255.252" ),
            new ClasslessInterDomainRouting("/31","255.255.255.254" ),
            new ClasslessInterDomainRouting("/32","255.255.255.255" )
        };
        #endregion

        bool IsValidIP = false;
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            Debug.WriteLine("CONSTRUCTOR");
            
            DeviceNetworkInfo();
            
            /// detect when the availability of the network changes
            DeviceNetworkInformation.NetworkAvailabilityChanged += new EventHandler<NetworkNotificationEventArgs>(DeviceNetworkInformation_NetworkAvailabilityChanged);

            IsSelectionChangedEnabled = false; // workaround for selectiong changing when starting app
            lpNotation.ItemsSource = CIDR_Notation;
            lpDecimal.ItemsSource = CIDR_Decimal;
           // lpBinary.ItemsSource = CIDR_Binary;
            lpMaximumSubnets.ItemsSource = CIDR_MaximumSubnets;
            lpMaximumAddresses.ItemsSource = CIDR_MaximumAddresses;
            IsSelectionChangedEnabled = true;
            // SettingsRestore();
            //tbCellularMobileOperator.Text = "Mobile Operator: " + CellularMobileOperator.ToString();
            //tbIsCellularDataEnabled.Text = "Data Available: " + IsCellularDataEnabled.ToString();
            //tbIsCellularDataRoamingEnabled.Text = "Roaming Available: " + IsCellularDataRoamingEnabled.ToString();
            //tbIsNetworkAvailable.Text = "Network Available: " + IsNetworkAvailable.ToString();
            //tbIsWifiEnabled.Text = "Wifi Available: " + IsWifiEnabled.ToString();
            if (App.IsTrial == false)
            {
                adControl1.Visibility = System.Windows.Visibility.Collapsed;
            }
        }


        private void StateSave()
        {
            // this.State is a dictionary, where one can store page state information
            //if (IsBinaryListPickerClosed == true && IsDecimalListPickerClosed == true && IsMaximumAddressesListPickerClosed == true && IsMaximumSubnetsListPickerClosed == true)
            //{
            this.State["IPAddress"] = tbIPAddress.Text;
            this.State["Netmask"] = lpDecimal.SelectedIndex;
            this.State["MaskBits"] = lpNotation.SelectedIndex;
            this.State["MaxSubnets"] = lpMaximumSubnets.SelectedIndex;
            this.State["MaxAddresses"] = lpMaximumAddresses.SelectedIndex;
            //}
        }

        private void StateRestore()
        {
            // this.State is a dictionary, where one can store page state information
            //if (IsBinaryListPickerClosed == true && IsDecimalListPickerClosed == true && IsMaximumAddressesListPickerClosed == true && IsMaximumSubnetsListPickerClosed == true)
            //{
            if (State.ContainsKey("IPAddress"))
                tbIPAddress.Text = (string)this.State["IPAddress"];

            if (State.ContainsKey("Netmask"))
                lpDecimal.SelectedIndex = (int)this.State["Netmask"];

            if (State.ContainsKey("MaskBits"))
                lpNotation.SelectedIndex = (int)this.State["MaskBits"];

            if (State.ContainsKey("MaxSubnets"))
                lpMaximumSubnets.SelectedIndex = (int)this.State["MaxSubnets"];

            if (State.ContainsKey("MaxAddresses"))
                lpMaximumAddresses.SelectedIndex = (int)this.State["MaxAddresses"];
            //}
        }

        /// <summary>
        /// Called when network changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DeviceNetworkInformation_NetworkAvailabilityChanged(object sender, NetworkNotificationEventArgs e)
        {
            DeviceNetworkInfo();
        }


        public void DeviceNetworkInfo()
        {
            //ConnectionSettingsTask connectionSettings = new ConnectionSettingsTask();
            //connectionSettings.ConnectionSettingsType.ToString();


            //DeviceNetworkInformation.ResolveHostNameAsync(
            //new DnsEndPoint("microsoft.com", 80),
            //new NameResolutionCallback(nrr =>
            //{
            //    NetworkInterfaceInfo info = nrr.NetworkInterface;
            //    if (info != null)
            //    {
            //        switch (info.InterfaceType)
            //        {
            //            case NetworkInterfaceType.Ethernet:
            //                // USB connected
            //                break;
            //            case NetworkInterfaceType.MobileBroadbandCdma:
            //            case NetworkInterfaceType.MobileBroadbandGsm:
            //                switch (info.InterfaceSubtype)
            //                {
            //                    case NetworkInterfaceSubType.Cellular_3G:
            //                    case NetworkInterfaceSubType.Cellular_EVDO:
            //                    case NetworkInterfaceSubType.Cellular_EVDV:
            //                    case NetworkInterfaceSubType.Cellular_HSPA:
            //                        // 3g
            //                        break;
            //                    default:
            //                        break;
            //                }
            //                break;
            //            case NetworkInterfaceType.Wireless80211:
            //                // WIFI
            //                break;
            //            default:
            //                break;
            //        }
            //    }
            //}), null);
            
            NetworkInterfaceList networkInterfaceList = new NetworkInterfaceList();
            foreach (NetworkInterfaceInfo networkInterfaceInfo in networkInterfaceList)
            {
                //Debug.WriteLine(networkInterfaceInfo.InterfaceName.ToString());
                //Debug.WriteLine(networkInterfaceInfo.Bandwidth.ToString());
                //Debug.WriteLine(networkInterfaceInfo.Characteristics.ToString());
                //Debug.WriteLine(networkInterfaceInfo.Description.ToString());
                //Debug.WriteLine(networkInterfaceInfo.InterfaceState.ToString());
                //Debug.WriteLine(networkInterfaceInfo.InterfaceSubtype.ToString());
                //Debug.WriteLine(networkInterfaceInfo.InterfaceType.ToString());
                //Debug.WriteLine(networkInterfaceInfo.ToString());
            }


            //NetworkInterfaceInfo NetworkInterface = App.Current.Resources;
            //int Bandwidth = NetworkInterface.Bandwidth;
            //NetworkCharacteristics mNetworkCharacteristics = NetworkInterface.Characteristics;
            //string Description = NetworkInterface.Description;
            //string InterfaceName = NetworkInterface.InterfaceName;

            //Debug.WriteLine(NetworkInterface.ToString());
            //Debug.WriteLine(Bandwidth);
            //Debug.WriteLine(mNetworkCharacteristics.ToString());
            //Debug.WriteLine(Description.ToString());
            //Debug.WriteLine(InterfaceName.ToString());

            // Microsoft.Phone.Net.NetworkInformation
             CellularMobileOperator = Microsoft.Phone.Net.NetworkInformation.DeviceNetworkInformation.CellularMobileOperator;
             IsCellularDataEnabled = DeviceNetworkInformation.IsCellularDataEnabled;
             IsCellularDataRoamingEnabled = DeviceNetworkInformation.IsCellularDataRoamingEnabled;
             IsNetworkAvailable = DeviceNetworkInformation.IsNetworkAvailable;
             IsWifiEnabled = DeviceNetworkInformation.IsWiFiEnabled;
        }


        bool IsBinaryListPickerClosed = true;
        private void lpBinary_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            // Fixes second listpicker in a scroll view
            // http://forums.create.msdn.com/forums/t/90983.aspx
            // http://forums.create.msdn.com/forums/t/90983.aspx
            ListPicker lp = (ListPicker)sender;

            if (IsBinaryListPickerClosed)
            {
                if (lp.ListPickerMode == ListPickerMode.Normal)
                    lp.Open();
                // lp.ListPickerMode = ListPickerMode.Expanded;

                IsBinaryListPickerClosed = false;
            }
            else
            {
                IsBinaryListPickerClosed = true;
            }
        }


        bool IsDecimalListPickerClosed = true;
        private void lpDecimal_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            // Fixes second listpicker in a scroll view
            // http://forums.create.msdn.com/forums/t/90983.aspx
            // http://forums.create.msdn.com/forums/t/90983.aspx
            ListPicker lp = (ListPicker)sender;

            if (IsDecimalListPickerClosed)
            {
                if (lp.ListPickerMode == ListPickerMode.Normal)
                    lp.Open();
                // lp.ListPickerMode = ListPickerMode.Expanded;

                IsDecimalListPickerClosed = false;
            }
            else
            {
                IsDecimalListPickerClosed = true;
            }
        }


        bool IsMaximumSubnetsListPickerClosed = true;
        private void lpMaximumSubnets_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            // Fixes second listpicker in a scroll view
            // http://forums.create.msdn.com/forums/t/90983.aspx
            // http://forums.create.msdn.com/forums/t/90983.aspx
            ListPicker lp = (ListPicker)sender;

            if (IsMaximumSubnetsListPickerClosed)
            {
                if (lp.ListPickerMode == ListPickerMode.Normal)
                    lp.Open();
                // lp.ListPickerMode = ListPickerMode.Expanded;

                IsMaximumSubnetsListPickerClosed = false;
            }
            else
            {
                IsMaximumSubnetsListPickerClosed = true;
            }
        }




        bool IsMaximumAddressesListPickerClosed = true;
        private void lpMaximumAddresses_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            // Fixes second listpicker in a scroll view
            // http://forums.create.msdn.com/forums/t/90983.aspx
            // http://forums.create.msdn.com/forums/t/90983.aspx
            ListPicker lp = (ListPicker)sender;

            if (IsMaximumAddressesListPickerClosed)
            {
                if (lp.ListPickerMode == ListPickerMode.Normal)
                    lp.Open();
                // lp.ListPickerMode = ListPickerMode.Expanded;

                IsMaximumAddressesListPickerClosed = false;
            }
            else
            {
                IsMaximumAddressesListPickerClosed = true;
            }
        }



        bool IsNotationListPickerClosed = true;
        private void lpNotation_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            // Fixes second listpicker in a scroll view
            // http://forums.create.msdn.com/forums/t/90983.aspx
            // http://forums.create.msdn.com/forums/t/90983.aspx
            ListPicker lp = (ListPicker)sender;

            if (IsNotationListPickerClosed)
            {
                if (lp.ListPickerMode == ListPickerMode.Normal)
                    lp.Open();
                // lp.ListPickerMode = ListPickerMode.Expanded;

                IsNotationListPickerClosed = false;
            }
            else
            {
                IsNotationListPickerClosed = true;
            }
        }

  


        public void UpdateInfo()
        {
            IPAddress ipAddress = IPAddress.Parse(tbIPAddress.Text);

            //Debug.WriteLine(IPAddress.Parse(tbIPAddress.Text).ToString());
            //Debug.WriteLine(ipAddress.Address.ToString());
            //Debug.WriteLine(ipAddress.AddressFamily);
            //Debug.WriteLine(ipAddress.IsIPv6LinkLocal);
            //Debug.WriteLine(ipAddress.IsIPv6Multicast);
            //Debug.WriteLine(ipAddress.IsIPv6SiteLocal);
            ////Debug.WriteLine(ipAddress.ScopeId);
            //Debug.WriteLine(ipAddress.ToString());
            //Debug.WriteLine(ipAddress.GetAddressBytes());
            foreach (byte b in ipAddress.GetAddressBytes())
            {
             //   Debug.WriteLine(b.ToString());
            }

            foreach (byte b in ipAddress.GetAddressBytes())
            {
                int Base = 2;
                string val = Convert.ToString(b, Base);
          //      Debug.WriteLine(Convert.ToString(b, Base));
            //    Debug.WriteLine(val.PadLeft(8, '0'));

                //Debug.WriteLine(((ClasslessInterDomainRouting)lpDecimal.SelectedItem).ToBinary32());


                //To Binary from Integer
                //int startVal = 7; 
                //int base = 2; 
                //string binary = Convert.ToString(startVal, base); 

                ////To Integer From Binary
                //string binary = "111";
                //int base = 2; 
                //int integer = Convert.ToInt32(binary, base);
            }

            CalculateCIDR();
            
        }

        public void CalculateCIDR()
        {
            if(IsValidIP)
            if ((lpNotation.Items.Count > 0) && (lpDecimal.Items.Count > 0) && (lpMaximumAddresses.Items.Count > 0) && (lpMaximumSubnets.Items.Count > 0) )
            {

                ClasslessInterDomainRouting CIDR = new ClasslessInterDomainRouting(tbIPAddress.Text);
                //Debug.WriteLine("CIDR binary " + CIDR.ToBinary32());
                //Debug.WriteLine("CIDR first " + CIDR.FirstOctetBinary);
                //Debug.WriteLine("CIDR second " + CIDR.SecondOctetBinary);
                //Debug.WriteLine("CIDR third " + CIDR.ThirdOctetBinary);
                //Debug.WriteLine("CIDR fourth " + CIDR.FourthOctetBinary);
                // we want to compare
         //       Debug.WriteLine(((ClasslessInterDomainRouting)lpDecimal.SelectedItem).ToBinary32());


           //     Debug.WriteLine(Convert.ToInt32(CIDR.FirstOctetDecimal) & Convert.ToInt32( ( (ClasslessInterDomainRouting)lpDecimal.SelectedItem).FirstOctetDecimal) );
                int bitwise1 = Convert.ToInt32(CIDR.FirstOctetDecimal) & Convert.ToInt32(((ClasslessInterDomainRouting)lpDecimal.SelectedItem).FirstOctetDecimal);
            

             //   Debug.WriteLine(Convert.ToInt32(CIDR.SecondOctetDecimal) & Convert.ToInt32(((ClasslessInterDomainRouting)lpDecimal.SelectedItem).SecondOctetDecimal));
                int bitwise2 = Convert.ToInt32(CIDR.SecondOctetDecimal) & Convert.ToInt32(((ClasslessInterDomainRouting)lpDecimal.SelectedItem).SecondOctetDecimal);

               // Debug.WriteLine(Convert.ToInt32(CIDR.ThirdOctetDecimal) & Convert.ToInt32(((ClasslessInterDomainRouting)lpDecimal.SelectedItem).ThirdOctetDecimal));
                int bitwise3 = Convert.ToInt32(CIDR.ThirdOctetDecimal) & Convert.ToInt32(((ClasslessInterDomainRouting)lpDecimal.SelectedItem).ThirdOctetDecimal);
                //Debug.WriteLine(Convert.ToInt32(CIDR.FourthOctetDecimal) & Convert.ToInt32(((ClasslessInterDomainRouting)lpDecimal.SelectedItem).FourthOctetDecimal));
                int bitwise4 = Convert.ToInt32(CIDR.FourthOctetDecimal) & Convert.ToInt32(((ClasslessInterDomainRouting)lpDecimal.SelectedItem).FourthOctetDecimal);

                //Debug.WriteLine("bitwised = " + bitwise1 + "." + bitwise2 + "." + bitwise3 + "." + bitwise4);
                //Debug.WriteLine("start range = " + bitwise1 + "." + bitwise2 + "." + bitwise3 + "." + bitwise4);

                int endRange1 = bitwise1 + Convert.ToInt32(((ClasslessInterDomainRouting)lpDecimal.SelectedItem).FirstOctetWildcard);
                int endRange2 = bitwise2 + Convert.ToInt32(((ClasslessInterDomainRouting)lpDecimal.SelectedItem).SecondOctetWildcard);
                int endRange3 = bitwise3 + Convert.ToInt32(((ClasslessInterDomainRouting)lpDecimal.SelectedItem).ThirdOctetWildcard);
                int endRange4 = bitwise4 + Convert.ToInt32(((ClasslessInterDomainRouting)lpDecimal.SelectedItem).FourthOctetWildcard);
                string wildcard = ((ClasslessInterDomainRouting)lpDecimal.SelectedItem).Wildcard;
                //Debug.WriteLine("end range = " + endRange1 + "." + endRange2 + "." + endRange3 + "." + endRange4); // add the wildcard.  wildcard + bitwised


                tbWildcard.Text =  wildcard;
                tbBroadcastAddress.Text = endRange1 + "." + endRange2 + "." + endRange3 + "." + endRange4;
                tbNetworkAddress.Text = bitwise1 + "." + bitwise2 + "." + bitwise3 + "." + bitwise4;
                tbFirstHost.Text = bitwise1 + "." + bitwise2 + "." + bitwise3 + "." + (bitwise4); // fix
                tbLastHost.Text = endRange1 + "." + endRange2 + "." + endRange3 + "." + (endRange4); // fix


                tbBinaryIP.Text = CIDR.FirstOctetBinary + "." + CIDR.SecondOctetBinary + "." + CIDR.ThirdOctetBinary + "." + CIDR.FourthOctetBinary;
                tbBinaryNetmask.Text = ((ClasslessInterDomainRouting)lpDecimal.SelectedItem).FirstOctetBinary + "." + 
                                                        ((ClasslessInterDomainRouting)lpDecimal.SelectedItem).SecondOctetBinary + "."
                                                        + ((ClasslessInterDomainRouting)lpDecimal.SelectedItem).ThirdOctetBinary + "."
                                                        + ((ClasslessInterDomainRouting)lpDecimal.SelectedItem).FourthOctetBinary;

                tbBinaryWildcard.Text = ((ClasslessInterDomainRouting)lpDecimal.SelectedItem).WildcardBinary;
                tbBinaryBroadcast.Text =  ToBinary32(endRange1) + "." + ToBinary32(endRange2) + "." + ToBinary32(endRange3) + "." + ToBinary32(endRange4);
                tbBinaryFirstHost.Text =  ToBinary32(bitwise1) + "." + ToBinary32(bitwise2) + "." + ToBinary32(bitwise3) + "." + ToBinary32(bitwise4); // fix
                tbBinaryLastHost.Text = ToBinary32(endRange1) + "." + ToBinary32(endRange2) + "." + ToBinary32(endRange3) + "." + ToBinary32(endRange4); // fix
                //tbHostCount.Text = "Host count " + ((ClasslessInterDomainRouting)lpDecimal.SelectedItem).MaximumAddresses.ToString();

                //tbMaximumAddresses.Text = "Maximum Addresses " + ((ClasslessInterDomainRouting)lpDecimal.SelectedItem).MaximumAddresses.ToString();
                //tbMaximumSubnets.Text = "Maximum Subnets " + ((ClasslessInterDomainRouting)lpDecimal.SelectedItem).MaximumSubnets.ToString();

               // ((ClasslessInterDomainRouting)lpDecimal.SelectedItem).
            } 
        }

        private void lpNotation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = (sender as ListPicker).SelectedIndex;
            //int index = lpNotation.SelectedIndex;
            Debug.WriteLine("selected index notation=" + index);
            //if(lpDecimal.Items.Count >0 ) lpDecimal.SelectedIndex = index;
            //if (lpMaximumAddresses.Items.Count > 0) lpMaximumAddresses.SelectedIndex = index;
            //if (lpMaximumSubnets.Items.Count > 0) lpMaximumSubnets.SelectedIndex = index;
            SaveSelectedIndex(index);
            CalculateCIDR();
        }

        private void lpDecimal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lpDecimal.SelectedIndex;
            Debug.WriteLine("selected index decimal=" + index);
      //      if (lpNotation.Items.Count > 0) lpNotation.SelectedIndex = index;
      //      if (lpMaximumAddresses.Items.Count > 0) lpMaximumAddresses.SelectedIndex = index;
      //      if (lpMaximumSubnets.Items.Count > 0) lpMaximumSubnets.SelectedIndex = index;
            SaveSelectedIndex(index);
            CalculateCIDR();
        }

        private void lpMaximumSubnets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lpMaximumSubnets.SelectedIndex;
            Debug.WriteLine("selected index subnet=" + index);
     //       if (lpNotation.Items.Count > 0) lpNotation.SelectedIndex = index;
       //     if (lpMaximumAddresses.Items.Count > 0) lpMaximumAddresses.SelectedIndex = index;
         //   if (lpDecimal.Items.Count > 0) lpDecimal.SelectedIndex = index;


            SaveSelectedIndex(index);
            CalculateCIDR();
        }


        private void SaveSelectedIndex(int index)
        {
            isolatedStorage = IsolatedStorageSettings.ApplicationSettings;

            if (IsSelectionChangedEnabled == true)
            {
                Debug.WriteLine("saving selected index=" + index);
                //                if (lpDecimal.Items.Count > 0)
                isolatedStorage["Netmask"] = index;
                //              if (lpNotation.Items.Count > 0)
                isolatedStorage["MaskBits"] = index;
                //            if (lpMaximumSubnets.Items.Count > 0)
                isolatedStorage["MaxSubnets"] = index;
                //          if (lpMaximumAddresses.Items.Count > 0)
                isolatedStorage["MaxAddresses"] = index;
                isolatedStorage.Save();
            }
            else
            {
                Debug.WriteLine("skipping save selected index=" + index);
            }
        }


        private void lpMaximumAddresses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lpMaximumAddresses.SelectedIndex;
            Debug.WriteLine("selected index addresses=" + index);
            //if (lpNotation.Items.Count > 0) lpNotation.SelectedIndex = index;
            //if (lpMaximumSubnets.Items.Count > 0) lpMaximumSubnets.SelectedIndex = index;
            //if (lpDecimal.Items.Count > 0) lpDecimal.SelectedIndex = index;
            SaveSelectedIndex(index);
            
            CalculateCIDR();
        }

        public string ToBinary32(int Decimal)
        {
            return Convert.ToString(Decimal, 2).PadLeft(8, '0');
        }



        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
         //   StateRestore();
            Debug.WriteLine("ON NAVIGATED TO");
            // PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            SettingsRestore();
            base.OnNavigatedTo(e);
        }


        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
         //   StateSave();
            Debug.WriteLine("ON NAVIGATED FROM");
           // PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Enabled;
            SettingsSave();
            base.OnNavigatedFrom(e);
        }



        private void SettingsRestore()
        {
            isolatedStorage = IsolatedStorageSettings.ApplicationSettings;
            if (isolatedStorage.Contains("IPAddress"))
            {
                Debug.WriteLine("restore ipaddress=" + (string)this.isolatedStorage["IPAddress"]);
                tbIPAddress.Text = (string)this.isolatedStorage["IPAddress"];
            }

            if (isolatedStorage.Contains("Netmask"))
            {
                Debug.WriteLine("restore netmask=" + (int)this.isolatedStorage["Netmask"]);
                lpDecimal.SelectedIndex = (int)this.isolatedStorage["Netmask"];
            }

            if (isolatedStorage.Contains("MaskBits"))
            {
                Debug.WriteLine("restore MaskBits=" + (int)this.isolatedStorage["MaskBits"]);
                lpNotation.SelectedIndex = (int)this.isolatedStorage["MaskBits"];
            }

            if (isolatedStorage.Contains("MaxSubnets"))
            {
                Debug.WriteLine("restore MaxSubnets=" + (int)this.isolatedStorage["MaxSubnets"]);
                lpMaximumSubnets.SelectedIndex = (int)this.isolatedStorage["MaxSubnets"];
            }
            if (isolatedStorage.Contains("MaxAddresses"))
            {
                Debug.WriteLine("restore MaxAddresses=" + (int)this.isolatedStorage["MaxAddresses"]);
                lpMaximumAddresses.SelectedIndex = (int)this.isolatedStorage["MaxAddresses"];
            }
        }

        private void SettingsSave()
        {
            isolatedStorage = IsolatedStorageSettings.ApplicationSettings;
            if (tbIPAddress.Text != null)
            {
                Debug.WriteLine("saving ipaddress=" + tbIPAddress.Text.ToString());
                isolatedStorage["IPAddress"] = tbIPAddress.Text.ToString();
            }

            //if (lpDecimal.Items.Count > 0)
            //{
            //    Debug.WriteLine("saving netmask=" + lpDecimal.SelectedIndex);
            //    isolatedStorage["Netmask"] = lpDecimal.SelectedIndex; 
            //}

            //if (lpNotation.Items.Count > 0)
            //{
            //    Debug.WriteLine("saving MaskBits=" + lpNotation.SelectedIndex);
            //    isolatedStorage["MaskBits"] = lpNotation.SelectedIndex;
            //}

            //if (lpMaximumSubnets.Items.Count > 0)
            //{
            //    Debug.WriteLine("saving MaxSubnets=" + lpMaximumSubnets.SelectedIndex);
            //    isolatedStorage["MaxSubnets"] = lpMaximumSubnets.SelectedIndex; 
            //}

            //if (lpMaximumAddresses.Items.Count > 0)
            //{
            //    Debug.WriteLine("saving MaxAddresses=" + lpMaximumAddresses.SelectedIndex);
            //    isolatedStorage["MaxAddresses"] = lpMaximumAddresses.SelectedIndex;
            //}
            isolatedStorage.Save();
        }


        private void About_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.RelativeOrAbsolute));
        }



        private void tbIPAddress_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (tbIPAddress.Text.Length > 15)
            {
                tbIPAddress.Text = tbIPAddress.Text.Substring(0, 15);
                tbIPAddress.Select(15, 0);
            }
            else
            {
                //create our match pattern
                string pattern = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";
                //create our Regular Expression object
                Regex check = new Regex(pattern);
                //boolean variable to hold the status
                bool valid = false;
                //check to make sure an ip address was provided
                if (tbIPAddress.Text == "")
                {
                    //no address provided so return false
                    valid = false;
                }
                else
                {
                    //address provided so use the IsMatch Method
                    //of the Regular Expression object
                    valid = check.IsMatch(tbIPAddress.Text, 0);
                    
                }

                if (valid == true)
                {
                //    Debug.WriteLine("yayayya");
                    IsValidIP = true;
                    CalculateCIDR();
                }
                else
                {
                 //   Debug.WriteLine("nonono");
                    IsValidIP = false;
                }
            }
        }

    }
}