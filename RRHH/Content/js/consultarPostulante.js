$(document).ready(function () {
    $('#GuardarCambios').attr("disabled", true);
    var jobApp_original = $("#jobAppSelect").val();
    var interview_original = $("#interviewSelect").val();
    var jobApp_modified=jobApp_original;
    var interview_modified = interview_original;
    $("#jobAppSelect").change(function () {
        jobApp_modified = $(this).val();
        if ((jobApp_original != jobApp_modified) || (interview_original != interview_modified)) {
            $('#GuardarCambios').attr("disabled", false);
        }
    });
    $("#interviewSelect").change(function () {
        interview_modified = $(this).val();
        if ((jobApp_original != jobApp_modified) || (interview_original != interview_modified)) {
            $('#GuardarCambios').attr("disabled", false);
        }
    });
});