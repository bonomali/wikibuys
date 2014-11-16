function telliformermaps() { }

telliformermaps.dragShape = null;
telliformermaps.dragPixel = null;
telliformermaps.MapDivId = 'theMap';
telliformermaps._map = null;
telliformermaps._points = [];
telliformermaps._shapes = [];
telliformermaps.ipInfoDbKey = '';

telliformermaps.LoadMap = function (latitude, longitude, onMapLoaded) {
    telliformermaps._map = new VEMap(telliformermaps.MapDivId);

    var options = new VEMapOptions();

    options.EnableBirdseye = false

    this._map.SetDashboardSize(VEDashboardSize.Small);

    if (onMapLoaded != null)
        telliformermaps._map.onLoadMap = onMapLoaded;

    if (latitude != null && longitude != null) {
        var center = new VELatLong(latitude, longitude);
    }

    telliformermaps._map.LoadMap(center, null, null, null, null, null, null, options);
}

telliformermaps.EnableMapMouseClickCallback = function () {
    telliformermaps._map.AttachEvent("onmousedown", telliformermaps.onMouseDown);
    telliformermaps._map.AttachEvent("onmouseup", telliformermaps.onMouseUp);
    telliformermaps._map.AttachEvent("onmousemove", telliformermaps.onMouseMove);
}

telliformermaps.onMouseDown = function (e) {
    if (e.elementID != null) {
        telliformermaps.dragShape = telliformermaps._map.GetShapeByID(e.elementID);
        return true;
    }
}

telliformermaps.onMouseUp = function (e) {
    if (telliformermaps.dragShape != null) {
        var x = e.mapX;
        var y = e.mapY;
        telliformermaps.dragPixel = new VEPixel(x, y);
        var LatLong = telliformermaps._map.PixelToLatLong(telliformermaps.dragPixel);
        $("#Latitude").val(LatLong.Latitude.toString());
        $("#Longitude").val(LatLong.Longitude.toString());
        telliformermaps.dragShape = null;

        telliformermaps._map.FindLocations(LatLong, telliformermaps.getLocationResults);
    }
}

telliformermaps.onMouseMove = function (e) {
    if (telliformermaps.dragShape != null) {
        var x = e.mapX;
        var y = e.mapY;
        telliformermaps.dragPixel = new VEPixel(x, y);
        var LatLong = telliformermaps._map.PixelToLatLong(telliformermaps.dragPixel);
        telliformermaps.dragShape.SetPoints(LatLong);
        return true;
    }
}

telliformermaps.ClearMap = function () {
    if (telliformermaps._map != null) {
        telliformermaps._map.Clear();
    }
    telliformermaps._points = [];
    telliformermaps._shapes = [];
}

telliformermaps.LoadPin = function (LL, name, description, draggable) {
    if (LL.Latitude == 0 || LL.Longitude == 0) {
        return;
    }

    var shape = new VEShape(VEShapeType.Pushpin, LL);

    if (draggable == true) {
        shape.Draggable = true;
        shape.onenddrag = telliformermaps.onEndDrag;
    }

    //Make a Pushpin with a title and description
    shape.SetTitle("<span class=\"pinTitle\"> " + escape(name) + "</span>");

    if (description !== undefined) {
        shape.SetDescription("<p class=\"pinDetails\">" + escape(description) + "</p>");
    }

    telliformermaps._map.AddShape(shape);
    telliformermaps._points.push(LL);
    telliformermaps._shapes.push(shape);
}

telliformermaps.FindAddressOnMap = function (where) {
    var numberOfResults = 1;
    var setBestMapView = true;
    var showResults = true;
    var defaultDisambiguation = true;

    telliformermaps._map.Find("", where, null, null, null,
                         numberOfResults, showResults, true, defaultDisambiguation,
                         setBestMapView, telliformermaps._callbackForLocation);
}

telliformermaps._callbackForLocation = function (layer, resultsArray, places, hasMore, VEErrorMessage) {
    telliformermaps.ClearMap();

    if (places == null) {
        telliformermaps._map.ShowMessage(VEErrorMessage);
        return;
    }

    //Make a pushpin for each place we find
    $.each(places, function (i, item) {
        var description = "";
        if (item.Description !== undefined) {
            description = item.Description;
        }
        var LL = new VELatLong(item.LatLong.Latitude,
                        item.LatLong.Longitude);

        telliformermaps.LoadPin(LL, item.Name, description, true);
    });

    //Make sure all pushpins are visible
    if (telliformermaps._points.length > 1) {
        telliformermaps._map.SetMapView(telliformermaps._points);
    }

    //If we've found exactly one place, that's our address.
    //lat/long precision was getting lost here with toLocaleString, changed to toString
    if (telliformermaps._points.length === 1) {
        $("#Latitude").val(telliformermaps._points[0].Latitude.toString());
        $("#Longitude").val(telliformermaps._points[0].Longitude.toString());
    }
}

telliformermaps._callbackUpdateMapDonors = function (layer, resultsArray, places, hasMore, VEErrorMessage) {
    var center = telliformermaps._map.GetCenter();

    $.post("/Search/SearchByLocation",
           { latitude: center.Latitude, longitude: center.Longitude },
           telliformermaps._renderDonors,
           "json");
}

telliformermaps._renderDonors = function (Donors) {
    $("#DonorsList").empty();

    telliformermaps.ClearMap();

    $.each(Donors, function (i, Donors) {

        var LL = new VELatLong(Donors.Latitude, Donors.Longitude, 0, null);

        // Add Pin to Map
        telliformermaps.LoadPin(LL, _getDonorsLinkHTML(Donors), _getDonorsDescriptionHTML(Donors), false);

        //Add a Donors to the <ul> DonorsList on the right
        $('#DonorsList').append($('<li/>')
                        .attr("class", "DonorsItem")
                        .append(_getDonorsLinkHTML(Donors))
                        .append($('<br/>'))
                        .append(_getDonorsDate(Donors, "mmm d"))
                        .append(" with " + _getRSVPMessage(Donors.RSVPCount)));
    });

    // Adjust zoom to display all the pins.
    if (telliformermaps._points.length > 1) {
        telliformermaps._map.SetMapView(telliformermaps._points);
    }

    // Display the event's pin-bubble on hover.
    $(".DonorsItem").each(function (i, Donors) {
        $(Donors).hover(
            function () { telliformermaps._map.ShowInfoBox(telliformermaps._shapes[i]); },
            function () { telliformermaps._map.HideInfoBox(telliformermaps._shapes[i]); }
        );
    });

}

telliformermaps.onEndDrag = function (e) {
    $("#Latitude").val(e.LatLong.Latitude.toString());
    $("#Longitude").val(e.LatLong.Longitude.toString());
}



