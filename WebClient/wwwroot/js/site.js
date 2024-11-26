const { get } = require("jquery");

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
    let f = document.getElementById("order-frm");
    let inputs = f.getElementsByClassName("order-frm__input");

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

// Отображает модальное окно для редактирования соц. сетей
async function show_modal() {
    let lnkColl = document.getElementsByClassName("social__lnk");
    let imgColl = document.getElementsByClassName("social-img");
    let inputsItems = document.getElementsByClassName("modal__links")[0];  // Получаем список inputs
    let filereader = new FileReader();

        let modal = document.getElementById("edit-contacts");
        modal.style.visibility = "visible";
        let basket = modal.getElementsByClassName("modal__basket")[0];
        basket.onclick = remove_item;
        let file = modal.getElementsByClassName("modal-file")[0];
        file.onchange = fileOnChanged;

    for (let i = 0; i < lnkColl.length; i++) {
        let input = inputsItems.children.item(inputsItems.children.length - 1);

        if (input) {
            let img = input.children.item(0).children.item(0);
            let txtEl = input.children.item(1);
            img.src = imgColl[i].src;
            txtEl.value = lnkColl[i].parentElement.children.item(0).value;
        }

        if (i < lnkColl.length - 1) add_item();
    }

    let saveBtn = document.getElementsByClassName("btn_right")[0];
    saveBtn.innerHTML = "Сохранить";
    saveBtn.onclick = save;
    saveBtn.style.zIndex = 1001;
}

function save(e) {
    let items = document.getElementsByClassName("modal__input");
    let full = true;

    for (let i = 0; i < items.length; i++) {
        if (!items[i].value) {
            full = false;
            break;
        }

        let file = items[i].parentElement.getElementsByClassName("modal-file")[0];

        if (!file.value) {
            full = false;
            break;
        }
    }

    if (full) {
        for (let i = 0; i < items.length; i++) {
            items[i].setAttribute("name", `Links[${i}]`);
            let file = items[i].parentElement.getElementsByClassName("modal-file")[0];
            file.setAttribute("name", `Filess[${i}]`);
        }

        let btn = e.currentTarget;
        btn.innerHTML = "Редактировать";
        btn.onclick = show_modal;
        //btn.Form = null;
        btn.zIndex = 10;

        let frm = document.getElementById("socialFrm");
        frm.submit();

        let modal = document.getElementById("edit-contacts");
        modal.style.visibility = "hidden";
    }
}

//Добавляет элемент в список соц. сетей
function add_item() {
    let modal = document.getElementById("edit-contacts"); // Получаем экземпляр модального окна

    let inputs = modal.getElementsByClassName("modal__input");
    if (inputs[inputs.length - 1].value) {

        let item = document.createElement("li");
        let input = document.createElement("input");
        let img = document.createElement("img");
        let basket = document.createElement("img");
        let label = document.createElement("label");
        let inputFile = document.createElement("input");
        let inputHide = document.createElement("input");

        inputHide.setAttribute("type", "hidden");

        inputFile.setAttribute("type", "file");
        inputFile.classList.add("modal-file");
        inputFile.onchange = fileOnChanged;

        label.appendChild(img);
        label.appendChild(inputFile);

        item.classList.add("modal__item");

        input.setAttribute("type", "text");
        input.classList.add("modal__input");

        img.style.width = "24px";
        img.style.height = "24px";
        img.src = "../img/add-icon.svg";
        img.classList.add("modal__add");

        basket.style.width = "24px";
        basket.style.height = "24px";
        basket.src = "../img/delete.svg";
        basket.classList.add("modal__basket");
        basket.onclick = remove_item;

        //item.append(inputHide);
        item.appendChild(label);
        item.appendChild(input);
        item.appendChild(basket);

        let items = modal.getElementsByClassName("modal__links")[0];
        items.appendChild(item);
    }
}

// Удаляет ссылку из модального окна
function remove_item(e) {
    let modal = document.getElementById("edit-contacts");           // Получаем экземпляр модального окна
    let items = modal.getElementsByClassName("modal__links")[0];    // Получаем список ссылок
    let basket = e.currentTarget;
    let removing = basket.parentNode;                               // Получаем item
    if (items.childElementCount > 1) {
        items.removeChild(removing);                                // Удаляем item
    }
}

function fileOnChanged(e) {
    let file = e.currentTarget;
    let item = file.parentNode;
    let img = item.getElementsByClassName("modal__add")[0];
    let path = file.files[0];
    img.src = URL.createObjectURL(path);
}