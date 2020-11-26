var clickNum = 0;

function showMenu() {
    clickNum++;
    var row = document.getElementById("opNav");
    if (clickNum % 2 == 1)
        row.style.height = "22px";
    else
        row.style.height = "0px";


}