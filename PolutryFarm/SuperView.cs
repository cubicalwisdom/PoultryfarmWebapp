using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DALayer;
using VOLayer.cs;

namespace PolutryFarm
{
    public partial class SuperView : Form
    {

        //public SuperView()
        //{
        //    InitializeComponent();
        //    datagridsvc.Visible = false;
        //    datagridsvcm.Visible = false;
        //    datagridsvcs.Visible = false;
        //    datagridsvi.Visible = false;
        //    datagridsvf.Visible = false;
        //    datagridsvfs.Visible = false;
        //    datagridsvm.Visible = false;
        //    datagridsvma.Visible = false;
        //    datagridsvep.Visible = false;
        //    datagridsves.Visible = false;
        //    tsvedate.Value = DateTime.Today.AddDays(-0);
        //    tsvsdate.Value = DateTime.Today.AddDays(-10);


        //    SVcheckbox ovsvcb2 = null;
        //    SqlConnection connection = new SqlConnection("Data Source=RC;Initial Catalog=PoultryForm;Persist Security Info=True;User ID=sa;Password=8012123295r.c.");
        //    SqlCommand comm = new SqlCommand();
        //    connection.Open();
        //    comm.Connection = connection;
        //    comm.CommandType = CommandType.StoredProcedure;
        //    comm.CommandText = "ViewSVCB";
        //    SqlDataReader read = comm.ExecuteReader();
        //    ovsvcb2 = new SVcheckbox();
        //    while (read.Read())
        //    {
        //        ovsvcb2.C = Convert.ToBoolean(read["C"].ToString());
        //        ovsvcb2.CM = Convert.ToBoolean(read["CM"].ToString());
        //        ovsvcb2.CS = Convert.ToBoolean(read["CS"].ToString());
        //        ovsvcb2.F = Convert.ToBoolean(read["F"].ToString());
        //        ovsvcb2.FS = Convert.ToBoolean(read["FS"].ToString());
        //        ovsvcb2.I = Convert.ToBoolean(read["I"].ToString());
        //        ovsvcb2.M = Convert.ToBoolean(read["M"].ToString());
        //        ovsvcb2.MA = Convert.ToBoolean(read["MA"].ToString());
        //        ovsvcb2.EP = Convert.ToBoolean(read["EP"].ToString());
        //        ovsvcb2.ES = Convert.ToBoolean(read["ES"].ToString());
        //    }
        //    connection.Close();

        //    Chicks.Checked = ovsvcb2.C == true ? true : false;
        //    ChicksMortality.Checked = ovsvcb2.CM == true ? true : false;
        //    ChicksSold.Checked = ovsvcb2.CS == true ? true : false;
        //    Feed.Checked = ovsvcb2.F == true ? true : false;
        //    FeedStock.Checked = ovsvcb2.FS == true ? true : false;
        //    Infra.Checked = ovsvcb2.I == true ? true : false;
        //    Med.Checked = ovsvcb2.M == true ? true : false;
        //    MedApplied.Checked = ovsvcb2.MA == true ? true : false;
        //    EggPro.Checked = ovsvcb2.EP == true ? true : false;
        //    EggSold.Checked = ovsvcb2.ES == true ? true : false;

        //    Temp();

        //}

        //public void Temp()
        //{
        //    DateTime Endd = Convert.ToDateTime(tsvedate.Value);
        //    DateTime Startd = Convert.ToDateTime(tsvsdate.Value);

        //    if (Chicks.Checked == true)
        //    {
        //        datagridsvc.Visible = true;
        //        Bindgridchicks(Startd, Endd);
        //    }
        //    else if (Chicks.Checked == false)
        //    {
        //        datagridsvc.Visible = false;
        //    }



