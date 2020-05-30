hljs.initHighlighting()

var themer = document.querySelector(".theme")
var get = sessionStorage.getItem("class", themer.getAttribute("href"))

var themer2 = document.querySelector(".theme2")
var get2 = sessionStorage.getItem("class2", themer.getAttribute("href"))

if (get == null) {
    themecode3()
}

if (get2 == null) {
    themecode5()
}
else {
    themer.setAttribute("href", get)
    themer2.setAttribute("href", get2)
}
function themecode() {
    var themer = document.querySelector(".theme")
    themer.setAttribute("href", "../../css/styles/atelier-cave-light.css")
    sessionStorage.setItem("class", themer.getAttribute("href"))
    var get = sessionStorage.getItem("class", themer.getAttribute("href"))
    themer.setAttribute("href", get)

}

function themecode2() {
    var themer = document.querySelector(".theme")
    themer.setAttribute("href", "../../css/styles/atelier-cave-dark.css")
    sessionStorage.setItem("class", themer.getAttribute("href"))
    var get = sessionStorage.getItem("class", themer.getAttribute("href"))
    themer.setAttribute("href", get)

}

function themecode3() {
    var themer = document.querySelector(".theme")
    themer.setAttribute("href", "../../css/styles/dracula.css" )
    sessionStorage.setItem("class", themer.getAttribute("href"))
    var get = sessionStorage.getItem("class", themer.getAttribute("href"))
    themer.setAttribute("href", get)

}

function themecode4() {
    var themer2 = document.querySelector(".theme2")
    themer2.setAttribute("href", "../../../css/DarkMode.css")
    sessionStorage.setItem("class2", themer2.getAttribute("href"))
    var get2 = sessionStorage.getItem("class2", themer2.getAttribute("href"))
    themer2.setAttribute("href", get2)
    window.location.reload();
}

function themecode5() {
    var themer2 = document.querySelector(".theme2")
    themer2.setAttribute("href", "../../../css/site.css")
    sessionStorage.setItem("class2", themer2.getAttribute("href"))
    var get2 = sessionStorage.getItem("class2", themer2.getAttribute("href"))
    themer2.setAttribute("href", get2)
    window.location.reload();
}