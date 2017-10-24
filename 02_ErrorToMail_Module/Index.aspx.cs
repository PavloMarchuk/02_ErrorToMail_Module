using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _02_ErrorToMail_Module
{
	public partial class Index : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			//
		}

		protected void Button1_Click(object sender, EventArgs e)
		{
			throw new Exception("@@@@@@@@@@@@@моя помилка@@@@@@@@@@@@@@@@@");
		}
	}
}