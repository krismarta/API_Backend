
var text2 = "";
$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon"
}).done((result) => {
    console.log(result.results);
    $.each(result.results, function (key, val) {
        text2 += `<tr>
                    <td>${key + 1}</td>
                    <td>${val.name}</td>
                    <td>
                        <button type="button" data-toggle="modal" data-target="#detailpoke" onclick="GetData('${val.url}')" class="btn btn-info">
                         Detail Pokemon
                        </button>
                    </td>
                </tr>`;
    });
    $("#listPK").html(text2);
    //console.log(text2)
}).fail((error) => {
    console.log(error);
});

function GetData(urls) {
    var infoPoke = "";
    var fromPoke = "";
    var skilPoke = "";
    var typePoke = "";
    var statsPoke = "";
    var imagePoke = "";
    $.ajax({
        url: urls
    }).done((result) => {
        //console.log(result.name);
        for (var i = 0; i < result.forms.length; i++) {
            fromPoke += `
                <tr>
                    <td class="font-weight-bold" >From ${i + 1} :</td>
                    <td>${result.forms[i].name}</td>
                </tr>
            `;
        }
        infoPoke += `
                <tr>
                    <td class="font-weight-bold">Name :</td>
                    <td>${result.name}</td>
                </tr>
                <tr>
                    <td class="font-weight-bold">Height :</td>
                    <td>${result.height}</td>
                </tr>
                <tr>
                    <td class="font-weight-bold">Weight :</td>
                    <td>${result.weight}</td>
                </tr>
                <tr>
                    <td class="font-weight-bold">Base Experience :</td>
                    <td>${result.base_experience}</td>
                </tr>
        `;

        $.each(result.abilities, function (key, val) {
            skilPoke += `
                <tr>
                  <td>${val.ability.name}</td>
                </tr>
            `;
        });
        $.each(result.types, function (key, val) {
            let badgeColor = "";
            switch (val.type.name) {
                case "grass":
                    badgeColor = "badge-success";
                    break;
                case "poison":
                    badgeColor = "badge-dark";
                    break;
                case "fire":
                    badgeColor = "badge-danger";
                    break;
                case "flying":
                    badgeColor = "badge-light";
                    break;
                case "water":
                    badgeColor = "badge-primary";
                    break;
                case "bug":
                    badgeColor = "badge-secondary";
                    break;
                case "normal":
                    badgeColor = "badge-info";
                    break;
            }

            typePoke += `
                <h4 class="badge ${badgeColor}" >${val.type.name}</h4>
            `;
        });
        $.each(result.stats, function (key, val) {
            statsPoke += `
                <tr>
                  <td>${val.stat.name}</td>
                  <td>${val.effort}</td>
                  <td>${val.base_stat}</td>
                </tr>
            `;
        });
        var testimg = "";
        testimg = result.sprites.other["official-artwork"].front_default;

        imagePoke += `
                <img class="rounded-circle bg-warning align-center" src="${testimg}" class="img-responsive center-block d-block mx-auto" alt="Alternate Text" width="200" />
            `;

        $("#infopoke").html(infoPoke + fromPoke);
        $("#skillpoke").html(skilPoke);
        $("#typepoke").html(typePoke);
        $("#statspoke").html(statsPoke);
        $("#imagepoke").html(imagePoke);
    console.log(result);
    }).fail((error) => {
        console.log(error);
    });
}


$(document).ready(function () {
    $('#example').DataTable({
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.11.3/i18n/id_alt.json",
            "sEmptyTable" : "Data tidak ada"
        },
        
    });
});