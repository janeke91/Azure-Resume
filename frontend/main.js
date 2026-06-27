window.addEventListener('DOMContentLoaded', event => {
    GetVisitCount();
});

const functionApi = 'https://getresumecounter-cmbyhaa9ayb7a5fc.southeastasia-01.azurewebsites.net/api/GetResumeCounter';

const GetVisitCount = () => {
    fetch(functionApi).then(response => {
        return response.json();
    }).then(response => {
        console.log("Website called function API.");
        document.getElementById("counter").innerText = response;
    }).catch(function(error) {
        console.log(error);
    });
};