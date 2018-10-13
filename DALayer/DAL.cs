using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using VOLayer;
using VOLayer.cs;
using System.ComponentModel;
using System.Configuration;


namespace DALayer
{
    public class DAL
    {
        //Chicks
        BindingList<ChicksVO> ovc1 = new BindingList<ChicksVO>();

        ChicksVO ovc2 = null;
        private string constring;


        public BindingList<ChicksVO> ViewC(int pageSize, int pageIndex, int temp, int tempAC, out int recordCount, int AttNamLoc)
        {
            if (AttNamLoc == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
            }
            else if (AttNamLoc == 2)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = constring;

            connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "ViewC";
                comm.Parameters.AddWithValue("@PageIndex", pageIndex);
                comm.Parameters.AddWithValue("@PageSize", pageSize );
                comm.Parameters.AddWithValue("@Temp", temp);
                comm.Parameters.AddWithValue("@TempAC", tempAC);
                comm.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                comm.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                SqlDataReader read = comm.ExecuteReader();

                while (read.Read())
                {
                    ovc2 = new ChicksVO();

                    ovc2.Cid = Convert.ToInt32(read["C_id"].ToString());
                    ovc2.Cdate = Convert.ToDateTime(read["Cdate"].ToString());
                    ovc2.Cbatchno = read["Cbatchno"].ToString();
                    ovc2.Cbreed = read["Cbreed"].ToString();
                    ovc2.Cqty = Convert.ToInt32( read["Cqty"].ToString());
                    ovc2.Ctotalwt = Convert.ToDecimal(read["Ctotalwt"].ToString());
                    ovc2.Cwmea= read["Cwmea"].ToString();
                    ovc2.Cavgwt = Convert.ToDecimal(read["Cavgwt"].ToString());
                    ovc2.Ctotalamt = Convert.ToDecimal(read["Ctotalamt"].ToString());
                    ovc2.Cavgamt = Convert.ToDecimal(read["Cavgamt"].ToString());
                    ovc2.Cbate = Convert.ToDateTime(read["Cbate"].ToString());
                    ovc2.Caid = Convert.ToInt32(read["Caid"].ToString());
                    ovc2.Cnotes = read["Cnotes"].ToString();


                    ovc1.Add(ovc2);

                }

                connection.Close();
                 recordCount = Convert.ToInt32(comm.Parameters["@RecordCount"].Value);
                return ovc1;        
           
        }

        public int InupC(ChicksVO oiuc,string un,int AttNamLoc)
        {

            try
            {
                if (AttNamLoc == 1)
                {
                    constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
                }
                else if (AttNamLoc == 2)
                {
                    constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
                }

                if (oiuc.Cid == 0)
                {
                   
                   
                    SqlConnection connection = new SqlConnection();
                    connection.ConnectionString = constring;

                    connection.Open();
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "AddC";
                comm.Parameters.AddWithValue("@Cdate", oiuc.Cdate);
                comm.Parameters.AddWithValue("@Cbatchno", oiuc.Cbatchno);
                comm.Parameters.AddWithValue("@Cbreed", oiuc.Cbreed);
                comm.Parameters.AddWithValue("@Cqty", oiuc.Cqty);
                comm.Parameters.AddWithValue("@Ctotalwt", oiuc.Ctotalwt);
                comm.Parameters.AddWithValue("@Cwmea", oiuc.Cwmea);
                comm.Parameters.AddWithValue("@Cavgwt", oiuc.Cavgwt);
                comm.Parameters.AddWithValue("@Ctotalamt", oiuc.Ctotalamt);
                comm.Parameters.AddWithValue("@Cavgamt", oiuc.Cavgamt);
                comm.Parameters.AddWithValue("@Cbate", oiuc.Cbate);
                comm.Parameters.AddWithValue("@Caid", oiuc.Caid);
                comm.Parameters.AddWithValue("@Cnotes", oiuc.Cnotes);
                comm.Parameters.AddWithValue("@un", un);
                    comm.ExecuteNonQuery();
                  connection.Close();
                return 2;
                }
                else
                {
              
                    SqlConnection connection = new SqlConnection();
                    connection.ConnectionString = constring;
                    connection.Open();
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.CommandText = "UpdateC";
                    comm.Parameters.AddWithValue("@C_id", oiuc.Cid);
                    comm.Parameters.AddWithValue("@Cdate", oiuc.Cdate);
                    comm.Parameters.AddWithValue("@Cbatchno", oiuc.Cbatchno);
                    comm.Parameters.AddWithValue("@Cbreed", oiuc.Cbreed);
                    comm.Parameters.AddWithValue("@Cqty", oiuc.Cqty);
                    comm.Parameters.AddWithValue("@Ctotalwt", oiuc.Ctotalwt);
                    comm.Parameters.AddWithValue("@Cwmea", oiuc.Cwmea);
                    comm.Parameters.AddWithValue("@Cavgwt", oiuc.Cavgwt);
                    comm.Parameters.AddWithValue("@Ctotalamt", oiuc.Ctotalamt);
                    comm.Parameters.AddWithValue("@Cavgamt", oiuc.Cavgamt);
                    comm.Parameters.AddWithValue("@Cbate", oiuc.Cbate);
                    comm.Parameters.AddWithValue("@Caid", oiuc.Caid);
                    comm.Parameters.AddWithValue("@Cnotes", oiuc.Cnotes);
                    comm.Parameters.AddWithValue("@un", un);

                    comm.ExecuteNonQuery();
                  

                    connection.Close();
                    return 1;

                }
            }
            catch (Exception e)
            {
                return 0;
            }

        }


        public int DelC(int id, int AttNamLoc)
        {

            if (AttNamLoc == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
            }
            else if (AttNamLoc == 2)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = constring;
            connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
                    comm.CommandText = "DelC";
                    comm.Parameters.AddWithValue("@C_id", id);
                    int i = comm.ExecuteNonQuery();

            connection.Close();
            return i;
            
            }
        //Chicks
        //Chicks Sold



        BindingList<ChicksSoldVO> ovcs1 = new BindingList<ChicksSoldVO>();

        ChicksSoldVO ovcs2 = null;


        public BindingList<ChicksSoldVO> ViewCS(int pageSize, int pageIndex, int temp, int tempAC, out int recordCount, int AttNamLoc)
        {

            if (AttNamLoc == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
            }
            else if (AttNamLoc == 2)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = constring;

            connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "ViewCS";
            comm.Parameters.AddWithValue("@PageIndex", pageIndex);
            comm.Parameters.AddWithValue("@PageSize", pageSize);
            comm.Parameters.AddWithValue("@Temp", temp);
            comm.Parameters.AddWithValue("@TempAC", tempAC);
            comm.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
            comm.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
            SqlDataReader read = comm.ExecuteReader();
            while (read.Read())
            {
                ovcs2 = new ChicksSoldVO();

                ovcs2.CSid = Convert.ToInt32(read["CS_id"].ToString());
                ovcs2.CSdate = Convert.ToDateTime(read["CSdate"].ToString());
                ovcs2.CSbatchno = read["CSbatchno"].ToString();
                ovcs2.CSqty = Convert.ToInt32(read["CSqty"].ToString());
                ovcs2.CSBalqty = Convert.ToInt32(read["CSBalqty"].ToString());
                ovcs2.CStotalwt = Convert.ToDecimal(read["CStotwt"].ToString());
                ovcs2.CSwmea = read["CSwmea"].ToString();
                ovcs2.CSavgwt = Convert.ToDecimal(read["CSavgwt"].ToString());
                ovcs2.CStotalamt = Convert.ToDecimal(read["CStotamt"].ToString());
                ovcs2.CSavgamt = Convert.ToDecimal(read["CSavgamt"].ToString()); 
                ovcs2.CSnotes = read["CSnotes"].ToString();


                ovcs1.Add(ovcs2);

            }

            connection.Close();
            recordCount = Convert.ToInt32(comm.Parameters["@RecordCount"].Value);
            return ovcs1;



        }