        //    if (ChicksMortality.Checked == true)
        //    {
        //        datagridsvcm.Visible = true;
        //        BindgridchicksMor(Startd, Endd);
        //    }
        //    else if (ChicksMortality.Checked == false)
        //    {
        //        datagridsvcm.Visible = false;
        //    }
        //    if (ChicksSold.Checked == true)
        //    {
        //        datagridsvcs.Visible = true;
        //        BindgridchicksSold(Startd, Endd);
        //    }
        //    else if (ChicksSold.Checked == false)
        //    {
        //        datagridsvcs.Visible = false;
        //    }
        //    if (Infra.Checked == true)
        //    {
        //        datagridsvi.Visible = true;
        //        Bindgridinfra(Startd, Endd);
        //    }
        //    else if (Infra.Checked == false)
        //    {
        //        datagridsvi.Visible = false;
        //    }
        //    if (Feed.Checked == true)
        //    {
        //        datagridsvf.Visible = true;
        //        Bindgridfeed(Startd, Endd);
        //    }

        //    else if (Feed.Checked == false)
        //    {
        //        datagridsvf.Visible = false;
        //    }

        //    if (FeedStock.Checked == true)
        //    {
        //        datagridsvfs.Visible = true;
        //        BindgridfeedStock(Startd, Endd);
        //    }

        //    else if (FeedStock.Checked == false)
        //    {
        //        datagridsvfs.Visible = false;
        //    }

        //    if (Med.Checked == true)
        //    {
        //        datagridsvm.Visible = true;
        //        Bindgridmed(Startd, Endd);
        //    }
        //    else if (Med.Checked == false)
        //    {
        //        datagridsvm.Visible = false;
        //    }
        //    if (MedApplied.Checked == true)
        //    {
        //        datagridsvma.Visible = true;
        //        BindgridmedApplies(Startd, Endd);
        //    }


        //    else if (MedApplied.Checked == false)
        //    {
        //        datagridsvma.Visible = false;
        //    }
        //    if (EggPro.Checked == true)
        //    {
        //        datagridsvep.Visible = true;
        //        BindgrideggPro(Startd, Endd);
        //    }

        //    else if (EggPro.Checked == false)
        //    {
        //        datagridsvep.Visible = false;
        //    }
        //    if (EggSold.Checked == true)
        //    {
        //        datagridsves.Visible = true;
        //        Bindgrideggsold(Startd, Endd);
        //    }

        //    else if (EggSold.Checked == false)
        //    {
        //        datagridsves.Visible = false;
        //    }
        //    if (Chicks.Checked == false && ChicksMortality.Checked == false && ChicksSold.Checked == false &&
        //        Infra.Checked == false
        //        && Feed.Checked == false && FeedStock.Checked == false
        //        && Med.Checked == false && MedApplied.Checked == false
        //        && EggPro.Checked == false && EggSold.Checked == false)
        //    {
        //        MessageBox.Show("Select some checkox ");
        //    }

        //}

        //public BindingList<ChicksVO> ocv1 = new BindingList<ChicksVO>();
        //public DAL ocv2 = null;
        //private void Bindgridchicks(DateTime Startdate, DateTime Enddate)
        //{
        //    ocv2 = new DAL();

        //    ocv1 = ocv2.ViewSVC(Startdate, Enddate);
        //    datagridsvc.DataSource = ocv1;
        //    this.datagridsvc.AllowUserToAddRows = false;
        //    datagridsvc.Columns["Cid"].HeaderText = "Id";
        //    datagridsvc.Columns["Cid"].Visible = false;
        //    datagridsvc.Columns["Cdate"].HeaderText = "Date";
        //    datagridsvc.Columns["Cbatchno"].HeaderText = "Batch NO";
        //    datagridsvc.Columns["Cbreed"].HeaderText = "Breed";
        //    datagridsvc.Columns["Cqty"].HeaderText = "Qty";
        //    datagridsvc.Columns["Ctotalwt"].HeaderText = "Tot Weight";
        //    datagridsvc.Columns["Cavgwt"].HeaderText = "Avg Weight";
        //    datagridsvc.Columns["Cwmea"].HeaderText = "Measure";
        //    datagridsvc.Columns["Ctotalamt"].HeaderText = "Tot Amount";
        //    datagridsvc.Columns["Cavgamt"].HeaderText = "Price/Chicks";
        //    datagridsvc.Columns["Cbate"].HeaderText = "Birth Date";
        //    datagridsvc.Columns["Caid"].HeaderText = "Age";
        //    datagridsvc.Columns["Cnotes"].HeaderText = "Notes";

