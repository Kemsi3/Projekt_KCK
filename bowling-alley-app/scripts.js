const modal = document.getElementById('modal');
const closeButton = document.querySelector('.close-button');
const reserveButton = document.getElementById('reserve-button');
const submitButton = document.getElementById('submit-button');

let selectedAlleyId = ``;


function showReservations()
{
  document.getElementById("card-container").style.display="flex";
  document.getElementById("reservation-form").style.display="flex";
  document.getElementById("reservation-container").style.display="none";
}

function showUserReservations(){
  document.getElementById("reservation-container").style.display="flex";
  document.getElementById("reservation-form").style.display="none";
  document.getElementById("card-container").style.display="none";
  searchByUserId();
}

fetch("http://localhost:5068/alleys", {
  method: "GET",
  mode: "cors",
  header: "Access-Control-Allow-Origin:*"
})
  .then(response => response.json())
  .then(data => {
    console.log(data);
  })

  function searchByDates(alley)
  {
  
  const cardContainer = document.getElementById("card-container");
  cardContainer.replaceChildren();
  
  var selectedDate = document.getElementById("startDateInput").value;
  startDate = selectedDate + document.getElementById("start-hour-select").value;
  console.log(startDate);
  var endDate = selectedDate + document.getElementById("end-hour-select").value;

    fetch(`http://localhost:5068/alleys/${startDate}/${endDate}`, {
          method: "GET",
          mode: "cors",
          header: "Access-Control-Allow-Origin:*"
})
      .then(response => response.json())
      .then(data => {
        for (let i = 0; i < data.length; i++) {
          const alley = data[i];
          const alleyCard = createAlleyCard(alley);
          alleyCard.addEventListener('click', function() {
            selectedAlleyId = alley.alleyId;
            displayModal();
          });
          cardContainer.appendChild(alleyCard);
        }
      })
      .catch(error => {
        console.log("Error:", error);
      });
  }


  function searchByUserId(reservation){

    var userId = "39E20710-8F7E-460D-8631-A649750949DF";
    const reservationContainer = document.getElementById("reservation-container");
    reservationContainer.replaceChildren();

 fetch(`http://localhost:5068/reservations/${userId}`, {
          method: "GET",
          mode: "cors",
          header: "Access-Control-Allow-Origin:*"
})
      .then(response => response.json())
      .then(data => {
        for (let i = 0; i < data.length; i++) {
          const reservation = data[i];
          const reservationCard = createReservationCard(reservation);
          reservationContainer.appendChild(reservationCard);
        }
      })
      .catch(error => {
        console.log("Error:", error);
      });

  }

  function createAlleyCard(alley) {
    const cardContainer = document.createElement("div");
    cardContainer.classList.add("alley-card");

  
    const alleyIdElement = document.createElement("p");
    alleyIdElement.textContent = "Numer toru: " + alley.alleyId;
  
    const alleyCapacityElement = document.createElement("p");
    alleyCapacityElement.textContent = "Limit osób na torze: " + alley.alleyLimit;
  
    cardContainer.appendChild(alleyIdElement);
    cardContainer.appendChild(alleyCapacityElement);

  
    return cardContainer;
  }

  function createReservationCard(reservation) {
    const reservationContainer = document.createElement("div");
    reservationContainer.classList.add("reservation-card");

    const reservationIdElement = document.createElement("p");
    reservationIdElement.textContent = "ID rezerwacji: " + reservation.reservationId;

    const reservationStartDateElement = document.createElement("p");
    reservationStartDateElement.textContent = "Czas rozpoczęcia: " +  reservation.startDate;

    const reservationEndDateElement = document.createElement("p");
    reservationEndDateElement.textContent = "Czas zakończenia: " +  reservation.endDate;

    reservationContainer.appendChild(reservationIdElement);
    reservationContainer.appendChild(reservationStartDateElement);
    reservationContainer.appendChild(reservationEndDateElement);

    return reservationContainer;

  }
  
  function displayModal()
  {

    modal.style.display = 'block';
    console.log(selectedAlleyId);
  }

  
  function closeModal() {
    modal.style.display = 'none';
  }

  function displayAllAlleyCards() {
    const cardContainer = document.getElementById("card-container");
    cardContainer.innerText += "Dostępne pokoje";
  
    fetch("http://localhost:5068/alleys/", {
          method: "GET",
          mode: "cors",
          header: "Access-Control-Allow-Origin:*"
})
      .then(response => response.json())
      .then(data => {
        for (let i = 0; i < data.length; i++) {
          const alley = data[i];
          const alleyCard = createalleyCard(alley);
          cardContainer.appendChild(alleyCard);
        }
      })
      .catch(error => {
        console.log("Error:", error);
      });
      
  }
  

  function generateUUID() { 
    var d = new Date().getTime();
    var d2 = ((typeof performance !== 'undefined') && performance.now && (performance.now()*1000)) || 0;
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
        var r = Math.random() * 16;
        if(d > 0){
            r = (d + r)%16 | 0;
            d = Math.floor(d/16);
        } else {
            r = (d2 + r)%16 | 0;
            d2 = Math.floor(d2/16);
        }
        return (c === 'x' ? r : (r & 0x3 | 0x8)).toString(16);
    });
}

var currentDate = new Date();

var year = currentDate.getFullYear();
var month = (currentDate.getMonth() + 1).toString().padStart(2, '0');
var day = currentDate.getDate().toString().padStart(2, '0');
var hour = currentDate.getHours().toString().padStart(2, '0');
var minute = currentDate.getMinutes().toString().padStart(2, '0');
var second = currentDate.getSeconds().toString().padStart(2, '0');


var formattedDate = year + '-' + month + '-' + day + 'T' + hour + ':' + minute + ':' + second;


  submitButton.addEventListener('click', function() {




    var selectedDate = document.getElementById("startDateInput").value;
    startDate = selectedDate + document.getElementById("start-hour-select").value;
    console.log(startDate);
    var endDate = selectedDate + document.getElementById("end-hour-select").value;


    fetch('http://localhost:5068/reservation', {
    method: 'POST',
    mode: "cors",
    headers: {
        'Access-Control-Allow-Headers': '*',
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin':'*',

    },
    body: JSON.stringify({ "reservationId": generateUUID(),
                            "userId": "39E20710-8F7E-460D-8631-A649750949DF",
                            "startDate":startDate,
                            "endDate": endDate,
                            "alleyId": selectedAlleyId,
                            "creationDate": formattedDate,
                            "isDeleted": false
                            })
})
   .then(response => response.json())
  
   alert("Dokonano rezerwacji. Po więcej szczegółów sprawdź adres email");

    closeModal();
  });

  
  closeButton.addEventListener('click', closeModal);


