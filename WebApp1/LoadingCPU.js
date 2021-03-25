show = function () {
    $.ajax({
        type: "POST",
        url: "index.aspx/MonitoringCPU",
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
        },
        success: function (result) {
            var LoadingCPU = document.getElementById('valCPUload');
            LoadingCPU.innerHTML = result.d;
            var valCPU = parseInt(result.d);
            if (valCPU > 90) {
                document.getElementById('loadingCPU').style.background = '#8B0000';
            }
            else {
                document.getElementById('loadingCPU').style.background = 'rgb(0,255,0)';
            }
        }
    });
}
window.onload = function () {
    show();
};
setInterval(show, 1000);