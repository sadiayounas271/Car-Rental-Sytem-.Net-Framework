using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace RentARide
{
    public partial class AddEditVehicle : Form
    {
        private readonly Rent_A_RideEntities db;
        private bool isEditMode;
        private ManageVehicleListing _manageVehicleListing;
        public AddEditVehicle(ManageVehicleListing manageVehicleListing = null)
        {
            InitializeComponent();
            db= new Rent_A_RideEntities();
            isEditMode = false;
            lblText.Text = "Add New Vehicle";
            _manageVehicleListing= manageVehicleListing;
        }
        public AddEditVehicle(CarTypesRecord carToEdit, ManageVehicleListing manageVehicleListing=null)
        {
            isEditMode = true;
            InitializeComponent();
            lblText.Text = "Edit Vehicle";
            _manageVehicleListing = manageVehicleListing;
            db = new Rent_A_RideEntities();
            PopulateFields(carToEdit);
        }
        private int carIdToEdit;
        private void PopulateFields(CarTypesRecord car)
        {
            carIdToEdit = car.Id;
            tbMake.Text = car.Make;
            tbModel.Text = car.Model;
            tbVIN.Text = car.VIN;
            tbYear.Text = car.Year.ToString();
            tbLicensePlateNum.Text = car.LicensePlateNum.ToString();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(isEditMode==false)
            {
                var newCar = new CarTypesRecord
                {
                    Make = tbMake.Text,
                    Model =tbModel.Text,
                    VIN=tbVIN.Text,
                    Year=int.Parse(tbYear.Text),
                    LicensePlateNum= int.Parse(tbLicensePlateNum.Text),

                };
                db.CarTypesRecords.Add(newCar);
                db.SaveChanges();
                //after saving in db
                _manageVehicleListing.PopulateGrid();
                MessageBox.Show("New Car Data Added");
                Close();
            }
            else
            {   
                var car = db.CarTypesRecords.FirstOrDefault(q => q.Id == carIdToEdit);
                car.Make = tbMake.Text;
                car.Model = tbModel.Text;
                car.VIN = tbVIN.Text;
                car.Year = int.Parse(tbYear.Text);
                car.LicensePlateNum = int.Parse(tbLicensePlateNum.Text);
                db.SaveChanges();

                MessageBox.Show("Car Data Edited");
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddEditVehicle_Load(object sender, EventArgs e)
        {

        }
    }
}
