let btnselcol = document.getElementById("butonselect");
btnselcol.addEventListener("click", function () {
    console.log("Hallo");
    let xjudul = document.getElementById("xjudul");
    xjudul.style.backgroundColor = "red";
    xjudul.style.color = "white";
})

$("#btnchangecolgen").click(function () {
    let listcolect = document.getElementsByClassName("list");
    console.log("Test");
    for (var i = 0; i < listcolect.length; i++) {
        if (i % 2) {
            listcolect[i].style.backgroundColor = "blue";
            listcolect[i].style.color = "white";
        }
    }
})

$("#btnchangetext3").click(function () {
    let xsub3 = document.getElementById("xsub3");
    xsub3.innerHTML = "Ini diganti dengan cara di klik";
})

let btnalert1 = document.getElementById("btnalert1");
btnalert1.addEventListener("click", function () {
    alert("Hallo, Inii test Alert");
})

let btnmousemove = document.getElementById("btnmousemove");
btnmousemove.addEventListener("click", function () {
    alert("Engga diklik tapi di hover saja");
    let par1 = document.getElementById("paragraf1");
    par1.style.color = "red";
    par1.style.backgroundColor = "yellow";
})

let btnselpa = document.getElementById("btnselpa");
btnselpa.addEventListener("click", function () {
    let par1 = document.getElementById("paragraf1");
    par1.style.fontSize = "22px";
})

