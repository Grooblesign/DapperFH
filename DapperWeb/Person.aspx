<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Person.aspx.cs" Inherits="DapperWeb.Person" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Person</title>
    <link href="StyleSheet.css" rel="stylesheet" />
</head>
<body>
    <div id="exceptionDiv" runat="server">
        <h3 id="exception" runat="server"></h3>
    </div>
    <h1 id="Name" runat="server"></h1>
    <br />
    <form id="form1" runat="server">
    <div id="mainDiv" runat="server">
        <div id="parentsDiv" runat="server">
        </div>
        <br />
        <div id="eventsDiv" runat="server">
        </div>
        <br />
        <div id="censusDiv" runat="server">
        </div>
        <div id="childrenDiv" runat="server">
        </div>
        <br />
    </div>
    </form>
</body>
</html>
