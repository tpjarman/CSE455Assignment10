<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteCompareUserTool.ascx.cs" Inherits="FinalProject.SiteCompareUserTool" %>
<asp:Label runat="server" Id="Website1Label">Website 1</asp:Label>
<asp:TextBox runat="server" ID="Website1TextBox" style="margin-left: 39px; margin-top: 0px" Width="250px"></asp:TextBox>
<br />
<asp:Label runat="server" Id="Website2Label">Website 2</asp:Label>
<asp:TextBox runat="server" ID="Website2TextBox" style="margin-left: 38px" Width="250px"></asp:TextBox>
<p>
<asp:Button runat="server" Text="Compare!" OnClick="CompareSites_Click" />
</p>
<asp:Label runat="server" ID="WebsiteResultLabel">Results:</asp:Label>