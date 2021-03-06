﻿"use strict";
var _url = "hub";
var connection = new signalR.HubConnectionBuilder().withUrl(_url).build();


//Disable send button until connection is established
document.getElementById("btnAcessar").disabled = true;

//RESPOSTAS DO SERVIDOR
connection.on("EntrouNaFila", function (msg) {    
    document.getElementById("msgServidor").innerHTML = "";
    document.getElementById("msgServidor").innerHTML = msg;

});

connection.on("JogadaAprovada", function (icone, posicao) {
    try {
        console.log($("#" + posicao)[0]);
        $("#" + posicao)[0].innerHTML = icone;
        
       
    } catch (e) {
        console.log(e);
    }
    
});

connection.on("PartidaIniciada", function (jogardorA, jogadorB) {
    $("#login").hide();
    $("#msgServidor")[0].innerHTML = "";
    $("#Tabuleiro").show();
    $("#jogadorA")[0].innerHTML = jogardorA;
    $("#jogadorB")[0].innerHTML = jogadorB;
});

connection.start().then(function () {
    document.getElementById("btnAcessar").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("btnAcessar").addEventListener("click", function (event) {
    var apelido = document.getElementById("apelido").value;
    connection.invoke("AguadarNaFila", apelido).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

$(document).ready(function () {
    IniciarTabuleiro();
    Jogada();
});



function Jogada() {
    $(".tabuleiro").click(function () {        
        connection.invoke("jogar", $(this).attr("posicao"))
            .catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();
        //$(this)[0].innerHTML = $("#meuIcone").val();
    });

}
function IniciarTabuleiro() {
    $(".tabuleiro").mouseover(function () {
        $(this).addClass("grid-item-disponivel")
    }).mouseout(function () {
        $(this).removeClass("grid-item-disponivel")
    }).click(function () {
        $(this).innerHTML = $("#meuIcone").val()
    });
}