        public int InupCS(ChicksSoldVO oiucs, string un, out int CSBalqty, int AttNamLoc)
        {



            if (oiucs.CSid == 0)
            {
                if (AttNamLoc == 1)
                {
                    constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
                }
                else if (AttNamLoc == 2)
                {
                    constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
                }
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = constring;
                connection.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandType = CommandType.StoredProcedure;
                    comm.CommandText = "AddCS";
                    comm.Parameters.AddWithValue("@CSdate", oiucs.CSdate);
                    comm.Parameters.AddWithValue("@CSqty", oiucs.CSqty);
                    comm.Parameters.AddWithValue("@CSbatchno", oiucs.CSbatchno);
                comm.Parameters.AddWithValue("@CStotalwt", oiucs.CStotalwt);
                comm.Parameters.AddWithValue("@CSwmea", oiucs.CSwmea);
                comm.Parameters.AddWithValue("@CSavgwt", oiucs.CSavgwt);
                comm.Parameters.AddWithValue("@CStotalamt", oiucs.CStotalamt);
                comm.Parameters.AddWithValue("@CSavgamt", oiucs.CSavgamt);
                comm.Parameters.AddWithValue("@CSnotes", oiucs.CSnotes);
                comm.Parameters.AddWithValue("@un", un);
                comm.Parameters.Add("@CSBalqty", SqlDbType.Int, 4);
                comm.Parameters["@CSBalqty"].Direction = ParameterDirection.Output;             
                 comm.ExecuteNonQuery();
            
                CSBalqty = Convert.ToInt32(comm.Parameters["@CSBalqty"].Value);
                connection.Close();
                return 2;
            }
            else
            {


                if (AttNamLoc == 1)
                {
                    constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
                }
                else if (AttNamLoc == 2)
                {
                    constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
                }
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = constring;
                connection.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "UpdateCS";
                comm.Parameters.AddWithValue("@CS_id", oiucs.CSid);
                comm.Parameters.AddWithValue("@CSdate", oiucs.CSdate);
                comm.Parameters.AddWithValue("@CSqty", oiucs.CSqty);
                comm.Parameters.AddWithValue("@CSbatchno", oiucs.CSbatchno);
                comm.Parameters.AddWithValue("@CStotalwt", oiucs.CStotalwt);
                comm.Parameters.AddWithValue("@CSwmea", oiucs.CSwmea);
                comm.Parameters.AddWithValue("@CSavgwt", oiucs.CSavgwt);
                comm.Parameters.AddWithValue("@CStotalamt", oiucs.CStotalamt);
                comm.Parameters.AddWithValue("@CSavgamt", oiucs.CSavgamt);
                comm.Parameters.AddWithValue("@CSnotes", oiucs.CSnotes);
                comm.Parameters.AddWithValue("@un", un);
                comm.Parameters.Add("@CSBalqty", SqlDbType.Int, 4);
                comm.Parameters["@CSBalqty"].Direction = ParameterDirection.Output;
                 comm.ExecuteNonQuery();
               

                CSBalqty = Convert.ToInt32(comm.Parameters["@CSBalqty"].Value);
                connection.Close();
                return 1;

            }
        }

    public int DelCS(int id, int AttNamLoc)
        {

            if (AttNamLoc == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
            }
            else if (AttNamLoc == 2)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = constring;
            connection.Open();

            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "DelCS";
            comm.Parameters.AddWithValue("@CS_id", id);
            int i = comm.ExecuteNonQuery();
            connection.Close();
            return i;

        }
        //Chicks Sold
        //Chicks Mot



        BindingList<ChicksMotVO> ovcm1 = new BindingList<ChicksMotVO>();

        ChicksMotVO ovcm2 = null;


        public BindingList<ChicksMotVO> ViewCM(int pageSize, int pageIndex, int temp, int tempAC, out int recordCount, int AttNamLoc)
        {

            if (AttNamLoc == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
            }
            else if (AttNamLoc == 2)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = constring;
            connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "ViewCM";
            comm.Parameters.AddWithValue("@PageIndex", pageIndex);
            comm.Parameters.AddWithValue("@PageSize", pageSize);
            comm.Parameters.AddWithValue("@Temp", temp);
            comm.Parameters.AddWithValue("@TempAC", tempAC);
            comm.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
            comm.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
            SqlDataReader read = comm.ExecuteReader();
            while (read.Read())
            {
                ovcm2 = new ChicksMotVO();

                ovcm2.CMid = Convert.ToInt32(read["CM_id"].ToString());
                ovcm2.CMdate = Convert.ToDateTime(read["CMsdate"].ToString());
                ovcm2.CMbatchno = read["CMbatchno"].ToString();
                ovcm2.CMqty = Convert.ToInt32(read["CMqty"].ToString());
                ovcm2.CMBalqty = Convert.ToInt32(read["CMBalqty"].ToString());
                ovcm2.CMaid = Convert.ToInt32(read["CMaid"].ToString());
                ovcm2.CMnotes = read["CMnotes"].ToString();


                ovcm1.Add(ovcm2);

            }

            connection.Close();
            recordCount = Convert.ToInt32(comm.Parameters["@RecordCount"].Value);
            return ovcm1;



        }

        public int InupCM(ChicksMotVO oiucm, string un, out DateTime CMbdate,  out int CMBalqty, out int CMaid, int AttNamLoc)
        {



            if (oiucm.CMid == 0)
            {
                if (AttNamLoc == 1)
                {
                    constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
                }
                else if (AttNamLoc == 2)
                {
                    constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
                }
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = constring;
                connection.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "AddCM";
                comm.Parameters.AddWithValue("@CMdate", oiucm.CMdate);
                comm.Parameters.AddWithValue("@CMqty", oiucm.CMqty);
                comm.Parameters.AddWithValue("@CMbatchno", oiucm.CMbatchno);
                comm.Parameters.AddWithValue("@CMnotes", oiucm.CMnotes);
                comm.Parameters.AddWithValue("@un", un);
                comm.Parameters.Add("@CMbdate", SqlDbType.Date);
                comm.Parameters["@CMbdate"].Direction = ParameterDirection.Output;
                comm.Parameters.Add("@CMBalqty", SqlDbType.Int, 4);
                comm.Parameters["@CMBalqty"].Direction = ParameterDirection.Output;
                comm.Parameters.Add("@CMaid", SqlDbType.Int, 4);
                comm.Parameters["@CMaid"].Direction = ParameterDirection.Output;
                comm.ExecuteNonQuery();
                CMbdate = Convert.ToDateTime(comm.Parameters["@CMbdate"].Value);
         
                CMBalqty = Convert.ToInt32(comm.Parameters["@CMBalqty"].Value);
                CMaid = Convert.ToInt32(comm.Parameters["@CMaid"].Value);
                connection.Close();
                return 2;
            }
            else
            {
                if (AttNamLoc == 1)
                {
                    constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
                }
                else if (AttNamLoc == 2)
                {
                    constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
                }
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = constring;
                connection.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "UpdateCM";
                comm.Parameters.AddWithValue("@CM_id", oiucm.CMid);
                comm.Parameters.AddWithValue("@CMdate", oiucm.CMdate);
                comm.Parameters.AddWithValue("@CMqty", oiucm.CMqty);
                comm.Parameters.AddWithValue("@CMbatchno", oiucm.CMbatchno);
                comm.Parameters.AddWithValue("@CMnotes", oiucm.CMnotes);
                comm.Parameters.AddWithValue("@un", un);
                comm.Parameters.Add("@CMbdate", SqlDbType.Date);
                comm.Parameters["@CMbdate"].Direction = ParameterDirection.Output;

                comm.Parameters.Add("@CMBalqty", SqlDbType.Int, 4);
                comm.Parameters["@CMBalqty"].Direction = ParameterDirection.Output;
                comm.Parameters.Add("@CMaid", SqlDbType.Int, 4);
                comm.Parameters["@CMaid"].Direction = ParameterDirection.Output;
               comm.ExecuteNonQuery();
                
                CMbdate = Convert.ToDateTime(comm.Parameters["@CMbdate"].Value);
                CMBalqty = Convert.ToInt32(comm.Parameters["@CMBalqty"].Value);
                CMaid = Convert.ToInt32(comm.Parameters["@CMaid"].Value);
                connection.Close();
                return 1;
            }
        }
        public int DelCM(int id, int AttNamLoc)
        {

            if (AttNamLoc == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
            }
            else if (AttNamLoc == 2)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = constring;
            connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "DelCM";
            comm.Parameters.AddWithValue("@CM_id", id);
            int i = comm.ExecuteNonQuery();
            connection.Close();
            return i;

        }
        //Chicks Mot


