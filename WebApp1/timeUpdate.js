function zero_first_format(num) { return ("0" + num).slice(-2); };
timeUpdate = function () {
    timestamp = document.getElementById('time');
    t = new Date();
    t.setHours(t.getHours() - 3);
    day = zero_first_format(t.getDate());
    month = zero_first_format(t.getMonth() + 1);
    hours = zero_first_format(t.getHours());
    minutes = zero_first_format(t.getMinutes());
    seconds = zero_first_format(t.getSeconds());
    timestamp.innerHTML = day + '.' + month + '.' + t.getFullYear() + '<br />' + hours + ':' + minutes + ":" + seconds;

    return t.getFullYear() + "-" + month + "-" + day + " " + hours + ":" + minutes + ":" + seconds;
};
window.onload = function () {
    timeUpdate();
};
setInterval(timeUpdate, 1000);