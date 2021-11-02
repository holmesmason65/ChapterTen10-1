/*
 Stopped at page 10-16. Add the form closing event
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ChapterTen10_1
{
    public partial class frmKWSales : Form
    {
        int orderNumber;
        SqlConnection KWSalesConnection;
        SqlCommand ordersCommand;
        SqlDataAdapter ordersAdapter;
        DataTable ordersTable;

        SqlCommand customersCommand;
        SqlDataAdapter customersAdapter;
        DataTable customersTable;
        CurrencyManager customersManager; 

        public frmKWSales()
        {
            InitializeComponent();
        }

        private void frmKWSales_Load(object sender, EventArgs e)
        {
            KWSalesConnection = new SqlConnection($@"Data Source=\SQLEXPRESS; AttachDbFilename=" + Application.StartupPath + "SQLKWSalesDB.mdf");
            KWSalesConnection.Open();
            ordersCommand = new SqlCommand("SELECT * FROM Orders ORDER BY OrderID");
            ordersAdapter = new SqlDataAdapter();
            ordersAdapter.SelectCommand = ordersCommand;
            ordersTable = new DataTable();
            ordersAdapter.Fill(ordersTable);

            customersCommand = new SqlCommand("SELECT * FROM Customers", KWSalesConnection);
            customersAdapter = new SqlDataAdapter();
            customersAdapter.SelectCommand = customersCommand;
            customersTable = new DataTable();
            customersAdapter.Fill(customersTable);

            txtFirstName.DataBindings.Add("Text", customersTable, "FirstName");
            txtLastName.DataBindings.Add("Text", customersTable, "LastName");
            txtAddress.DataBindings.Add("Text", customersTable, "Address");
            txtCity.DataBindings.Add("Text", customersTable, "City");
            txtState.DataBindings.Add("Text", customersTable, "State");
            txtZip.DataBindings.Add("Text", customersTable, "Zip");

            customersManager = (CurrencyManager)this.BindingContext[customersTable]; 

            orderNumber = 0;
            NewOrder(); 
        }

        private void frmKWSales_FormClosing(object sender, FormClosingEventArgs e)
        {
            KWSalesConnection.Close();
            ordersCommand.Dispose();
            ordersAdapter.Dispose();
            ordersTable.Dispose(); 
        }

        public void NewOrder() 
        {
            string IDString;
            DateTime thisDay = DateTime.Now;
            lblDate.Text = thisDay.ToShortDateString();

            orderNumber++;
            IDString = thisDay.Year.ToString().Substring(2);
            if (thisDay.Month < 10)
                IDString += "0" + thisDay.Month.ToString();
            else
                IDString += thisDay.Month.ToString();
            if (thisDay.Month < 10)
                IDString += "0" + thisDay.Month.ToString();
            else
                IDString += thisDay.Month.ToString();
            if (thisDay.Month < 10)
                IDString += "00" + thisDay.Month.ToString();
            else if (thisDay.Month < 100)
                IDString += thisDay.Month.ToString();
            else
                IDString += thisDay.Month.ToString();
            lblOrderID.Text = IDString;
        }
    }
}
