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
    public partial class CreateAccount : Form
    {
        private Rent_A_RideEntities db;
        public CreateAccount()
        {
            InitializeComponent();
            db= new Rent_A_RideEntities();
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = tbUsername.Text;
                int age;
                string city = tbCity.Text;
                string password = tbPassword.Text;
                string reEnterPasssword = tbReEnterPassword.Text;

                bool isValid = true;
                var errorMessage = "";

                if (!int.TryParse(tbAge.Text, out age))
                {
                    errorMessage = "Invalid age\n";
                }
                if (string.IsNullOrEmpty(userName) ||
                    string.IsNullOrEmpty(city))
                {
                    isValid = false;
                    errorMessage += "Please fill empty data fields \n";
                }
                if (password == string.Empty || password != reEnterPasssword)
                {
                    isValid = false;
                    errorMessage += "EnterPassword Correctly \n";
                }
                if (isValid)
                {
                    var newUser = new User
                    {
                        username = userName,
                        isActive = true,
                        Age = age,
                        City = city,
                        password = Utils.HashPassword(password),
                    };

                    db.Users.Add(newUser);
                    db.SaveChanges();
                    MessageBox.Show("New User Added Successfully!");
                    Close();
                }
                else
                {
                    MessageBox.Show(errorMessage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
