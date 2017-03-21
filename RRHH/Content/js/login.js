$(document).ready(function(){
    $("#Login").click(function () {
        var dataObject = { UserTableEmail: $("#inputEmail").val(), UserTablePassword: $("#inputPassword").val() };
        $.ajax({
            url: $("#inputEmail").data("url"),
            type: "POST",
            data: dataObject,
            datatype: "json",
            success: function (result) {
                if (result.toString() == "Correcto") {
                    window.location.href = $("#inputPassword").data("url");
                }
                else {
                    //alert(result);
                    $("#UserName").val("");
                    $("#Password").val("");
                    $("#UserName").focus();
                    $("#messenger").html('<div class="failed">' + result + '</div>');
                }
            },
            error: function (result) {
                //alert("Error");
                $("#UserName").val("");
                $("#Password").val("");
                $("#UserName").focus();
                $("#messenger").html('<div class="failed"> Error! volve a intentarlo </div>');
            }
        });
    });
});