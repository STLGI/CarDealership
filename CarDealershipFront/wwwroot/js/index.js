async function loadData(companyId = null) {
    const url = companyId
        ? `https://localhost:5119/api/home/index/${companyId}`
        : `https://localhost:5119/api/home/index`;

    const response = await fetch(url);
    if (!response.ok) {
        console.error("Ошибка при получении данных:", response.status);
        return;
    }

    const data = await response.json();

    renderCompanies(data.companies);
    renderCars(data.cars);
}

function renderCompanies(companies) {
    const container = document.getElementById("companies");
    container.innerHTML = "";

    companies.forEach(comp => {
        const a = document.createElement("a");
        a.className = "btn col";
        a.href = "#";
        a.innerHTML = `<img src="/img/${comp.img}" class="img-thumbnail rounded" alt="">`;

        a.addEventListener("click", (e) => {
            e.preventDefault();
            loadData(comp.id);
        });

        container.appendChild(a);
    });
}

function renderCars(cars) {
    const container = document.getElementById("list");
    container.innerHTML = "";

    if (cars.length === 0) {
        container.innerHTML = `<h1 class="text-center mt-5">Nothing here yet</h1>`;
        return;
    }

    cars.forEach(car => {
        const div = document.createElement("div");
        div.className = "col-4 mt-1";
        div.id = `car-${car.id}`;

        div.innerHTML = `
            <a class="card text-decoration-none" href="/Home/Vehicle?carID=${car.id}">
                <img class="card-img-top" src="https://localhost:5119/api/home/${car.id}/images/${car.images[0].id}" alt="Card image cap" width="300" height="200">
                <div class="card-body">
                    <h1 class="fs-3" style="color:darkslategrey">
                        ${(car.manufacturer?.sName)} ${car.model}
                    </h1>
                </div>
            </a>
        `;
        container.appendChild(div);
    });
}

loadData();
