<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FinalProject._Default" %>
<%@ Register TagPrefix="myControl" TagName="SiteCompareTool" Src="~/SiteCompareUserTool.ascx" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Service Directory</h1>
    <table class="table-bordered table table-hover table-striped">
        <thead>
            <tr>
                <th>Component Type</th>
                <th>User</th>
                <th>Description</th>
                <th>Demo</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>Global.asax: Event Handler</td>
                <td>Tydin Jarman</td>
                <td>
                     This counter increases for each session start
                </td>
                <td>
                    
                     <asp:Label runat="server" ID="SessionCounterTextBox"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>DLL Class Library: Encrpytion and Decrpytion</td>
                <td>Tydin Jarman</td>
                <td>Encrpytion/Decrpytion to be used for authentication</td>
                <td>
                    <asp:Label runat="server" ID="PasswordDemoLabel">Enter password</asp:Label>
                    <asp:TextBox runat="server" ID="PasswordTextBox"></asp:TextBox>
                    <asp:Button runat="server" Text="Run" OnClick="PasswordDemo_Click" /> <br />
                    <asp:Label runat="server" ID="PasswordDemoResult"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>User Control & Web Service: Service - Site Compare Tool</td>
                <td>Tydin Jarman</td>
                <td>Give a score based on how close two sites are</td>
                <td>
                      <myControl:SiteCompareTool runat="server"></myControl:SiteCompareTool>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