        //}
        //public BindingList<ChicksMotVO> ocmv1 = new BindingList<ChicksMotVO>();
        //public DAL ocmv2 = null;

        //private void BindgridchicksMor(DateTime Startdate, DateTime Enddate)
        //{
        //    ocmv2 = new DAL();
        //    ocmv1 = ocmv2.ViewSVCM(Startdate, Enddate);
        //    datagridsvcm.DataSource = ocmv1;
        //    this.datagridsvcm.AllowUserToAddRows = false;
        //    datagridsvcm.Columns["CMid"].HeaderText = "Id";
        //    datagridsvcm.Columns["CMid"].Visible = false;
        //    datagridsvcm.Columns["CMdate"].HeaderText = "Date";
        //    datagridsvcm.Columns["CMbatchno"].HeaderText = "Batch NO";
        //    datagridsvcm.Columns["CMqty"].HeaderText = "QTY";
        //    datagridsvcm.Columns["CMBalqty"].HeaderText = "Balance QTY";
        //    datagridsvcm.Columns["CMaid"].HeaderText = "Age in Days";
        //    datagridsvcm.Columns["CMnotes"].HeaderText = "Notes";

        //}

        //public BindingList<ChicksSoldVO> ocsv1 = new BindingList<ChicksSoldVO>();
        //public DAL ocsv2 = null;

        //private void BindgridchicksSold(DateTime Startdate, DateTime Enddate)
        //{
        //    ocsv2 = new DAL();
        //    ocsv1 = ocsv2.ViewSVCS(Startdate, Enddate);
        //    datagridsvcs.DataSource = ocsv1;
        //    this.datagridsvcs.AllowUserToAddRows = false;
        //    datagridsvcs.Columns["CSid"].HeaderText = "Id";
        //    datagridsvcs.Columns["CSid"].Visible = false;
        //    datagridsvcs.Columns["CSdate"].HeaderText = "Date";
        //    datagridsvcs.Columns["CSbatchno"].HeaderText = "Batch NO";
        //    datagridsvcs.Columns["CSqty"].HeaderText = "QTY";
        //    datagridsvcs.Columns["CSBalqty"].HeaderText = "Balance QTY";
        //    datagridsvcs.Columns["CStotalwt"].HeaderText = "Tot Weight";
        //    datagridsvcs.Columns["CSavgwt"].HeaderText = "Avg Weight";
        //    datagridsvcs.Columns["CSwmea"].HeaderText = "Measure";
        //    datagridsvcs.Columns["CStotalamt"].HeaderText = "Tot Amount";
        //    datagridsvcs.Columns["CSavgamt"].HeaderText = "Price/Chicks";
        //    datagridsvcs.Columns["CSnotes"].HeaderText = "Notes";

        //}



        //public BindingList<InfraVO> oinv1 = new BindingList<InfraVO>();
        //public DAL oinv2 = null;
        //private void Bindgridinfra(DateTime Startdate, DateTime Enddate)
        //{
        //    oinv2 = new DAL();
        //    oinv1 = oinv2.ViewSVI(Startdate, Enddate);
        //    datagridsvi.DataSource = oinv1;
        //    this.datagridsvi.AllowUserToAddRows = false;
        //    datagridsvi.Columns["Iid"].HeaderText = "Id";
        //    datagridsvi.Columns["Iid"].Visible = false;
        //    datagridsvi.Columns["Idate"].HeaderText = "Date";

        //    datagridsvi.Columns["Iexpensetype"].HeaderText = "Expense Type";

        //    datagridsvi.Columns["Iamount"].HeaderText = "Amount";

