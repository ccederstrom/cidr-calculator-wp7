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
using Microsoft.Phone.Tasks;
using Coding4Fun.Phone.Controls.Data;

namespace cidr_calculator
{
    public partial class AboutPage : PhoneApplicationPage
    {
        MarketplaceReviewTask _marketplaceReview = new MarketplaceReviewTask();
        MarketplaceSearchTask _marketplaceSearch = new MarketplaceSearchTask();
        EmailComposeTask _emailComposeTask = new EmailComposeTask();

        public AboutPage()
        {
            InitializeComponent();
            txtHelp.Text = "Your suggestions and feedback is greatly appreciated.";
            txtAppName.Text = PhoneHelper.GetAppAttribute("Title") + " by " + PhoneHelper.GetAppAttribute("Author");
            txtVersion.Text = "version " + PhoneHelper.GetAppAttribute("Version");
            txtDescription.Text = PhoneHelper.GetAppAttribute("Description");

            //if (App.IsTrial == false)
            //{
            //    adControl1.Visibility = System.Windows.Visibility.Collapsed;
            //}
        }

        private void btnMarketplace_Click(object sender, RoutedEventArgs e)
        {
            _marketplaceSearch.SearchTerms = "Chris Cederstrom";
            _marketplaceSearch.Show();
        }

        private void btnReview_Click(object sender, RoutedEventArgs e)
        {
            _marketplaceReview.Show();
        }

        private void btnContact_Click(object sender, RoutedEventArgs e)
        {
            _emailComposeTask.To = "cederstrom@acm.org";
            _emailComposeTask.Subject = "CIDR Calculator Feedback v" + PhoneHelper.GetAppAttribute("Version");
            _emailComposeTask.Show();
        }
    }
}