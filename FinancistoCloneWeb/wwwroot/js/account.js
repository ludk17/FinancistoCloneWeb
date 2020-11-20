

//var $link = $('#link');
//$link.click(function (e) {
//    alert("Hola hice clic en el link");
//    e.preventDefault();
//});


//function saludar(e) {

//    console.log("Llamando request HTTP");

//    var resp = $.ajax({
//        url: "https://localhost:44310/Account",
//        type: "get"
//    });

//    resp.done(function (html) {
//    })


//    console.log("Prevenir default");
//    e.preventDefault();
   
//}

function cargarContenido() {
    $.ajax({
        url: "/Account/_index",
        type: "get"
    }).done(function (x) {
        var $content = $('#table-content');
        $content.html(x);
    });    
}

cargarContenido();