        //    datagridsvi.Columns["Inotes"].HeaderText = "Notes";


        //}


        //public BindingList<FeedVO> ofv1 = new BindingList<FeedVO>();
        //public DAL ofv2 = null;

        //private void Bindgridfeed(DateTime Startdate, DateTime Enddate)
        //{
        //    ofv2 = new DAL();
        //    ofv1 = ofv2.ViewSVF(Startdate, Enddate);
        //    datagridsvf.DataSource = ofv1;
        //    this.datagridsvf.AllowUserToAddRows = false;
        //    datagridsvf.Columns["Fid"].HeaderText = "Id";
        //    datagridsvf.Columns["Fid"].Visible = false;
        //    datagridsvf.Columns["Fdate"].HeaderText = "Date";

        //    datagridsvf.Columns["Fpurno"].HeaderText = "Purchase No";

        //    datagridsvf.Columns["Fbrand"].HeaderText = "Brand";

        //    datagridsvf.Columns["Fwt"].HeaderText = "Weight";

        //    datagridsvf.Columns["Fwmea"].HeaderText = "Measure";

        //    datagridsvf.Columns["Fnob"].HeaderText = "No of Bags";

        //    datagridsvf.Columns["Famt"].HeaderText = "Amount";

        //    datagridsvf.Columns["Fnotes"].HeaderText = "Notes";


        //}


        //public BindingList<FeedStoVO> ofsv1 = new BindingList<FeedStoVO>();
        //public DAL ofsv2 = null;

        //private void BindgridfeedStock(DateTime Startdate, DateTime Enddate)
        //{
        //    ofsv2 = new DAL();
        //    ofsv1 = ofsv2.ViewSVFS(Startdate, Enddate);
        //    datagridsvfs.DataSource = ofsv1;
        //    this.datagridsvfs.AllowUserToAddRows = false;
        //    datagridsvfs.Columns["FSid"].HeaderText = "Id";
        //    datagridsvfs.Columns["FSid"].Visible = false;
        //    datagridsvfs.Columns["FSsdate"].HeaderText = "Start Date";


        //    datagridsvfs.Columns["FSedate"].HeaderText = "End Date";

        //    datagridsvfs.Columns["FSfpurno"].HeaderText = "Feed PurchaseNo";

        //    datagridsvfs.Columns["FSfbn"].HeaderText = "Feed Bag Count";

        //    datagridsvfs.Columns["FSbalc"].HeaderText = "Balance Feed Bag count";

        //    datagridsvfs.Columns["FScbn"].HeaderText = "Chicks BatchNo";

        //    datagridsvfs.Columns["FSnotes"].HeaderText = "Notes";

        //}
        //public BindingList<MedicineVO> omv1 = new BindingList<MedicineVO>();
        //public DAL omv2 = null;

        //private void Bindgridmed(DateTime Startdate, DateTime Enddate)
        //{
        //    omv2 = new DAL();
        //    omv1 = omv2.ViewSVM(Startdate, Enddate);


        //    datagridsvm.DataSource = omv1;



        //    this.datagridsvm.AllowUserToAddRows = false;


        //    datagridsvm.Columns["Mid"].HeaderText = "Id";
        //    datagridsvm.Columns["Mid"].Visible = false;
        //    datagridsvm.Columns["Mdate"].HeaderText = "Date";
        //    datagridsvm.Columns["Mpurno"].HeaderText = "Purchase No";
        //    datagridsvm.Columns["Mbrand"].HeaderText = "Name";
        //    datagridsvm.Columns["Msupplier"].HeaderText = "Supplier";
        //    datagridsvm.Columns["Mnature"].HeaderText = "Nature";
        //    datagridsvm.Columns["Mqty"].HeaderText = "Qty";
        //    datagridsvm.Columns["Mamt"].HeaderText = "Amount";
        //    datagridsvm.Columns["Mnotes"].HeaderText = "Notes";

        //}

        //public BindingList<MedicineAppVO> omav1 = new BindingList<MedicineAppVO>();
        //public DAL omav2 = null;

