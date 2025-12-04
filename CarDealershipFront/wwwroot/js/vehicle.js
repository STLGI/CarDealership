async function loadData(carId, MainImageId) {
    // Убедитесь, что carId - это простое значение (например, число или строка),
    // а не сложный объект.
    const url = "https://localhost:5119/api/home/vehicle/" + carId;

    try {
        const response = await fetch(url);
        if (!response.ok) {
            console.error("Ошибка при получении данных:", response.status);
            return;
        }

        const data = await response.json();

        // Предполагая, что API возвращает { car: { ... } }
        // Если API возвращает просто { ... (данные машины) },
        // то нужно вызывать renderCar(data, imageId);
        console.log(data);
        console.log(carId);
        console.log(MainImageId);
        console.log(data.vehicleCar.id);
        renderCar(data.vehicleCar, MainImageId);

    } catch (error) {
        console.error("Ошибка сети или JSON:", error);
    }
}

// Ключевое слово 'async' здесь не обязательно, 
// так как внутри нет 'await', но оно не мешает.
async function renderCar(car, imageId) {
    var nameAndPrice = document.getElementById("ModelAndPrice");
    nameAndPrice.innerHTML = ` 
        <h1 class="col-10 fs-1">${car.manufacturer.sname} ${car.model}</h1>
        <h1 class="col fs-1 align-content-end" > ${car.price} $</h1 >`;

    // РЕКОМЕНДАЦИЯ: ID с пробелами ('main image') работают, 
    // но считаются плохой практикой. Лучше использовать 'main-image'.
    var mainImage = document.getElementById("main image");
    console.log(car.images);
    mainImage.src = `https://localhost:5119/api/home/${car.id}/images/${imageId}`;

    var carFuel = document.getElementById("CarFuel");
    carFuel.innerHTML = `${car.fuel}`;

    var mileAge = document.getElementById("MileAge");
    mileAge.innerHTML = `${car.mileage}`;

    var transmission = document.getElementById("Transmission");
    transmission.innerHTML = `${car.transmission}`;

    var sName = document.getElementById("ManufacturerSName");
    // ОШИБКА 1: Вы присваивали значение переменной 'transmission', а не 'sName'.
    sName.innerHTML = `${car.manufacturer.sname}`;

    var carModel = document.getElementById("CarModel");
    carModel.innerHTML = `${car.model}`;

    var carPrice = document.getElementById("CarPrice");
    carPrice.innerHTML = `${car.price} $`;

    // Находим родительский элемент один раз
    var imagesContainer = document.getElementById("Images");

    // ОШИБКА 2: 'foreach' -> 'forEach' (с большой 'E')
    car.images.forEach(image => {
        // ОШИBKA 3: 'continue' не работает в forEach. Используйте 'return', чтобы пропустить итерацию.
        if (image.id == imageId) {
            return;
        }

        var a = document.createElement("a");

        // ОШИБКА 4: Для установки класса используется 'className' или 'classList.add()'
        a.className = "col-3";
        // Альтернатива: a.classList.add("col-3");
        a.href = `/Home/Vehicle?carID=${car.id}&mainImageID=${image.id}`;
        a.innerHTML = `<img src="https://localhost:5119/api/home/${car.id}/images/${image.id}" class="img-fluid" alt="">`;

        // ОШИБКА 5: 'parentElement' - свойство только для чтения. 
        // Для добавления элемента используйте 'appendChild()'.
        imagesContainer.appendChild(a);
    });
}