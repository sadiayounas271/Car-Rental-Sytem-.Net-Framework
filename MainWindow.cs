using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RentARide
{
    public partial class MainWindow : Form
    {
        private Login _login;
        public User _user;
      //  public string _roleName;
        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(Login login, User user= null)
        {
            InitializeComponent();
            _login = login;
            _user = user;
           // _roleName = user.UserRoles.FirstOrDefault().Role.shortname;
        }
        private void addRentalRecordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Utils.FormIsOpen("AddEditRentalRecord"))
            {
                var addRentalRecord = new AddEditRentalRecord();
                addRentalRecord.MdiParent = this;
                addRentalRecord.Show();
            }
        }

        private void manageVehicleListingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!Utils.FormIsOpen("ManageVehicleListing"))
            {
                var manageVehicleListing = new ManageVehicleListing();
                manageVehicleListing.MdiParent = this;
                manageVehicleListing.Show();
            }
        }

        private void viewArchiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Utils.FormIsOpen("ManageVehicleRecord"))
            {
                var manageRentalRecord = new ManageRentalRecord(); ;
                manageRentalRecord.MdiParent = this;
                manageRentalRecord.Show();
            }

        }
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            _login.Close();
        }
    }
}
