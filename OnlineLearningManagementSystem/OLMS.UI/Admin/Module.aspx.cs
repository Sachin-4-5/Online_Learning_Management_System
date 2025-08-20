using OLMS.BLL.Services;
using OLMS.Models.Entities;
using System;
using System.Web.UI.WebControls;

namespace OLMS.UI.Admin
{
    public partial class Module : System.Web.UI.Page
    {
        private readonly ModuleService _moduleService;
        private readonly CourseService _courseService; // To load course dropdown

        public Module()
        {
            _moduleService = new ModuleService();
            _courseService = new CourseService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCourses();
            }
        }

        private void LoadCourses()
        {
            var courses = _courseService.GetAll();
            ddlCourses.DataSource = courses;
            ddlCourses.DataTextField = "Title";
            ddlCourses.DataValueField = "CourseID";
            ddlCourses.DataBind();
            ddlCourses.Items.Insert(0, new ListItem("-- Select Course --", "0"));
        }

        protected void ddlCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(ddlCourses.SelectedValue, out int courseId) && courseId > 0)
            {
                BindModules(courseId);
            }
            else
            {
                gvModules.DataSource = null;
                gvModules.DataBind();
            }
        }

        private void BindModules(int courseId)
        {
            var modules = _moduleService.GetByCourseId(courseId);
            gvModules.DataSource = modules ?? new System.Collections.Generic.List<OLMS.Models.Entities.Module>();
            gvModules.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (int.TryParse(ddlCourses.SelectedValue, out int courseId) && courseId > 0)
            {
                int sortOrder = 0;
                int.TryParse(txtSortOrder.Text.Trim(), out sortOrder);

                var module = new OLMS.Models.Entities.Module
                {
                    CourseID = courseId,
                    Title = txtTitle.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    SortOrder = sortOrder
                };

                _moduleService.Add(module);
                BindModules(courseId);

                // clear form
                txtTitle.Text = txtDescription.Text = txtSortOrder.Text = string.Empty;
            }
        }

        protected void gvModules_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvModules.EditIndex = e.NewEditIndex;
            BindModules(int.Parse(ddlCourses.SelectedValue));
        }

        protected void gvModules_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int moduleId = Convert.ToInt32(gvModules.DataKeys[e.RowIndex].Value);
            GridViewRow row = gvModules.Rows[e.RowIndex];

            string title = ((TextBox)row.Cells[1].Controls[0]).Text;
            string description = ((TextBox)row.Cells[2].Controls[0]).Text;

            int sortOrder = 0;
            int.TryParse(((TextBox)row.Cells[3].Controls[0]).Text, out sortOrder);

            var module = new OLMS.Models.Entities.Module
            {
                ModuleID = moduleId,
                CourseID = int.Parse(ddlCourses.SelectedValue),
                Title = title,
                Description = description,
                SortOrder = sortOrder
            };

            _moduleService.Update(module);
            gvModules.EditIndex = -1;
            BindModules(module.CourseID);
        }

        protected void gvModules_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvModules.EditIndex = -1;
            BindModules(int.Parse(ddlCourses.SelectedValue));
        }

        protected void gvModules_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int moduleId = Convert.ToInt32(gvModules.DataKeys[e.RowIndex].Value);
            _moduleService.Delete(moduleId);
            BindModules(int.Parse(ddlCourses.SelectedValue));
        }
    }
}