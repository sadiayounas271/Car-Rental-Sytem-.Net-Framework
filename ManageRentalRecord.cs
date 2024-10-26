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
    public partial class ManageRentalRecord : Form
    {
        private readonly Rent_A_RideEntities db;
        public ManageRentalRecord()
        {
            InitializeComponent();
            db= new Rent_A_RideEntities();
        }

        private void ManageRentalRecord_Load(object sender, EventArgs e)
        {
         PopulateGrid();
        }
      void  PopulateGrid()
        {
            var typesOfRecord = db.CarRentalRecords.Select(q => new
            {
                Customer = q.CustomerName,
                Cost = q.Cost,
                DateIn = q.DateRented,
                DateReturn = q.DateReturn,
                ID = q.Id,
                Car = q.CarTypesRecord.Make + " " + q.CarTypesRecord.Model,

            }).ToList();

            gvRecordList.DataSource = typesOfRecord;
            gvRecordList.Columns["ID"].Visible = false;
            gvRecordList.Columns["DateIN"].HeaderText = "Date Rented";
        }
        private void btnEditRecord_Click(object sender, EventArgs e)
        {
            try
            {
                // get selected id
                var id = (int)gvRecordList.SelectedRows[0].Cells["Id"].Value;
                // get record of that id
                var record = db.CarRentalRecords.FirstOrDefault(q => q.Id == id);

                var addEditVehicle = new AddEditRentalRecord(record);
                addEditVehicle.MdiParent = this.MdiParent;
                addEditVehicle.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteRecord_Click(object sender, EventArgs e)
        {
            try
            {
                // get selected id
                var id = (int)gvRecordList.SelectedRows[0].Cells["Id"].Value;
                // get record of that id
                var record = db.CarRentalRecords.FirstOrDefault(q => q.Id == id);
                db.CarRentalRecords.Remove(record);
                db.SaveChanges();
                gvRecordList.Refresh();
                PopulateGrid();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAddRecord_Click(object sender, EventArgs e)
        {
            var addEditRentalRecord= new AddEditRentalRecord();
            addEditRentalRecord.MdiParent= this.MdiParent;
            addEditRentalRecord.Show();
        }
    }
}
