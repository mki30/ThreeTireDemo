using System;
using System.Web.UI.WebControls;
using BEL_BookApp;
using BLL_BookApp;
using System.Data;

namespace ThreeTireDemo
{
    public partial class Default : System.Web.UI.Page
    {
        BooksDetails_BLL objBookDetailsBLL = new BooksDetails_BLL();
        BooksDetails_BEL objBookDetailsBEL = new BooksDetails_BEL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            BindGridView();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            objBookDetailsBEL.BookName = txtBookName.Text;
            objBookDetailsBEL.Author = txtAuthor.Text;
            objBookDetailsBEL.Price = Cmn.ToInt(txtPrice.Text);
            objBookDetailsBEL.Publisher = txtPublisher.Text;
            int ret = objBookDetailsBLL.SaveBookDetails(objBookDetailsBEL);
            try
            {
                if (ret > 0)
                {
                    lblStatus.Text = "Book detail saved successfully";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                    ClearControls();
                }
                else
                {
                    lblStatus.Text = "Book details couldn't be saved";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Oops! error occured :" + ex.Message.ToString();
            }
            finally
            {
                objBookDetailsBEL = null;
                objBookDetailsBLL = null;
            }
        }

        private void BindGridView()
        {
            DataSet ds = new DataSet();
            try
            {
                ds = objBookDetailsBLL.GetBookRecords();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdBookDetails.DataSource = ds;
                    grdBookDetails.DataBind();
                }
                else
                {
                    grdBookDetails.DataSource = null;
                    grdBookDetails.DataBind();
                }
            }
            catch (Exception ex) 
            {
                lblStatus.Text = "Oops! error occured :" + ex.Message.ToString();
            }
            finally 
            {
                objBookDetailsBEL = null;
                objBookDetailsBLL = null;
            }
        }
        private void ClearControls()
        {
            txtBookName.Text = string.Empty;
            txtAuthor.Text = string.Empty;
            txtPublisher.Text = string.Empty;
            txtPrice.Text = string.Empty;
            txtBookName.Focus();
        }

        protected void grdBookDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void grdBookDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdBookDetails.EditIndex = -1;
            BindGridView();
        }

        protected void grdBookDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int Book_Id = Convert.ToInt32(grdBookDetails.DataKeys[e.RowIndex].Value);
            objBookDetailsBEL.BookId = Book_Id;
            try
            {
                int retVal = objBookDetailsBLL.DeleteBookRecord(objBookDetailsBEL);

                if (retVal > 0)
                {
                    lblStatus.Text = "Book detail deleted successfully";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                    ClearControls();
                    BindGridView();
                }
                else
                {
                    lblStatus.Text = "Book details couldn't be deleted";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                Response.Write("Oops! error occured :" + ex.Message.ToString());
            }
            finally
            {
                objBookDetailsBEL = null;
                objBookDetailsBLL = null;
            }
        }

        protected void grdBookDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdBookDetails.EditIndex = e.NewEditIndex;
            BindGridView();
        }

        protected void grdBookDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            objBookDetailsBEL.BookId = Convert.ToInt32(grdBookDetails.DataKeys[e.RowIndex].Value);
            objBookDetailsBEL.BookName = ((TextBox)(grdBookDetails.Rows[e.RowIndex].FindControl("txtBookNameEdit"))).Text.Trim();
            objBookDetailsBEL.Author = ((TextBox)(grdBookDetails.Rows[e.RowIndex].FindControl("txtAuthorEdit"))).Text.Trim();
            objBookDetailsBEL.Publisher = ((TextBox)(grdBookDetails.Rows[e.RowIndex].FindControl("txtPublisherEdit"))).Text.Trim();
            objBookDetailsBEL.Price = Convert.ToDecimal(((TextBox)(grdBookDetails.Rows[e.RowIndex].FindControl("txtPriceEdit"))).Text.Trim());

            try
            {
                int retVal = objBookDetailsBLL.UpdateBookRecord(objBookDetailsBEL);
                if (retVal > 0)
                {
                    lblStatus.Text = "Book detail updated successfully";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                    ClearControls();
                    grdBookDetails.EditIndex = -1;
                    BindGridView();
                }
                else
                {
                    lblStatus.Text = "Book details couldn't be updated";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                Response.Write("Oops! error occured :" + ex.Message.ToString());
            }
            finally
            {
                objBookDetailsBEL = null;
                objBookDetailsBLL = null;
            }

        }


    }
}