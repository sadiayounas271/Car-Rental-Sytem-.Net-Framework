using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RentARide
{
    public partial class Login : Form
    {
        private Rent_A_RideEntities db;
        public Login()
        {
            InitializeComponent();
            db= new Rent_A_RideEntities();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SHA256 sha= SHA256.Create();
            try
            {
                var _username=tbUsername.Text.Trim();
                var _password=tbPassword.Text;



                var hashed_Password = Utils.HashPassword(_password);
                var user = db.Users.FirstOrDefault(q => q.username == _username
                && q.password==hashed_Password && q.isActive==true);

                if (user == null)
                {
                    MessageBox.Show("Please provide valid credential");
                }
                else
                {
                    var mainWindow= new MainWindow(this, user);
                    mainWindow.Show();
                    Hide();
                }

            }
            catch(Exception)
            {
            }
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            if(!Utils.FormIsOpen("CreateAccount"))
            {
                var createAccount=  new CreateAccount();
                createAccount.Show();
                
            }
        }
    }
}
