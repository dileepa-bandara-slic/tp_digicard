<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" Async="true" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript"> //language="javascript" 
        $(function () {
            today = new Date();
            var month, day, year;
            year = today.getFullYear();
            month = today.getMonth();
            date = today.getDate();
            year = today.getFullYear() - 60;
            var backdate = new Date(year, month, date)
            $("input[id$='Txtboxfrom']").datepicker({ dateFormat: "yy/mm/dd", changeMonth: true, changeYear: true, maxDate: 0 });
            $("input[id$='TxtboxTo']").datepicker({ dateFormat: "yy/mm/dd", changeMonth: true, changeYear: true, maxDate: 0 });
        });

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            today = new Date();
            var month, day, year;
            year = today.getFullYear();
            month = today.getMonth();
            date = today.getDate();
            year = today.getFullYear() - 60;
            var backdate = new Date(year, month, date)

            function EndRequestHandler(sender, args) {
                $("input[id$='Txtboxfrom']").datepicker({ dateFormat: "yy/mm/dd", changeMonth: true, changeYear: true, maxDate: 0 });
                $("input[id$='TxtboxTo']").datepicker({ dateFormat: "yy/mm/dd", changeMonth: true, changeYear: true, maxDate: 0 });


            }

        });
    </script>

    <style type="text/css">
        .blck1 {
            width: 95%;
            text-align: left;
        }

        div.scroll {
            height: 300px;
            overflow: auto;
            overflow-y: scroll;
            text-align: justify;
            width: 80%;
            background-color: lightgray;
            border-width: 1px;
            border-color: darkslateblue;
            border-style: solid;
        }

        .divWaiting {
            position: absolute;
            background-color: lightgray;
            z-index: 2147483647 !important;
            opacity: 0.80;
            overflow: hidden;
            top:30vh;
            left:30vw;
            height: 300px;
            width: 40%;
            vertical-align:central !important;
            text-align:center;
            padding-top: 7.5%;
            border-radius:5px;
            border-color: #808080;
        }

