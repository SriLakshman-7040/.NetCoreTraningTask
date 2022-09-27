

$(function () {
    $("#loginsubmitBtn").on('click', userDetailValidate);
})

function userDetailValidate() {

    let name = document.getElementById("username").value;
    let password = document.getElementById("secret").value;

    const userCredential = {
        userName: name,
        userPassword: password
    }
    //const requestOptions = {
    //    method: 'POST',
    //    headers: { 'Content-Type': 'application/json'},
    //    body: JSON.stringify(userCredential)
    //};

    //fetch(`https://localhost:7135/api/Users/authenticate` + requestOptions)
    //    .then(response => {
    //        console.log(response);
    //    })
    //    .catch(err => {
    //        console.log(err);
    //    })
    $.ajax({
        type: 'POST',
        url: "https://localhost:7135/api/Users/authenticate",
        dataType: 'json',
        data: JSON.stringify(userCredential),
        success: function (data) {
            console.log(data);
        },
        complete: function (jqXht) {
            if (jqXht.status == '401') {
                console.log('not authorized');
            }
            else if (jqXht.status == '200') {
                console.log('Success');
                return JavaScript("window.location = 'http://www.google.co.uk'");
            }
            else {
                console.log('something is wrong');
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(textStatus, errorThrown);
        }
    })
}