        //private void BindgridmedApplies(DateTime Startdate, DateTime Enddate)
        //{
        //    omav2 = new DAL();
        //    omav1 = omav2.ViewSVMA(Startdate, Enddate);


        //    datagridsvma.DataSource = omav1;
        //    this.datagridsvma.AllowUserToAddRows = false;


        //    datagridsvma.Columns["MAid"].HeaderText = "Id";
        //    datagridsvma.Columns["MAid"].Visible = false;
        //    datagridsvma.Columns["MAsdate"].HeaderText = "Start Date";

        //    datagridsvma.Columns["MAedate"].HeaderText = "End Date";

        //    datagridsvma.Columns["MApurno"].HeaderText = "Medicine PurchaseNo";

        //    datagridsvma.Columns["MAbrand"].HeaderText = "Medicine Name";

        //    datagridsvma.Columns["MAcbatchno"].HeaderText = "Chicks BatchNo";

        //    datagridsvma.Columns["MAnotes"].HeaderText = "Notes";

        //}

        //public BindingList<EggVO> oev1 = new BindingList<EggVO>();
        //public DAL oev2 = null;
        //private void BindgrideggPro(DateTime Startdate, DateTime Enddate)
        //{
        //    oev2 = new DAL();
        //    oev1 = oev2.ViewSVE(Startdate, Enddate);
        //    datagridsvep.DataSource = oev1;
        //    this.datagridsvep.AllowUserToAddRows = false;
        //    datagridsvep.Columns["Eid"].HeaderText = "Id";
        //    datagridsvep.Columns["Eid"].Visible = false;
        //    datagridsvep.Columns["Edate"].HeaderText = "Date";
        //    datagridsvep.Columns["Ecbatchno"].HeaderText = "Chicks BatctNo";
        //    datagridsvep.Columns["Eqty"].HeaderText = "Egg Qty";
        //    datagridsvep.Columns["Einstock"].HeaderText = "Egg Stock";
        //    datagridsvep.Columns["Enotes"].HeaderText = "Notes";

        //}
        //public BindingList<EggSoldVO> oesv1 = new BindingList<EggSoldVO>();
        //public DAL oesv2 = null;
        //private void Bindgrideggsold(DateTime Startdate, DateTime Enddate)
        //{
        //    oesv2 = new DAL();
        //    oesv1 = oesv2.ViewSVES(Startdate, Enddate);
        //    datagridsves.DataSource = oesv1;
        //    this.datagridsves.AllowUserToAddRows = false;
        //    datagridsves.Columns["ESid"].HeaderText = "Id";
        //    datagridsves.Columns["ESid"].Visible = false;
        //    datagridsves.Columns["ESdate"].HeaderText = "Date";
        //    datagridsves.Columns["ESqty"].HeaderText = "Egg Qty";
        //    datagridsves.Columns["ESamt"].HeaderText = "Price/Egg";
        //    datagridsves.Columns["ESinstock"].HeaderText = "Egg Instock";
        //    datagridsves.Columns["ESnotes"].HeaderText = "Notes";

        //}

        //private void tsvsdate_Leave(object sender, EventArgs e)
        //{
        //    DateTime Startd = Convert.ToDateTime(tsvsdate.Text);
        //    DateTime Endd = Convert.ToDateTime(tsvedate.Text);
        //    if (Startd > Endd)
        //    {
        //        MessageBox.Show("Enddate shold be greater than the start date");
        //    }
        //}

        //private void tsvedate_Leave(object sender, EventArgs e)
        //{
        //    DateTime Startd = Convert.ToDateTime(tsvsdate.Text);
        //    DateTime Endd = Convert.ToDateTime(tsvedate.Text);
        //    if (Startd > Endd)
        //    {
        //        MessageBox.Show("Enddate shold be greater than the start date");
        //    }
        //}

        //private void Show_Click(object sender, EventArgs e)
        //{
        //    Temp();
        //}
    }
}
