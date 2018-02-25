<%@ Page Title="Contacto" Language="C#" AutoEventWireup="true" MasterPageFile="~/Layout.master" CodeBehind="Contacts.aspx.cs" Inherits="Web.Contacts" %>
<%@ Register Src="~/UserControls/AddCommentForm.ascx" TagPrefix="dx" TagName="AddCommentForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
<div class="container-fluid">
    <div id="map" class="map row"></div>
</div>
<script src="https://cdn.polyfill.io/v2/polyfill.min.js?features=requestAnimationFrame,Element.prototype.classList,URL"></script>
<script src="https://openlayers.org/en/v3.18.2/build/ol.js"></script>
<script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?sensor=false"></script>
<script type="text/javascript">
    function InitializeMap() {
        var latlng = new google.maps.LatLng(10.0030143, -84.1160851);
        var myOptions = {
            zoom: 15,
            center: latlng,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var map = new google.maps.Map(document.getElementById("map"), myOptions);

        var marker = new google.maps.Marker
        (
            {
                position: new google.maps.LatLng(10.0030143, -84.1160851),
                map: map,
                title: 'MSA Soft'
            }
        );
    }

    window.onload = InitializeMap;
</script>
<div class="container">
    <div class="row">
        <section class="col-md-8 marginTop40">
            <h3>Formulario de Contacto</h3>
            <p>Puede enviarnos sus consultas en el siguiente formulario</p>
            <dx:AddCommentForm runat="server"/>
            <hr class="marginTop40 visible-sm visible-xs"/>
        </section>
        <aside class="col-md-4 marginTop40">
            <h3>Dirección</h3>
            <p>Heredia, Costa Rica</p>
            <h3>Servicio al Cliente</h3>
            <ul class="list-unstyled">
                <li><i class="glyphicon glyphicon-phone-alt"></i> + (506) 8872 9065</li>
                <li><i class="glyphicon glyphicon-envelope"></i> msalamanca@msasoft.net</li>
                <li><i class="glyphicon glyphicon-envelope"></i> manchia@msasoft.net</li>
                <li><i class="glyphicon glyphicon-envelope"></i> areyes@msasoft.net</li>
            </ul>
            <h3>Ventas</h3>
            <ul class="list-unstyled">
                <li><i class="glyphicon glyphicon-phone-alt"></i> + (506) 8872 9065</li>
                <li><i class="glyphicon glyphicon-envelope"></i> msalamanca@msasoft.net</li>
                <li><i class="glyphicon glyphicon-envelope"></i> manchia@msasoft.net</li>
                <li><i class="glyphicon glyphicon-envelope"></i> areyes@msasoft.net</li>
            </ul>
            <h3>Soporte</h3>
            <ul class="list-unstyled">
                <li><i class="glyphicon glyphicon-hand-right"></i> <a href="#">Centro de Soporte</a></li>
                <li><i class="glyphicon glyphicon-envelope"></i> msalamanca@msasoft.net</li>
                <li><i class="glyphicon glyphicon-envelope"></i> manchia@msasoft.net</li>
                <li><i class="glyphicon glyphicon-envelope"></i> areyes@msasoft.net</li>
            </ul>
        </aside>
    </div>
</div>
</asp:Content>