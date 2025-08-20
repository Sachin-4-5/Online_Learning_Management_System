<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TakeQuiz.aspx.cs" Inherits="OLMS.UI.Student.TakeQuiz" MasterPageFile="~/MasterPages/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="card shadow-sm border-0">
            <div class="card-header bg-primary text-white">
                <h4 class="mb-0">🎯 Take Quiz</h4>
            </div>

            <div class="card-body">
                <!-- Quiz selection dropdown -->
                <div class="form-group mb-3">
                    <label for="ddlQuizzes" class="form-label fw-bold">Select a Quiz</label>
                    <asp:DropDownList ID="ddlQuizzes" runat="server" CssClass="form-select"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlQuizzes_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>

                <!-- Questions panel -->
                <asp:Panel ID="pnlQuestions" runat="server" Visible="false">
                    <div id="quiz-questions" class="mt-4">
                        <asp:PlaceHolder ID="phQuestions" runat="server"></asp:PlaceHolder>
                    </div>

                    <div class="d-flex justify-content-end mt-4">
                        <asp:Button ID="btnSubmitQuiz" runat="server" Text="Submit Quiz"
                            CssClass="btn btn-success px-4" OnClick="btnSubmitQuiz_Click" />
                    </div>
                </asp:Panel>

                <!-- Status / Message -->
                <div class="mt-3">
                    <asp:Label ID="lblMessage" runat="server" CssClass="text-success fw-bold"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>