        //Feed
        BindingList<FeedVO> ovf1 = new BindingList<FeedVO>();

        FeedVO ovf2 = null;


        public BindingList<FeedVO> ViewF(int pageSize, int pageIndex, int temp, int tempAC, out int recordCount, int AttNamLoc)
        {

            if (AttNamLoc == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
            }
            else if (AttNamLoc == 2)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = constring;
            connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "ViewF";
            comm.Parameters.AddWithValue("@PageIndex", pageIndex);
            comm.Parameters.AddWithValue("@PageSize", pageSize);
            comm.Parameters.AddWithValue("@Temp", temp);
            comm.Parameters.AddWithValue("@TempAC", tempAC);
            comm.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
            comm.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
            SqlDataReader read = comm.ExecuteReader();
            while (read.Read())
            {
                ovf2 = new FeedVO();

                ovf2.Fid = Convert.ToInt32(read["F_id"].ToString());
                ovf2.Fdate = Convert.ToDateTime(read["Fdate"].ToString());
                ovf2.Fbrand = read["Fbrand"].ToString();
                ovf2.Fpurno = read["Fpurno"].ToString();
                ovf2.Fnob = Convert.ToInt32(read["Fnob"].ToString());
                ovf2.Fwt = Convert.ToDecimal(read["Fwt"].ToString());
                ovf2.Fwav = Convert.ToDecimal(read["Fwav"].ToString());
                ovf2.Fwmea = read["Fwmea"].ToString();              
                ovf2.Ftotamt = Convert.ToDecimal(read["Ftotamt"].ToString());
                ovf2.Favgamt = Convert.ToDecimal(read["Favgamt"].ToString());
                ovf2.Fnotes = read["Fnotes"].ToString();


                ovf1.Add(ovf2);

            }

            connection.Close();
            recordCount = Convert.ToInt32(comm.Parameters["@RecordCount"].Value);
            return ovf1;

        }

        public int InupF(FeedVO oiuf, string un, int AttNamLoc)
        {

            try
            {

                if (oiuf.Fid == 0)
                {
                    if (AttNamLoc == 1)
                    {
                        constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
                    }
                    else if (AttNamLoc == 2)
                    {
                        constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
                    }
                    SqlConnection connection = new SqlConnection();
                    connection.ConnectionString = constring;
                    connection.Open();
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.CommandText = "AddF";
                    comm.Parameters.AddWithValue("@Fdate", oiuf.Fdate);
                    comm.Parameters.AddWithValue("@Fbrand", oiuf.Fbrand);
                    comm.Parameters.AddWithValue("@Fpurno", oiuf.Fpurno);
                    comm.Parameters.AddWithValue("@Fnob", oiuf.Fnob);
                    comm.Parameters.AddWithValue("@Fwt", oiuf.Fwt);
                    comm.Parameters.AddWithValue("@Fwav", oiuf.Fwav);
                    comm.Parameters.AddWithValue("@Fwmea", oiuf.Fwmea);
                    comm.Parameters.AddWithValue("@Ftotamt", oiuf.Ftotamt);
                    comm.Parameters.AddWithValue("@Favgamt", oiuf.Favgamt);
                    comm.Parameters.AddWithValue("@Fnotes", oiuf.Fnotes);
                    comm.Parameters.AddWithValue("@un", un);
                    comm.ExecuteNonQuery();
                    connection.Close();
                    return 2;
                }
                else
                {


                    if (AttNamLoc == 1)
                    {
                        constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
                    }
                    else if (AttNamLoc == 2)
                    {
                        constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
                    }
                    SqlConnection connection = new SqlConnection();
                    connection.ConnectionString = constring;
                    connection.Open();
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.CommandText = "UpdateF";
                    comm.Parameters.AddWithValue("@F_id", oiuf.Fid);
                    comm.Parameters.AddWithValue("@Fdate", oiuf.Fdate);
                    comm.Parameters.AddWithValue("@Fbrand", oiuf.Fbrand);
                    comm.Parameters.AddWithValue("@Fpurno", oiuf.Fpurno);
                    comm.Parameters.AddWithValue("@Fnob", oiuf.Fnob);
                    comm.Parameters.AddWithValue("@Fwt", oiuf.Fwt);
                    comm.Parameters.AddWithValue("@Fwav", oiuf.Fwav);
                    comm.Parameters.AddWithValue("@Fwmea", oiuf.Fwmea);
                    comm.Parameters.AddWithValue("@Ftotamt", oiuf.Ftotamt);
                    comm.Parameters.AddWithValue("@Favgamt", oiuf.Favgamt);
                    comm.Parameters.AddWithValue("@Fnotes", oiuf.Fnotes);
                    comm.Parameters.AddWithValue("@un", un);

                    comm.ExecuteNonQuery();
                   

                    connection.Close();
                    return 1;

                }
            }
            catch (Exception e)
            {
                return 0;
            }

        }


        public int DelF(int id, int AttNamLoc)
        {

            if (AttNamLoc == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
            }
            else if (AttNamLoc == 2)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = constring;
            connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "DelF";
            comm.Parameters.AddWithValue("@F_id", id);
            int i = comm.ExecuteNonQuery();

            connection.Close();
            return i;

        }

        //Feed

        //Feed Sto



        BindingList<FeedStoVO> ovfs1 = new BindingList<FeedStoVO>();

        FeedStoVO ovfs2 = null;


        public BindingList<FeedStoVO> ViewFS(int pageSize, int pageIndex, int temp, int tempAC, out int recordCount, int AttNamLoc)
        {

            if (AttNamLoc == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
            }
            else if (AttNamLoc == 2)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = constring;
            connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "ViewFS";
            comm.Parameters.AddWithValue("@PageIndex", pageIndex);
            comm.Parameters.AddWithValue("@PageSize", pageSize);
            comm.Parameters.AddWithValue("@Temp", temp);
            comm.Parameters.AddWithValue("@TempAC", tempAC);
            comm.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
            comm.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
            SqlDataReader read = comm.ExecuteReader();
            while (read.Read())
            {
                ovfs2 = new FeedStoVO();

                ovfs2.FSid = Convert.ToInt32(read["FS_id"].ToString());
                ovfs2.FSsdate = Convert.ToDateTime(read["FSsdate"].ToString());
                ovfs2.FSedate = Convert.ToDateTime(read["FSedate"].ToString());
                ovfs2.FSfpurno = read["FSpurno"].ToString();
                ovfs2.FBrand = read["Fbrand"].ToString();
                ovfs2.FSfbn = Convert.ToInt32(read["FSbn"].ToString());
                ovfs2.FSbalc = Convert.ToInt32(read["FSbalc"].ToString());
                ovfs2.FScbn = read["FScbn"].ToString();
                ovfs2.FSnotes = read["FSnotes"].ToString();


                ovfs1.Add(ovfs2);

            }

            connection.Close();
            recordCount = Convert.ToInt32(comm.Parameters["@RecordCount"].Value);
            return ovfs1;



        }

