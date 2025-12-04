var allFiles = new DataTransfer();

function previewImages(event) {
    var files = event.target.files;
    var container = document.getElementById('imagePreviewContainer');

    for (var i = 0; i < files.length; i++) {
        var file = files[i];
        allFiles.items.add(file); // Add each new file to the DataTransfer object

        var reader = new FileReader();

        reader.onload = (function (file, index) {
            return function (e) {
                var div = document.createElement('div');
                div.classList = 'col-3'
                div.style.position = 'relative';
                div.style.display = 'inline-block';
                div.style.margin = '10px';

                var img = document.createElement('img');
                img.src = e.target.result;
                img.classList = 'img col-12'

                var button = document.createElement('button');
                button.classList = 'btn btn-danger ';
                button.textContent = '—';
                button.style.position = 'absolute';
                button.style.top = '10px';
                button.style.right = '10px';

                button.onclick = function () {
                    div.remove();
                    // Remove the file from the DataTransfer object
                    allFiles.items.remove(index);
                    document.getElementById('file-input').files = allFiles.files; // Update the file input element
                };

                div.appendChild(img);
                div.appendChild(button);
                container.appendChild(div);
            };
        })(file, allFiles.items.length - 1);

        reader.readAsDataURL(file);
    }

    document.getElementById('AddCarForm').files = allFiles.files; // Update the file input element with all files
}

document.getElementById("AddCarForm").addEventListener("submit", async function (e) {
    e.preventDefault();

    const formData = new FormData();

    const fileInput = document.getElementById("file-input");
    for (let file of fileInput.files) {
        formData.append("Files", file);
    }

    const manufacturerID = document.getElementById("compId").value;

    const modelInput = document.querySelector('input[name="Car.Model"]').value;

    const transmission = document.getElementById("transmission").value;

    const fuel = document.querySelector('input[name="Car.Fuel"]').value;

    const mileAge = document.querySelector('input[name="Car.MileAge"]').value;

    const price = document.querySelector('input[name="Car.Price"]').value;

   
    formData.append("Car.ManufacturerId", manufacturerID);
    formData.append("Car.Model", modelInput);
    formData.append("Car.Transmission", transmission);
    formData.append("Car.Fuel", fuel);
    formData.append("Car.MileAge", mileAge);
    formData.append("Car.Price", price);

    try {
        const res = await fetch("https://localhost:5119/api/home/add", {
            method: "POST",
            body: formData
        });
        if (res.ok) {
            window.location.href = "/Home/Index";
        }
        else {
            const container = document.getElementById("ExceptionAlert");
            container.innerHTML = '<span style="font - weight: 500">All the fields should contain information about the car and at least one image should be uploaded!</span>'
        }
    }
    catch (error) {
        console.error("Network error:", error);
        const container = document.getElementById("ExceptionAlert");
        container.innerHTML = `<span style="color:red">Network error: ${error.message}</span>`;
    }


    const text = await res.text();
    console.log(text);
});

async function getData() {


    const url = "https://localhost:5119/api/home/sell"

    const response = await fetch(url);
    if (!response.ok) {
        console.error("Ошибка при получении данных:", response.status);
        return;
    }

    const data = await response.json();
    renderData(data);
}

async function renderData(data) {
    container = document.getElementById("ExceptionAlert")
    const select = document.getElementById("compId")
    data.index.companies.forEach(company => {
        const option = document.createElement("option");
        select.appendChild(option);
        option.value = company.id;
        console.log(option.value);
        option.innerHTML = `${company.name}`;

    }
        )
}
getData()