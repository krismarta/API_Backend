
const animals = [
    { name: 'fluffy', species: 'cat', class: { name: 'mamalia' } },
    { name: 'Nemo', species: 'fish', class: { name: 'vertebrata' } },
    { name: 'hely', species: 'cat', class: { name: 'mamalia' } },
    { name: 'Dory', species: 'fish', class: { name: 'vertebrata' } },
    { name: 'ursa', species: 'cat', class: { name: 'mamalia' } }
]

//tugas 1
let onlyCat = [];
for (var i = 0; i < animals.length; i++) {
    if (animals[i].species == "cat") {
        onlyCat.push(animals[i]);
    }
}
//console.log(onlyCat);


//tugas 2
for (var i = 0; i < animals.length; i++) {
    if (animals[i].species == "fish") {
        animals[i].class.name = "Non-Mamalia";
    }
}

//console.log(animals);