        public int InupFS(FeedStoVO oiufs, string un, out DateTime FSfpurdate, out int FSbalbagcou, int AttNamLoc)
        {



            if (oiufs.FSid == 0)
            {
                if (AttNamLoc == 1)
                {
                    constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
                }
                else if (AttNamLoc == 2)
                {
                    constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
                }
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = constring;
                connection.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "AddFS";
                comm.Parameters.AddWithValue("@FSsdate", oiufs.FSsdate);
                comm.Parameters.AddWithValue("@FSedate", oiufs.FSedate);
                comm.Parameters.AddWithValue("@FSpurno", oiufs.FSfpurno);
                comm.Parameters.AddWithValue("@FSbn", oiufs.FSfbn);
                comm.Parameters.AddWithValue("@FScbn", oiufs.FScbn);
                comm.Parameters.AddWithValue("@FSnotes", oiufs.FSnotes);
                comm.Parameters.AddWithValue("@un", un);
                comm.Parameters.Add("@FSfpurdate", SqlDbType.Date);
                comm.Parameters["@FSfpurdate"].Direction = ParameterDirection.Output;
                comm.Parameters.Add("@FSbalc", SqlDbType.Int, 4);
                comm.Parameters["@FSbalc"].Direction = ParameterDirection.Output;
              comm.ExecuteNonQuery();
                FSfpurdate = Convert.ToDateTime(comm.Parameters["@FSfpurdate"].Value);
                FSbalbagcou = Convert.ToInt32(comm.Parameters["@FSbalc"].Value);
              
                connection.Close();
                return 2;
            }
            else
            {


                if (AttNamLoc == 1)
                {
                    constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
                }
                else if (AttNamLoc == 2)
                {
                    constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
                }
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = constring;
                connection.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "UpdateFS";
                comm.Parameters.AddWithValue("@FS_id", oiufs.FSid);
                comm.Parameters.AddWithValue("@FSsdate", oiufs.FSsdate);
                comm.Parameters.AddWithValue("@FSedate", oiufs.FSedate);
                comm.Parameters.AddWithValue("@FSpurno", oiufs.FSfpurno);
                comm.Parameters.AddWithValue("@FSbn", oiufs.FSfbn);
                comm.Parameters.AddWithValue("@FScbn", oiufs.FScbn);
                comm.Parameters.AddWithValue("@FSnotes", oiufs.FSnotes);
                comm.Parameters.AddWithValue("@un", un);
                comm.Parameters.Add("@FSfpurdate", SqlDbType.Date);
                comm.Parameters["@FSfpurdate"].Direction = ParameterDirection.Output;
                comm.Parameters.Add("@FSbalc", SqlDbType.Int, 4);
                comm.Parameters["@FSbalc"].Direction = ParameterDirection.Output;
               comm.ExecuteNonQuery();

             
                FSfpurdate = Convert.ToDateTime(comm.Parameters["@FSfpurdate"].Value);
          
                FSbalbagcou = Convert.ToInt32(comm.Parameters["@FSbalc"].Value);
                connection.Close();
                return 1;

            }




        }


        public int DelFS(int id, int AttNamLoc)
        {

            if (AttNamLoc == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
            }
            else if (AttNamLoc == 2)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = constring;
            connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "DelFS";
            comm.Parameters.AddWithValue("@FS_id", id);




            int i = comm.ExecuteNonQuery();


            connection.Close();
            return i;

        }
        //Feed Sto



        //Medicine
        BindingList<MedicineVO> ovm1 = new BindingList<MedicineVO>();

        MedicineVO ovm2 = null;


        public BindingList<MedicineVO> ViewM(int pageSize, int pageIndex, int temp, int tempAC, out int recordCount, int AttNamLoc)
        {

            if (AttNamLoc == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
            }
            else if (AttNamLoc == 2)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = constring;
            connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "ViewM";
            comm.Parameters.AddWithValue("@PageIndex", pageIndex);
            comm.Parameters.AddWithValue("@PageSize", pageSize);
            comm.Parameters.AddWithValue("@Temp", temp);
            comm.Parameters.AddWithValue("@TempAC", tempAC);
            comm.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
            comm.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
            SqlDataReader read = comm.ExecuteReader();
            while (read.Read())
            {
                ovm2 = new MedicineVO();

                ovm2.Mid = Convert.ToInt32(read["M_id"].ToString());
                ovm2.Mdate = Convert.ToDateTime(read["Mdate"].ToString());
                ovm2.Mpurno = read["Mpurno"].ToString();
                ovm2.Mbrand = read["Mbrand"].ToString();
                ovm2.Msupplier = read["Msupplier"].ToString();
                ovm2.Mnature = read["Mnature"].ToString();
                ovm2.Mqty = Convert.ToInt32(read["Mqty"].ToString());
                ovm2.Mamt = Convert.ToDecimal(read["Mamt"].ToString());
                ovm2.Mnotes = read["Mnotes"].ToString();


                ovm1.Add(ovm2);

            }

            connection.Close();
            recordCount = Convert.ToInt32(comm.Parameters["@RecordCount"].Value);
            return ovm1;

        }

        public int InupM(MedicineVO oium, string un, int AttNamLoc)
        {

            try
            {

                if (oium.Mid == 0)
                {
                    if (AttNamLoc == 1)
                    {
                        constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
                    }
                    else if (AttNamLoc == 2)
                    {
                        constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
                    }
                    SqlConnection connection = new SqlConnection();
                    connection.ConnectionString = constring;
                    connection.Open();
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.CommandText = "AddM";
                    comm.Parameters.AddWithValue("@Mdate", oium.Mdate);
                    comm.Parameters.AddWithValue("@Mpurno", oium.Mpurno);
                    comm.Parameters.AddWithValue("@Mbrand", oium.Mbrand);
                    comm.Parameters.AddWithValue("@Msupplier", oium.Msupplier);
                    comm.Parameters.AddWithValue("@Mnature", oium.Mnature);
                    comm.Parameters.AddWithValue("@Mqty", oium.Mqty);
                    comm.Parameters.AddWithValue("@Mamt", oium.Mamt);
                    comm.Parameters.AddWithValue("@Mnotes", oium.Mnotes);
                    comm.Parameters.AddWithValue("@un", un);
                    comm.ExecuteNonQuery();
                    connection.Close();
                    return 2;
                }
                else
                {


                    if (AttNamLoc == 1)
                    {
                        constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
                    }
                    else if (AttNamLoc == 2)
                    {
                        constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
                    }
                    SqlConnection connection = new SqlConnection();
                    connection.ConnectionString = constring;
                    connection.Open();
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.CommandText = "UpdateM";
                    comm.Parameters.AddWithValue("@M_id", oium.Mid);
                    comm.Parameters.AddWithValue("@Mdate", oium.Mdate);
                    comm.Parameters.AddWithValue("@Mpurno", oium.Mpurno);
                    comm.Parameters.AddWithValue("@Mbrand", oium.Mbrand);
                    comm.Parameters.AddWithValue("@Msupplier", oium.Msupplier);
                    comm.Parameters.AddWithValue("@Mnature", oium.Mnature);
                    comm.Parameters.AddWithValue("@Mqty", oium.Mqty);
                    comm.Parameters.AddWithValue("@Mamt", oium.Mamt);
                    comm.Parameters.AddWithValue("@Mnotes", oium.Mnotes);
                    comm.Parameters.AddWithValue("@un", un);

                    comm.ExecuteNonQuery();
                  

                    connection.Close();
                    return 1;

                }
            }
            catch (Exception e)
            {
                return 0;
            }

        }


        public int DelM(int id, int AttNamLoc)
        {
            if (AttNamLoc == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
            }
            else if (AttNamLoc == 2)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = constring;
            connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "DelM";
            comm.Parameters.AddWithValue("@M_id", id);
            int i = comm.ExecuteNonQuery();

            connection.Close();
            return i;

        }

        //Medicine


        //Medicine Apply



        BindingList<MedicineAppVO> ovma1 = new BindingList<MedicineAppVO>();

        MedicineAppVO ovma2 = null;


        public BindingList<MedicineAppVO> ViewMA(int pageSize, int pageIndex, int temp, int tempAC, out int recordCount, int AttNamLoc)
        {

            if (AttNamLoc == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
            }
            else if (AttNamLoc == 2)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = constring;
            connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "ViewMA";
            comm.Parameters.AddWithValue("@PageIndex", pageIndex);
            comm.Parameters.AddWithValue("@PageSize", pageSize);
            comm.Parameters.AddWithValue("@Temp", temp);
            comm.Parameters.AddWithValue("@TempAC", tempAC);
            comm.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
            comm.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
            SqlDataReader read = comm.ExecuteReader();
            while (read.Read())
            {
                ovma2 = new MedicineAppVO();

                ovma2.MAid = Convert.ToInt32(read["MA_id"].ToString());
                ovma2.MAsdate = Convert.ToDateTime(read["MAsdate"].ToString());
                ovma2.MAedate = Convert.ToDateTime(read["MAedate"].ToString());
                ovma2.MApurno = read["MApurno"].ToString();
                ovma2.MAbrand = read["MAbrand"].ToString();
                ovma2.MAcbatchno = read["MAcbatchno"].ToString();
                ovma2.MAnotes = read["MAnotes"].ToString();


                ovma1.Add(ovma2);

            }

            connection.Close();
            recordCount = Convert.ToInt32(comm.Parameters["@RecordCount"].Value);
            return ovma1;



        }

