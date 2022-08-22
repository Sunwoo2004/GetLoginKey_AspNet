using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GetLoginKey_WebClient
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string userID = Request.QueryString["userID"];
            string userPWD = Request.QueryString["userPWD"];

            if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(userPWD))
            {
                Response.Write("errorcode1");
                return;
            }

            string result = SeleniumHelper.GetLoginKey(userID, userPWD);

            Response.Write(result);
        }
    }
}