.GridPager a,
        .GridPager span {
            display: inline-block;
            padding: 0px 9px;
            margin-right: 4px;
            border-radius: 3px;
            border: solid 1px #c0c0c0;
            background: #e9e9e9;
            box-shadow: inset 0px 1px 0px rgba(255,255,255, .8), 0px 1px 3px rgba(0,0,0, .1);
            font-size: .875em;
            font-weight: bold;
            text-decoration: none;
            color: #717171;
            text-shadow: 0px 1px 0px rgba(255,255,255, 1);
        }

        .GridPager a {
            background-color: #f5f5f5;
            color: #969696;
            border: 1px solid #969696;
        }

        .GridPager span {
            background: #ff3385;
            box-shadow: inset 0px 0px 8px rgba(0,0,0, .5), 0px 1px 0px rgba(255,255,255, .8);
            color: #404040;
            text-shadow: 0px 0px 3px rgba(0,0,0, .5);
            border: 1px solid #339966;
        }

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="600"></asp:ScriptManager>&nbsp;
    <asp:UpdatePanel ID="Ggs14451" runat="server">

        <ContentTemplate>
            <div class="container-fluid jumbotron" style="padding: 25px 0px 0px 30px; margin: 0 5px 0 5px">
                <table>
                    <tr style="height: 45px">
                        <td>
                            <asp:Label ID="lbl_Branch" runat="server" Text="Branch" Width="125"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownBranch" runat="server" CssClass="form-control textbox" AppendDataBoundItems="True" Width="325">
                                <asp:ListItem Value="0">All</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr style="height: 45px">
                        <td>
                            <asp:Label ID="lbl_PolicyNo" runat="server" Text="Policy&nbsp;No."></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtpolno" runat="server" CssClass="form-control textbox" Width="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="height: 45px">
                        <td>
                            <asp:Label ID="lbl_VehNo" runat="server" Text="Vehicle&nbsp;No."></asp:Label>
                        </td>
                        <td class="row">
                            <table>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="DropDownProvince" runat="server" CssClass="form-control textbox" Width="75" AppendDataBoundItems="True">
                                            <asp:ListItem Value="0">All</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtVehicleNo_1" PlaceHolder="AAA" runat="server" CssClass="form-control textbox" MaxLength="3" Width="50"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtVehicleNo_2" PlaceHolder="1000" runat="server" CssClass="form-control textbox" MaxLength="4" Width="75"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height: 45px">
                        <td>
                            <asp:Label ID="lbl_Dt" runat="server" Text="Date&nbsp;From"></asp:Label>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="Txtboxfrom" PlaceHolder="YYYY/MM/DD" runat="server" autocomplete="off" CssClass="form-control textbox" Width="110"></asp:TextBox>
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lbl_To" runat="server" Text="&nbsp;&nbsp;&nbsp;To&nbsp;&nbsp;&nbsp;"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtboxTo" PlaceHolder="YYYY/MM/DD" runat="server" autocomplete="off" CssClass=" form-control textbox" Width="110"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="&nbsp;&nbsp;&nbsp;Please Enter a Date Range." Font-Bold="True" Font-Names="Calibri" Font-Size="10pt" ForeColor="Red" OnServerValidate="check_eithr">
                                        </asp:CustomValidator>
                                    </td>
                                </tr>
                            </table>

                        </td>
                    </tr>
                    <tr style="height: 45px">
                        <td>
                            <asp:Label ID="lbl_MobileNo" runat="server" Text="Mobile&nbsp;No."></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DDMobile" runat="server" CssClass="form-control" Width="110">
                                <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                                <asp:ListItem Value="Valid">Valid</asp:ListItem>
                                <asp:ListItem Value="Invalid">Invalid</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="height: 45px">
                        <td>
                            <asp:Label ID="lbl_CoverNote" runat="server" Text="Cover&nbsp;note"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DDCoverNote" runat="server" CssClass="form-control" Width="200">
                                <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                                <asp:ListItem Value="NoCN">No cover notes</asp:ListItem>
                                <asp:ListItem Value="YesCN">Cover note Issued</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="height: 45px">
                        <td>
                            <asp:Label ID="lbl_NIC" runat="server" Text="NIC&nbsp;No."></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DDNIC" runat="server" CssClass="form-control" Width="110">
                                <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                                <asp:ListItem Value="1">Valid</asp:ListItem>
                                <asp:ListItem Value="2">Invalid</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="height: 45px">
                        <td>&nbsp;
                        </td>
                        <td>
                            <asp:Label ID="txterror" runat="server" Style="text-align: left" Font-Bold="True" Font-Names="Calibri" Font-Size="10pt" ForeColor="Red" Width="400"></asp:Label>
                        </td>
                    </tr>
                    <tr style="height: 45px">
                        <td>&nbsp;
                        </td>
                        <td>
                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" OnClick="Button1_Click" Text="Search" />
                        </td>
                    </tr>
                    
                    <asp:PlaceHolder ID="ph_grid" runat="server" Visible="false">
                        <tr style="height: 50px; margin:0px 0px 15px 0px">
                            <td>&nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btn_nxt" Text="Select 100 Records" OnClick="btn_nxt_Click" CssClass="btn btn-danger" runat="server" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btn_non" Text="Select None" OnClick="btn_non_Click" CssClass="btn btn-default" runat="server" BackColor="Black" ForeColor="White" />&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="Button2" runat="server" Text="Send SMS" CssClass="btn btn-warning" OnClick="Button2_Click" />
                            </td>
                        </tr>

                        <tr>
                            <td>&nbsp;
                            </td>
                            <td>
                                <div class="scrol">
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="GridView_Css" AllowPaging="true" PageSize="100" OnPageIndexChanging="GridView1_PageIndexChanging">
                                        <PagerStyle BackColor="#ffb3d1" CssClass = "GridPager" HorizontalAlign="Center" VerticalAlign="Middle" /> <%--CssClass="pagination"--%> 
                                    <PagerSettings Mode="NumericFirstLast" Position="Bottom" LastPageImageUrl="~/Images/last.png" FirstPageImageUrl="~/Images/first.png" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="No.&nbsp;">
                                                <ItemTemplate>
                                                    <%-- <%# Container.DataItemIndex + 1 %> --%>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField HeaderText="Policy&nbsp;No&nbsp;" DataField="pi_policyno" />
                                            <asp:BoundField HeaderText="Br&nbsp;No&nbsp;" DataField="pi_bracode" />
                                            <asp:BoundField HeaderText="Vehicle&nbsp;No&nbsp;" DataField="vehno" />
                                            <asp:BoundField HeaderText="Premium&nbsp;" DataField="netprm" />
                                            <asp:BoundField HeaderText="Com.&nbsp;Date&nbsp;" DataField="datcomm" />
                                            <%--<asp:BoundField HeaderText="Mobile No" DataField="MobileNo" SortExpression="MobileNo" />--%>
                                            <asp:BoundField HeaderText="Name&nbsp;" DataField="Name" />
                                            <asp:BoundField HeaderText="NIC&nbsp;" DataField="nic_no" />
                                            <asp:BoundField HeaderText="Covernote&nbsp;" DataField="covernote" />
                                            <asp:BoundField HeaderText="CN&nbsp;Reason&nbsp;" DataField="cnreason" />
                                            <asp:TemplateField HeaderText="Mobile&nbsp;No&nbsp;" SortExpression="MobileNo">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtMobile" runat="server"
                                                        Text='<%# Bind("pi_teleno") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtMobileE" runat="server"
                                                        Text='<%# Bind("pi_teleno") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="SMS&nbsp;" ItemStyle-HorizontalAlign="Center">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSMS" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Message&nbsp;">
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblMsg" runat="server" Text='<%# Bind("rec_exists") %>'></asp:Label>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMsgE" runat="server" Text='<%# Bind("rec_exists") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#808080" />
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>

                        <%--<tr style="height: 45px">
                            <td>&nbsp;
                            </td>
                            <td>
                                
                            </td>
                        </tr>--%>

                    </asp:PlaceHolder>
                </table>
                <%--<div style="padding: 25px 0px 0px 10px; margin: 0 5px 0 5px">
                    <asp:PlaceHolder ID="ph_grid_1" runat="server" Visible="false">

                    </asp:PlaceHolder>
                </div>--%>
            </div>

            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="Ggs14451">
                <ProgressTemplate>
                    <div class="divWaiting">
                        <img src="Images/load.gif" /><br />
                        <asp:Label ID="lblWait" runat="server" Text="Fetching Data. Please wait..." />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

