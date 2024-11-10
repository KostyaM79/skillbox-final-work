const { read } = require("@popperjs/core");

function setDateRange() {
    let dateElement1 = document.getElementById("date1");
    let dateElement2 = document.getElementById("date2");

    let d1 = dateElement1.value ? new Date(dateElement1.value) : undefined;
    let d2 = dateElement2.value ? new Date(dateElement2.value) : undefined;

    if (d1 && d2) {
        let now = new Date();
        let startOffset = diffDate(now, d1);
        let endOffset = diffDate(now, d2);

        let h = document.location.origin;

        document.location.href = `${h}/AdminDesktop/Range/${startOffset}/${endOffset}`;
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

    if (!img) {
        img = document.createElement("img");
        img.classList.add("add-project-frm_img");
        img.id = "proj-img";
        document.getElementById("frm-image").appendChild(img);
    }

    img.file = f;

    var reader = new FileReader();
    reader.onload = (function (aImg) {
        return function (e) {
            aImg.src = e.target.result;
        }
    })(img);

    reader.readAsDataURL(f);
}