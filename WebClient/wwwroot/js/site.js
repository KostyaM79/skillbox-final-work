
// Фразы для случайного отображения
let phrases = [
    "Первая случайная фраза.",
    "Вторая случайная фраза.",
    "Третья случайная фраза.",
    "Четвёртая случайная фраза.",
    "Пятая случайная фраза."
];

let text = phrases[Math.floor(Math.random() * phrases.length)];
let el = document.querySelector(".header__title").textContent = text;

// Переходит по ссылке, передаёт введённый пользователем диапазон дат
function setDateRange() {
    let dateElement1 = document.getElementById("date1");
    let dateElement2 = document.getElementById("date2");

    let d1 = dateElement1.value ? new Date(dateElement1.value) : undefined;
    let d2 = dateElement2.value ? new Date(dateElement2.value) : undefined;

    if (d1 && d2) {
        let now = new Date();
        let startOffset = diffDate(now, d1);
        let endOffset = diffDate(now, d2);

        document.location.href = `${document.location.origin}/read/filter/Range/${startOffset}/${endOffset}`;
    }
}

function diffDate(d1, d2) {
    let t1 = d1.getTime();
    let t2 = d2.getTime();
    return Math.floor((t1 - t2) / (24 * 3600 * 1000));
}

function show_picture() {
    let element = document.getElementById("proj-photo");        // Получаем input-элемент
    let f = element.files[0];                                   // Получаем ссылку на выбранный файл

    var img = document.getElementById("proj-img");

    var i = document.querySelector(".input-wrapper");
    img.src = URL.createObjectURL(f);

    //if (!img) {
    //    img = document.createElement("img");
    //    img.classList.add("add-project-frm_img");
    //    img.id = "proj-img";
    //    document.getElementById("frm-image").appendChild(img);
    //}

    //img.file = f;

    //var reader = new FileReader();
    //reader.onload = (function (aImg) {
    //    return function (e) {
    //        aImg.src = e.target.result;
    //    }
    //})(img);

    //reader.readAsDataURL(f);
}

let inputs = [];
addevents();

function addevents() {

    inputs = document.getElementsByClassName("order-frm__input");
    console.log(`Получено ${inputs.length} элементов.`);
    for (let i = 0; i < inputs.length; i++) {
        console.log(i);
        inputs[i].onchange = check;
    }
}


function check() {
    let btn = document.getElementById("btn");

    for (let i = 0; i < inputs.length; i++) {
        if (!inputs[i].value) {
            btn.disabled = true;
            return;
        }
    }

    btn.disabled = false;
}

// Отображает форму для отправления заявки
function showFrm() {
    let frm = document.getElementById("order-frm");
    frm.style.display = "flex";
}