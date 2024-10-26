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
    public partial class ManageVehicleListing : Form
    {
        private readonly Rent_A_RideEntities db;
        public ManageVehicleListing()
        {
            InitializeComponent();
            db= new Rent_A_RideEntities();
        }

        private void btnAddNewCar_Click(object sender, EventArgs e)
        {
            var addEditVehicle = new AddEditVehicle(this);
            addEditVehicle.MdiParent = this.MdiParent;
            addEditVehicle.Show();
        }

        private void btnEditCar_Click(object sender, EventArgs e)
        {
            try
            {
                // get selected id
                var id = (int)gvVehicleList.SelectedRows[0].Cells["Id"].Value;
                // get record of that id
                var car = db.CarTypesRecords.FirstOrDefault(q => q.Id == id);

                var addEditVehicle = new AddEditVehicle(car, this);
                addEditVehicle.MdiParent = this.MdiParent;
                addEditVehicle.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnDeleteCar_Click(object sender, EventArgs e)
        {
            try
            {
                // get selected id
                var id = (int)gvVehicleList.SelectedRows[0].Cells["Id"].Value;
                // get record of that id
                var car = db.CarTypesRecords.FirstOrDefault(q => q.Id == id);
                DialogResult dr = MessageBox.Show("Are you sure you want to delete the record? ",
                    "Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (dr == DialogResult.Yes)
                {
                    db.CarTypesRecords.Remove(car);
                    db.SaveChanges();
                    gvVehicleList.Refresh();
                    PopulateGrid();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ManageVehicleListing_Load(object sender, EventArgs e)
        {
            PopulateGrid();
        }

        public void PopulateGrid()
        {
            var typesOfCarTable = db.CarTypesRecords.Select(q => new
            {
                ID = q.Id,
                MODEL = q.Model,
                MAKE = q.Make,
                VIN = q.VIN,
                YEAR = q.Year,
                LicensePlateNumber = q.LicensePlateNum,

            }).ToList();

            gvVehicleList.DataSource = typesOfCarTable;
            gvVehicleList.Columns[0].Visible = false;
            gvVehicleList.Columns[5].HeaderText = "Licence Plate Number";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void gvVehicleList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
