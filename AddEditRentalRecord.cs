using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace RentARide
{
    public partial class AddEditRentalRecord : Form
    {
        private bool isEditMode;
        private readonly Rent_A_RideEntities db;
        public AddEditRentalRecord()
        {
            InitializeComponent();
            db = new Rent_A_RideEntities();
            isEditMode = false;
            lblText.Text = "RENT A RIDE SYTEM (Add New Record)";
        }
        public AddEditRentalRecord(CarRentalRecord carRentalRecord)
        {
            InitializeComponent();
            lblText.Text = "RENT A RIDE SYTEM (Edit Record)";
           
            if (carRentalRecord == null)
            {
                MessageBox.Show("Please ensure that you selected a valid record to edit");
                Close();
            }
            else
            {
                db = new Rent_A_RideEntities();
                isEditMode = true;
                PopulateRecord(carRentalRecord);
            }
        }
        int recordIdToEdit;
        void PopulateRecord(CarRentalRecord recordToEdit)
        {
            recordIdToEdit = recordToEdit.Id;
             tbCustomerName.Text= recordToEdit.CustomerName;
            tbCost.Text = recordToEdit.Cost.ToString();
            dtRented.Value= recordToEdit.DateRented.Value;
            dtReturn.Value = recordToEdit.DateReturn.Value;
           cbCarType.Text= recordToEdit.CarTypeId.ToString();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string _customerName = tbCustomerName.Text;
                double _cost;
                var _dateRented = dtRented.Value;
                var _dateReturn = dtReturn.Value;
                var _carType = cbCarType.Text;

                bool isValid = true;
                var errorMessage = "";
               
                if(!double.TryParse(tbCost.Text, out _cost))
                {
                    isValid = false;
                    errorMessage = "Enter Valid Cost\n";
                }
               
                if (string.IsNullOrWhiteSpace(_customerName) ||
                    string.IsNullOrWhiteSpace(_carType))
                {
                    isValid = false;
                    errorMessage += "Please enter missing data\n";
                }
               
                if (_dateRented >= _dateReturn)
                {
                    isValid = false;
                    errorMessage += "Enter valid date\n";
                }

                if (isValid)
                {
                    if(isEditMode)
                    {
                        var carRentalRecord = db.CarRentalRecords.FirstOrDefault(q => q.Id == recordIdToEdit);
                        carRentalRecord.CustomerName = _customerName;
                        carRentalRecord.Cost = (decimal)_cost;
                        carRentalRecord.CarTypeId = cbCarType.SelectedIndex+1;
                        carRentalRecord.DateRented = _dateRented;
                        carRentalRecord.DateReturn = _dateReturn;

                        db.SaveChanges();

                        MessageBox.Show("Car Data Edited");
                        Close();
                    }
                    else
                    {
                        var carRentalRecord = new CarRentalRecord();
                        carRentalRecord.CustomerName = _customerName;
                        carRentalRecord.Cost = (decimal)_cost;
                        var idd = (int)cbCarType.SelectedIndex;
                        carRentalRecord.CarTypeId = idd + 1;          
                        carRentalRecord.DateRented = _dateRented;
                        carRentalRecord.DateReturn = _dateReturn;

                        db.CarRentalRecords.Add(carRentalRecord);
                        db.SaveChanges();


                        MessageBox.Show($"Customer Name : {_customerName}\n" +
                            $"Cost : {_cost}\n" +
                            $"Car Type : {_carType}\n" +
                            $"Date Rented : {_dateRented}\n" +
                            $"Date Returned : {_dateReturn}\n");
                    }
                  
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
        
        private void Form1_Load(object sender, EventArgs e)
        {
            var cars=db.CarTypesRecords.ToList();
            cbCarType.DisplayMember = "Make";
            cbCarType.DataSource = cars;    
        }
    }
}
