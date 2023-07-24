const urlParams = new URLSearchParams(window.location.search);
const companyId = urlParams.get('id');
let employeeId = null;

function chooseEmployee(employee) {
    if (employee) {
        employeeId = employee.id;

        document.getElementById('firstName').value = `${employee.firstName}`;
        document.getElementById('lastName').value = `${employee.lastName}`;
        document.getElementById('title').value = `${employee.title}`;
        document.getElementById('birthDate').value = `${convertDateFormat(employee.birthDate)}`;
        document.getElementById('position').value = `${employee.position}`;
    }
    else {
        document.getElementById('firstName').value = null;
        document.getElementById('lastName').value = null;
        document.getElementById('title').value = null;
        document.getElementById('birthDate').value = null;
        document.getElementById('position').value = null;
    }
}

function refreshData() {
    getInfo();
    getHistory();
    getNotes();
    getEmployees();
}

async function applyInfo() {

    let company = {
        "id": companyId,
        "name": document.getElementById('name').value,
        "city": document.getElementById('city').value,
        "address": document.getElementById('address').value,
        "state": document.getElementById('state').value,
        "phone": document.getElementById('phone').value,
    };

    let response = await fetch('/api/Company/', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(company)
    });

    window.location.href = '/';
}

async function getInfo() {
    if (companyId) {
        const dataUrl = `api/Company/${companyId}`;
        try {
            const response = await fetch(dataUrl);
            const companyData = await response.json();

            //info
            document.getElementById('companyNameTitle').innerText = companyData.name;
            document.getElementById('id').setAttribute('value', `${companyData.id}`);
            document.getElementById('name').setAttribute('value', `${companyData.name}`);
            document.getElementById('address').setAttribute('value', `${companyData.address}`);
            document.getElementById('city').setAttribute('value', `${companyData.city}`);
            document.getElementById('state').setAttribute('value', `${companyData.state}`);
            document.getElementById('phone').setAttribute('value', `${companyData.phone}`);

        } catch (error) {
            alert('Error fetching company details:', error);
        }
    }
    else {
        alert('No company ID found in the URL parameters.');
        window.location.href = "/404";
    }
}

async function getHistory() {
    if (companyId) {
        const dataUrl = `api/Company/${companyId}`;
        try {
            const response = await fetch(dataUrl);
            const companyData = await response.json();

            //history
            const tbodyHistory = document.getElementById('tbody-history');
            tbodyHistory.innerHTML = '';
            companyData.histories.forEach(history => {
                const row = document.createElement('tr');

                const orderDateCell = document.createElement('td');
                orderDateCell.textContent = convertDateFormat(history.orderDate);
                row.appendChild(orderDateCell);

                const storeCityCell = document.createElement('td');
                storeCityCell.textContent = history.storeCity;
                row.appendChild(storeCityCell);

                tbodyHistory.appendChild(row);
            });

        } catch (error) {
            alert('Error fetching company details:', error);
        }
    }
    else {
        alert('No company ID found in the URL parameters.');
        window.location.href = "/404";
    }
}

async function getNotes() {
    if (companyId) {
        const dataUrl = `api/Company/${companyId}`;
        try {
            const response = await fetch(dataUrl);
            const companyData = await response.json();

            //notes
            const tbodyNotes = document.getElementById('tbody-notes');
            tbodyNotes.innerHTML = '';
            companyData.employees.forEach(employee => {
                employee.notes.forEach(note => {
                    const row = document.createElement('tr');

                    const invoiceNumberCell = document.createElement('td');
                    invoiceNumberCell.textContent = note.id;
                    row.appendChild(invoiceNumberCell);

                    const employeeCell = document.createElement('td');
                    employeeCell.textContent = `${employee.firstName} ${employee.lastName}`;
                    row.appendChild(employeeCell);

                    tbodyNotes.appendChild(row);
                });

            });

        } catch (error) {
            alert('Error fetching company details:', error);
        }
    }
    else {
        alert('No company ID found in the URL parameters.');
        window.location.href = "/404";
    }
}

async function addEmployee() {
    let employee = {
        firstName: "(new employee)",
        companyId: companyId,
    };

    let response = await fetch('/api/Employee/', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(employee)
    });

    employeeId = null;
    getEmployees();
    chooseEmployee();
}

async function editEmployee() {
    if (employeeId) {

        let employee = {
            "id": employeeId,
            "firstName": document.getElementById('firstName').value,
            "lastName": document.getElementById('lastName').value,
            "title": document.getElementById('title').value,
            "birthDate": document.getElementById('birthDate').value,
            "position": document.getElementById('position').value,
            "companyId": companyId
        };

        let response = await fetch('/api/Employee/', {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify(employee)
        });

        employeeId = null;
        getEmployees();
        chooseEmployee();
    }
}

async function getEmployees() {
    if (companyId) {
        const dataUrl = `api/Company/${companyId}`;
        try {
            const response = await fetch(dataUrl);
            const companyData = await response.json();

            //employees
            const tbodyEmployees = document.getElementById('tbody-employees');
            tbodyEmployees.innerHTML = '';
            companyData.employees.forEach(employee => {
                const row = document.createElement('tr');

                const firstNameCell = document.createElement('td');
                const firstNameLink = document.createElement('a');
                firstNameLink.textContent = employee.firstName;
                firstNameLink.href = "javascript:void(0);";
                firstNameLink.onclick = function () {
                    chooseEmployee(employee);
                };
                firstNameCell.appendChild(firstNameLink);
                row.appendChild(firstNameCell);

                const lastNameCell = document.createElement('td');
                lastNameCell.textContent = employee.lastName;
                row.appendChild(lastNameCell);

                tbodyEmployees.appendChild(row);
            });

        } catch (error) {
            alert('Error fetching company details:', error);
        }
    }
    else {
        alert('No company ID found in the URL parameters.');
        window.location.href = "/404";
    }
}

async function deleteEmployee() {
    if (employeeId) {

        let response = await fetch(`api/Employee/${employeeId}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
        });

        employeeId = null;
        getEmployees();
        chooseEmployee();
    }
}

function convertDateFormat(inputDate) {
    const date = new Date(inputDate);
    const year = date.getFullYear().toString().padStart(4, '0');
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const day = date.getDate().toString().padStart(2, '0');

    return `${year}-${month}-${day}`;
}

window.addEventListener('load', refreshData);