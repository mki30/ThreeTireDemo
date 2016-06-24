using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BEL_BookApp;

namespace DAL_BookApp
{
    public class BooksDetails_DAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DemoData"].ConnectionString);
        public Int32 SaveBookDetails(BooksDetails_BEL objBEL)
        {
            int result;
            try
            {
                //SqlCommand cmd = new SqlCommand("InsertBookDetails_SP", con);
                //cmd.CommandType = CommandType.StoredProcedure
                SqlCommand cmd = new SqlCommand("Insert into BookDetails(BookName,Author,Publisher,Price) values(@BookName,@Author,@Publisher,@Price)", con);
                cmd.Parameters.AddWithValue("@BookName", objBEL.BookName);
                cmd.Parameters.AddWithValue("@Author", objBEL.Author);
                cmd.Parameters.AddWithValue("@Publisher", objBEL.Publisher);
                cmd.Parameters.AddWithValue("@Price", objBEL.Price);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (result > 0)
                {
                    return result;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }

        public DataSet GetBookRecords()
        {
            DataSet ds = new DataSet();
            try
            {
                //SqlCommand cmd = new SqlCommand("FetchBookRecords_Sp", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                SqlCommand cmd = new SqlCommand("Select * from BookDetails", con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                ds.Dispose();
            }
            return ds;
        }

        public Int32 DeleteBookRecord(BooksDetails_BEL objBEL)
        {
            int result;
            try
            {
                //SqlCommand cmd = new SqlCommand("DeleteBookRecords_Sp", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                SqlCommand cmd = new SqlCommand("Delete from BookDetails where BookId=@BookId", con);
                cmd.Parameters.AddWithValue("@BookId", objBEL.BookId);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (result > 0)
                {
                    return result;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }

        public Int32 UpdateBookRecord(BooksDetails_BEL objBEL)
        {
            int result;
            try
            {
                //SqlCommand cmd = new SqlCommand("UpdateBookRecord_SP", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                SqlCommand cmd = new SqlCommand("Update BookDetails Set BookName=@BookName,Author=@Author,Publisher=@Publisher,Price=@Price", con);
                //cmd.Parameters.AddWithValue("@BookId", objBEL.BookId);
                cmd.Parameters.AddWithValue("@BookName", objBEL.BookName);
                cmd.Parameters.AddWithValue("@Author", objBEL.Author);
                cmd.Parameters.AddWithValue("@Publisher", objBEL.Publisher);
                cmd.Parameters.AddWithValue("@Price", objBEL.Price);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                if (result > 0)
                {
                    return result;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }
        }
    }
}