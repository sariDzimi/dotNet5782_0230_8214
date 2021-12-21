using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BlApi;


namespace PL
{
    /// <summary>
    /// Interaction logic for Parcel.xaml
    /// </summary>
    /// 
    public partial class ParcelWindow : Window
    {
        public ParcelWindow()
        {
            InitializeComponent();
        }

        public ParcelWindow( IBL bl, Parcel parcel )
        {
            InitializeComponent();
            idParcelLabel.Text = $"{parcel.Id}";
            weightLabel.Text = $"{parcel.Weight}";
            priorityLabel.Text = $"{parcel.Pritority}";
            ScheduledLabel.Text = $"{parcel.Scheduled}";
            PickedUpLabel.Text = $"{parcel.PickedUp}";
            DeliveredLabel.Text = $"{parcel.Delivered}";
            customerAtParcelSenderLabel.Text = $"{parcel.customerAtParcelSender}";
            customerAtParcelReciverText.Text = $"{parcel.customerAtParcelReciver}";

        }

    }
}



    
        
    



        
                



                 
                          
                                         
                                                  
                                                                   
                                                                                           
                                                                                                       
                                                                                                                   
                               
                                                                                                                                           
                                                                                                                                                  
                                                                                                                                           
                                                                                                                                                
                                                                                                                                              

                                                                                                                                                                                                    

                                                                                                                                                                                                          