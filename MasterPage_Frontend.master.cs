using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class MasterPage_Frontend : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BuildMenu();
    }

    /// <summary>
    /// Konstruer hele menuen på Frontend. Der hentes kategorier og det aktive menupunkt fremhæves
    /// </summary>
    private void BuildMenu()
    {
        // link til forsiden
        MenuItem item = new MenuItem("Forside", "", "", "~/Default.aspx");
        // hvis den nuværende URL er default eller tom, sættes dette element til aktiv
        item.Selected = (Request.RawUrl == "/Default.aspx" || Request.RawUrl == "/");
        // tilføj link til menu kontrollen
        Menu_Frontend.Items.Add(item);

        // link til kontakt siden
        item = new MenuItem("Kontakt", "", "", "~/Contact.aspx");
        item.Selected = (Request.RawUrl == "/Contact.aspx"); ;
        Menu_Frontend.Items.Add(item);

        // link til kategorierne 
        SqlConnection conn = new SqlConnection(Helpers.ConnectionString);
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "SELECT category_id, category_title FROM categories ORDER BY category_title ASC";

        conn.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            item = new MenuItem(reader["category_title"].ToString(), "", "", "~/Categories.aspx?category_id=" + reader["category_id"]);
            // hvis URL indeholder "category_id=XXX" hvor XXX er den der udskrives lige nu,
            // så sættes dette menupunkt til at være aktiv
            item.Selected = (Request.RawUrl.Contains("category_id=" + reader["category_id"]));
            Menu_Frontend.Items.Add(item);
        }
        conn.Close();

        // link til login/logud
        if (Session["user_id"] != null)
        {

            item = new MenuItem("Logud <i class='icon-unlock'></i>", "", "", "~/Login.aspx?action=logout");
            Menu_Frontend.Items.Add(item);
            if (Convert.ToInt32(Session["role_access"]) >= 10)
            {
                item = new MenuItem("Administration <i class='icon-cogs'></i>", "", "", "~/Admin/Default.aspx");
                Menu_Frontend.Items.Add(item);
            }
        }
        else
        {
            item = new MenuItem("Login <i class='icon-lock'></i>", "", "", "~/Login.aspx");
            item.Selected = (Request.RawUrl == "/Login.aspx");
            Menu_Frontend.Items.Add(item);
        }
    }


}
