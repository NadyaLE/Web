function SendingData() {
    select = document.getElementById('SendData');
    value1 = document.getElementById('CRLRate');
    newvalue = value1.value;
    if (value1.value == "") {
        alert("Ошибка: Поле для ввода пустое, необходимо ввести соответствующее значение!");
    }
    if ((select.value == "CPU" || "RAM") && ( (parseInt(newvalue) < 1 || parseInt(newvalue) > 100))) {
        alert("Ошибка: Введено некорректное значение в поле для ввода процентов загрузки!");
        value1.value = "";
    }
    if ((select.value == "CPU" || "RAM") && (Number.isNaN(parseInt(newvalue)) != true) && (parseInt(newvalue) > 0 && parseInt(newvalue) <= 100)) {
        if (select.value == "CPU") {
            $.ajax({
                url: "index.aspx/FillingDB",
                type: 'post',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({
                    valnumcpu: newvalue,
                    valnumram: 0,
                    datefixed: timeUpdate(),
                    logstring: null
                }),
                dataType: 'json',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
                },
            });
            value1.value = "";
        }
        if (select.value == "RAM") {
            $.ajax({
                url: "index.aspx/FillingDB",
                type: 'post',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({
                    valnumcpu: 0,
                    valnumram: newvalue,
                    datefixed: timeUpdate(),
                    logstring: null
                }),
                dataType: 'json',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
                },
            });
            value1.value = "";
        }
    }
    if (select.value == "Log") {
        $.ajax({
            url: "index.aspx/FillingDB",
            type: 'post',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({
                logstring: newvalue,
                valnumcpu: 0,
                valnumram: 0,
                datefixed: timeUpdate()
            }),
            dataType: 'json',
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
            },
        });
        value1.value = "";
    }
}
function ClearingData() {
    $.ajax({
        url: "index.aspx/ClearingDB",
        type: 'post',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        succes: function (res) {
            alert("База данных очищена.");
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
        },
    });
}