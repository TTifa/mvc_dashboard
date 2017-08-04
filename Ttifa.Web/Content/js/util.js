﻿function GetDateDiff(startTime, endTime, diffType) {
    //将xxxx-xx-xx的时间格式，转换为 xxxx/xx/xx的格式 
    startTime = startTime.replace(/\-/g, "/");
    endTime = endTime.replace(/\-/g, "/");
    //将计算间隔类性字符转换为小写
    diffType = diffType.toLowerCase();
    if (startTime == '') {
        var sTime = new Date();  //开始时间
    }
    else {
        var sTime = new Date(startTime);      //开始时间
    }
    var eTime = new Date();
    if (endTime != '') {
        eTime = new Date(endTime);  //结束时间
    }
    //作为除数的数字
    var divNum = 1;
    switch (diffType) {
        case "second":
            divNum = 1000;
            break;
        case "minute":
            divNum = 1000 * 60;
            break;
        case "hour":
            divNum = 1000 * 3600;
            break;
        case "day":
            divNum = 1000 * 3600 * 24;
            break;
        default:
            break;
    }
    return parseInt((eTime.getTime() - sTime.getTime()) / parseInt(divNum));
}

function queryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return decodeURI(r[2]); return null;
}
function showLoading() {
    $('#status').delay(300).fadeIn();
    $('#preloader').delay(300).fadeIn('slow');
}
function hideLoading() {
    $('#status').delay(300).fadeOut();
    $('#preloader').delay(300).fadeOut('slow');
}