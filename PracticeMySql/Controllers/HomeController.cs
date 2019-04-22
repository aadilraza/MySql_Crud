using System.Linq;
using System.Web.Mvc;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace PracticeMySql.Controllers
{
    public class HomeController : Controller
    {
        private string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public ActionResult Index()
        {
            StudentTableEntities entites = new StudentTableEntities();
            var List = entites.students.ToList();
            Fetch3();
            return View();      
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public void Fetch()
        {
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM mysql_mvc.student"))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                        }
                    }
                }
            }
        }

        public void Fetch2()
        {
            using (var ctx = new StudentTableEntities())
            {
                var studentList = ctx.students
                                    .SqlQuery("SELECT * FROM mysql_mvc.student Where StudentName = 'Andy'")
                                    .ToList<student>();
            }
        }
        public void Fetch3()
        {
            var Dt = fillDataTable("SELECT * FROM mysql_mvc.student");
        }

        public DataTable fillDataTable(string query)
        {
            using (MySqlConnection sqlConn = new MySqlConnection(constr))
            using (MySqlCommand cmd = new MySqlCommand(query, sqlConn))
            {
                sqlConn.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
        }
    }
}