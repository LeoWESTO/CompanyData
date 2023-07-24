const dataUrl = 'api/Company';

async function fetchData() {
    try {
        const response = await fetch(dataUrl);
        const data = await response.json();
        const tableBody = document.getElementById('tableBody');
        tableBody.innerHTML = '';

        data.forEach(company => {
            const row = document.createElement('tr');

            const companyNameCell = document.createElement('td');
            const companyNameLink = document.createElement('a');
            companyNameLink.textContent = company.name;
            companyNameLink.href = `details.html?id=${company.id}`; //The text in the "Company Name" column should be a link that opens "Details" for its object
            companyNameCell.appendChild(companyNameLink);
            row.appendChild(companyNameCell);

            const cityCell = document.createElement('td');
            cityCell.textContent = company.city;
            row.appendChild(cityCell);

            const stateCell = document.createElement('td');
            stateCell.textContent = company.state;
            row.appendChild(stateCell);

            const phoneCell = document.createElement('td');
            phoneCell.textContent = company.phone;
            row.appendChild(phoneCell);

            tableBody.appendChild(row);
        });
    } catch (error) {
        console.error('Error fetching data:', error);
    }
}

async function addData() {
    let company = {
        name: "(new company)",
    };

    let response = await fetch(dataUrl, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(company)
    });

    refreshData();
}

//Implement behavior for 'Refresh' button: reload data from the server and recreate DOM in browser
function refreshData() {
    fetchData();
}

window.addEventListener('load', refreshData);