        public int InupMA(MedicineAppVO oiuma, string un, out DateTime MApurdate, out string MAbrand, int AttNamLoc)
        {



            if (oiuma.MAid == 0)
            {
                if (AttNamLoc == 1)
                {
                    constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
                }
                else if (AttNamLoc == 2)
                {
                    constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
                }
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = constring;
                connection.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "AddMA";
                comm.Parameters.AddWithValue("@MAsdate  ", oiuma.MAsdate);
                comm.Parameters.AddWithValue("@MAedate", oiuma.MAedate);
                comm.Parameters.AddWithValue("@MApurno", oiuma.MApurno);
                comm.Parameters.AddWithValue("@MAcbatchno", oiuma.MAcbatchno);
                comm.Parameters.AddWithValue("@MAnotes", oiuma.MAnotes);
                comm.Parameters.AddWithValue("@un", un);
                comm.Parameters.Add("@MAmpurdate", SqlDbType.Date);
                comm.Parameters["@MAmpurdate"].Direction = ParameterDirection.Output;

                comm.Parameters.Add("@MAmbrand", SqlDbType.NVarChar,200);
                comm.Parameters["@MAmbrand"].Direction = ParameterDirection.Output;
                 comm.ExecuteNonQuery();
              
                MApurdate = Convert.ToDateTime(comm.Parameters["@MAmpurdate"].Value);
                MAbrand = Convert.ToString(comm.Parameters["@MAmbrand"].Value);

                connection.Close();
                return 2;
            }
            else
            {


                if (AttNamLoc == 1)
                {
                    constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
                }
                else if (AttNamLoc == 2)
                {
                    constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
                }
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = constring;
                connection.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "UpdateMA";
                comm.Parameters.AddWithValue("@MA_id", oiuma.MAid);
                comm.Parameters.AddWithValue("@MAsdate  ", oiuma.MAsdate);
                comm.Parameters.AddWithValue("@MAedate", oiuma.MAedate);
                comm.Parameters.AddWithValue("@MApurno", oiuma.MApurno);
                comm.Parameters.AddWithValue("@MAcbatchno", oiuma.MAcbatchno);
                comm.Parameters.AddWithValue("@MAnotes", oiuma.MAnotes);
                comm.Parameters.AddWithValue("@un", un);
                comm.Parameters.Add("@MAmpurdate", SqlDbType.Date);
                comm.Parameters["@MAmpurdate"].Direction = ParameterDirection.Output;
                comm.Parameters.Add("@MAmbrand", SqlDbType.NVarChar, 200);
                comm.Parameters["@MAmbrand"].Direction = ParameterDirection.Output;
                comm.ExecuteNonQuery();
              
                MApurdate = Convert.ToDateTime(comm.Parameters["@MAmpurdate"].Value);
                MAbrand = Convert.ToString(comm.Parameters["@MAmbrand"].Value);

                connection.Close();
                return 1;

            }
        }


        public int DelMA(int id,int AttNamLoc)
        {

            if (AttNamLoc == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
            }
            else if (AttNamLoc == 2)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = constring;
            connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "DelMA";
            comm.Parameters.AddWithValue("@MA_id", id);
            int i = comm.ExecuteNonQuery();
            connection.Close();
            return i;

        }
        //Infra

        BindingList<InfraVO> ovin1 = new BindingList<InfraVO>();

        InfraVO ovin2 = null;


        public BindingList<InfraVO> ViewI(int pageSize, int pageIndex, int temp, int tempAC, out int recordCount, int AttNamLoc)
        {

            if (AttNamLoc == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
            }
            else if (AttNamLoc == 2)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = constring;
            connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "ViewI";
            comm.Parameters.AddWithValue("@PageIndex", pageIndex);
            comm.Parameters.AddWithValue("@PageSize", pageSize);
            comm.Parameters.AddWithValue("@Temp", temp);
            comm.Parameters.AddWithValue("@TempAC", tempAC);
            comm.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
            comm.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
            SqlDataReader read = comm.ExecuteReader();
            while (read.Read())
            {
                ovin2 = new InfraVO();

                ovin2.Iid = Convert.ToInt32(read["I_id"].ToString());
                ovin2.Idate = Convert.ToDateTime(read["Idate"].ToString());
                ovin2.Iexpensetype = read["Iexpensetype"].ToString();
                ovin2.Iamount = Convert.ToDecimal(read["Iamount"].ToString());
                ovin2.Inotes = read["Inotes"].ToString();


                ovin1.Add(ovin2);

            }

            connection.Close();
            recordCount = Convert.ToInt32(comm.Parameters["@RecordCount"].Value);
            return ovin1;

        }

        public int InupI(InfraVO oiuin, string un, int AttNamLoc)
        {

            try
            {

                if (oiuin.Iid == 0)
                {
                    if (AttNamLoc == 1)
                    {
                        constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
                    }
                    else if (AttNamLoc == 2)
                    {
                        constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
                    }
                    SqlConnection connection = new SqlConnection();
                    connection.ConnectionString = constring;
                    connection.Open();
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.CommandText = "AddI";
                    comm.Parameters.AddWithValue("@Idate", oiuin.Idate);
                    comm.Parameters.AddWithValue("@Iexpensetype", oiuin.Iexpensetype);
                    comm.Parameters.AddWithValue("@Iamount", oiuin.Iamount);
                    comm.Parameters.AddWithValue("@Inotes", oiuin.Inotes);
                    comm.Parameters.AddWithValue("@un", un);
                    comm.ExecuteNonQuery();
                    connection.Close();
                    return 2;
                }
                else
                {


                    if (AttNamLoc == 1)
                    {
                        constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
                    }
                    else if (AttNamLoc == 2)
                    {
                        constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
                    }
                    SqlConnection connection = new SqlConnection();
                    connection.ConnectionString = constring;
                    connection.Open();
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.CommandText = "UpdateI";
                    comm.Parameters.AddWithValue("@I_id", oiuin.Iid);
                    comm.Parameters.AddWithValue("@Idate", oiuin.Idate);
                    comm.Parameters.AddWithValue("@Iexpensetype", oiuin.Iexpensetype);
                    comm.Parameters.AddWithValue("@Iamount", oiuin.Iamount);
                    comm.Parameters.AddWithValue("@Inotes", oiuin.Inotes);
                    comm.Parameters.AddWithValue("@un", un);

                    comm.ExecuteNonQuery();
   
                    connection.Close();
                    return 1;

                }
            }
            catch (Exception e)
            {
                return 0;
            }

        }


        public int DelI(int id, int AttNamLoc)
        {

            if (AttNamLoc == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
            }
            else if (AttNamLoc == 2)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = constring;
            connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "DelI";
            comm.Parameters.AddWithValue("@I_id", id);
            int i = comm.ExecuteNonQuery();

            connection.Close();
            return i;

        }
        //Infra
        //Egg



        BindingList<EggVO> ove1 = new BindingList<EggVO>();

        EggVO ove2 = null;


        public BindingList<EggVO> ViewE(int pageSize, int pageIndex, int temp,int tempAC,out int recordCount, int AttNamLoc)
        {

            if (AttNamLoc == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
            }
            else if (AttNamLoc == 2)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = constring;
            connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "ViewE";
            comm.Parameters.AddWithValue("@PageIndex", pageIndex);
            comm.Parameters.AddWithValue("@PageSize", pageSize);
            comm.Parameters.AddWithValue("@Temp", temp);
            comm.Parameters.AddWithValue("@TempAC", tempAC);

            comm.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
            comm.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
            SqlDataReader read = comm.ExecuteReader();
            while (read.Read())
            {
                ove2 = new EggVO();

                ove2.Eid = Convert.ToInt32(read["E_id"].ToString());
                ove2.Edate = Convert.ToDateTime(read["Edate"].ToString());
                ove2.Ecbatchno = read["Ecbatchno"].ToString();
                ove2.Eqty = Convert.ToInt32(read["Eqty"].ToString());
                ove2.Einstock = Convert.ToInt32(read["Einstock"].ToString());
                ove2.Enotes = read["Enotes"].ToString();


                ove1.Add(ove2);

            }

            connection.Close();
            recordCount = Convert.ToInt32(comm.Parameters["@RecordCount"].Value);
            return ove1;



        }

