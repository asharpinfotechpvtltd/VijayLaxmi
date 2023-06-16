
$("#State").change(function () {
    $("#City").empty();
    $.ajax({
        type: "GET",
        url: "/api/City",
        data: { StateId: $("#State").val() },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        success: function (response) {            
            $.each(response, function (i, item) {
                var html = '';
                html += "<option value=" + item.id + ">" + item.cityName + "</option>";
                $("#City").append(html);
            })
        },
        failure: function (response) {
            console.log(response.responseText);
        },
        error: function (response) {
            console.log(response.responseText);
        }
    });

});
