


function DashBoardFetch()
{
    var StartDatetime = $("#StartDate").val();
    var EndDatetime = $("#EndDate").val();
    var flag = false;

    if (StartDatetime) {
        flag = true;
    }

    if (EndDatetime) {
        flag = true;
    }

    if (StartDatetime < EndDatetime) {


        alert("EndDatetime must be greater than StartDatetime");
        flag = true;
    }

    if (flag) {
        $.ajax({
            cache: false,
            url: "/Home/GetDashBoard",
            type: "POST",
            data: {
                'StartDatetime': StartDatetime,
                'EndDatetime': EndDatetime
            },
            success: function (result) {

                if (result) {
                    $("#Scheduled").text(result.Scheduled);
                    $("#Completed").text(result.Completed);
                    $("#StrongConsider").text(result.StrongConsider);
                    $("#InProgress").text(result.InProgress);
                    $("#Expired").text(result.Expired);
                    $("#Pending").text(result.Pending);
                    $("#SingleLogin").text(result.SingleLogin);
                    $("#CommonLogin").text(result.CommonLogin);
                    $("#BulkLogin").text(result.BulkLogin);	
                }
            },
            complete: function () {

            },
            error: function (result) {
            }
        });
    }

}