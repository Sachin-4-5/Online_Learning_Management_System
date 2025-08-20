using OLMS.BLL.Services;
using OLMS.DAL.Repositories;
using OLMS.Models.Entities;
using System;
using System.Web.UI.WebControls;

namespace OLMS.UI.Admin
{
    public partial class Lesson : System.Web.UI.Page
    {
        private LessonService _lessonService;
        private ModuleService _moduleService;

        protected void Page_Load(object sender, EventArgs e)
        {
            _lessonService = new LessonService(new LessonRepository());
            _moduleService = new ModuleService();

            if (!IsPostBack)
            {
                LoadModules();
            }
        }

        private void LoadModules()
        {
            var modules = _moduleService.GetAll(); // Or filter by course if needed
            ddlModules.DataSource = modules;
            ddlModules.DataTextField = "Title";
            ddlModules.DataValueField = "ModuleID";
            ddlModules.DataBind();
            ddlModules.Items.Insert(0, new ListItem("-- Select Module --", "0"));
        }

        private void LoadLessons()
        {
            if (ddlModules.SelectedValue == "0") return;

            int moduleId = int.Parse(ddlModules.SelectedValue);
            gvLessons.DataSource = _lessonService.GetByModuleId(moduleId);
            gvLessons.DataBind();
        }

        protected void ddlModules_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLessons();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (ddlModules.SelectedValue == "0") return;

            if (!int.TryParse(txtSortOrder.Text.Trim(), out int sortOrder))
                sortOrder = 0;

            OLMS.Models.Entities.Lesson newLesson = new OLMS.Models.Entities.Lesson
            {
                ModuleID = int.Parse(ddlModules.SelectedValue),
                Title = txtTitle.Text.Trim(),
                Content = txtContent.Text.Trim(),
                VideoUrl = txtVideoUrl.Text.Trim(),
                SortOrder = sortOrder
            };

            _lessonService.Add(newLesson);
            ClearForm();
            LoadLessons();
        }

        private void ClearForm()
        {
            txtTitle.Text = "";
            txtContent.Text = "";
            txtVideoUrl.Text = "";
            txtSortOrder.Text = "";
        }

        protected void gvLessons_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvLessons.EditIndex = e.NewEditIndex;
            LoadLessons();
        }

        protected void gvLessons_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvLessons.EditIndex = -1;
            LoadLessons();
        }

        protected void gvLessons_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int lessonId = (int)gvLessons.DataKeys[e.RowIndex].Value;
            GridViewRow row = gvLessons.Rows[e.RowIndex];

            string title = ((TextBox)row.Cells[1].Controls[0]).Text;
            string content = ((TextBox)row.Cells[2].Controls[0]).Text;
            string videoUrl = ((TextBox)row.Cells[3].Controls[0]).Text;
            int sortOrder = int.TryParse(((TextBox)row.Cells[4].Controls[0]).Text, out int so) ? so : 0;

            OLMS.Models.Entities.Lesson updatedLesson = new OLMS.Models.Entities.Lesson
            {
                LessonID = lessonId,
                ModuleID = int.Parse(ddlModules.SelectedValue),
                Title = title,
                Content = content,
                VideoUrl = videoUrl,
                SortOrder = sortOrder
            };

            _lessonService.Update(updatedLesson);
            gvLessons.EditIndex = -1;
            LoadLessons();
        }

        protected void gvLessons_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int lessonId = (int)gvLessons.DataKeys[e.RowIndex].Value;
            _lessonService.Delete(lessonId);
            LoadLessons();
        }
    }
}