        public int InupE(EggVO oiue, string un, out int Einstock, int AttNamLoc)
        {



            if (oiue.Eid == 0)
            {
                if (AttNamLoc == 1)
                {
                    constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
                }
                else if (AttNamLoc == 2)
                {
                    constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
                }
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = constring;
                connection.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "AddE";
                comm.Parameters.AddWithValue("@Edate", oiue.Edate);
                comm.Parameters.AddWithValue("@Ecbatchno", oiue.Ecbatchno);
                comm.Parameters.AddWithValue("@Eqty", oiue.Eqty);
                comm.Parameters.AddWithValue("@Enotes", oiue.Enotes);
                comm.Parameters.AddWithValue("@un", un);
                comm.Parameters.Add("@Einstock", SqlDbType.Int, 4);
                comm.Parameters["@Einstock"].Direction = ParameterDirection.Output;
               
                 comm.ExecuteNonQuery();

                Einstock = Convert.ToInt32(comm.Parameters["@Einstock"].Value);
      
                connection.Close();
                return 2;
            }
            else
            {


                if (AttNamLoc == 1)
                {
                    constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
                }
                else if (AttNamLoc == 2)
                {
                    constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
                }
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = constring;
                connection.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "UpdateE";
                comm.Parameters.AddWithValue("@E_id", oiue.Eid);
                comm.Parameters.AddWithValue("@Edate", oiue.Edate);
                comm.Parameters.AddWithValue("@Ecbatchno", oiue.Ecbatchno);
                comm.Parameters.AddWithValue("@Eqty", oiue.Eqty);
                comm.Parameters.AddWithValue("@Enotes", oiue.Enotes);
                comm.Parameters.AddWithValue("@un", un);
                comm.Parameters.Add("@Einstock", SqlDbType.Int, 4);
                comm.Parameters["@Einstock"].Direction = ParameterDirection.Output;
              
              comm.ExecuteNonQuery();
              
                Einstock = Convert.ToInt32(comm.Parameters["@Einstock"].Value);

                connection.Close();
                return 1;

            }




        }


        public int DelE(int id, int AttNamLoc)
        {

            if (AttNamLoc == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
            }
            else if (AttNamLoc == 2)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = constring;
            connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "DelE";
            comm.Parameters.AddWithValue("@E_id", id);
            int i = comm.ExecuteNonQuery();
            connection.Close();
            return i;

        }
        //Egg


        //Egg Sold

        BindingList<EggSoldVO> oves1 = new BindingList<EggSoldVO>();

        EggSoldVO oves2 = null;

        public BindingList<EggSoldVO> ViewES(int pageSize, int pageIndex, int temp, int tempAC, out int recordCount, int AttNamLoc)
        {

            if (AttNamLoc == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
            }
            else if (AttNamLoc == 2)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = constring;
            connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "ViewES";
            comm.Parameters.AddWithValue("@PageIndex", pageIndex);
            comm.Parameters.AddWithValue("@PageSize", pageSize);
            comm.Parameters.AddWithValue("@Temp", temp);
            comm.Parameters.AddWithValue("@TempAC", tempAC);
            comm.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
            comm.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
            SqlDataReader read = comm.ExecuteReader();
            while (read.Read())
            {
                oves2 = new EggSoldVO();

                oves2.ESid = Convert.ToInt32(read["ES_id"].ToString());
                oves2.ESdate = Convert.ToDateTime(read["ESdate"].ToString());
                oves2.ESqty = Convert.ToInt32(read["ESqty"].ToString());
                oves2.ESamt = Convert.ToDecimal(read["ESamt"].ToString());
                oves2.EStotamt = Convert.ToDecimal(read["EStotamt"].ToString());
                oves2.ESinstock = Convert.ToInt32(read["ESinstock"].ToString());
                oves2.ESnotes = read["ESnotes"].ToString();


                oves1.Add(oves2);

            }

            connection.Close();
            recordCount = Convert.ToInt32(comm.Parameters["@RecordCount"].Value);
            return oves1;
        }

        public int InupES(EggSoldVO oiues, string un, out int ESinstock, int AttNamLoc)
        {
            if (oiues.ESid == 0)
            {
                if (AttNamLoc == 1)
                {
                    constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
                }
                else if (AttNamLoc == 2)
                {
                    constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
                }
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = constring;
                connection.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "AddES";
                comm.Parameters.AddWithValue("@ESdate", oiues.ESdate);
                comm.Parameters.AddWithValue("@ESqty", oiues.ESqty);
                comm.Parameters.AddWithValue("@ESamt", oiues.ESamt);
                comm.Parameters.AddWithValue("@EStotamt", oiues.EStotamt);
                comm.Parameters.AddWithValue("@ESnotes", oiues.ESnotes);
                comm.Parameters.Add("@ESinstock", SqlDbType.Int, 4);
                comm.Parameters["@ESinstock"].Direction = ParameterDirection.Output;
                comm.ExecuteNonQuery();

                ESinstock = Convert.ToInt32(comm.Parameters["@ESinstock"].Value);
                connection.Close();
                return 2;
            }
            else
            {
                if (AttNamLoc == 1)
                {
                    constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
                }
                else if (AttNamLoc == 2)
                {
                    constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
                }
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = constring;
                connection.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "UpdateES";
                comm.Parameters.AddWithValue("@ES_id", oiues.ESid);
                comm.Parameters.AddWithValue("@ESdate", oiues.ESdate);
                comm.Parameters.AddWithValue("@ESqty", oiues.ESqty);
                comm.Parameters.AddWithValue("@ESamt", oiues.ESamt);
                comm.Parameters.AddWithValue("@EStotamt", oiues.EStotamt);
                comm.Parameters.AddWithValue("@ESnotes", oiues.ESnotes);
                comm.Parameters.Add("@ESinstock", SqlDbType.Int, 4);
                comm.Parameters["@ESinstock"].Direction = ParameterDirection.Output;
              comm.ExecuteNonQuery();
               
                ESinstock = Convert.ToInt32(comm.Parameters["@ESinstock"].Value);
                connection.Close();
                return 1;

            }

        }


        public int DelES(int id, int AttNamLoc)
        {

            if (AttNamLoc == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
            }
            else if (AttNamLoc == 2)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = constring;
            connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "DelES";
            comm.Parameters.AddWithValue("@ES_id", id);
            int i = comm.ExecuteNonQuery();
            connection.Close();
            return i;

        }
        //Egg Sold






        //Super view

        BindingList<ChicksVO> ovsvc1 = new BindingList<ChicksVO>();

        ChicksVO ovsvc2 = null;
        public BindingList<ChicksVO> ViewSVC(DateTime Startdate, DateTime Enddate, int AttNamLoc)
        {

            if (AttNamLoc == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
            }
            else if (AttNamLoc == 2)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = constring;
            connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "ViewSVC";
            comm.Parameters.AddWithValue("@Startdate", Startdate);
            comm.Parameters.AddWithValue("@Enddate", Enddate);
            SqlDataReader read = comm.ExecuteReader();

            while (read.Read())
            {
                ovsvc2 = new ChicksVO();

                ovsvc2.Cid = Convert.ToInt32(read["C_id"].ToString());
                ovsvc2.Cdate = Convert.ToDateTime(read["Cdate"].ToString());
                ovsvc2.Cbatchno = read["Cbatchno"].ToString();
                ovsvc2.Cbreed = read["Cbreed"].ToString();
                ovsvc2.Cqty = Convert.ToInt32(read["Cqty"].ToString());
                ovsvc2.Ctotalwt = Convert.ToDecimal(read["Ctotalwt"].ToString());
                ovsvc2.Cwmea = read["Cwmea"].ToString();
                ovsvc2.Cavgwt = Convert.ToDecimal(read["Cavgwt"].ToString());
                ovsvc2.Ctotalamt = Convert.ToDecimal(read["Ctotalamt"].ToString());
                ovsvc2.Cavgamt = Convert.ToDecimal(read["Cavgamt"].ToString());
                ovsvc2.Cbate = Convert.ToDateTime(read["Cbate"].ToString());
                ovsvc2.Caid = Convert.ToInt32(read["Caid"].ToString());
                ovsvc2.Cnotes = read["Cnotes"].ToString();


                ovsvc1.Add(ovsvc2);

            }

            connection.Close();
       
            return ovsvc1;
        }
        BindingList<ChicksMotVO> ovsvcm1 = new BindingList<ChicksMotVO>();

