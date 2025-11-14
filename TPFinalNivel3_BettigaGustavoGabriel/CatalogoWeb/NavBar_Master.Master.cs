using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CatalogoWeb
{
    public partial class NavBar_Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear(); //BORRAMOS TODO PORQUE NO QUEREMOS NADA QUE CORRESPONDA A ESE USUARIO
            Response.Redirect("Login.aspx");
        }
    }
}