<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="WebApp1.Index" %>

<!DOCTYPE html>
<html lang="ru">

<head>
    <meta charset="utf-8">
    <title>Мониторинг CPU и RAM</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/normalize/8.0.1/normalize.css">
    <link rel="shortcut icon" type="image/x-icon" href="img/favicon/icons-96.png" />
    <link rel="stylesheet" href="main.css">
    <meta name="description" content="Разработка и верстка главной страницы на которой отображаются данные мониторинга CPU, RAM.">
    <script src="https://cdn.jsdelivr.net/npm/chart.js@2.8.0"></script>
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="LoadingCPU.js"></script>
    <script src="timeUpdate.js"></script>
    <script src="UpChart.js"></script>
    <script src="SendingOrClearingData.js"></script>

</head>

<body>

    <div class="container">
        <!-- ..................................................................................
                                     HEADER
....................................................................................-->

        <div class="header">
            <div class="header_title" style="flex: auto">Мониторинг производительности</div>
            <div id="time" class="header_time"></div>

        </div>

        <!-- ..................................................................................
                                MAIN ACTIVITY
....................................................................................-->

        <div class="clearfix">
            <!-- ..................................................................................
                                  LEFT MENU
....................................................................................-->
            <div class="left_menu">
                <form name="formSend" id="formId" runat="server" class="form_menu" autocomplete="off" onsubmit="return false">
                    <div>
                        <div>Форма отправки данных:</div>
                        <br />
                        <select id="SendData" name="SendData">
                            <option value="CPU" selected>CPU (%)</option>
                            <option value="RAM">RAM (%) </option>
                            <option value="Log">Log message (text)</option>
                        </select>
                        <br />
                        <input type="text" id="CRLRate" name="CRLRate" placeholder="Ввод значения" />
                        <br />
                        <input id="ClickSending" type="submit" value="Отправить" onclick="SendingData()" />
                        <input id="ClickClearing" type="button" value="Очистить БД" onclick="ClearingData()" />
                    </div>
                </form>
            </div>
            <!-- ..................................................................................
                                  WORKING PART
....................................................................................-->

            <div class="section">
                <div class="chartWrapper">
                    <div class="chartAreaWrapper" id="element">
                        <div class="chartAreaWrapper2">
                            <canvas id="cpu_chart"></canvas>
                        </div>
                    </div>
                </div>
                <div class="textCPU">
                    Индикатор высокой загрузки CPU 
                <div class="loadingCPU" id="loadingCPU">
                    <div class="ValCPULoad" id="valCPUload"></div>

                </div>
                </div>
                <div class="LogWrapper">
                    <p class="title">Отображение лога событий</p>
                    <div class="LogAreaWrapper">
                        <div class="LogTxt" id="LogTxt"></div>
                    </div>
                </div>
            </div>

        </div>
        <!-- ..................................................................................
                                     FOOTER
....................................................................................-->

        <div class="creator">
            Литке Надежда<br />
            СМБ-701-О
        </div>


    </div>
</body>
</html>
