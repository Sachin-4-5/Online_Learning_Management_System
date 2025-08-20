using OLMS.BLL.Services;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace OLMS.UI.Admin
{
    public partial class Reports : System.Web.UI.Page
    {
        private ReportService _reportService;

        protected void Page_Load(object sender, EventArgs e)
        {
            // ✅ Always initialize service (not just on first load)
            _reportService = new ReportService();

            if (!IsPostBack)
            {
                BindCourses();
                BindWorkshops();
            }
        }

        private void BindCourses()
        {
            ddlCourses.DataSource = _reportService.GetAllCourses();
            ddlCourses.DataTextField = "Title";
            ddlCourses.DataValueField = "CourseID";
            ddlCourses.DataBind();
            ddlCourses.Items.Insert(0, new ListItem("--Select Course--", "0"));
        }

        private void BindWorkshops()
        {
            ddlWorkshops.DataSource = _reportService.GetAllWorkshops();
            ddlWorkshops.DataTextField = "Title";
            ddlWorkshops.DataValueField = "WorkshopID";
            ddlWorkshops.DataBind();
            ddlWorkshops.Items.Insert(0, new ListItem("--Select Workshop--", "0"));
        }

        protected void ddlCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            int courseId = Convert.ToInt32(ddlCourses.SelectedValue);
            gvReport.DataSource = courseId > 0 ? _reportService.GetQuizResultsByCourse(courseId) : null;
            gvReport.DataBind();
        }

        protected void ddlWorkshops_SelectedIndexChanged(object sender, EventArgs e)
        {
            int workshopId = Convert.ToInt32(ddlWorkshops.SelectedValue);
            gvReport.DataSource = workshopId > 0 ? _reportService.GetMaterialsByWorkshop(workshopId) : null;
            gvReport.DataBind();
        }
    }
}