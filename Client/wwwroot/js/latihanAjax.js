var text = "";
$.ajax({
    url: "https://swapi.dev/api/people"
}).done((result) => {
    console.log(result.results);
    $.each(result.results, function (key, val) {
        text += `<tr>
                    <td>${key+1}</td>
                    <td>${val.name}</td>
                    <td>${val.hair_color}</td>
                    <td>${val.skin_color}</td>
                    <td>${val.eye_color}</td>
                    <td>${val.gender}</td>
                </tr>`;
    });
    $("#listSW").html(text);
    //console.log(text)
}).fail((error) => {
    console.log(error);
});

var text2 = "";
$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon"
}).done((result) => {
    console.log(result.results);
    $.each(result.results, function (key, val) {
        text2 += `<tr>
                    <td>${key+1}</td>
                    <td>${val.name}</td>
                    <td><a id="btnklikdetail" target="_blank" class="btn btn-primary" href="${val.url}">Klik Detail Pokemon</a></td>
                </tr>`;
    });
    $("#listPK").html(text2);
    //console.log(text2)
}).fail((error) => {
    console.log(error);
});


