<%@ Page Title="Contactenos" Language="C#" AutoEventWireup="true" MasterPageFile="~/Layout.master" CodeBehind="Contacts.aspx.cs" Inherits="Web.Contacts" %>
<%@ Register Src="~/UserControls/AddCommentForm.ascx" TagPrefix="dx" TagName="AddCommentForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
<div class="container-fluid">
    <div id="map" class="map row"></div>
</div>
<script src="http://cdn.polyfill.io/v2/polyfill.min.js?features=requestAnimationFrame,Element.prototype.classList,URL"></script>
<script src="http://openlayers.org/en/v3.18.2/build/ol.js"></script>
<script type="text/javascript">
    (function () { 
        var location = ol.proj.fromLonLat([9.998352, -84.1307602]);

    var map = new ol.Map({
        layers: [
            new ol.layer.Tile({
                source: new ol.source.OSM()
            }),
            getMarkerLayer()
        ],
        target: 'map',
        controls: ol.control.defaults(),
        interactions: ol.interaction.defaults({
            mouseWheelZoom: false
        }),
        view: new ol.View({
            center: location,
            zoom: 16
        })
    });

    function getMarkerLayer() {
        var iconFeature = new ol.Feature({
            geometry: new ol.geom.Point(location),
        });

        var iconStyle = new ol.style.Style({
            image: new ol.style.Circle({
                radius: 5,
                fill: new ol.style.Fill({
                    color: 'rgba(255,51,0,0.9)'
                })
            })
        });
        iconFeature.setStyle(iconStyle);

        var vectorSource = new ol.source.Vector({
            features: [iconFeature]
        });

        return new ol.layer.Vector({
            source: vectorSource
        });
    }
})();
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
            </ul>
            <h3>Ventas</h3>
            <ul class="list-unstyled">
                <li><i class="glyphicon glyphicon-phone-alt"></i> + (506) 8872 9065</li>
                <li><i class="glyphicon glyphicon-envelope"></i> msalamanca@msasoft.net</li>
            </ul>
            <h3>Soporte</h3>
            <ul class="list-unstyled">
                <li><i class="glyphicon glyphicon-hand-right"></i> <a href="#">Centro de Soporte</a></li>
                <li><i class="glyphicon glyphicon-envelope"></i> msalamanca@msasoft.net</li>
            </ul>
        </aside>
    </div>
</div>
</asp:Content>