        ChicksMotVO ovsvcm2 = null;


        public BindingList<ChicksMotVO> ViewSVCM(DateTime Startdate, DateTime Enddate, int AttNamLoc)
        {

            if (AttNamLoc == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
            }
            else if (AttNamLoc == 2)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = constring;
            connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "ViewSVCM";
            comm.Parameters.AddWithValue("@Startdate", Startdate);
            comm.Parameters.AddWithValue("@Enddate", Enddate);
            SqlDataReader read = comm.ExecuteReader();
            while (read.Read())
            {
                ovsvcm2 = new ChicksMotVO();

                ovsvcm2.CMid = Convert.ToInt32(read["CM_id"].ToString());
                ovsvcm2.CMdate = Convert.ToDateTime(read["CMsdate"].ToString());
                ovsvcm2.CMbatchno = read["CMbatchno"].ToString();
                ovsvcm2.CMqty = Convert.ToInt32(read["CMqty"].ToString());
                ovsvcm2.CMBalqty = Convert.ToInt32(read["CMBalqty"].ToString());
                ovsvcm2.CMaid = Convert.ToInt32(read["CMaid"].ToString());
                ovsvcm2.CMnotes = read["CMnotes"].ToString();
                ovsvcm1.Add(ovsvcm2);
            }

            connection.Close();
            return ovsvcm1;
        }



        BindingList<ChicksSoldVO> ovsvcs1 = new BindingList<ChicksSoldVO>();

        ChicksSoldVO ovsvcs2 = null;


        public BindingList<ChicksSoldVO> ViewSVCS(DateTime Startdate, DateTime Enddate, int AttNamLoc)
        {

            if (AttNamLoc == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
            }
            else if (AttNamLoc == 2)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = constring;
            connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "ViewSVCS";
            comm.Parameters.AddWithValue("@Startdate", Startdate);
            comm.Parameters.AddWithValue("@Enddate", Enddate);
    
            SqlDataReader read = comm.ExecuteReader();
            while (read.Read())
            {
                ovsvcs2 = new ChicksSoldVO();

                ovsvcs2.CSid = Convert.ToInt32(read["CS_id"].ToString());
                ovsvcs2.CSdate = Convert.ToDateTime(read["CSdate"].ToString());
                ovsvcs2.CSbatchno = read["CSbatchno"].ToString();
                ovsvcs2.CSqty = Convert.ToInt32(read["CSqty"].ToString());
                ovsvcs2.CSBalqty = Convert.ToInt32(read["CSBalqty"].ToString());
                ovsvcs2.CStotalwt = Convert.ToDecimal(read["CStotwt"].ToString());
                ovsvcs2.CSwmea = read["CSwmea"].ToString();
                ovsvcs2.CSavgwt = Convert.ToDecimal(read["CSavgwt"].ToString());
                ovsvcs2.CStotalamt = Convert.ToDecimal(read["CStotamt"].ToString());
                ovsvcs2.CSavgamt = Convert.ToDecimal(read["CSavgamt"].ToString());
                ovsvcs2.CSnotes = read["CSnotes"].ToString();


                ovsvcs1.Add(ovsvcs2);

            }

            connection.Close();
          
            return ovsvcs1;



        }



        BindingList<InfraVO> ovsvin1 = new BindingList<InfraVO>();

        InfraVO ovsvin2 = null;


        public BindingList<InfraVO> ViewSVI(DateTime Startdate, DateTime Enddate, int AttNamLoc)
        {
            if (AttNamLoc == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
            }
            else if (AttNamLoc == 2)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = constring;
            connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "ViewSVI";
            comm.Parameters.AddWithValue("@Startdate", Startdate);
            comm.Parameters.AddWithValue("@Enddate", Enddate);

            
            SqlDataReader read = comm.ExecuteReader();
            while (read.Read())
            {
                ovsvin2 = new InfraVO();

                ovsvin2.Iid = Convert.ToInt32(read["I_id"].ToString());
                ovsvin2.Idate = Convert.ToDateTime(read["Idate"].ToString());
                ovsvin2.Iexpensetype = read["Iexpensetype"].ToString();
                ovsvin2.Iamount = Convert.ToDecimal(read["Iamount"].ToString());
                ovsvin2.Inotes = read["Inotes"].ToString();


                ovsvin1.Add(ovsvin2);

            }

            connection.Close();

            return ovsvin1;

        }
        BindingList<FeedVO> ovsvf1 = new BindingList<FeedVO>();

        FeedVO ovsvf2 = null;


        public BindingList<FeedVO> ViewSVF(DateTime Startdate, DateTime Enddate, int AttNamLoc)
        {

            if (AttNamLoc == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
            }
            else if (AttNamLoc == 2)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = constring;
            connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "ViewSVF";
            comm.Parameters.AddWithValue("@Startdate", Startdate);
            comm.Parameters.AddWithValue("@Enddate", Enddate);

            SqlDataReader read = comm.ExecuteReader();
            while (read.Read())
            {
                ovsvf2 = new FeedVO();

                ovsvf2.Fid = Convert.ToInt32(read["F_id"].ToString());
                ovsvf2.Fdate = Convert.ToDateTime(read["Fdate"].ToString());
                ovsvf2.Fbrand = read["Fbrand"].ToString();
                ovsvf2.Fpurno = read["Fpurno"].ToString();
                ovsvf2.Fnob = Convert.ToInt32(read["Fnob"].ToString());
                ovsvf2.Fwt = Convert.ToDecimal(read["Fwt"].ToString());
                ovsvf2.Fwav = Convert.ToDecimal(read["Fwav"].ToString());
                ovsvf2.Fwmea = read["Fwmea"].ToString();
                ovsvf2.Ftotamt = Convert.ToDecimal(read["Ftotamt"].ToString());
                ovsvf2.Favgamt = Convert.ToDecimal(read["Favgamt"].ToString());
                ovsvf2.Fnotes = read["Fnotes"].ToString();
                ovsvf1.Add(ovsvf2);

            }

            connection.Close();
    
            return ovsvf1;

        }



        BindingList<FeedStoVO> ovsvfs1 = new BindingList<FeedStoVO>();

        FeedStoVO ovsvfs2 = null;


        public BindingList<FeedStoVO> ViewSVFS(DateTime Startdate, DateTime Enddate, int AttNamLoc)
        {

            if (AttNamLoc == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
            }
            else if (AttNamLoc == 2)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = constring;
            connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "ViewSVFS";
            comm.Parameters.AddWithValue("@Startdate", Startdate);
            comm.Parameters.AddWithValue("@Enddate", Enddate);

            SqlDataReader read = comm.ExecuteReader();
            while (read.Read())
            {
                ovsvfs2 = new FeedStoVO();

                ovsvfs2.FSid = Convert.ToInt32(read["FS_id"].ToString());
                ovsvfs2.FSsdate = Convert.ToDateTime(read["FSsdate"].ToString());
                ovsvfs2.FSedate = Convert.ToDateTime(read["FSedate"].ToString());
                ovsvfs2.FSfpurno = read["FSpurno"].ToString();
                ovsvfs2.FBrand = read["Fbrand"].ToString();
                ovsvfs2.FSfbn = Convert.ToInt32(read["FSbn"].ToString());
                ovsvfs2.FSbalc = Convert.ToInt32(read["FSbalc"].ToString());
                ovsvfs2.FScbn = read["FScbn"].ToString();
                ovsvfs2.FSnotes = read["FSnotes"].ToString();


                ovsvfs1.Add(ovsvfs2);

            }

            connection.Close();

            return ovsvfs1;



        }



        BindingList<MedicineVO> ovsvm1 = new BindingList<MedicineVO>();

        MedicineVO ovsvm2 = null;


        public BindingList<MedicineVO> ViewSVM(DateTime Startdate, DateTime Enddate, int AttNamLoc)
        {

            if (AttNamLoc == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
            }
            else if (AttNamLoc == 2)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = constring;
            connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "ViewSVM";
            comm.Parameters.AddWithValue("@Startdate", Startdate);
            comm.Parameters.AddWithValue("@Enddate", Enddate);

            SqlDataReader read = comm.ExecuteReader();
            while (read.Read())
            {
                ovsvm2 = new MedicineVO();

                ovsvm2.Mid = Convert.ToInt32(read["M_id"].ToString());
                ovsvm2.Mdate = Convert.ToDateTime(read["Mdate"].ToString());
                ovsvm2.Mpurno = read["Mpurno"].ToString();
                ovsvm2.Mbrand = read["Mbrand"].ToString();
                ovsvm2.Msupplier = read["Msupplier"].ToString();
                ovsvm2.Mnature = read["Mnature"].ToString();
                ovsvm2.Mqty = Convert.ToInt32(read["Mqty"].ToString());
                ovsvm2.Mamt = Convert.ToDecimal(read["Mamt"].ToString());
                ovsvm2.Mnotes = read["Mnotes"].ToString();
                ovsvm1.Add(ovsvm2);
            }

            connection.Close();

            return ovsvm1;

        }
        BindingList<MedicineAppVO> ovsvma1 = new BindingList<MedicineAppVO>();

        MedicineAppVO ovsvma2 = null;


        public BindingList<MedicineAppVO> ViewSVMA(DateTime Startdate, DateTime Enddate, int AttNamLoc)
        {

            if (AttNamLoc == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
            }
            else if (AttNamLoc == 2)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = constring;
            connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "ViewSVMA";
            comm.Parameters.AddWithValue("@Startdate", Startdate);
            comm.Parameters.AddWithValue("@Enddate", Enddate);

            SqlDataReader read = comm.ExecuteReader();
            while (read.Read())
            {
                ovsvma2 = new MedicineAppVO();

                ovsvma2.MAid = Convert.ToInt32(read["MA_id"].ToString());
                ovsvma2.MAsdate = Convert.ToDateTime(read["MAsdate"].ToString());
                ovsvma2.MAedate = Convert.ToDateTime(read["MAedate"].ToString());
                ovsvma2.MApurno = read["MApurno"].ToString();
                ovsvma2.MAbrand = read["MAbrand"].ToString();
                ovsvma2.MAcbatchno = read["MAcbatchno"].ToString();
                ovsvma2.MAnotes = read["MAnotes"].ToString();


                ovsvma1.Add(ovsvma2);

            }

            connection.Close();

            return ovsvma1;



        }

        BindingList<EggVO> ovsve1 = new BindingList<EggVO>();

        EggVO ovsve2 = null;


        public BindingList<EggVO> ViewSVE(DateTime Startdate, DateTime Enddate, int AttNamLoc)
        {

            if (AttNamLoc == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
            }
            else if (AttNamLoc == 2)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = constring;
            connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "ViewSVEP";
            comm.Parameters.AddWithValue("@Startdate", Startdate);
            comm.Parameters.AddWithValue("@Enddate", Enddate);

            SqlDataReader read = comm.ExecuteReader();
            while (read.Read())
            {
                ovsve2 = new EggVO();

                ovsve2.Eid = Convert.ToInt32(read["E_id"].ToString());
                ovsve2.Edate = Convert.ToDateTime(read["Edate"].ToString());
                ovsve2.Ecbatchno = read["Ecbatchno"].ToString();
                ovsve2.Eqty = Convert.ToInt32(read["Eqty"].ToString());
                ovsve2.Einstock = Convert.ToInt32(read["Einstock"].ToString());
                ovsve2.Enotes = read["Enotes"].ToString();


                ovsve1.Add(ovsve2);

            }

            connection.Close();

            return ovsve1;



        }


        BindingList<EggSoldVO> ovsves1 = new BindingList<EggSoldVO>();

        EggSoldVO ovsves2 = null;


        public BindingList<EggSoldVO> ViewSVES(DateTime Startdate, DateTime Enddate, int AttNamLoc)
        {
            if (AttNamLoc == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
            }
            else if (AttNamLoc == 2)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = constring;
            connection.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connection;
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandText = "ViewSVES";
            comm.Parameters.AddWithValue("@Startdate", Startdate);
            comm.Parameters.AddWithValue("@Enddate", Enddate);

            SqlDataReader read = comm.ExecuteReader();
            while (read.Read())
            {
                ovsves2 = new EggSoldVO();

                ovsves2.ESid = Convert.ToInt32(read["ES_id"].ToString());
                ovsves2.ESdate = Convert.ToDateTime(read["ESdate"].ToString());
                ovsves2.ESqty = Convert.ToInt32(read["ESqty"].ToString());
                ovsves2.ESamt = Convert.ToDecimal(read["ESamt"].ToString());
                ovsves2.EStotamt = Convert.ToDecimal(read["EStotamt"].ToString());
                ovsves2.ESinstock = Convert.ToInt32(read["ESinstock"].ToString());
                ovsves2.ESnotes = read["ESnotes"].ToString();


                ovsves1.Add(oves2);

            }

            connection.Close();

            return ovsves1;
        }
        public void Savesvcheckbox(SVcheckbox svob, int AttNamLoc)
        {
            if (AttNamLoc == 1)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
            }
            else if (AttNamLoc == 2)
            {
                constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
            }
            SqlConnection connection = new SqlConnection();
                connection.ConnectionString = constring;
                connection.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "AddSVCB";
                comm.Parameters.AddWithValue("@C", svob.C);
            comm.Parameters.AddWithValue("@CM", svob.CM);
            comm.Parameters.AddWithValue("@CS", svob.CS);
            comm.Parameters.AddWithValue("@I", svob.I);
            comm.Parameters.AddWithValue("@F", svob.F);
            comm.Parameters.AddWithValue("@FS", svob.FS);
            comm.Parameters.AddWithValue("@M", svob.M);
            comm.Parameters.AddWithValue("@MA", svob.MA);
            comm.Parameters.AddWithValue("@EP", svob.EP);
            comm.Parameters.AddWithValue("@ES", svob.ES);

            comm.ExecuteNonQuery();

              
                connection.Close();
               
          

            }



        BindingList<Quicknotes> ovqn1 = new BindingList<Quicknotes>();

        Quicknotes ovqn2 = null;




        public BindingList<Quicknotes> ViewQN(DateTime Startdate, DateTime Enddate, int AttNamLoc)
        {
            try
            {
                if (AttNamLoc == 1)
                {
                    constring = ConfigurationManager.ConnectionStrings["LocalconnectionAttur"].ConnectionString;
                }
                else if (AttNamLoc == 2)
                {
                    constring = ConfigurationManager.ConnectionStrings["LocalconnectionNamakkal"].ConnectionString;
                }
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = constring;
                connection.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "ViewQN";
                comm.Parameters.AddWithValue("@Startdate", Startdate);
                comm.Parameters.AddWithValue("@Enddate", Enddate);

                SqlDataReader read = comm.ExecuteReader();
                while (read.Read())
                {
                    ovqn2 = new Quicknotes();

                    ovqn2.Notid = Convert.ToInt32(read["Not_id"].ToString());
                    ovqn2.Notdate = Convert.ToDateTime(read["Not_date"].ToString());
                    ovqn2.Notdes = read["Not_desc"].ToString();



                    ovqn1.Add(ovqn2);

                }

                connection.Close();

                return ovqn1;

            }
            catch (Exception e)
            {
                return ovqn1;
            }
          


        }




        

    }

    }

